using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lenium.NeuralNetwork.Education.Interfaces;
using Lenium.NeuralNetwork.InputDatas.Interfaces;
using Lenium.NeuralNetwork.Interfaces;
using Lenium.NeuralNetwork.Logging;

namespace Lenium.NeuralNetwork.Education.Backpropagation
{
    public class BackpropagationEducationStrategy : IEducationStrategy<IMultiLayerSegment>
    {
        private BackpropagationSettings _settings;

        private double[][] delta;
        private double[][][] previousWeights;
        private double[][] previousBiases;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public BackpropagationEducationStrategy(BackpropagationSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="inputData"></param>
        public void Train(IMultiLayerSegment segment, IList<IInputData<double>> inputData)
        {
            if (_settings.LogMessageFrequency <= 0) _settings.LogMessageFrequency = 100;

            if (inputData == null || inputData.Count == 0)
                throw new ArgumentNullException("inputData");
            if (segment == null)
                throw new ArgumentNullException("segment");

            if (segment.Layers == null)
                segment.GenerateLayers(inputData.First());

            InitializeVariables(segment);

            double[][] expectedOutput = new double[inputData.Count][];

            for (int i = 0; i < inputData.Count; i++)
            {
                expectedOutput[i] = new double[inputData[i].Output.Length];
                for (int j = 0; j < inputData[i].Output.Length; j++)
                {
                    expectedOutput[i][j] = inputData[i].Output[j];
                }
            }

            double error = 0.0;
            int epoch = 0;

            Logger.Current.AddMessage("Education started.");

            do
            {
                epoch++;
                error = 0.0;

                for (int i = 0; i < inputData.Count; i++)
                {
                    error += Train(segment, inputData[i], expectedOutput[i]);
                }

                if (epoch % _settings.LogMessageFrequency == 0)
                {
                    Logger.Current.AddMessage(string.Format("Epoch {0} completed with error {1:0.0000}", epoch, error));
                }

            } while (error > _settings.NormalError && epoch <= _settings.MaxEpoches);

            Logger.Current.AddMessage(string.Format("Education completed with error {0:0.0000}", error));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        private void InitializeVariables(IMultiLayerSegment segment)
        {
            delta = new double[segment.Layers.Length][];
            previousBiases = new double[segment.Layers.Length][];
            previousWeights = new double[segment.Layers.Length][][];

            for (int l = 0; l < segment.Layers.Length; l++)
            {
                delta[l] = new double[segment.Layers[l].Neurons.Length];

                previousBiases[l] = new double[segment.Layers[l].Neurons.Length];

                previousWeights[l] = new double[segment.Layers[l].Neurons.Length][];

                for (int i = 0; i < segment.Layers[l].Neurons.Length; i++)
                {
                    previousWeights[l][i] = new double[segment.Layers[l].Neurons[i].Weights.Length];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private double Train(IMultiLayerSegment segment, IInputData<double> inputData, double[] expectedOutput)
        {
            double error = 0.0, sum = 0.0, weightDelta = 0.0, biasDelta = 0.0;

            var outputData = segment.CalculateOutput(inputData, false);

            for (int l = segment.Layers.Length - 1; l >= 0; l--)
            {
                if (l == segment.Layers.Length - 1)
                {
                    for (int k = 0; k < segment.Layers[l].Neurons.Length; k++)
                    {
                        delta[l][k] = outputData.Output[k] - expectedOutput[k];

                        error += Math.Pow(delta[l][k], 2);

                        delta[l][k] *= segment.Layers[l].Neurons[k].TransferFunction.Derivative(segment.Layers[l].Neurons[k].LastNET);
                    }
                }
                else
                {
                    for (int i = 0; i < segment.Layers[l].Neurons.Length; i++)
                    {
                        sum = 0.0;
                        for (int j = 0; j < segment.Layers[l + 1].Neurons.Length; j++)
                        {
                            sum += segment.Layers[l + 1].Neurons[j].Weights[i] * delta[l + 1][j];
                        }
                        sum *= segment.Layers[l].Neurons[i].TransferFunction.Derivative(segment.Layers[l].Neurons[i].LastNET);

                        delta[l][i] = sum;
                    }
                }
            }

            for (int l = 0; l < segment.Layers.Length; l++)
                for (int i = 0; i < segment.Layers[l].Neurons.Length; i++)
                    for (int j = 0; j < segment.Layers[l].Neurons[i].Weights.Length; j++)
                    {
                        weightDelta = _settings.EducationSpeed * delta[l][i] * ((l == 0)
                                          ? inputData.Input[j]
                                          : segment.Layers[l - 1].LastOutput[j]);

                        segment.Layers[l].Neurons[i].Weights[j] -= weightDelta +
                                                                   _settings.Momentum * previousWeights[l][i][j];

                        previousWeights[l][i][j] = weightDelta;
                    }

            for (int l = 0; l < segment.Layers.Length; l++)
                for (int i = 0; i < segment.Layers[l].Neurons.Length; i++)
                {
                    biasDelta = _settings.EducationSpeed * delta[l][i];

                    segment.Layers[l].Neurons[i].Bias = biasDelta + _settings.Momentum * previousBiases[l][i];

                    previousBiases[l][i] = biasDelta;
                }


            return error;
        }
    }
}

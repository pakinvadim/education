using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lenium.NeuralNetwork.Education.Backpropagation
{
    /// <summary>
    /// 
    /// </summary>
    public class BackpropagationSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public double NormalError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double EducationSpeed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Momentum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxEpoches { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LogMessageFrequency { get; set; }
    }
}

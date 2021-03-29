using System;

namespace OBig {
    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine(Sum(5));

            Console.WriteLine(PairSumSequence(5));
        }
        
        //Big О - описывает только скорость роста
        //Поэтому мы отбрасываем константы при оценки сложности
        //Поэтому алгоритм, описываемый как O(2N) должен описываться как O(N)
        
        //Если буквы одинаковые, то оставляем самую быстрорастущую функцию.
        //O(N^2 + N) = O(N^2)
        //O(N + logN) = O(N)
        //O(5*2^N + 1000N^100) = O(2^N)
        //но
        //O(N^2 + B) = O(N^2 + B) так как мы ничего не знаем о B

        //потребует времени O(n) и памяти O(n). Чем глубже стек, тем больше нужно памяти.
        public static int Sum(int n) {
            Console.WriteLine($"Sum {n}");
            if (n <= 0) {
                return 0;
            }
            return n + Sum(n - 1);
        }

        //потребует времени O(n) и памяти O(1). Стек глубиной в 1 метод PairSum
        public static int PairSumSequence(int n) {
            int sum = 0;
            for (int i = 0; i < n; i++) {
                sum += PairSum(i, i + 1);
            }
            return sum;
        }

        public static int PairSum(int a, int b) {
            Console.WriteLine($"PairSum {a} + {b}");
            return a + b;
        }

        //O(A + B)
        public static void Ex1(int[] arrayA, int[] arrayB) {
            for (int a = 0; a < arrayA.Length; a++) {
                Console.WriteLine(a);
            }
            for (int b = 0; b < arrayB.Length; b++) {
                Console.WriteLine(b);
            }
        }
        
        //O(A^2)
        public static void Ex1(int[] arrayA) {
            for (int a1 = 0; a1 < arrayA.Length; a1++) {
                for (int a2 = 0; a2 < arrayA.Length; a2++) {
                    Console.WriteLine(a1);
                }
            }
        }
        
        //O(A * B)
        public static void Ex4(int[] arrayA, int[] arrayB) {
            for (int a = 0; a < arrayA.Length; a++) {
                for (int b = 0; b < arrayB.Length; b++) {
                    Console.WriteLine(b);
                }
            }
        }
        
        //O(A / 2) => O(A) так как 2 константа
        public static void Ex3(int[] arrayA) {
            for (int a1 = 0; a1 < arrayA.Length / 2; a1++) {
                Console.WriteLine(a1);
            }
        }

        //O(A * B * 10000000000000000000) => O(A * B) так как 10000000000000000000 константа
        public static void Ex4(int[] arrayA, int[] arrayB) {
            for (int a = 0; a < arrayA.Length; a++) {
                for (int b = 0; b < arrayB.Length; b++) {
                    for (int c = 0; c < 10000000000000000000; c++) {
                        Console.WriteLine(b);
                    }
                }
            }
        }
    }
}
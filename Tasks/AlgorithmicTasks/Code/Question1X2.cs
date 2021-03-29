using System;
using System.Collections.Generic;
using System.Linq;

namespace Code {
	public class Question1X2 {
		// Для двух строк напишите метод, определяющий, является ли одна строка
		// перестановкой другой.
		// Ilодсказки:1,84, 122, 131

		public bool Implementation1(string line1, string line2) {
			if (string.IsNullOrEmpty(line1) || string.IsNullOrEmpty(line2)) {
				throw new ArgumentException("Input strings cannot be null or empty");
			}

			if (line1.Length != line2.Length) {
				return false;
			}

			line1 = string.Concat(line1.Reverse());
			return line1.Equals(line2);
		}

		public bool Implementation2(string line1, string line2) {
			if (string.IsNullOrEmpty(line1) || string.IsNullOrEmpty(line2)) {
				throw new ArgumentException("Input strings cannot be null or empty");
			}

			if (line1.Length != line2.Length) {
				return false;
			}

			for (var i = 0; i < line1.Length; i++) {
				if (line1[i] != line2[line1.Length - i - 1]) {
					return false;
				}
			}
			return true;
		}
	}
}
using System.Collections.Generic;
using System.Linq;

namespace Code {
	public class Question1X1 {
		// Реализуйте алгоритм, определяющий, все ли символы в строке встречаются
		// только один раз. А если при этом запрещено использование дополнительных
		// структур данных?
		// Ilодсказки:44, 117, 132

		public bool Implementation1(string line) {
			if (string.IsNullOrEmpty(line)) {
				return true;
			}

			foreach (char c in line) {
				if (line.Count(i => c == i) > 1) {
					return false;
				}
			}
			return true;
		}

		public bool Implementation2(string line) {
			if (string.IsNullOrEmpty(line)) {
				return true;
			}

			var hash = new HashSet<char>();
			foreach (char c in line) {
				if (!hash.Add(c)) {
					return false;
				}
			}
			return true;
		}

		public bool Implementation3(string line) {
			if (string.IsNullOrEmpty(line)) {
				return true;
			}

			HashSet<char> result = line.ToHashSet();
			return result.Count == line.Length;
		}
	}
}
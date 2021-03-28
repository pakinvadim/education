using System; // Импортирование пространства имен

namespace programm1 {
	class Program {
		static void Main() { }
	}

	// Объявление класса
	class Test1 {
		// Объявление метода
		static void Main1() {
			int х = 12 * 30; // Оператор 1
			Console.WriteLine(х); // Оператор 2
			Console.ReadLine();
		} // Конец метода
	} // Конец класса

	class Test2 {
		static void Main2() {
			Console.WriteLine(FeetToinches(30));
			Console.WriteLine(FeetToinches(100));
		}

		static int FeetToinches(int feet) {
			int inches = feet * 12;
			return inches;
		}
	}

	public class UnitConverter {
		int ratio;

		public UnitConverter(int unitRatio) {
			ratio = unitRatio;
		}

		public int Convert(int unit) {
			return unit * ratio;
		}
	}

	class Test3 {
		static void Main3() {
			var feetToinchesConverter = new UnitConverter(12);
			var milesToFeetConverter = new UnitConverter(5280);
			Console.WriteLine(feetToinchesConverter.Convert(30));
			Console.WriteLine(feetToinchesConverter.Convert(100));
			Console.WriteLine(feetToinchesConverter.Convert(milesToFeetConverter.Convert(l)));
		}
	}
}
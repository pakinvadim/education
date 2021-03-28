using System;
using System.Linq;
using NUnit.Framework;

namespace Stairs {
	/*100 хуёв рандомно раскидывают по лестнице из 100 ступенек, на одну ступеньку
	 может упасть сколько угодно хуёв. Нужно спуститься по этой лестнице вниз. Каждый раз наступая на
	 ступеньку с хуями, коэффициент петушиности увеличивается по количеству хуёв.  
	 Спускаться можно на одну или две ступеньки за раз. 
	 Написать функцию спускания с лестницы минимизирующую коэффициент петушиности*/

	public class DicksService {
		public StairsWithDicks DropDicks(int stepCount, int dickCount) {
			var stairsWithDicks = new StairsWithDicks(stepCount);

			var random = new Random();
			for (int i = 0; i < dickCount; i++) {
				int step = random.Next(1, stepCount + 1);
				stairsWithDicks.AddDicksForStep(step);
			}

			return stairsWithDicks;
		}

		public (string way, int k) Find(StairsWithDicks stairsWithDicks) {
			(string way, int k) result = GetNewK(string.Empty, k: 0, stairsWithDicks, step: 0);
			return result;
		}

		private (string way, int k) GetNewK(string way, int k, StairsWithDicks stairsWithDicks, int step) {
			int nextStep = step + 1;
			int jumpStep = step + 2;
			if (jumpStep <= stairsWithDicks.Count) {
				(string way, int k) next = GetNewK(way, k, stairsWithDicks, nextStep);
				(string way, int k) jump = GetNewK(way, k, stairsWithDicks, jumpStep);
				if (next.k < jump.k) {
					k += next.k;
					way = $"{nextStep};{next.way}";
				} else {
					k += jump.k;
					way = $"{jumpStep};{jump.way}";
				}
			} else {
				way = $"End;{way}";
			}
			if (stairsWithDicks.HasStep(step)) {
				int count = stairsWithDicks.GetDicksForStep(step);
				k += count;
			}
			return (way, k);
		}
	}

	public class StairsWithDicks {
		private readonly int[] _dickByStep;

		public int Count => _dickByStep.Length;

		public StairsWithDicks(int count) {
			_dickByStep = new int[count];
		}

		public void AddDicksForStep(int step) {
			_dickByStep[step - 1]++;
		}

		public int GetDicksForStep(int step) {
			return _dickByStep[step - 1];
		}

		public bool HasStep(int step) {
			var index = step - 1;
			return index >= 0 && index < _dickByStep.Length;
		}
	}

	[TestFixture]
	public class StairsServiceTests {
		private DicksService _dickService;

		[SetUp]
		public void SetUp() {
			_dickService = new DicksService();
		}

		[TestCase(10, 10)]
		public void Test1(int stepCount, int dickCount) {
			StairsWithDicks stairsWithDicks = _dickService.DropDicks(stepCount, dickCount);
			(string way, int k) result = _dickService.Find(stairsWithDicks);

			Console.WriteLine("Stairs: " + string.Join(';', Enumerable.Range(1, stepCount).Select(s => stairsWithDicks.GetDicksForStep(s))));
			Console.WriteLine("Way: " + result.way);
			Console.WriteLine("K: " + result.k);
		}
	}
}
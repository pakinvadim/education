using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Tests {
	public abstract class QuestionTestsBase {
		protected T Do<T>(Func<T> func) {
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			T result = func.Invoke();

			stopwatch.Stop();
			TestContext.WriteLine(stopwatch.Elapsed.ToString());
			return result;
		}
	}
}
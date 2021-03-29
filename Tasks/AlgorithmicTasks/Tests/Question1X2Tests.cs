using Code;
using FluentAssertions;
using NUnit.Framework;

namespace Tests {
	[TestFixture]
	public class Question1X2Tests : QuestionTestsBase {
		private static readonly TestCaseData[] TestCaseData = {
			new TestCaseData("abcde", "abcde", false),
			new TestCaseData("abcde", "edcba", true),
			new TestCaseData("a", "A", false),
		};
		private Question1X2 _question;

		[SetUp]
		public void SetUp() {
			_question = new Question1X2();
		}

		[TestCaseSource(nameof(TestCaseData))]
		public void QuestionImplementation1(string line1, string line2, bool expected) {
			bool result = Do(() => _question.Implementation1(line1, line2));

			result.Should().Be(expected);
		}

		[TestCaseSource(nameof(TestCaseData))]
		public void QuestionImplementation2(string line1, string line2, bool expected) {
			bool result = Do(() => _question.Implementation2(line1, line2));

			result.Should().Be(expected);
		}
	}
}
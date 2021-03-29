using Code;
using FluentAssertions;
using NUnit.Framework;

namespace Tests {
	[TestFixture]
	public class Question1X1Tests : QuestionTestsBase {
		private static readonly TestCaseData[] TestCaseData = {
			new TestCaseData("abcde", true),
			new TestCaseData("aabcde", false),
			new TestCaseData("abacde", false),
			new TestCaseData("abcdea", false),
			new TestCaseData("Qqwertyuiopasdfghkl;'zxcvbnm,./1234567890", true)
		};
		private Question1X1 _question;

		[SetUp]
		public void SetUp() {
			_question = new Question1X1();
		}

		[TestCaseSource(nameof(TestCaseData))]
		public void QuestionImplementation1(string input, bool expected) {
			bool result = Do(() => _question.Implementation1(input));

			result.Should().Be(expected);
		}

		[TestCaseSource(nameof(TestCaseData))]
		public void QuestionImplementation2(string input, bool expected) {
			bool result = Do(() => _question.Implementation2(input));

			result.Should().Be(expected);
		}

		[TestCaseSource(nameof(TestCaseData))]
		public void QuestionImplementation3(string input, bool expected) {
			bool result = Do(() => _question.Implementation3(input));

			result.Should().Be(expected);
		}
	}
}
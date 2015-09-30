using NUnit.Framework;

namespace GorillaQuiz.Test
{
    [TestFixture]
    public class ValidatorTest
    {

        [Test]
        public void ValidateWithoutAnswers()
        {
            var q = QuizFactory.CreateQuiz();
            var result = Validator.Validate("[]", q);

            Assert.AreEqual(.0, result.Score);
            Assert.AreEqual(false, result.Approved);
        }

        [Test]
        public void ValidationSummary()
        {
            var q = QuizFactory.CreateQuiz();
            var result = Validator.Validate("[]", q);

            Assert.IsInstanceOf(typeof(object), result.Quizz);
            Assert.IsInstanceOf(typeof(object), result.Response);
        }

        [Test]
        public void ValidateWithOnlySomeAnswers()
        {
            var q = QuizFactory.CreateQuiz();
            var result = Validator.Validate("[{\"question\":0,\"answer\":2}]", q);

            Assert.AreEqual(10, result.Score);
            Assert.AreEqual(false, result.Approved);
        }

        [Test]
        public void ValidateWithEverythingCorrect()
        {
            var q = QuizFactory.CreateQuiz();
            var result = Validator.Validate("[{\"question\":0,\"answer\":2},{\"question\":1,\"answer\":[0,1]},{\"question\":2,\"answer\":\"Azul\"}]", q);
            Assert.AreEqual(20, result.Score);
            Assert.AreEqual(true, result.Approved);
        }

    }
}
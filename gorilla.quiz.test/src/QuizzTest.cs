using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GorillaQuiz.Test
{
    [TestClass]
    public class QuizzTest
    {
        [TestMethod]
        public void QuizzCreate()
        {
            var quiz = Quiz.Create("My Quiz");

            Assert.AreEqual("My Quiz", quiz.Title);
            Assert.AreEqual(0, quiz.QuestionCount());
        }

        [TestMethod]
        public void ShouldAddQuestion()
        {
            var quiz = Quiz.Create("test");

            var question = new Mock<IQuestion>();

            Assert.AreEqual(0, quiz.QuestionCount());
            quiz.AddQuestion(question.Object);
            Assert.AreEqual(1, quiz.QuestionCount());
        }

        [TestMethod]
        public void ShouldRemoveQuestion()
        {
            var quiz = Quiz.Create("test");

            var question = new Mock<IQuestion>();
            quiz.AddQuestion(question.Object);
            Assert.AreEqual(1, quiz.QuestionCount());

            quiz.RemoveQuestion(question.Object);
            Assert.AreEqual(0, quiz.QuestionCount());
        }

        [TestMethod]
        public void SerializeQuiz()
        {
            var quiz = Quiz.Create("test");

            var json = quiz.Serialize();

            Assert.AreEqual("{\"title\":\"test\",\"questions\":[]}", json);
        }

        [TestMethod]
        public void UnserializeQuiz()
        {
            var quiz = Quiz.Create("test");
            var json = quiz.Serialize();

            var quiz2 = Quiz.CreateFromJsonString(json);

            Assert.AreEqual(quiz.Title, quiz2.Title);
            Assert.AreEqual(quiz.QuestionCount(), quiz2.QuestionCount());
        }

        
    }
}

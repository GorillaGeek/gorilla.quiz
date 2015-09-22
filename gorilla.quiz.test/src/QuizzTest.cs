using System;
using GorillaQuiz.Exception;
using Moq;
using NUnit.Framework;

namespace GorillaQuiz.Test
{
    [TestFixture]
    public class QuizzTest
    {
        [Test]
        public void QuizzCreate()
        {
            var quiz = Quiz.Create("My Quiz");

            Assert.AreEqual("My Quiz", quiz.Title);
            Assert.AreEqual(0, quiz.Questions.Count);
        }

        [Test]
        public void ShouldAddQuestion()
        {
            var quiz = Quiz.Create("test");

            var question = new Mock<IQuestion>();

            Assert.AreEqual(0, quiz.Questions.Count);
            quiz.AddQuestion(question.Object);
            Assert.AreEqual(1, quiz.Questions.Count);
        }

        [Test]
        public void ShouldNotAddNullQuestion()
        {
            var quiz = Quiz.Create("test");

            Assert.Throws<ArgumentException>(() => quiz.AddQuestion(null));
        }

        [Test]
        public void ShouldNotAddTheSameQuestionMoreThanOneTime()
        {
            var quiz = Quiz.Create("test");

            var question = new Mock<IQuestion>();
            quiz.AddQuestion(question.Object);

            Assert.Throws<QuizException>(() => quiz.AddQuestion(question.Object));
        }

        [Test]
        public void ShouldRemoveQuestion()
        {
            var quiz = Quiz.Create("test");

            var question = new Mock<IQuestion>();
            quiz.AddQuestion(question.Object);
            Assert.AreEqual(1, quiz.Questions.Count);

            quiz.RemoveQuestion(question.Object);
            Assert.AreEqual(0, quiz.Questions.Count);
        }

        [Test]
        public void SerializeQuiz()
        {
            var quiz = Quiz.Create("test");

            var json = quiz.Serialize();
            Assert.AreEqual("{\"title\":\"test\",\"questions\":[],\"neededScore\":0.0}", json);
        }

        [Test]
        public void UnserializeQuiz()
        {
            var json = "{\"title\":\"test\",\"questions\":[{\"type\":\"SingleChoice\",\"question\":\"how much is pi ?\",\"score\":10.0,\"correct\":0,\"choices\":[{\"type\":\"Text\",\"text\":\"3.14\"}]}],\"neededScore\":10.0}";

            var quiz = Quiz.CreateFromJsonString(json);

            Assert.AreEqual("test", quiz.Title);
            Assert.AreEqual(10, quiz.NeededScore);
            Assert.AreEqual(1, quiz.Questions.Count);
        }

    }
}

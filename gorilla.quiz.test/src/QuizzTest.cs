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
            var quiz = QuizFactory.CreateQuiz();

            var json = quiz.Serialize();
            Assert.AreEqual("{\"title\":\"teste\",\"questions\":[{\"type\":\"SingleChoice\",\"question\":\"Qual o nome do prado\",\"score\":10.0,\"correct\":2,\"choices\":[{\"type\":\"Text\",\"text\":\"Paulo\"},{\"type\":\"Text\",\"text\":\"Danilo\"},{\"type\":\"Text\",\"text\":\"Daniel\"}]},{\"type\":\"MultipleChoice\",\"question\":\"Quais desses números são primos\",\"score\":5.0,\"corrects\":[0,1],\"choices\":[{\"type\":\"Text\",\"text\":\"2\"},{\"type\":\"Text\",\"text\":\"3\"},{\"type\":\"Text\",\"text\":\"4\"}]},{\"type\":\"ExactAnswer\",\"question\":\"Qual é a cor do ceu?\",\"score\":5.0,\"answers\":[\"Azul\"]}],\"neededScore\":12.0}", json);
        }

        [Test]
        public void UnserializeQuiz()
        {
            var json = "{\"title\":\"teste\",\"questions\":[{\"type\":\"SingleChoice\",\"question\":\"Qual o nome do prado\",\"score\":10.0,\"correct\":2,\"choices\":[{\"type\":\"Text\",\"text\":\"Paulo\"},{\"type\":\"Text\",\"text\":\"Danilo\"},{\"type\":\"Text\",\"text\":\"Daniel\"}]},{\"type\":\"MultipleChoice\",\"question\":\"Quais desses números são primos\",\"score\":5.0,\"corrects\":[0,1],\"choices\":[{\"type\":\"Text\",\"text\":\"2\"},{\"type\":\"Text\",\"text\":\"3\"},{\"type\":\"Text\",\"text\":\"4\"}]},{\"type\":\"ExactAnswer\",\"question\":\"Qual é a cor do ceu?\",\"score\":5.0,\"answers\":[\"Azul\"]}],\"neededScore\":12.0}";

            var quiz = Quiz.CreateFromJsonString(json);

            Assert.AreEqual("teste", quiz.Title);
            Assert.AreEqual(12, quiz.NeededScore);
            Assert.AreEqual(3, quiz.Questions.Count);
        }

    }
}

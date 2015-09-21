using GorillaQuiz.Question;
using Moq;
using NUnit.Framework;

namespace GorillaQuiz.Test.Question
{
    [TestFixture]
    public class SingleChoiceTest
    {
        [Test]
        public void SingleChoiceQuestionCreate()
        {
            var question = SingleChoice.Create("Hello World", 10);

            Assert.AreEqual(10, question.Score);
            Assert.AreEqual("Hello World", question.Title);
        }

        [Test]
        public void SingleChoiceAddChoice()
        {
            var question = SingleChoice.Create("asd");
            var choice = new Mock<IChoice>();

            Assert.AreEqual(0, question.Choices.Count);
            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.Choices.Count);
        }

        [Test]
        public void SingleChoiceRemoveChoice()
        {
            var question = SingleChoice.Create("asd");
            var choice = new Mock<IChoice>();

            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.Choices.Count);
            question.RemoveChoice(choice.Object);
            Assert.AreEqual(0, question.Choices.Count);
        }
    }
}

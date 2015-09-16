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

            Assert.AreEqual(0, question.ChoiceCount());
            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.ChoiceCount());
        }

        [Test]
        public void SingleChoiceRemoveChoice()
        {
            var question = SingleChoice.Create("asd");

            var choice = new Mock<IChoice>();


            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.ChoiceCount());
            question.RemoveChoice(choice.Object);
            Assert.AreEqual(0, question.ChoiceCount());
        }
    }
}

using GorillaQuiz.Question;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GorillaQuiz.Test.Question
{
    [TestClass]
    public class SingleChoiceTest
    {
        [TestMethod]
        public void SingleChoiceQuestionCreate()
        {
            var question = SingleChoice.Create("Hello World", 10);

            Assert.AreEqual(10, question.Score);
            Assert.AreEqual("Hello World", question.Title);
        }

        [TestMethod]
        public void SingleChoiceAddChoice()
        {
            var question = SingleChoice.Create("asd");

            var choice = new Mock<IChoice>();

            Assert.AreEqual(0, question.ChoiceCount());
            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.ChoiceCount());
        }

        [TestMethod]
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

using System;
using GorillaQuiz.Question;
using Moq;
using Newtonsoft.Json;
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

        [Test]
        public void SingleChoiceQuestionTitleCanNotBeNull()
        {
            Assert.Throws<ArgumentException>(() => SingleChoice.Create(""));
            Assert.Throws<ArgumentException>(() => SingleChoice.Create(null));
        }

        [Test]
        public void SingleChoiceCanNotHaveTwoCorrectChoices()
        {
            var choice1 = new Mock<IChoice>();
            var choice2 = new Mock<IChoice>();

            choice1.SetupGet(x => x.Correct).Returns(true);
            choice2.SetupGet(x => x.Correct).Returns(true);

            var q = SingleChoice.Create("asd").AddChoice(choice1.Object);

            Assert.Throws<ArgumentException>(() => q.AddChoice(choice2.Object));

        }

        [Test]
        public void SingleChoiceShouldNotExportTheCorrectAnswerWhenPublic()
        {
            var question = SingleChoice.Create("asd");
            var json = JsonConvert.SerializeObject(question.Export(true));
            Assert.False(json.Contains("correct"));
            Assert.True(json.Contains("asd"));
        }

        [Test]
        public void SingleChoiceMustBeAutoValidateable()
        {
            var question = SingleChoice.Create("asd");
            Assert.True(question.AutoValidate);
        }
    }
}

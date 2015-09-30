using System;
using GorillaQuiz.Choice;
using GorillaQuiz.Question;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GorillaQuiz.Test.Question
{
    [TestFixture]
    public class MultipleChoiceTest
    {
        [Test]
        public void MultipleChoiceQuestionCreate()
        {
            var question = MultipleChoice.Create("Hello World", 10);

            Assert.AreEqual(10, question.Score);
            Assert.AreEqual("Hello World", question.Title);
            Assert.AreEqual("MultipleChoice", question.Type);
        }

        [Test]
        public void MultipleChoiceQuestionCantHaveEmptyTitle()
        {
            Assert.Throws<ArgumentException>(() => MultipleChoice.Create(null));
            Assert.Throws<ArgumentException>(() => MultipleChoice.Create(""));
        }

        [Test]
        public void MultipleChoiceAddChoice()
        {
            var question = MultipleChoice.Create("asd");
            var choice = new Mock<IChoice>();

            Assert.AreEqual(0, question.Choices.Count);
            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.Choices.Count);
        }

        [Test]
        public void MultipleChoiceRemoveChoice()
        {
            var question = MultipleChoice.Create("asd");
            var choice = new Mock<IChoice>();

            question.AddChoice(choice.Object);
            Assert.AreEqual(1, question.Choices.Count);
            question.RemoveChoice(choice.Object);
            Assert.AreEqual(0, question.Choices.Count);
        }

        [Test]
        public void MultipleChoiceCanHaveManyCorrectChoices()
        {
            var choice1 = new Mock<IChoice>();
            var choice2 = new Mock<IChoice>();

            choice1.SetupGet(x => x.Correct).Returns(true);
            choice2.SetupGet(x => x.Correct).Returns(true);

            var question = MultipleChoice.Create("asd")
                .AddChoice(choice1.Object)
                .AddChoice(choice2.Object);

            Assert.AreEqual(2, question.Choices.Count);
        }

        [Test]
        public void MultipleChoiceShouldNotExportTheCorrectAnswerWhenPublic()
        {
            var question = MultipleChoice.Create("asd").AddChoice(TextChoice.Create("teste", true));

            var json = JsonConvert.SerializeObject(question.Export(true));
            Assert.False(json.Contains("corrects"));
            Assert.True(json.Contains("test"));
            Assert.True(json.Contains("asd"));
        }

        [Test]
        public void MultipleChoiceShouldExportTheCorrectAnswerWhenNotPublic()
        {
            var question = MultipleChoice.Create("asd").AddChoice(TextChoice.Create("teste", true));

            var json = JsonConvert.SerializeObject(question.Export());
            Assert.True(json.Contains("corrects"));
        }

        [Test]
        public void MultipleChoiceMustBeAutoValidateable()
        {
            var question = MultipleChoice.Create("asd");
            Assert.True(question.AutoValidate);
        }

    }
}

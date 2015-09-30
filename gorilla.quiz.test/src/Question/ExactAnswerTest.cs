using System;
using GorillaQuiz.Question;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GorillaQuiz.Test.Question
{
    [TestFixture]
    public class ExactAnswerTest
    {
        [Test]
        public void ExactAnswerTestQuestionCreate()
        {
            var question = ExactAnswer.Create("Hello World", 10);

            Assert.AreEqual(10, question.Score);
            Assert.AreEqual("Hello World", question.Title);
            Assert.AreEqual("ExactAnswer", question.Type);
        }

        [Test]
        public void MultipleChoiceQuestionCantHaveEmptyTitle()
        {
            Assert.Throws<ArgumentException>(() => ExactAnswer.Create(null));
            Assert.Throws<ArgumentException>(() => ExactAnswer.Create(""));
        }

        [Test]
        public void ExactAnswerTestAddAnswer()
        {
            var question = ExactAnswer.Create("asd");

            Assert.AreEqual(0, question.Answers.Count);
            question.AddAnswer("test");
            Assert.AreEqual(1, question.Answers.Count);
        }

        [Test]
        public void ExactAnswerRemoveAnswer()
        {
            var question = ExactAnswer.Create("asd");

            question.AddAnswer("test");
            Assert.AreEqual(1, question.Answers.Count);
            question.RemoveAnswer("test");
            Assert.AreEqual(0, question.Answers.Count);
        }

        [Test]
        public void ExactAnswerCanHaveManyCorrectAnswers()
        {
            var question = ExactAnswer.Create("asd")
                .AddAnswer("a")
                .AddAnswer("b");

            Assert.AreEqual(2, question.Answers.Count);
        }

        [Test]
        public void ExactAnswerShouldNotExportTheCorrectAnswerWhenPublic()
        {
            var question = ExactAnswer.Create("asd").AddAnswer("xyz");

            var json = JsonConvert.SerializeObject(question.Export(true));
            Assert.False(json.Contains("answers"));
            Assert.True(json.Contains("asd"));
        }

        [Test]
        public void ExactAnswerShouldExportTheCorrectAnswerWhenNotPublic()
        {
            var question = ExactAnswer.Create("asd").AddAnswer("xyz");

            var json = JsonConvert.SerializeObject(question.Export());
            Assert.True(json.Contains("answers"));
        }

        [Test]
        public void ExactAnswerMustBeAutoValidateable()
        {
            var question = ExactAnswer.Create("asd");
            Assert.True(question.AutoValidate);
        }

    }
}

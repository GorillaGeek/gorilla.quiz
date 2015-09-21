using GorillaQuiz.Choice;
using GorillaQuiz.Question;
using NUnit.Framework;

namespace GorillaQuiz.Test
{
    [TestFixture]
    public class ValidatorTest
    {

        private Quiz CreateQuiz()
        {
            return Quiz.Create("teste", 12)
                .AddQuestion(
                    SingleChoice.Create("Qual o nome do prado", 10)
                        .AddChoice(TextChoice.Create("Paulo"))
                        .AddChoice(TextChoice.Create("Danilo"))
                        .AddChoice(TextChoice.Create("Daniel", true))
                )
                .AddQuestion(
                    SingleChoice.Create("Que cor é o ceu?", 5)
                        .AddChoice(TextChoice.Create("Verde"))
                        .AddChoice(TextChoice.Create("Azul", true))
                        .AddChoice(TextChoice.Create("Vermelho"))
                )
                .AddQuestion(
                    SingleChoice.Create("Qual o valor de PI?", 5)
                        .AddChoice(TextChoice.Create("3.14", true))
                        .AddChoice(TextChoice.Create("6.28"))
                        .AddChoice(TextChoice.Create("1.61"))
                );
        }

        [Test]
        public void ValidateWithoutAnswers()
        {
            var q = CreateQuiz();
            var result = Validator.Validate("[]", q);

            Assert.AreEqual(.0, result.Score);
            Assert.AreEqual(false, result.Approved);
        }

        [Test]
        public void ValidateWithOnlySomeAnswers()
        {
            var q = CreateQuiz();
            var result = Validator.Validate("[{\"question\":0,\"answer\":2}]", q);

            Assert.AreEqual(10, result.Score);
            Assert.AreEqual(false, result.Approved);
        }

        [Test]
        public void ValidateWithEverythingCorrect()
        {
            var q = CreateQuiz();
            var result = Validator.Validate("[{\"question\":0,\"answer\":2},{\"question\":1,\"answer\":1},{\"question\":2,\"answer\":0}]", q);
            Assert.AreEqual(20, result.Score);
            Assert.AreEqual(true, result.Approved);
        }

    }
}
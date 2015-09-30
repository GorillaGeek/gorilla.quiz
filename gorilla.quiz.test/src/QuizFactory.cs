using GorillaQuiz.Choice;
using GorillaQuiz.Question;

namespace GorillaQuiz.Test
{
    public class QuizFactory
    {
        public static Quiz CreateQuiz()
        {
            return Quiz.Create("teste", 12)
                .AddQuestion(
                    SingleChoice.Create("Qual o nome do prado", 10)
                        .AddChoice(TextChoice.Create("Paulo"))
                        .AddChoice(TextChoice.Create("Danilo"))
                        .AddChoice(TextChoice.Create("Daniel", true))
                )
                .AddQuestion(
                    MultipleChoice.Create("Quais desses números são primos", 5)
                        .AddChoice(TextChoice.Create("2", true))
                        .AddChoice(TextChoice.Create("3", true))
                        .AddChoice(TextChoice.Create("4"))
                )
                .AddQuestion(
                    ExactAnswer.Create("Qual é a cor do ceu?", 5)
                        .AddAnswer("Azul")
                );
        }
    }
}
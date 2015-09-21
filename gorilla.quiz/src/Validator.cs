using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GorillaQuiz
{
    public class Validator
    {

        public static ValidationSummary Validate(string response, IQuiz quiz)
        {

            var objResponse = JsonConvert.DeserializeObject<List<dynamic>>(response);

            var score = .0f;

            foreach (var question in objResponse)
            {
                var q = quiz.Questions.ElementAt((int)question.question);

                if (q.Validate(question.answer))
                {
                    score += q.Score;
                }

            }

            return new ValidationSummary()
            {
                Score = score,
                Approved = score >= quiz.NeededScore,
                Quizz = quiz.Export(),
                Response = objResponse
            };
        }

    }
}
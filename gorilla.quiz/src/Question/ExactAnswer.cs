using System;
using System.Collections.Generic;

namespace GorillaQuiz.Question
{
    public class ExactAnswer : AbstractQuestion, IQuestion
    {

        List<string> answers;

        protected ExactAnswer(string title, float score) : base(title, score)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Question title can't be null or empty");
            }
        }

        public override bool AutoValidate => true;

        public static ExactAnswer Create(string title, float score = 0)
        {
            return new ExactAnswer(title, score);
        }

        public override object Export(bool @public = false)
        {

            if (@public)
            {
                return new { type = this.Type, question = Title };
            }

            return new { type = this.Type, question = Title, score = Score, answers = this.answers };
        }

        public static ExactAnswer CreateFromJsonDynamic(dynamic obj)
        {
            var question = ExactAnswer.Create((string)obj.question, (float)obj.score);
            return question;
        }

        public override bool Validate(dynamic response)
        {
            var answer = (string)response;
            return this.answers.Contains(answer);
        }
    }
}
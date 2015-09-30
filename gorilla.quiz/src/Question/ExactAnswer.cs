using System;
using System.Collections.Generic;

namespace GorillaQuiz.Question
{
    public class ExactAnswer : AbstractQuestion, IQuestion
    {

        List<string> _answers;

        protected ExactAnswer(string title, float score) : base(title, score)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Question title can't be null or empty");
            }

            _answers = new List<string>();
        }

        public override bool AutoValidate => true;

        public static ExactAnswer Create(string title, float score = 0)
        {
            return new ExactAnswer(title, score);
        }

        public ExactAnswer AddAnswer(string answer)
        {
            this._answers.Add(answer);
            return this;
        }

        public ExactAnswer RemoveAnswer(string answer)
        {
            if (this._answers.Contains(answer))
            {
                this._answers.Remove(answer);
            }
            return this;
        }

        public IReadOnlyCollection<string> Answers => _answers.AsReadOnly();

        public override object Export(bool @public = false)
        {

            if (@public)
            {
                return new { type = this.Type, question = Title };
            }

            return new { type = this.Type, question = Title, score = Score, answers = this._answers };
        }

        public static ExactAnswer CreateFromJsonDynamic(dynamic obj)
        {
            var question = ExactAnswer.Create((string)obj.question, (float)obj.score);

            foreach (var answer in obj.answers)
            {
                question.AddAnswer((string)answer);
            }

            return question;
        }

        public override bool Validate(dynamic response)
        {
            var answer = (string)response;

            return this._answers.Contains(answer);
        }
    }
}
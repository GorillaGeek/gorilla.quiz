using System;
using System.Collections.Generic;
using System.Linq;
using GorillaQuiz.Choice;

namespace GorillaQuiz.Question
{
    public class MultipleChoice : AbstractQuestion, IQuestion
    {

        private readonly List<IChoice> _choices;

        protected MultipleChoice(string title, float score) : base(title, score)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Question title can't be null or empty");
            }

            this._choices = new List<IChoice>();
        }

        public override bool AutoValidate => true;

        public MultipleChoice AddChoice(IChoice choice)
        {

            if (!_choices.Contains(choice))
            {
                _choices.Add(choice);
            }

            return this;
        }

        public MultipleChoice RemoveChoice(IChoice choice)
        {
            if (_choices.Contains(choice))
            {
                _choices.Remove(choice);
            }

            return this;
        }

        public IReadOnlyCollection<IChoice> Choices => _choices.AsReadOnly();

        public static MultipleChoice Create(string title, float score = 0)
        {
            return new MultipleChoice(title, score);
        }

        public override object Export(bool @public = false)
        {
            var choices = new List<object>();
            var correct = 0;

            var index = 0;
            var corrects = new List<int>();

            foreach (var choice in _choices)
            {
                if (choice.Correct)
                {
                    corrects.Add(index);
                }

                choices.Add(choice.Export());
                index++;
            }

            if (@public)
            {
                return new { type = this.Type, question = Title, choices = choices };
            }

            return new { type = this.Type, question = Title, score = Score, corrects = corrects, choices = choices };
        }

        public static MultipleChoice CreateFromJsonDynamic(dynamic obj)
        {
            var corrects = (List<int>)obj.corrects;
            var question = MultipleChoice.Create((string)obj.question, (float)obj.score);

            var index = 0;

            foreach (var choice in obj.choices)
            {
                var isCorrect = corrects.Contains(index);
                var c = AbstractChoice.CreateFromJsonDynamic(choice, isCorrect);
                question.AddChoice(c);
                index++;
            }

            return question;
        }

        public override bool Validate(dynamic response)
        {
            var index = (int)response;
            return _choices.ElementAt(index).Correct;
        }
    }
}
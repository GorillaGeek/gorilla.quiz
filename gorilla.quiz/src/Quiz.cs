using System;
using System.Collections.Generic;
using GorillaQuiz.Exception;
using GorillaQuiz.Question;
using Newtonsoft.Json;

namespace GorillaQuiz
{
    public class Quiz : IQuiz
    {
        public string Title { get; set; }

        public float NeededScore { get; set; }

        private readonly List<IQuestion> _questions;

        private Quiz(string title, float neededScore)
        {
            Title = title;
            NeededScore = neededScore;
            _questions = new List<IQuestion>();
        }

        public Quiz AddQuestion(IQuestion question)
        {
            if (question == null)
            {
                throw new ArgumentException("Question must not be null");
            }

            if (_questions.Contains(question))
            {
                throw new QuizException();
            }

            _questions.Add(question);

            return this;
        }

        public Quiz RemoveQuestion(IQuestion question)
        {
            if (_questions.Contains(question))
            {
                _questions.Remove(question);
            }

            return this;
        }

        public IReadOnlyCollection<IQuestion> Questions => _questions.AsReadOnly();

        public static Quiz Create(string title, float neededScore = .0f)
        {
            return new Quiz(title, neededScore);
        }

        public string Serialize(bool @public = false)
        {
            return JsonConvert.SerializeObject(Export(@public));
        }

        public object Export(bool @public = false)
        {
            var questions = new List<object>();

            foreach (var question in _questions)
            {
                questions.Add(question.Export(@public));
            }

            return new
            {
                title = Title,
                questions = questions,
                neededScore = NeededScore
            };
        }

        public static Quiz CreateFromJsonString(string json)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(json);
            var quiz = Quiz.Create((string)obj.title, (float)obj.neededScore);

            foreach (var q in obj.questions)
            {
                var type = (string)q.type;

                switch (type)
                {
                    case "SingleChoice":
                        quiz.AddQuestion(SingleChoice.CreateFromJsonDynamic(q));
                        break;
                }
            }

            return quiz;
        }

    }
}
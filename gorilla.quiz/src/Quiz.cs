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

        private List<IQuestion> _questions;

        private Quiz(string title)
        {
            Title = title;
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
                throw new RepeatedQuestionException();
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

        public int QuestionCount()
        {
            return _questions.Count;
        }

        public static Quiz Create(string title)
        {
            return new Quiz(title);
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
                questions = questions
            };
        }

        public static Quiz CreateFromJsonString(string json)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(json);

            var quiz = Quiz.Create((string)obj.title);

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
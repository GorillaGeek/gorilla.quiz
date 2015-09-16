﻿using System;
using System.Collections.Generic;
using System.Linq;
using GorillaQuiz.Choice;

namespace GorillaQuiz.Question
{
    public class SingleChoice : AbstractQuestion, IQuestion
    {

        private readonly List<IChoice> _choices;

        protected SingleChoice(string title, float score) : base(title, score)
        {
            this._choices = new List<IChoice>();
        }

        public SingleChoice AddChoice(IChoice choice)
        {

            if (choice.Correct && _choices.Any(x => x.Correct))
            {
                throw new ArgumentException("Single choice question can only have one correct choice");
            }

            if (!_choices.Contains(choice))
            {
                _choices.Add(choice);
            }

            return this;
        }

        public SingleChoice RemoveChoice(IChoice choice)
        {
            if (_choices.Contains(choice))
            {
                _choices.Remove(choice);
            }

            return this;
        }

        public int ChoiceCount()
        {
            return _choices.Count;
        }

        public static SingleChoice Create(string title, float score = 0)
        {
            return new SingleChoice(title, score);
        }

        public override object ToObject(bool @public = false)
        {
            var choices = new List<object>();
            var correct = 0;

            var index = 0;

            foreach (var choice in _choices)
            {
                if (choice.Correct)
                {
                    correct = index;
                }

                choices.Add(choice.ToObject());
                index++;
            }

            if (@public)
            {
                return new { type = "SingleChoice", question = Title, choices = choices };
            }

            return new { type = "SingleChoice", question = Title, score = Score, correct = correct, choices = choices };
        }

        public static SingleChoice CreateFromJsonDynamic(dynamic obj)
        {
            var correct = (int)obj.correct;
            var question = SingleChoice.Create((string)obj.question, (float)obj.score);

            var index = 0;

            foreach (var choice in obj.choices)
            {
                var isCorrect = (correct == index);
                var c = AbstractChoice.CreateFromJsonDynamic(choice, isCorrect);
                question.AddChoice(c);
                index++;
            }

            return question;
        }
    }
}
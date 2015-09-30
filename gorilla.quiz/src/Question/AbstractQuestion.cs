namespace GorillaQuiz.Question
{
    public abstract class AbstractQuestion : IQuestion
    {
        protected AbstractQuestion(string title, float score)
        {
            Title = title;
            Score = score;
        }

        public string Title { get; set; }
        public float Score { get; set; }

        public abstract object Export(bool @public = false);
        public abstract bool AutoValidate { get; }
        public string Type => this.GetType().Name;

        public abstract bool Validate(dynamic response);

        public static IQuestion CreateFromJsonDynamic(dynamic obj)
        {
            var type = (string)obj.type;

            IQuestion question;

            switch (type)
            {
                case "SingleChoice":
                    question = SingleChoice.CreateFromJsonDynamic(obj);
                    break;
                case "MultipleChoice":
                    question = MultipleChoice.CreateFromJsonDynamic(obj);
                    break;
                case "ExactAnswer":
                    question = ExactAnswer.CreateFromJsonDynamic(obj);
                    break;
                default:
                    throw new System.Exception("Invalid question type");
            }

            question.Score = (float)obj.score;

            return question;
        }
    }
}
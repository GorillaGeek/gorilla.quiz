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
        public abstract bool Validate(dynamic response);
    }
}
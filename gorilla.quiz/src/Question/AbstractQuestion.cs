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

        public abstract object ToObject(bool @public = false);
    }
}
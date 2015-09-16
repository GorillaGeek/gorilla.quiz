namespace GorillaQuiz
{
    public interface IQuiz
    {
        string Title { get; set; }

        string Serialize(bool @public = false);
    }
}
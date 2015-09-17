namespace GorillaQuiz
{
    public interface IQuiz : IExportable
    {
        string Title { get; set; }

        string Serialize(bool @public = false);
    }
}
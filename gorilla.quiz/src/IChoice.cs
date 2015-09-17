namespace GorillaQuiz
{
    public interface IChoice : IExportable
    {
        bool Correct { get; }
    }
}
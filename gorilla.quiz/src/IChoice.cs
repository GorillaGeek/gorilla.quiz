namespace GorillaQuiz
{
    public interface IChoice : ISerializable
    {
        bool Correct { get; }
    }
}
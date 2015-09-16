namespace GorillaQuiz
{
    public interface IQuestion : ISerializable
    {
        float Score { get; set; }

        object ToObject(bool @public = false);
    }
}
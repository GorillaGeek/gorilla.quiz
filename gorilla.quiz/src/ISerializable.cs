namespace GorillaQuiz
{
    public interface ISerializable
    {
        object ToObject(bool @public = false);
    }
}
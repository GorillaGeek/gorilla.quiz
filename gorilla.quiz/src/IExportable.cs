namespace GorillaQuiz
{
    public interface IExportable
    {
        object Export(bool @public = false);
    }
}
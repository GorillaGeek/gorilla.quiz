namespace GorillaQuiz
{
    public interface IQuestion : IExportable
    {
        float Score { get; set; }
        
    }
}
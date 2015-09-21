namespace GorillaQuiz
{
    public interface IQuestion : IExportable
    {
        float Score { get; set; }
        /// <summary>
        /// Does the question is able to auto verify itself?
        /// </summary>
        bool AutoValidate { get; }
        bool Validate(dynamic response);
    }
}
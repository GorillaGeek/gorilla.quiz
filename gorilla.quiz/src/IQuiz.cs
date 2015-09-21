using System.Collections.Generic;

namespace GorillaQuiz
{
    public interface IQuiz : IExportable
    {
        string Title { get; set; }
        IReadOnlyCollection<IQuestion> Questions { get; }
        float NeededScore { get; set; }
        string Serialize(bool @public = false);
        Quiz AddQuestion(IQuestion question);
        Quiz RemoveQuestion(IQuestion question);
    }
}
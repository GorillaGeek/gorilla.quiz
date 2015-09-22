using System;

namespace GorillaQuiz.Choice
{
    public abstract class AbstractChoice : IChoice
    {
        public abstract object Export(bool @public = false);
        public abstract bool Correct { get; }

        public static IChoice CreateFromJsonDynamic(dynamic choice, bool correct = false)
        {
            var type = (string)choice.type;

            if (type == "Text")
            {
                return TextChoice.Create((string)choice.text, correct);
            }

            throw new ArgumentException("Invalid choice type");
        }
    }
}
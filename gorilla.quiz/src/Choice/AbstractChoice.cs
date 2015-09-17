using System;

namespace GorillaQuiz.Choice
{
    public abstract class AbstractChoice : IChoice
    {
        public abstract object Export(bool @public = false);
        public abstract bool Correct { get; }

        public static IChoice CreateFromJsonDynamic(dynamic choice, bool correct)
        {
            var type = (string)choice.type;

            switch (type)
            {
                case "Text":
                    return TextChoice.Create((string)choice.text, correct);
            }

            throw new ArgumentException("Invalid choice type");
        }
    }
}
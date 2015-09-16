namespace GorillaQuiz.Choice
{
    public class TextChoice : IChoice
    {

        private readonly bool _correct;

        private TextChoice(string text, bool correct)
        {
            Text = text;
            _correct = correct;
        }

        public string Text { get; set; }

        public bool Correct => _correct;

        public static TextChoice Create(string text, bool isCorrect = false)
        {
            return new TextChoice(text, isCorrect);
        }

        public object ToObject(bool @public = false)
        {
            return new
            {
                type = "Text",
                text = Text
            };
        }
    }
}
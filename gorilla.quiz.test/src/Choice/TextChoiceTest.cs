using GorillaQuiz.Choice;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GorillaQuiz.Test.Choice
{
    [TestFixture]
    public class TextChoiceTest
    {
        [Test]
        public void TextChoiceCreate()
        {
            var choice = TextChoice.Create("Test", true);

            Assert.AreEqual("Test", choice.Text);
            Assert.AreEqual(true, choice.Correct);
        }

        [Test]
        public void SerializeTextChoice()
        {
            var choice = TextChoice.Create("Test", true);
            var serial = JsonConvert.SerializeObject(choice.Export());
            Assert.AreEqual("{\"type\":\"Text\",\"text\":\"Test\"}", serial);
        }

        [Test]
        public void ReconstructTextChoice()
        {
            var obj = JsonConvert.DeserializeObject("{\"type\":\"Text\",\"text\":\"Test\"}");

            var choice = AbstractChoice.CreateFromJsonDynamic(obj, false);

            Assert.AreEqual(choice.GetType(), typeof(TextChoice));
            Assert.AreEqual("Test", ((TextChoice)choice).Text);
        }
    }
}

using GorillaQuiz.Choice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GorillaQuiz.Test.Choice
{
    [TestClass]
    public class TextChoiceTest
    {
        [TestMethod]
        public void TextChoiceCreate()
        {
            var choice = TextChoice.Create("Test", true);

            Assert.AreEqual("Test", choice.Text);
            Assert.AreEqual(true, choice.Correct);
        }

        [TestMethod]
        public void SerializeTextChoice()
        {
            var choice = TextChoice.Create("Test", true);
            var serial = JsonConvert.SerializeObject(choice.ToObject());
            Assert.AreEqual("{\"type\":\"Text\",\"text\":\"Test\"}", serial);
        }

        [TestMethod]
        public void ReconstructTextChoice()
        {
            var obj = JsonConvert.DeserializeObject("{\"type\":\"Text\",\"text\":\"Test\"}");

            var choice = AbstractChoice.CreateFromJsonDynamic(obj, false);

            Assert.AreEqual(choice.GetType(), typeof(TextChoice));
            Assert.AreEqual("Test", ((TextChoice)choice).Text);
        }
    }
}

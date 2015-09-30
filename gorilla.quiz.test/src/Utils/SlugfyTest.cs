using GorillaQuiz.Utils;
using NUnit.Framework;

namespace GorillaQuiz.Test.Utils
{
    [TestFixture]
    public class SlugfyTest
    {

        [Test]
        public void SlugfyShouldMakeSlugs()
        {
            Assert.AreEqual("asd", Slugfy.ToSlug("ASD"));
            Assert.AreEqual("asd-as-d", Slugfy.ToSlug("ASD-AsD"));
            Assert.AreEqual("asd-as-d", Slugfy.ToSlug("------ASD----AsD----"));
            Assert.AreEqual("asd", Slugfy.ToSlug("ASDASDASD", 3));
        }

    }
}
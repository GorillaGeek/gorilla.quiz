using System;
using System.Dynamic;
using GorillaQuiz.Choice;
using NUnit.Framework;

namespace GorillaQuiz.Test.Choice
{
    [TestFixture]
    public class AbstractChoiceTest
    {
        [Test]
        public void ShoulThrowExceptionWhenCreateFromAnInvalidType()
        {

            dynamic obj = new ExpandoObject();
            obj.type = "Sbrubles";

            Assert.Throws<ArgumentException>(() => AbstractChoice.CreateFromJsonDynamic(obj, false));
        }

    }
}

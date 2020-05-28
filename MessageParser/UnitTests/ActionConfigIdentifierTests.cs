using MessageParser;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class MessageParserTests
    {
        [Test]
        public void TestOne()
        {
            var identifier = new ActionConfigIdentifier();
            var results = identifier.IdentifyFromMessage("some message");
            Assert.IsTrue(results.IsFail());
        }
    }
}
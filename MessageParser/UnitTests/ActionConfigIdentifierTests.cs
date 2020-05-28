using MessageParser;
using MessageParser.Model;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class MessageParserTests
    {
        [Test]
        public void TestOne()
        {
            var message = new TokenizedMessage("aa, bb, cc, dd", new MessageToken[] { });
            var aConfig = new ActionConfig("TestProduct", "SomeAction");
            
            var identifier = new ActionConfigIdentifier();
            var results = identifier.IdentifyFromMessage(new[] { aConfig }, message);
            Assert.IsTrue(results.IsFail());
        }
    }
}
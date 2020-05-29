using System.Linq;
using MessageParser;
using MessageParser.Helpers;
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
            var configs = new[]
            {
                new ActionConfig("ProductOne", "SomeActionOne") { Aliases = new[] { "SomeAliasOne" } },
                new ActionConfig("ProductOne", "SomeActionOne") { Aliases = new[] { "SomeAliasTwo" } },
                new ActionConfig("ProductOne", "SomeActionOne") { Aliases = new[] { "SomeAliasThree" } },
                new ActionConfig("ProductTwo", "SomeActionOne") { Aliases = new[] { "AnotherAliasOne" } },
                new ActionConfig("ProductTwo", "SomeActionTwo") { Aliases = new[] { "AnotherAliasTwo" } },
                new ActionConfig("ProductTwo", "SomeActionThree") { Aliases = new[] { "AnotherAliasThree" } }
            };
            
            var message = MessageTokenParser.ParseMessage("SomeAliasThree").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(configs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("ProductOne", results[0].Product);
            Assert.AreEqual("SomeActionOne", results[0].Name);
            Assert.AreEqual("SomeAliasThree", results[0].Aliases[0]);
        }
    }
}
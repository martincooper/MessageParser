using System.Linq;
using MessageParser;
using MessageParser.Helpers;
using MessageParser.Model;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ActionConfigIdentifierTestsWithNamesOnly
    {
        private ActionConfig[] getTestActionConfigsWithNamesOnly()
        {
            return new[]
            {
                new ActionConfig("Product_One", "SomeActionOne") { Aliases = new[] { "SomeAliasOne" } },
                new ActionConfig("Product_One", "SomeActionOne") { Aliases = new[] { "SomeAliasTwo" } },
                new ActionConfig("Product_One", "SomeActionOne") { Aliases = new[] { "SomeAliasThree" } },
                new ActionConfig("Product_Two", "SomeActionOne") { Aliases = new[] { "AnotherAliasOne" } },
                new ActionConfig("Product_Two", "SomeActionTwo") { Aliases = new[] { "AnotherAliasTwo" } },
                new ActionConfig("Product_Two", "SomeActionThree") { Aliases = new[] { "AnotherAliasThree" } },
                new ActionConfig("Product_Three", "SomeActionTwo") { Aliases = new[] { "AnotherNewAlias" } }
            };
        }
        
        [Test]
        public void GetActionByUniqueAlias()
        {
            var actionConfigs = getTestActionConfigsWithNamesOnly();
            var message = MessageTokenParser.ParseMessage("SomeAliasThree").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("Product_One", results[0].Product);
            Assert.AreEqual("SomeActionOne", results[0].Name);
            Assert.AreEqual("SomeAliasThree", results[0].Aliases[0]);
        }
        
        [Test]
        public void GetDuplicateActionsByName()
        {
            var actionConfigs = getTestActionConfigsWithNamesOnly();
            var message = MessageTokenParser.ParseMessage("SomeActionTwo").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 2);
            Assert.AreEqual("Product_Two", results[0].Product);
            Assert.AreEqual("SomeActionTwo", results[0].Name);
            Assert.AreEqual("AnotherAliasTwo", results[0].Aliases[0]);
            
            Assert.AreEqual("Product_Three", results[1].Product);
            Assert.AreEqual("SomeActionTwo", results[1].Name);
            Assert.AreEqual("AnotherNewAlias", results[1].Aliases[0]);
        }
        
        [Test]
        public void GetActionByUniqueProductAndName()
        {
            var actionConfigs = getTestActionConfigsWithNamesOnly();
            var message = MessageTokenParser.ParseMessage(" product_two  someactionthree ").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("Product_Two", results[0].Product);
            Assert.AreEqual("SomeActionThree", results[0].Name);
            Assert.AreEqual("AnotherAliasThree", results[0].Aliases[0]);
        }
    }
}
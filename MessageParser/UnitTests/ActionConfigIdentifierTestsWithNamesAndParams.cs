using System;
using System.Linq;
using MessageParser;
using MessageParser.Helpers;
using MessageParser.Model;
using NUnit.Framework;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestFixture]
    public class ActionConfigIdentifierTestsWithNamesAndParams
    {
        private ActionConfig[] getTestActionConfigsWithNamesAndParams()
        {
            return new[]
            {
                new ActionConfig("Product_One", "SomeAction"),
                
                new ActionConfig("Product_One", "SomeActionOne")
                    .WithParam("RunDate", typeof(DateTime))
                    .WithParam("UserName", typeof(string)),
                
                new ActionConfig("Product_One", "SomeActionTwo")
                    .WithParam("RunDate", typeof(DateTime))
                    .WithParam("NumberOfItems", typeof(long)),
                
                new ActionConfig("Product_One", "SomeActionThree")
                    .WithParam("RunDate", typeof(DateTime))
                    .WithParam("NumberOfItems", typeof(long))
                    .WithParam("MaxCount", typeof(int)),
            };
        }
        
        [Test]
        public void GetActionWithNoParams()
        {
            var actionConfigs = getTestActionConfigsWithNamesAndParams();
            var message = MessageTokenParser.ParseMessage("product_one").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("Product_One", results[0].Product);
            Assert.AreEqual("SomeAction", results[0].Name);
        }
        
        [Test]
        public void GetActionByUniqueAlias()
        {
            var actionConfigs = getTestActionConfigsWithNamesAndParams();
            var message = MessageTokenParser.ParseMessage("product_one 01-jan-2020 numberofitems = 5000").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("Product_One", results[0].Product);
            Assert.AreEqual("SomeActionTwo", results[0].Name);
        }
        
        [Test]
        public void GetDuplicateActionsByName()
        {
            var actionConfigs = getTestActionConfigsWithNamesAndParams();
            var message = MessageTokenParser.ParseMessage("product_one 01-jan-2020 5000 100").GetValue();
            var results = ActionConfigIdentifier.IdentifyAllFromMessage(actionConfigs, message).ToArray();
            
            Assert.IsTrue(results.Length() == 1);
            Assert.AreEqual("Product_One", results[0].Product);
            Assert.AreEqual("SomeActionThree", results[0].Name);
        }
    }
}
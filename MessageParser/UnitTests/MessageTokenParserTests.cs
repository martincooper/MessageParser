using System.Linq;
using MessageParser;
using MessageParser.Model;
using NUnit.Framework;
using Sprache;

namespace UnitTests
{
    [TestFixture]
    public class MessageTokenParserTests
    {
        [Test]
        public void NameAndSimpleValueWithNoWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name=value");

            Assert.AreEqual(MessageTokenType.Double, result.TokenType);
            Assert.AreEqual("name", result.ItemOne);
            Assert.AreEqual("value", result.ItemTwo);
        }
        
        [Test]
        public void NameAndSimpleValueWithWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name  =    value");

            Assert.AreEqual(MessageTokenType.Double, result.TokenType);
            Assert.AreEqual("name", result.ItemOne);
            Assert.AreEqual("value", result.ItemTwo);
        }
        
        [Test]
        public void NameAndQuotedValueWithNoWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name=\"value\"");

            Assert.AreEqual(result.TokenType, MessageTokenType.Double);
            Assert.AreEqual(result.ItemOne, "name");
            Assert.AreEqual(result.ItemTwo, "value");
        }
        
        [Test]
        public void NameAndQuotedValueWithWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name  =  \"value\"");

            Assert.AreEqual(MessageTokenType.Double, result.TokenType);
            Assert.AreEqual("name", result.ItemOne);
            Assert.AreEqual("value", result.ItemTwo);
        }
        
        [Test]
        public void NameAndQuotedValueWithSpacesAndWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name  =  \" some test value \"");

            Assert.AreEqual(MessageTokenType.Double, result.TokenType);
            Assert.AreEqual("name", result.ItemOne);
            Assert.AreEqual(" some test value ", result.ItemTwo);
        }
        
        [Test]
        public void FullMessageSimple()
        {
            var result = MessageTokenParser.ParseFullMessage.Parse("name=value").ToArray();

            Assert.AreEqual(result.Length, 1);
            
            Assert.AreEqual(MessageTokenType.Double, result[0].TokenType);
            Assert.AreEqual("name", result[0].ItemOne);
            Assert.AreEqual("value", result[0].ItemTwo);
        }
        
        [Test]
        public void FullMessageComplex()
        {
            var msg = "  serviceName actionName  param1=50 param2 = 500  param3 = \" some text \"  \"final text\"  ";
            var result = MessageTokenParser.ParseFullMessage.Parse(msg).ToArray();

            Assert.AreEqual(result.Length, 6);
            
            Assert.AreEqual(MessageTokenType.Single, result[0].TokenType);
            Assert.AreEqual("serviceName", result[0].ItemOne);

            Assert.AreEqual(MessageTokenType.Single, result[1].TokenType);
            Assert.AreEqual("actionName", result[1].ItemOne);
            
            Assert.AreEqual(MessageTokenType.Double, result[2].TokenType);
            Assert.AreEqual("param1", result[2].ItemOne);
            Assert.AreEqual("50", result[2].ItemTwo);
            
            Assert.AreEqual(MessageTokenType.Double, result[3].TokenType);
            Assert.AreEqual("param2", result[3].ItemOne);
            Assert.AreEqual("500", result[3].ItemTwo);
            
            Assert.AreEqual(MessageTokenType.Double, result[4].TokenType);
            Assert.AreEqual("param3", result[4].ItemOne);
            Assert.AreEqual(" some text ", result[4].ItemTwo);
            
            Assert.AreEqual(MessageTokenType.Single, result[5].TokenType);
            Assert.AreEqual("final text", result[5].ItemOne);
        }
    }
}
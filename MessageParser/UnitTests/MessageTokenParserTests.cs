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

            Assert.AreEqual(result.TokenType, MessageTokenType.Double);
            Assert.AreEqual(result.ItemOne, "name");
            Assert.AreEqual(result.ItemTwo, "value");
        }
        
        [Test]
        public void NameAndSimpleValueWithWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name  =    value");

            Assert.AreEqual(result.TokenType, MessageTokenType.Double);
            Assert.AreEqual(result.ItemOne, "name");
            Assert.AreEqual(result.ItemTwo, "value");
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

            Assert.AreEqual(result.TokenType, MessageTokenType.Double);
            Assert.AreEqual(result.ItemOne, "name");
            Assert.AreEqual(result.ItemTwo, "value");
        }
        
        [Test]
        public void NameAndQuotedValueWithSpacesAndWhitespace()
        {
            var result = MessageTokenParser.NameAndValue.Parse("name  =  \" some test value \"");

            Assert.AreEqual(result.TokenType, MessageTokenType.Double);
            Assert.AreEqual(result.ItemOne, "name");
            Assert.AreEqual(result.ItemTwo, " some test value ");
        }
        
    }
}
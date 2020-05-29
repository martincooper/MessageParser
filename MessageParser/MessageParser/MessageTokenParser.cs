using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;
using MessageParser.Model;
using Sprache;

namespace MessageParser
{
    public static class MessageTokenParser
    {
        /// NOTE : Tokenize the message. A token is a Name OR Value only, or BOTH Name and Value.
        /// At this point we don't know what is a name or a value. That will be determined later when
        /// trying to match the tokens to an Action Config.
        ///
        /// A Message Token can be...
        /// 1 : someText                 (single string with no spaces)                  - TokenType.Single
        /// 2 : "someText with spaces"   (single quoted string with spaces)              - TokenType.Single
        /// 3 : value = something        (string = string) or (string=string)            - TokenType.Double
        /// 4 : value = "some text"      (string = "quoted string with maybe spaces")    - TokenType.Double
        
        private const string BotName = "@BOT";
        
        public static Try<TokenizedMessage> ParseMessage(string message)
        {
            return Try(() => ParseFullMessage.Parse(message))
                .Map(tokens => new TokenizedMessage(message, tokens.ToArray()));
        }

        /// <summary>
        /// NOTE : Parsers defined as public to allow ease of Unit Testing each distinct component.
        /// </summary>
        //public static readonly Parser<string> BotMention = Parse.String(BotName);
        public static readonly Parser<char> EqualSign = Parse.Char('=');
        public static readonly Parser<char> DoubleQuote = Parse.Char('"');
        public static readonly Parser<char> QuotedText = Parse.AnyChar.Except(DoubleQuote);
        public static readonly Parser<char> IgnoredChars = Parse.WhiteSpace.Or(DoubleQuote).Or(EqualSign);
        public static readonly Parser<string> Word = Parse.AnyChar.Except(IgnoredChars).AtLeastOnce().Text();
        public static readonly Parser<IOption<string>> MaybeSpace = Parse.WhiteSpace.AtLeastOnce().Text().Optional();
        
        public static readonly Parser<string> BotMention =
            from sp1 in MaybeSpace
            from name in Parse.IgnoreCase(BotName).Text()
            from sp2 in MaybeSpace
            select name;
        
        public static readonly Parser<string> QuotedString =
            from open in DoubleQuote
            from text in QuotedText.AtLeastOnce().Text()
            from close in DoubleQuote
            select text;
        
        /// <summary>
        /// Parses a single text item, a single word or quoted text. 
        /// </summary>
        public static readonly Parser<MessageToken> NameOrValue =
            from nameOrValue in Word.Or(QuotedString)
            select new MessageToken(nameOrValue);
        
        /// <summary>
        /// Parses a name with a value.
        /// </summary>
        public static readonly Parser<MessageToken> NameAndValue =
            from name in Word
            from sp1 in MaybeSpace
            from eq in EqualSign
            from sp2 in MaybeSpace
            from value in Word.Or(QuotedString)
            select new MessageToken(name, value);

        /// <summary>
        /// Parses a Token, either a Single or Double. 
        /// </summary>
        public static readonly Parser<MessageToken> MessageToken =
            from token in NameAndValue.Or(NameOrValue)
            select token;
        
        /// <summary>
        /// Parses a full message returning a collection of Message Tokens.
        /// </summary>
        public static readonly Parser<IEnumerable<MessageToken>> ParseFullMessage =
            from sp1 in MaybeSpace
            from botName in BotMention.Optional()
            from tokens in MessageToken.DelimitedBy(Parse.WhiteSpace.AtLeastOnce())
            from sp2 in MaybeSpace
            select tokens;
    }
}
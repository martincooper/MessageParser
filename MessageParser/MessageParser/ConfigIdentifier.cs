using System;
using LanguageExt;
using MessageParser.Model;
using static LanguageExt.Prelude;

namespace MessageParser
{
    public class ConfigIdentifier
    {
        public Try<MessageConfig> IdentifyFromMessage(string message)
        {
            return error("Not Implemented");
        }
        
        Try<MessageConfig> error(string error)
        {
            return Try<MessageConfig>(new MessageParseException(error));
        }
        
        Try<MessageConfig> error(string error, Exception exception)
        {
            return Try<MessageConfig>(new MessageParseException(error, exception));
        }
    }
}
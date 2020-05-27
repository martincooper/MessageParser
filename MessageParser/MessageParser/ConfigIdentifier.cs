using System;
using LanguageExt;
using MessageParser.Model;
using static LanguageExt.Prelude;

namespace MessageParser
{
    /// <summary>
    /// Notes :
    /// {serviceName} {actionName} {alias} {params}
    /// </summary>
    
    public class ConfigIdentifier
    {
        public Try<MessageConfig> IdentifyFromMessage(string message)
        {
            return error("Not Implemented");
        }

        static Try<MessageConfig> error(string error) => 
            Try<MessageConfig>(new MessageParseException(error));
        
        static Try<MessageConfig> error(string error, Exception exception) => 
            Try<MessageConfig>(new MessageParseException(error, exception));
    }
}
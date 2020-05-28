using System;
using LanguageExt;
using MessageParser.Model;
using static LanguageExt.Prelude;

namespace MessageParser
{
    

    
    /// <summary>
    /// {serviceName} {actionName} {alias} {params}
    /// 1 : Get first param
    /// 2 : Match on Service Name -> Action Name -> Alias
    /// 
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
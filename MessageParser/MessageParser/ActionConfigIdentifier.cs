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
    
    public class ActionConfigIdentifier
    {
        public Try<ActionConfig> IdentifyFromMessage(string message)
        {
            return error("Not Implemented");
        }

        static Try<ActionConfig> error(string error) => 
            Try<ActionConfig>(new MessageParseException(error));
        
        static Try<ActionConfig> error(string error, Exception exception) => 
            Try<ActionConfig>(new MessageParseException(error, exception));
    }
}
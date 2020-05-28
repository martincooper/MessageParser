using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using MessageParser.Model;
using static LanguageExt.Prelude;

namespace MessageParser
{
    /// <summary>
    /// {serviceName} {actionName} {alias} {params}
    /// 1 : Match first token to ServiceName
    /// 2 : Match 
    /// 2 : Match on Service Name -> Action Name -> Alias
    /// 
    /// </summary>
    
    public class ActionConfigIdentifier
    {
        public Try<ActionConfig> IdentifyFromMessage(ActionConfig[] actionConfigs, TokenizedMessage message)
        {
            var tokens = message.Tokens;
            
            // Need at least one "Single" token to be able to match anything.
            if (!tokens.Any() || tokens[0].TokenType == MessageTokenType.Double)
                return error($"Couldn't find matching action for message '{message.Message}'.");

            // First see if the first token matches Product Name...
            var products = matchByProduct(actionConfigs, tokens[0].ItemOne);

            if (!products.Any())
            {
                var names = matchByActionName(actionConfigs, tokens[0].ItemOne);

                if (!names.Any())
                {
                    var aliases = matchByAlias(actionConfigs, tokens[0].ItemOne);
                }
            }

            return null;
        }

        // Gets all Action Configs which match by product name.
        static IEnumerable<ActionConfig> matchByProduct(ActionConfig[] actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Product, value));

        // Gets all Action Configs which match by action name.
        static IEnumerable<ActionConfig> matchByActionName(ActionConfig[] actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Name, value));

        // Gets all Action Configs which match by any specified alias.
        static IEnumerable<ActionConfig> matchByAlias(ActionConfig[] actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Aliases, value));
        
        static bool isMatch(string one, string two) => 
            string.Equals(one, two, StringComparison.OrdinalIgnoreCase);
        
        static bool isMatch(string[] one, string two) =>
            one.Any(s => isMatch(s, two));
        
        static Try<ActionConfig> error(string error) => 
            Try<ActionConfig>(new MessageParseException(error));
        
        static Try<ActionConfig> error(string error, Exception exception) => 
            Try<ActionConfig>(new MessageParseException(error, exception));
    }
}
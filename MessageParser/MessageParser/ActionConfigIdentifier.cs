using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using MessageParser.Helpers;
using MessageParser.Model;
using static LanguageExt.Prelude;

namespace MessageParser
{
    /// <summary>
    /// {serviceName} {actionName} {alias} {params}
    /// Top level match ->
    ///     1 : Try matching on Product,
    ///     2 : Try matching on Action Name,
    ///     3 : Try Matching on a defined Alias.
    /// Secondary Parameter Match ->
    ///     1 : Match on Parameter Count -> Remaining Token count.
    ///     2 : Match on Parameter Types -> Check each token value can be parsed into the specified type. 
    /// </summary>
    public class ActionConfigIdentifier
    {
        public Try<ActionConfig> IdentifyFromMessage(ActionConfig[] actionConfigs, TokenizedMessage message)
        {
            var tokenQueue = new Queue<MessageToken>(message.Tokens);
            
            // Need at least one "Single" token to be able to match anything.
            if (!tokenQueue.Any() || tokenQueue.Peek().TokenType == MessageTokenType.Double)
                return error($"Couldn't find matching action for message '{message.Message}'.");

            // First try matching on a name before matching on parameters.
            var matchingActionConfigs = matchByName(actionConfigs, tokenQueue).ToArray();
            
            return null;
        }

        /// <summary>
        /// Attempts to match and filter by name or names, on Product Name, Action Name and / or Alias. 
        /// </summary>
        static IEnumerable<ActionConfig> matchByName(IEnumerable<ActionConfig> actionConfigs, Queue<MessageToken> tokens)
        {
            return actionConfigs.Pipe(
                r => matchByProduct(r, tokens),
                r => matchByActionName(r, tokens),
                r => matchByAlias(r, tokens));
        }
        
        static IEnumerable<ActionConfig> matchByProduct(IEnumerable<ActionConfig> actionConfigs, Queue<MessageToken> tokens) =>
            findMatch(actionConfigs, tokens, matchByProduct);

        static IEnumerable<ActionConfig> matchByActionName(IEnumerable<ActionConfig> actionConfigs, Queue<MessageToken> tokens) =>
            findMatch(actionConfigs, tokens, matchByActionName);
        
        static IEnumerable<ActionConfig> matchByAlias(IEnumerable<ActionConfig> actionConfigs, Queue<MessageToken> tokens) =>
            findMatch(actionConfigs, tokens, matchByAlias);
        
        // Gets all Action Configs which match by product name.
        static IEnumerable<ActionConfig> matchByProduct(IEnumerable<ActionConfig> actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Product, value));

        // Gets all Action Configs which match by action name.
        static IEnumerable<ActionConfig> matchByActionName(IEnumerable<ActionConfig> actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Name, value));

        // Gets all Action Configs which match by any specified alias.
        static IEnumerable<ActionConfig> matchByAlias(IEnumerable<ActionConfig> actionConfigs, string value) =>
            actionConfigs.Where(ac => isMatch(ac.Aliases, value));
        
        static IEnumerable<ActionConfig> findMatch(
            IEnumerable<ActionConfig> actionConfigs, 
            Queue<MessageToken> tokens,
            Func<IEnumerable<ActionConfig>, string, IEnumerable<ActionConfig>> actionFilter)
        {
            // If no tokens left in the queue, return original collection of action configs.
            if (!tokens.Any()) return actionConfigs;
            
            // If the Token Type is Double (name / value pair), return original collection of action configs.
            if (tokens.Peek().TokenType == MessageTokenType.Double) return actionConfigs;

            var actionConfigArray = actionConfigs.ToArray();
            var matchingConfigs = actionFilter(actionConfigArray, tokens.Peek().ItemOne).ToArray();

            // If no matches, return full set and don't pop current token from the queue.
            if (!matchingConfigs.Any()) return actionConfigArray;

            tokens.Dequeue();
            return matchingConfigs;
        }

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
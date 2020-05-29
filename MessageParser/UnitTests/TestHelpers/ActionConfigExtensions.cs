using System;
using System.Linq;
using MessageParser.Model;

namespace UnitTests.TestHelpers
{
    public static class ActionConfigExtensions
    {
        public static ActionConfig WithParam(this ActionConfig actionConfig, string name, Type type)
        {
            actionConfig.Parameters = actionConfig.Parameters
                .Concat(new[] { new ActionParameter(name, type) }).ToArray();

            return actionConfig;
        }
    }
}
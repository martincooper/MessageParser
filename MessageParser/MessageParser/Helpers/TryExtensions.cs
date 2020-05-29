using System;
using LanguageExt;

namespace MessageParser.Helpers
{
    public static class TryExtensions
    {
        public static T GetValue<T>(this Try<T> value)
        {
            return value.Match(
                Succ: v => v,
                Fail: err => throw new Exception("Invalid access to Try Value for a Fail."));
        }
    }
}
using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace MessageParser.Helpers
{
    public static class StringToType
    {
        public static bool ConvertsToType(Type type, string value)
        {
            if (type == typeof(int)) return IsInt(value).IsSome;
            if (type == typeof(long)) return IsLong(value).IsSome;
            if (type == typeof(bool)) return IsBool(value).IsSome;
            if (type == typeof(string)) return IsString(value).IsSome;
            if (type == typeof(DateTime)) return IsDateTime(value).IsSome;
            
            return false;
        }
        
        public static Option<string> IsString(string value) =>
            !string.IsNullOrEmpty(value)
                ? Some(value)
                : Option<string>.None;
        
        public static Option<bool> IsBool(string value) =>
            bool.TryParse(value, out var x)
                ? Some(x)
                : Option<bool>.None;

        public static Option<DateTime> IsDateTime(string value) => 
            DateTime.TryParse(value, out var x)
                ? Some(x)
                : Option<DateTime>.None;
        
        public static Option<int> IsInt(string value) =>
            int.TryParse(value, out var x)
                ? Some(x)
                : Option<int>.None;
        
        public static Option<long> IsLong(string value) =>
            long.TryParse(value, out var x)
                ? Some(x)
                : Option<long>.None;
    }
}
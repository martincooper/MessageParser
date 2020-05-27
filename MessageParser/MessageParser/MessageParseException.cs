using System;

namespace MessageParser
{
    public class MessageParseException : Exception
    {
        public MessageParseException()
        { }        
        
        public MessageParseException(string message) : base(message)
        { }
        
        public MessageParseException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
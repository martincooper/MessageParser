namespace MessageParser.Model
{
    public class TokenizedMessage
    {
        public TokenizedMessage(string message)
        {
            Message = message;
            Tokens = new MessageToken[] { };
        }
        
        public TokenizedMessage(string message, MessageToken[] tokens)
        {
            Message = message;
            Tokens = tokens;
        }        
        
        public string Message { get; }

        public MessageToken[] Tokens { get; }
    }
}
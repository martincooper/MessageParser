namespace MessageParser.Model
{
    public class TokenizedMessage
    {
        public TokenizedMessage(string message)
        {
            Message = message;
        }

        public TokenizedMessage(string message, MessageToken[] tokens)
        {
            Message = message;
            Tokens = tokens;
        }

        public string Message { get; }

        public MessageToken[] Tokens { get; } = { };
    }
}
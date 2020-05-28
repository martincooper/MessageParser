namespace MessageParser.Model
{
    /// <summary>
    /// Message Token.
    /// This stores a section of a message.
    /// </summary>
    public class MessageToken
    {
        public MessageToken(string item)
        {
            ItemOne = item;
            TokenType = MessageTokenType.Single;
        }
        
        public MessageToken(string itemOne, string itemTwo)
        {
            ItemOne = itemOne;
            ItemTwo = itemTwo;
            TokenType = MessageTokenType.Double;
        }
        
        public string ItemOne { get; }
        
        public string ItemTwo { get; }
        
        public MessageTokenType TokenType { get; } 
    }
}
namespace MessageParser.Model
{
    public enum MessageTokenType
    {
        /// <summary>
        /// Token contains a single string, either a name or a value.
        /// </summary>
        Single,
        
        /// <summary>
        /// Token contains two strings, a name and a value.
        /// </summary>
        Double
    }
}
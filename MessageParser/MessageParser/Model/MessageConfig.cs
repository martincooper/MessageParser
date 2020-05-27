namespace MessageParser.Model
{
    public class MessageConfig
    {
        public MessageConfig()
        { }

        public MessageConfig(string product, string name)
        {
            Product = product;
            Name = name;
        }
        
        public string Product { get; set; }
        
        /// <summary>
        ///  Name of action.
        /// </summary>
        public string Name { get; set; }
        
        public string[] Aliases { get; set; } = { };

        public MessageParameter[] Parameters { get; set; } = { };
    }
}
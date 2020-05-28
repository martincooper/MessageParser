namespace MessageParser.Model
{
    public class ActionConfig
    {
        public ActionConfig()
        { }

        public ActionConfig(string product, string name)
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
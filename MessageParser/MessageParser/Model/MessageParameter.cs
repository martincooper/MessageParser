using System;

namespace MessageParser.Model
{
    public class MessageParameter
    {
        public MessageParameter() 
        { }

        public MessageParameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }
        
        public string Name { get; set; }
        
        public Type Type { get; set; }
    }
}
using System;

namespace MessageParser.Model
{
    public class ActionParameter
    {
        public ActionParameter() 
        { }

        public ActionParameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }
        
        public string Name { get; set; }
        
        public Type Type { get; set; }
    }
}
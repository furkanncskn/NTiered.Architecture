using System;

namespace Entity.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ClassAttribute : System.Attribute
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class KeyAttribute : System.Attribute
    {
        private int _key;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}

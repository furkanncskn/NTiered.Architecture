using System;

namespace Entity.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : System.Attribute
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
    }
}

using System;

namespace Binance.Serialization
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class FormField : Attribute
    {
        public String Name { get; set; }
    }
}

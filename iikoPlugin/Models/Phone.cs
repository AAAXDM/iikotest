using Resto.Front.Api.Data.Brd;
using System.Runtime.Serialization;


namespace iikoPlugin
{
    internal class Phone : IPhone
    {
        public string Value { get; private set; }

        public bool IsMain { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value, typeof(string));
            info.AddValue("IsMain", IsMain, typeof(bool));
        }

        public Phone(SerializationInfo info, StreamingContext context) 
        {
            Value = info.GetString("Value");
            IsMain = info.GetBoolean("IsMain");
        }

        public Phone(string value,bool isMain) 
        { 
            Value = value;
            IsMain = isMain;
        }
    }
}

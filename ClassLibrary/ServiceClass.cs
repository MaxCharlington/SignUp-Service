using System;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class ServiceClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(ServiceClass);

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }
        
        [DataMember]
        public TimeClass Length { get; set; }

        public ServiceClass()
        {
            Name = "";
            Price = -1;
            Length = new TimeClass();
        }

        public ServiceClass(string name, int price, TimeClass length = null)
        {
            Name = name;
            Price = price;
            if (length == null) {
                Length = new TimeClass();
            }
            else {
                Length = length;
            }
        }
    }
}

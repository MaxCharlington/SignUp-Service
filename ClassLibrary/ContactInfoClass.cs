using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class ContactInfoClass
    {
        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Address { get; set; }

        public ContactInfoClass()
        {
            PhoneNumber = "";
            Email = "";
            Address = "";
        }

        public ContactInfoClass(string phoneNumber, string email = "", string address = "")
        {
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }
    }
}

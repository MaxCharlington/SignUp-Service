using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class UserClass
    {
        [DataMember]
        public ContactInfoClass ContactInfo { get; set; }

        [DataMember]
        public int Id { get; set; } = -1;
        //-1 - unregistred user
        //>-1 - valid id
        
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }
        
        public UserClass()
        {
            ContactInfo = new ContactInfoClass();
            Id = -1;
            Login = "";
            PasswordHash = "";
        }

        public UserClass(ContactInfoClass contactInfo)
        {
            ContactInfo = contactInfo;
            Login = "";
            PasswordHash = "";
            Id = -1;
        }

        //ctor for backend
        public UserClass(int id, string login, string passwordHash, ContactInfoClass contactInfo = null)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
            ContactInfo = contactInfo;
        }

        public UserClass(string login, string passwordHash, ContactInfoClass contactInfo = null)
        {
            Login = login;
            PasswordHash = passwordHash;
            ContactInfo = contactInfo;
        }
    }
}
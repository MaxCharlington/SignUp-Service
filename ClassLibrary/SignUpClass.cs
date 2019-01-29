using System;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SignUpClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(SignUpClass);

        [DataMember]
        public CompanyClass Company { get; set; }

        [DataMember]
        public UserClass User { get; set; }

        [DataMember]
        public ServiceClass Service { get; set; }

        [DataMember]
        public TimeClass Time { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public bool Confirmed { get; set; }

        public SignUpClass()
        {
            Company = new CompanyClass();
            User = new UserClass();
            Service = new ServiceClass();
            Time = new TimeClass();
            Comment = "";
            Confirmed = false;
        }

        public SignUpClass(UserClass user, CompanyClass company, ServiceClass service, TimeClass time, string comment = "")
        {
            User = user;
            Company = company;
            Service = service;
            Time = time;
            Comment = comment;
            Confirmed = false;
        }
    }
}

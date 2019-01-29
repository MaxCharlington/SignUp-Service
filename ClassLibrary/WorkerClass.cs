using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class WorkerClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(WorkerClass);

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string SecondName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public List<ServiceClass> Skills { get; set; }

        public WorkerClass()
        {
            FirstName = SecondName = MiddleName = "";
            Skills = new List<ServiceClass>();
        }

        public WorkerClass(string first, string second, string middle = "", List<ServiceClass> skills = null)
        {
            FirstName = first;
            SecondName = second;
            MiddleName = middle;
            if (skills != null) {
                Skills = skills;
            }
            else {
                Skills = new List<ServiceClass>();
            }
        }
    }
}

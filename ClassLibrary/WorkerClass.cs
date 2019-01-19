using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class WorkerClass
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<ServiceClass> Skills { get; set; }

        public WorkerClass()
        {
            Name = "";
            Skills = new List<ServiceClass>();
        }

        public WorkerClass(string name, List<ServiceClass> skills = null)
        {
            Name = name;
            if (skills != null) {
                Skills = skills;
            }
            else {
                Skills = new List<ServiceClass>();
            }
        }
    }
}

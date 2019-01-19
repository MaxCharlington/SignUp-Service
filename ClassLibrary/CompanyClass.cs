using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class CompanyClass : UserClass
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public WorkTimeClass WorkTime { get; set; }

        [DataMember]
        public List<ServiceClass> Services { get; set; } = new List<ServiceClass>();

        [DataMember]
        public List<WorkerClass> Workers{ get; set; } = new List<WorkerClass>();

        public CompanyClass()
        {
            Name = "";
            WorkTime = new WorkTimeClass();
            Services = new List<ServiceClass>();
            Workers = new List<WorkerClass>();
        }

        public CompanyClass(string name, WorkTimeClass workTime)
        {
            Name = name;
            WorkTime = workTime; 
            Services = new List<ServiceClass>();
            Workers = new List<WorkerClass>();
        }
    }
}

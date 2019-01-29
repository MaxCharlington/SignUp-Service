using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class CompanyClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(CompanyClass);

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public WorkTimeClass WorkTime { get; set; }

        [DataMember]
        public List<int> ServiceIds { get; set; } = new List<int>();

        public CompanyClass()
        {
            Name = "";
            WorkTime = new WorkTimeClass();
            ServiceIds = new List<int>();
        }

        public CompanyClass(string name, WorkTimeClass workTime)
        {
            Name = name;
            WorkTime = workTime;
            ServiceIds = new List<int>();
        }
    }
}

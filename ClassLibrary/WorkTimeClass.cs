using System;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class WorkTimeClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(WorkTimeClass);

        [DataMember]
        public TimeClass[] WorkTimes { get; set; }

        [DataMember]
        public TimeClass[] BreakTimes { get; set; }

        public WorkTimeClass()
        {
            WorkTimes = new TimeClass[7]{
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass()
            }; 
            BreakTimes = new TimeClass[7]{
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass()
            };
        }

        public WorkTimeClass(TimeClass[] workTimes, TimeClass[] breakTimes = null)
        {
            if (workTimes.Length == 7 && breakTimes.Length == 7) 
            {
                WorkTimes = workTimes;
                BreakTimes = breakTimes;
            }
            else {
                WorkTimes = new TimeClass[7]{
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass()
            };
                BreakTimes = new TimeClass[7]{
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass(),
                new TimeClass()
            };
            }
        }
    }
}

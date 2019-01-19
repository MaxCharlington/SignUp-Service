using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class TimeClass
    {
        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Beginning { get; set; }

        [DataMember]
        public string Length { get; set; }

        public TimeClass()
        {
            Date = "";
            Beginning = "";
            Length = "";
        }

        public TimeClass(string beginning, string date = "", string length = "")
        {
            Beginning = beginning;
            Date = date;
            Length = length;
        }
    }
}

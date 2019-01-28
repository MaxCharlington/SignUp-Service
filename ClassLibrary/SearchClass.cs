using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SearchClass
    {
        [DataMember]
        public string Input { get; private set; }

        public SearchClass(string input)
        {
            Input = input;
        }

        public SearchClass()
        {
            Input = "";
        }
    }
}

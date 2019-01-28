using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SearchResultClass
    {
        [DataMember]
        public string Answer { get; private set; }

        public SearchResultClass(string answer)
        {
            Answer = answer;
        }

        public SearchResultClass()
        {
            Answer = "";
        }
    }
}


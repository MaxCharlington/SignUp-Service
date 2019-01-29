using System;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SearchResultClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(SearchResultClass);
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


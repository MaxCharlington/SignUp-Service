using System;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SearchClass : IJSONConvertible
    {
        public Type Type { get; } = typeof(SearchClass);

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

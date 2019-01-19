using System.Runtime.Serialization;

namespace ToolLibrary
{
    [DataContract]
    public class RequestContext
    {
        public RequestContext() { }

        public RequestContext(int cmdId, string strData = "", int intData = 0, string login = "", string password = "") 
        {
            CmdId = cmdId;
            Login = login;
            Password = password;
            StrData = strData;
            IntData = intData;
        }

        [DataMember]
        public int CmdId { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string StrData { get; set; }
        [DataMember]
        public int IntData { get; set; }
    }
}

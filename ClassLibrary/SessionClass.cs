﻿using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class SessionClass
    {
        [DataMember]
        public int UserId { get; set; }
        
        [DataMember]
        public string SessionId { get; set; }

        public SessionClass(string sessionID, int userId = -1)
        {
            SessionId = sessionID;
            UserId = userId;
        }

        public SessionClass()
        {

        }
    }
}
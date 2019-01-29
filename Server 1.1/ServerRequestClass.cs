using System.IO;
using System.Net;

using ClassLibrary;

namespace Server
{
    public sealed class Request
    {
        public HttpListenerRequest Requested { get; private set; }

        public HttpListenerResponse Response { get; private set; }

        public Request() { }

        public Request(HttpListenerResponse client, HttpListenerRequest requested)
        {
            Requested = requested;
            Response = client;
        }

        public RequestContext GetPostRequestData()
        {
            if (Requested.HttpMethod == "POST")
            {
                Stream body = Requested.InputStream;
                StreamReader reader = new StreamReader(body);
                string json = reader.ReadToEnd();
                RequestContext context = json.JSONParse<RequestContext>();
                return context;
            }
            else return null;
        }
    }
}

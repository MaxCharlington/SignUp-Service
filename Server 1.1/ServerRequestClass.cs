using System.IO;
using System.Net;

using ToolLibrary;

namespace Server
{
    public class Request
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
            Stream body = Requested.InputStream;
            StreamReader reader = new StreamReader(body);
            string json = reader.ReadToEnd();
            RequestContext context = new RequestContext();
            context = (RequestContext)JSON.Parse(json, context.GetType());
            return context;
        }
    }
}

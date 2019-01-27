using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using ToolLibrary;
using Database;

namespace Server
{
    public class Server : IServer
    {
        //Singleton
        private static Server instance = null;
        private Server() { }
        public static IServer GetInstance()
        {
            if (instance == null)
                instance = new Server();
            return instance;
        }

        //Event interface
        public delegate void ServerEvent();
        public event ServerEvent onStart;
        public event ServerEvent onStop;
        public event ServerEvent onRequestAdded;
        public event ServerEvent onRequestHandled;

        public ushort Port { get; private set; }
        private HttpListener _HttpListener;
        private DatabaseClass Database { get; } = DatabaseClass.GetInstance();
        
        public int SessionRequestCount { private set; get; } = 0;
        private int UnhandledRequestsCount = 0;

        //IServer implementation
        public void StartServer(ushort port = 80)
        {
            Port = port;
            onStart += () => { }; 
            onStop += () => { };
            onRequestAdded += () => { UnhandledRequestsCount++; }; 
            onRequestHandled = () => { UnhandledRequestsCount--; SessionRequestCount++; };
            Database.OpenConnection();
            _HttpListener = new HttpListener();
            _HttpListener.Prefixes.Add($"http://*:{Port}/");
            _HttpListener.Start();
            Task.Run(() => onStart());
        }
        
        public void StopServer()
        {            
            _HttpListener.Stop();
            while (UnhandledRequestsCount != 0) { }
            Database.CloseConnection();
            Task.Run(() => onStop());
        }


        public Request GetRequest()
        {
            HttpListenerContext context = _HttpListener.GetContext();
            HttpListenerRequest request = context.Request;
            Task.Run(() => onRequestAdded());
            return new Request(context.Response, request);
        }
        
        public async Task<Request> GetRequestAsync()
        {
            HttpListenerContext context = await _HttpListener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            Task.Run(() => onRequestAdded());
            return new Request(context.Response, request);
        }


        public void RespondRequest(Request request)
        {
            byte[] response;
            if (request.Requested.HttpMethod == "GET")
            {
                try
                {
                    request.Response.StatusCode = (int)HttpStatusCode.OK;
                    if (request.Requested.Cookies.Count == 0)
                    {
                        request.Response.SetCookie(new Cookie("session", Security.GetUniqueKey()));
                    }
                    const string path = @"M:\YandexDisk\Projects\In progress\SignUp Service\WebSite";
                    var url = request.Requested.RawUrl;
                    if (url == "/")
                    {
                        response = Encoding.UTF8.GetBytes(File.ReadAllText(path + @"\index.html"));
                        request.Response.ContentType = "text/html; charset=UTF-8";
                    }
                    else
                    {
                        response = Encoding.UTF8.GetBytes(File.ReadAllText(path + url.Replace('/', '\\')));
                        request.Response.ContentType = $"{MimeMapping.GetMimeMapping(url)}; charset=UTF-8";
                    }
                }
                catch (FileNotFoundException)
                {
                    request.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new byte[0];
                }
            }
            else if (request.Requested.HttpMethod == "POST")
            {
                request.Response.StatusCode = (int)HttpStatusCode.OK;
                RequestContext ctx = request.GetPostRequestData();
                string requestText = JSON.Stringify(ctx);
                response = Encoding.UTF8.GetBytes(requestText);
                request.Response.ContentType = "application/json";
            }
            else //Unexpected HTTP method
            {
                request.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = new byte[0];
            }
            request.Response.ContentLength64 = response.Length;
            using (Stream output = request.Response.OutputStream)
            {
                output.Write(response, 0, response.Length);
            }
            Task.Run(() => onRequestHandled());
        }

        public async Task RespondRequestAsync(Request request)
        {
            await Task.Run(() => RespondRequest(request));
        }
    }
}
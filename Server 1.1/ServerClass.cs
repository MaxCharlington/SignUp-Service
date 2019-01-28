using System;
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
            var validator = ValidateSession(request);
            byte[] response;
            if (request.Requested.HttpMethod == "GET")
            {
                response = PrepareResponseToGET(request);
            }
            else if (request.Requested.HttpMethod == "POST")
            {
                response = PrepareResponseToPOST(request);
            }
            else
            {
                //Unexpected HTTP method
                request.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = new byte[0];
            }
            request.Response.ContentLength64 = response.Length;
            validator.Wait();
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

        private async Task ValidateSession(Request request) {
            await Task.Run(() =>
            {
                try
                {
                    request.Requested.Cookies["session"].ToString();
                }
                catch (NullReferenceException)
                {
                    string sessionId = Security.GetUniqueKey();
                    Database.CreateSession(sessionId);
                    request.Response.AppendCookie(new Cookie("session", sessionId));
                }
            });
        }

        private byte[] PrepareResponseToGET(Request request)
        {
            try
            {
                request.Response.StatusCode = (int)HttpStatusCode.OK;
                const string path = @"M:\YandexDisk\Projects\In progress\SignUp Service\WebSite";
                var url = request.Requested.RawUrl;
                if (url == "/")
                {
                    request.Response.ContentType = "text/html; charset=UTF-8";
                    return Encoding.UTF8.GetBytes(File.ReadAllText(path + @"\index.html"));
                }
                else
                {
                    request.Response.ContentType = $"{MimeMapping.GetMimeMapping(url)}; charset=UTF-8";
                    return Encoding.UTF8.GetBytes(File.ReadAllText(path + url.Replace('/', '\\')));
                }
            }
            catch (FileNotFoundException)
            {
                request.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new byte[0];
            }
        }

        private byte[] PrepareResponseToPOST(Request request)
        {
            request.Response.StatusCode = (int)HttpStatusCode.OK;
            RequestContext ctx = request.GetPostRequestData();

            string answer;
            switch (ctx.CmdId)
            {
                //Searching request
                case 0:
                    string input = (JSON.Parse(ctx.StrData, typeof(ClassLibrary.SearchClass)) as ClassLibrary.SearchClass).Input;
                    answer = new ClassLibrary.SearchResultClass(SearchMatchings(input)).ToJSON();
                    break;
                //Unknown request
                default:
                    answer = "";
                    break;
            }

            request.Response.ContentType = "application/json";
            return Encoding.UTF8.GetBytes(answer);
        }

        private string SearchMatchings(string input) {            
            return input;
        }
    }
}
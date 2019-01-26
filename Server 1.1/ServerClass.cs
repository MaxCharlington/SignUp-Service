using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using ToolLibrary;
using Database;

namespace Server
{
    public static class Server
    {
        private static HttpListener httpListener { set; get; }
        private static DatabaseClass Database { get; } = DatabaseClass.GetInstance();
        public static int sessionRequestCount { private set; get; } = 0;

        public static void StartServer()
        {
            Database.OpenConnection();
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://+/");
            httpListener.Start();
        }

        public static async Task<Request> GetRequest()
        {
            HttpListenerContext context = await httpListener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            return new Request(context.Response, request);
        }

        public static async Task RespondRequest(Request request)
        {
            await Task.Run(() =>
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
                sessionRequestCount++;
            });
        }

        public static void StopServer()
        {
            httpListener.Stop();
            Database.CloseConnection();
        }
    }    
}
using System.IO;
using System.Net;
using System.Text;

using ToolLibrary;

namespace Client
{
    public class ClientClass
    {
        private static string IPAddress { get; } = "95.79.50.186";
                
        public static string RequestHTTP(RequestContext context)
        {
            WebRequest webRequest = WebRequest.Create("http://" + IPAddress + "/");
            webRequest.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(JSON.Stringify(context));
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse webResponse = webRequest.GetResponse();
            string response;
            using (Stream stream = webResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    response = reader.ReadToEnd();
                }
            }
            webResponse.Close();
            return response;
        }
    }
}
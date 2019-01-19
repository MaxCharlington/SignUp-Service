using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

using ToolLibrary;

namespace Client
{
    public class ClientClass
    {
        private static string IPAddress { get; } = "95.79.6.50";

        private static string Request(string request)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = Encoding.Unicode.GetBytes(request);
            socket.Connect(IPAddress, 25000);
            socket.Send(buffer, 0, buffer.Length, 0);
            buffer = new byte[1024];
            int rec = socket.Receive(buffer, 0, buffer.Length, 0);
            socket.Close();
            Array.Resize(ref buffer, rec);
            return Encoding.Unicode.GetString(buffer);
        }

        public static string Register(int type, string login, string password)
        {
            string request = "0 " + Convert.ToString(type) + ' ' + login + ' ' + ToolClass.GetHash(password);
            return Request(request);
        }

        public static string Login(string login, string password)
        {
            string request = "1 " + login + ' ' + ToolClass.GetHash(password);
            return Request(request);
        }

        public static string GetData (string id, string password) 
        {
            return Request("2 " + id + ' ' + ToolClass.GetHash(password));
        }

        public static string GetType(string login, string password)
        {
            string request = "5 " + login + ' ' + ToolClass.GetHash(password);
            return Request(request);
        }

        public static string GetCompany(string login, string password, string request)
        {
            string requestR = "7 " + login + ' ' + ToolClass.GetHash(password)+ ' ' + request;
            return Request(requestR);
        }

        public static void AddTag(string id, string password, string tag)
        {
            Request("6 " + id + ' ' + ToolClass.GetHash(password) + ' ' + tag);
        }

        public static void SetData(string id, string password, string data)
        {
            Request("3 " + id + ' ' + ToolClass.GetHash(password) + ' ' + data);
        }

        public static void SetAddress(string id, string password, string address)
        {
            Request("4 " + id + ' ' + ToolClass.GetHash(password) + ' ' + address);
        }
        
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
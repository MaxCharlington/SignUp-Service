using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.IO;
using System.Data.OleDb;
using Client;
using ToolLibrary;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web;
using System.Runtime.Serialization.Json;
using ClassLibrary;
using System.Data.SqlClient;
using Database;
using Npgsql;
using Server;

namespace PgSql
{
    public class PostGreSQL
    {
        public static void Main() {
            Cookie cookie = new Cookie("session", Security.GetUniqueKey());

            Console.ReadKey();
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
                        response = new byte[0];
                    }
                }
                else if (request.Requested.HttpMethod == "POST")
                {
                    RequestContext ctx = request.GetPostRequestData();
                    string requestText = JSON.Stringify(ctx);
                    response = Encoding.UTF8.GetBytes(requestText);
                    request.Response.ContentType = "application/json";
                }
                else
                {
                    response = new byte[0];
                }
                request.Response.ContentLength64 = response.Length;
                using (Stream output = request.Response.OutputStream)
                {
                    output.Write(response, 0, response.Length);
                }
            });
        }
    }
}
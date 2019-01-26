using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ToolLibrary
{
    public static class JSON
    {
        public static string Stringify<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        public static object Parse(string json, Type type)
        {
            var deserialized = Activator.CreateInstance(type);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(type);
            deserialized = ser.ReadObject(stream);
            stream.Close();
            Console.WriteLine(type.ToString());
            return deserialized;
        }
        
        public static string ToJSON<T>(this T obj) {
            return Stringify(obj);
        }

        public static void InitializeWithJSON<T>(this T obj, string json)
        {
            obj = (T)Parse(json, obj.GetType());
        }
    }
}

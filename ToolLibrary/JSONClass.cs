using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ToolLibrary
{
    public class JSON
    {
        public static string Stringify(object obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
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
            return deserialized;
        }
    }
}

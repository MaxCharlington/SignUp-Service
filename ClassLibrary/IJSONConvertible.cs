using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClassLibrary
{
    public interface IJSONConvertible
    {
        Type Type { get; }
    }

    public static class IJSONConvertibleExtention {
        public static string JSONStringify(this IJSONConvertible instance) {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(instance.Type);
            ser.WriteObject(stream, instance);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        public static T JSONParse<T>(this string json) {
            var deserialized = Activator.CreateInstance(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            deserialized = ser.ReadObject(stream);
            stream.Close();
            return (T)deserialized;
        }
    }
}

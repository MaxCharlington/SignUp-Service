using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Threads;

namespace ToolLibrary
{
    public class ToolClass
    {
        // Converts a string to a hash
        public static string GetHash(string input)
        {
            byte[] hash;
            using (var sha1 = new SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        //Prints with color
        private static object MessageLock = new object();
        public static async Task Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            await ThreadManager.GetInstance().RunAction(() =>
            {
                lock (MessageLock)
                {
                    if (consoleColor == Console.ForegroundColor)
                    {
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.WriteLine(message);
                        Console.ResetColor();
                    }
                }
            });
        }

        /*
        // Converts an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Converts a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }*/
    }
}
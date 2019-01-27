using System.Threading.Tasks;

namespace Server
{
    public interface IServer
    {
        void StartServer(ushort port);
        void StopServer();

        Request GetRequest();
        Task<Request> GetRequestAsync();

        void RespondRequest(Request request);
        Task RespondRequestAsync(Request request);
    }
}
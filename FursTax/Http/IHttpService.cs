using MadWare.Furs.Requests;
using System.Threading.Tasks;

namespace MadWare.Furs.Http
{
    public interface IHttpService
    {
        Task<string> SendRequest(string url, string payload, BaseRequestBody b);
    }
}
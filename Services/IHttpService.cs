using System.Threading.Tasks;

namespace AnimalizeMe.Services
{
    public interface IHttpService
    {
        Task<string> Get(string url);
    }
}
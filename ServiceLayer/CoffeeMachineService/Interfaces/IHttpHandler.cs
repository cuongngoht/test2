using System.Net.Http;
using System.Threading.Tasks;
using CoffeeMachineService.ViewModels;

namespace CoffeeMachineService.Interfaces
{
    public interface IHttpHandler
    {
        Task<string> GetAsync(string url);
    }
}
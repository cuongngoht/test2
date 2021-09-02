using System.Threading.Tasks;
using CoffeeMachineService.ViewModels;

namespace CoffeeMachineService.Interfaces
{
    public interface ICoffeeService
    {
        Task<string> GetMessageAsync();
    }
}
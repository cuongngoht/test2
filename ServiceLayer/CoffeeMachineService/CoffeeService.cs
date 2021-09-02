using System;
using System.Threading.Tasks;
using CoffeeMachineService.Interfaces;
using CoffeeMachineService.ViewModels;

namespace CoffeeMachineService
{
    public class CoffeeService : ICoffeeService
    {

        public async Task<string> GetMessageAsync()
        {

            string message = "Your piping hot coffee is ready";
            return await Task.FromResult(message);
        }
    }
}
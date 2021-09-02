using System;

namespace CoffeeMachineService.ViewModels
{
    public class BrewCoffeeViewModel
    {
        public string Message { get; set; }
        public string Prepaired { get; set; } = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
    }
}
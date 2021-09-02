using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeMachineService.Interfaces;
using CoffeeMachineService;
using Microsoft.AspNetCore.Http;
using CoffeeMachineService.ViewModels;
using CoffeeMachineService.Helpper;
using System.Text.Json;
using Api.ViewModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly CommonValue _commonValues;
        public readonly ICoffeeService _coffeeService;
        public readonly IHttpHandler _httpHandler;
        private readonly SystemDateTime _systemDateTime;
        private const string API_KEY = "02918d80ad629e75b403448308190f27";
        public BrewCoffeeController(ICoffeeService coffeeService, IHttpHandler httpHandler, SystemDateTime systemDateTime)
        {
            _coffeeService = coffeeService;
            _commonValues = CommonValue.Instance;
            _httpHandler = httpHandler;
            _systemDateTime = systemDateTime;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = new BrewCoffeeViewModel();
            if (_commonValues.Count == 1)
            {
                _commonValues.Count = 5;
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            var dateTimeNow = _systemDateTime.Now().Date;
            _commonValues.Count -= 1;
            if (dateTimeNow.Month == _commonValues.DateTimeCompare.Month && dateTimeNow.Day == _commonValues.DateTimeCompare.Day)
            {
                return StatusCode(StatusCodes.Status418ImATeapot);
            }

            var fetchData = await _httpHandler.GetAsync($"https://api.openweathermap.org/data/2.5/weather?id=1566083&appid={API_KEY}&units=metric");

            if (!string.IsNullOrEmpty(fetchData))
            {
                WeatherRoot result = JsonSerializer.Deserialize<WeatherRoot>(fetchData);
                if (result?.Main?.Temp > _commonValues.Temp)
                {

                    data.Message = "Your refreshing iced coffee is ready";
                    return Ok(data);
                }
            }

            data.Message = await _coffeeService.GetMessageAsync();
            return Ok(data);
        }
    }
}

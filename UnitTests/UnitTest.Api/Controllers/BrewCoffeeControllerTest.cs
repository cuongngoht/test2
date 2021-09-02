using Moq;
using Xunit;
using CoffeeMachineService.Interfaces;
using Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeMachineService;
using CoffeeMachineService.ViewModels;
using CoffeeMachineService.Helpper;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace UniTest.Api.Controllers
{

    public class BrewCoffeeControllerTest
    {
        private readonly Mock<ICoffeeService> _mockCoffeeService;
        private readonly Mock<IHttpHandler> _mockHttpHandler;
        private readonly SystemDateTime _mockSystemDateTime;
        public BrewCoffeeControllerTest()
        {
            _mockCoffeeService = new Mock<ICoffeeService>();
            _mockHttpHandler = new Mock<IHttpHandler>();
            _mockSystemDateTime = new SystemDateTime();
            string json = string.Empty;
            using (StreamReader r = new StreamReader(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "//MockData//weatherDegreeLess30.json"))
            {
                json = r.ReadToEnd();
            }
            _mockHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(json));
        }


        [Fact]
        public async Task Should_Return_Status_Ok_When_Get_Count_Not_Equal_1()
        {
            // Arrange
            var controller = new BrewCoffeeController(new CoffeeService(), _mockHttpHandler.Object, _mockSystemDateTime);

            // Act
            var result = await controller.Get();
            var ojectResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, ojectResult.StatusCode);
            Assert.Equal("Your piping hot coffee is ready", ((BrewCoffeeViewModel)ojectResult.Value).Message);
        }

        [Fact]
        public async Task Should_Return_Status_ImATeapot_When_Today_Is_Month_April_Day_1()
        {
            // Arrange
            var mockSystemDate = new SystemDateTime();
            var controller = new BrewCoffeeController(_mockCoffeeService.Object, _mockHttpHandler.Object, mockSystemDate);

            // Act
            mockSystemDate.SetDateTime(new System.DateTime(2021, 4, 1));
            var result = await controller.Get();
            var ojectResult = Assert.IsType<StatusCodeResult>(result);

            // Assert
            Assert.Equal(StatusCodes.Status418ImATeapot, ojectResult.StatusCode);
        }


        [Fact]
        public async Task Should_Return_Status_ServiceUnavailable_When_Get_Count_Equal_1()
        {
            // Arrange
            var controller = new BrewCoffeeController(_mockCoffeeService.Object, _mockHttpHandler.Object, _mockSystemDateTime);
            CommonValue.Instance.Count = 5;

            // Act
            await controller.Get();
            await controller.Get();
            await controller.Get();
            await controller.Get();
            var result = await controller.Get();
            var ojectResult = Assert.IsType<StatusCodeResult>(result);

            // Assert
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, ojectResult.StatusCode);

        }

        [Fact]
        public async Task Should_Return_Status_Ok_When_Get_Weather_Api()
        {
            // Arrange
            string json = string.Empty;
            using (StreamReader r = new StreamReader(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "//MockData//weather.json"))
            {
                json = r.ReadToEnd();
            }
            _mockHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(json));
            var controller = new BrewCoffeeController(_mockCoffeeService.Object, _mockHttpHandler.Object, _mockSystemDateTime);

            // Act
            var result = await controller.Get();
            var ojectResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, ojectResult.StatusCode);
            Assert.Equal("Your refreshing iced coffee is ready", ((BrewCoffeeViewModel)ojectResult.Value).Message);

        }
    }
}
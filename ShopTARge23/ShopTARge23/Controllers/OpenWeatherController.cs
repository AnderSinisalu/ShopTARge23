using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.OpenWeather;

namespace ShopTARge23.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IOpenWeatherServices _OpenWeatherServices;

        public OpenWeatherController
            (
            IOpenWeatherServices OpenWeatherServices
            )
        {
            _OpenWeatherServices = OpenWeatherServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //SearchCityViewModel model = new();
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(SearchCityViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "OpenWeather", new { city = model.CityName });
            }
            return View(model);
        }

        public IActionResult City(string city)
        {
            OpenWeatherResultDto dto = new();
            dto.City = city;

            _OpenWeatherServices.OpenWeatherResult(dto);
            OpenWeatherViewModel vm = new();

            vm.City = dto.City;
            vm.Temp = dto.Temp;
            vm.FeelsLike = dto.FeelsLike;
            vm.Humidity = dto.Humidity;
            vm.Pressure = dto.Pressure;
            vm.WindSpeed = dto.WindSpeed;
            vm.Description = dto.Description;

            return View(vm);
        }
    }
}

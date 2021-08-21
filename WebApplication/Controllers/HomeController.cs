using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.ViewModels;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WebApplication.Repository;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CityRepository _City;
        private readonly RegionRepository _Region;
        private readonly CountryRepository _Country;
        public HomeController(CityRepository _City, RegionRepository _Region, CountryRepository _Country)
        {
            this._City = _City;
            this._Region = _Region;
            this._Country = _Country;
        }
        // -- Главная страница - Страница с поиском --
        public IActionResult Index()
        {
            return View();
        }
        // -- Страница с информацией по странам --
        public IActionResult ViewInfo()
        {
            ViewModel model = new ViewModel();
            model.Countries = _Country.All.OrderBy(i => i.Name);
            return View(model);
        }
        // -- Запрос к API --
        public static async Task<string> ConnectAsync(string country)
        {
            WebRequest request = WebRequest.Create("https://restcountries.eu/rest/v2/name/" + country);
            request.Method = "GET";
            WebResponse response = await request.GetResponseAsync();
            var answer = string.Empty;
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();
            return answer;
        }
        // -- Поиск информации о стране --
        public IActionResult FindInfo(string country)
        {
            ViewModel model = new ViewModel();
            // запрашиваем информацию
            try
            {
                ConnectAsync(country).Wait();
                dynamic data = JsonConvert.DeserializeObject(ConnectAsync(country).Result);
                // выбираем нужную информацию
                model.Region = data[0].region;
                model.City = data[0].capital;
                model.Name = data[0].name;
                model.Code = data[0].alpha3Code;
                model.Area = data[0].area;
                model.Population = data[0].population;
                // отображаем информацию
                return View(model);
            }
            catch
            {
                // отображаем информацию об ошибке
                return View("Error");
            }
        }
        // -- Сохранение информации --
        public IActionResult SaveInfo(ViewModel model)
        {
            // если столицы нет в БД, то добавляем
            if (!_City.All.Any(i => i.Name == model.City))
                _City.Add(model.City);
            // если региона нет в БД, то добавляем
            if (!_Region.All.Any(i => i.Name == model.Region))
                _Region.Add(model.Region);
            // получаем идентификаторы столицы и региона
            long RegionId = _Region.All.FirstOrDefault(i => i.Name == model.Region).Id,
                 CityId = _City.All.FirstOrDefault(i => i.Name == model.City).Id;
            // ищем страну в БД
            Country country = _Country.All.FirstOrDefault(i => i.Name == model.Name);
            // если страна не найдена, то добавляем
            if (country == null)
            {
                _Country.Add
                    (
                        model.Name,
                        model.Code,
                        CityId,
                        model.Area,
                        model.Population,
                        RegionId
                    );
            }
            // иначе обновляем имеющуюся запись
            else
            {
                country.Name = model.Name;
                country.Code = model.Code;
                country.CityId = CityId;
                country.Area = model.Area;
                country.Population = model.Population;
                country.RegionId = RegionId;
                _Country.Update(country);
            }
            // переходим к списку стран
            return RedirectToAction("ViewInfo");
        }
    }
}

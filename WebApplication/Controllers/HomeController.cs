using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.ViewModels;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityRepository _city;
        private readonly IRegionRepository _region;
        private readonly ICountryRepository _country;
        public HomeController(ICityRepository _city, IRegionRepository _region, ICountryRepository _country)
        {
            this._city = _city;
            this._region = _region;
            this._country = _country;
        }
        // -- Главная страница - Страница с поиском --
        public ViewResult Index()
        {
            return View();
        }
        // -- Страница с информацией по странам --
        public ViewResult DisplayInfoCountries()
        {
            ViewModel model = new ViewModel();
            model.countries = _country.GetCountryList.OrderBy(i => i.name);
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
        // -- Поиск информации по стране --
        [HttpPost]
        public IActionResult DisplayInfoCountry(string country)
        {
            ViewModel model = new ViewModel();
            // запрашиваем информацию
            try
            {
                ConnectAsync(country).Wait();
                dynamic data = JsonConvert.DeserializeObject(ConnectAsync(country).Result);
                // выбираем нужную информацию
                model.region = data[0].region;
                model.city = data[0].capital;
                model.name = data[0].name;
                model.code = data[0].alpha3Code;
                model.area = data[0].area;
                model.population = data[0].population;
                // отображаем информацию
                return View("DisplayInfoCountry", model);
            }
            catch
            {
                return View("Error");
            }
        }
        // -- Сохранение информации --
        [HttpPost]
        public IActionResult SaveCity(ViewModel model)
        {
            if (!_city.GetCityList.Any(i => i.name == model.city))
            {
                _city.Create(model.city);
                _city.Save();
            }
            return RedirectToAction("SaveRegion", model);
        }
        public IActionResult SaveRegion(ViewModel model)
        {
            if (!_region.GetRegionList.Any(i => i.name == model.region))
            {
                _region.Create(model.region);
                _region.Save();
            }
            return RedirectToAction("SaveCountry", model);
        }
        public IActionResult SaveCountry(ViewModel model)
        {
            // получаем идентификаторы столицы и региона
            long regionid = _region.GetRegionList.FirstOrDefault(i => i.name == model.region).id,
                 cityid = _city.GetCityList.FirstOrDefault(i => i.name == model.city).id;
            // ищем страну в БД
            Country country = _country.GetCountryList.FirstOrDefault(i => i.name == model.name);
            // если страна не найдена, то добавляем
            if (country == null)
            {
                _country.Create
                    (
                        model.name,
                        model.code,
                        cityid,
                        model.area,
                        model.population,
                        regionid
                    );
            }
            // иначе обновляем имеющуюся запись
            else
            {
                country.name = model.name;
                country.code = model.code;
                country.cityid = cityid;
                country.area = model.area;
                country.population = model.population;
                country.regionid = regionid;
                _country.Update(country);
            }
            _country.Save();
            // переходим к списку стран
            return RedirectToAction("DisplayInfoCountries");
        }
    }
}

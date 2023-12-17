using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Test_DotNetMVC.Models;
using Test_DotNetMVC.Models.Entities;

namespace Test_DotNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("sex", "1");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(HomeModel model)
        {
            
            return RedirectToAction("Index", "Users");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //set vào session


        public void Set<T>(string key, T item)
        {
            // Jsonserialli
            //var str = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            // JsonSerializer.SerializeToUtf8ytytes(item)
            HttpContext.Session.SetString(key, item.ToString());
        }

        // get session 

        public void getSession(string key)
        {
            var item = HttpContext.Session.Get(key);
            // var utf8Reader = new Utf8JsonReader(item);
            // return JsonSerialize.Deserialize<T>(ref utf8Reader);

            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);

            //var a = Session[key];
        }
    }
}
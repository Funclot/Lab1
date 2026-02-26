using Microsoft.AspNetCore.Mvc;

namespace Lr1.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Hello()
        {
            return Content("Привет из DemoController!");
        }

        public IActionResult Greeting(string name)
        {
            return Content($"Здравствуйте, {name ?? "гость"}!");
        }

        public IActionResult ShowView()
        {
            ViewBag.UserName = "Елена";
            ViewBag.RegistrationDate = new DateTime(2026, 2, 04);
            ViewData["PageTitle"] = "Информационная страница";
            ViewData["VisitCount"] = 42;
            return View();
        }
        public IActionResult UserInfo(string name, int age)
        {
            ViewBag.Name = name ?? "Неизвестный";
            ViewBag.Age = age;
            ViewBag.IsAdult = age >= 18;
            ViewData["CurrentYear"] = DateTime.Now.Year;
            ViewData["BirthYear"] = DateTime.Now.Year - age;
            return View();
        }
    }
}

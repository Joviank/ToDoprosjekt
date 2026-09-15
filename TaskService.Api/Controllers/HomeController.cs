using Microsoft.AspNetCore.Mvc;

namespace TaskService.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
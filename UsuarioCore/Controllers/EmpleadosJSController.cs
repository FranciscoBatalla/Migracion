using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadosJSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmpleadosJS()
        {
            return View();
        }
    } 
}

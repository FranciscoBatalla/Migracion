using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CardsGetAll()
        {
            return View(/*Pasar modelo*/);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadosController : Controller


    {
        private readonly BL.Empleado _contextEmpleado;

        public EmpleadosController(BL.Empleado contextEmpleado)
        {
            _contextEmpleado = contextEmpleado;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CardsGetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();

            ML.Result resultEmpleados = _contextEmpleado.EmpleadosGetAll();


            return View(resultEmpleados);
        }
    }
}

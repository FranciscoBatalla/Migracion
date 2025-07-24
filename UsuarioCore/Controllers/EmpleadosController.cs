using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadosController : Controller


    {
        private readonly BL.Empleado _contextEmpleado;
        private readonly BL.Departamento _contextDepartamento;

        public EmpleadosController(BL.Empleado contextEmpleado, BL.Departamento contextDepartamento)
        {
            _contextEmpleado = contextEmpleado;
            _contextDepartamento = contextDepartamento;
        }

        public IActionResult Index()
        {
            return View();
        }


        //CONTROLADOR PARA EL GETALL
        [HttpGet]
        public IActionResult CardsGetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();
            empleado.Nombre = "";
            empleado.ApellidoPaterno = "";
            empleado.Departamento.IdDepartamento = 0;
            empleado.SalarioBase = 0;

            ML.Result resultEmpleados = _contextEmpleado.EmpleadosGetAll(empleado);
            empleado.Empleados = new List<object>();


            ML.Result resultDeptoGetAll = _contextDepartamento.GetAllDepartamentos();
            if (resultDeptoGetAll.Correct)
            {
                empleado.Departamento.Departamentos = resultDeptoGetAll.Objects;
            }

            return View(empleado);
        }//FUNCIONANDO CORRECTAMENTE
        //FIN DEL CONTROLADOR GETALL


        //CONTROLADOR PARA VISTA ABIERTA


        [HttpPost]
        public IActionResult CardsGetAll(ML.Empleado Empleado)
        {
            Empleado.Departamento = new ML.Departamento();

            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoPaterno ?? "";
            Empleado.Departamento.IdDepartamento = Empleado.Departamento.IdDepartamento ?? 0;

            //TRAER OBJETOS DE RESULT Y PASARLOS AL MODELO DE EMPLEADO

            ML.Result resultpost = _contextEmpleado.EmpleadosGetAll(Empleado);
            if (resultpost.Correct)
            {
                Empleado.Empleados = resultpost.Objects;
            }

            ML.Result resultdepto = _contextDepartamento.GetAllDepartamentos();

            if (resultdepto.Correct)
            {
                Empleado.Departamento.Departamentos = resultdepto.Objects;
            }
            //FIN TRAER OBJETOS DE RESULT Y PASARLOS AL MODELO DE EMPLEADO

            return View(Empleado);
        }//FUNCIONANDO CORRECTAMENTE
        // FIN CONTROLADOR PARA VISTA ABIERTA




        //CONTROLADOR PARA EL CREATE, GETBYID Y UPDATE (VISTA_CREATEUPDATE)


        [HttpGet]
        public IActionResult Formulario(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();



            //LLENAR DDL DE DEPARTAMENTO
            ML.Result resultDept = _contextDepartamento.GetAllDepartamentos();
            if (resultDept.Correct)
            {
                empleado.Departamento.Departamentos = resultDept.Objects;
            }
            else
            {
                empleado.Departamento.Departamentos = new List<object>();
            }


            if (IdEmpleado > 0)
            {
                ML.Result resultEmpleado = _contextEmpleado.GetByIdEmpleado(IdEmpleado);
                if (resultEmpleado.Correct)
                {
                    empleado = (ML.Empleado)resultEmpleado.Object;
                    empleado.Departamento.Departamentos = resultDept.Objects;


                }
            }

            return View(empleado);
        }//FUNCIONANDO


        [HttpPost]
        public IActionResult Formulario(ML.Empleado Empleado)
        {
            if (!ModelState.IsValid)
            {
                Empleado.Departamento = new ML.Departamento();
                Empleado.Departamento.Departamentos = _contextDepartamento.GetAllDepartamentos().Objects;
                return View(Empleado);

            }

            if (Empleado.IdEmpleado > 0)
            {
                ML.Result resultUpdate = _contextEmpleado.UpdateEmpleado(Empleado);

                if (resultUpdate.Correct)
                {
                    return RedirectToAction("CardsGetAll");
                }
            }
            else
            {
                ML.Result resultAdd = _contextEmpleado.AgregarEmpleado(Empleado);
                if (resultAdd.Correct)
                {
                    return RedirectToAction("CardsGetAll");
                }
            }

            //ML.Result resultdept = _contextDepartamento.GetAllDepartamentos();
            //Empleado.Departamento.Departamentos = resultdept.Objects;


            return RedirectToAction("CardsGetAll");

        }//FUNCIONANDO

        //FIN CONTROLADOR PARA EL CREATE Y UPDATE (VISTA_CREATEUPDATE)



        //CONTROLADOR PARA DELETE

        [HttpGet]
        public IActionResult Delete(int IdEmpleado)
        {
            ML.Result resultDelete = _contextEmpleado.DeleteEmpleado(IdEmpleado);

            return RedirectToAction("CardsGetAll");
        }//FUNCIONANDO


        //FIN CONTROLADOR PARA DELETE




        [HttpGet]
        public IActionResult OnlyCardsGetAll(ML.Empleado empleado)
        {
            //ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();
            empleado.Nombre =  "";
            empleado.ApellidoPaterno = "";
            empleado.Departamento.IdDepartamento = 0;

            ML.Result resultdept = _contextDepartamento.GetAllDepartamentos();
            if (resultdept.Correct && resultdept.Objects != null)
            {
                empleado.Departamento.Departamentos = resultdept.Objects;
            }
            else
            {
                empleado.Departamento.Departamentos = new List<object>();
            }

            ML.Result resultegetall = _contextEmpleado.EmpleadosGetAll(empleado);
            if (resultegetall.Correct && resultegetall.Objects != null)
            {
                empleado.Empleados = resultegetall.Objects;
            }
            else
            {
                empleado.Empleados = new List<object>();
            }

            return View(empleado);
        }
    }


}



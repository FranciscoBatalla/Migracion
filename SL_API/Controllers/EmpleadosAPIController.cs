using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosAPIController : ControllerBase
    {


        private readonly BL.Empleado _context;
        private readonly BL.Departamento _contextDepartamento;
        public EmpleadosAPIController(BL.Empleado context, BL.Departamento contextDepartamento)
        {
            _context = context;
            _contextDepartamento = contextDepartamento;
        }



        [HttpPost]
        [Route("GetAllEmpleados")]
        public IActionResult EmpleadosGetAll([FromBody] ML.Empleado Empleado)
        {
            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoMaterno ?? "";
            Empleado.ApellidoMaterno = Empleado.ApellidoPaterno ?? "";
            //Empleado.SalarioBase = Empleado.SalarioBase ?? 0;

            Empleado.Departamento = new ML.Departamento { IdDepartamento = 0 };

            ML.Result resultGetAll = _context.EmpleadosGetAll(Empleado);

            if (resultGetAll.Correct)
            {
                return Ok(resultGetAll.Objects);
            }
            else
            {
                return BadRequest(resultGetAll.ErrorMessage);
            }
        }//FUNCINANDO


        [HttpPost]
        [Route("AgregarEmpleado")]
        public IActionResult EmpleadosAdd([FromBody] ML.Empleado empleado)
        {
            ML.Result resultAdd = _context.AgregarEmpleado(empleado);
            if (resultAdd.Correct)
            {
                return Ok($"El empleado {empleado.Nombre} fue agregado con exito!");
            }
            else
            {
                return BadRequest(resultAdd.ErrorMessage);
            }
        }//FUNCIONANDO

        [HttpDelete]
        //[HttpGet]
        [Route("EliminarEmpleado/{IdEmpleado}")]
        public IActionResult EliminarEmpleado(int IdEmpleado)
        {
            ML.Result resultDelete = _context.DeleteEmpleado(IdEmpleado);
            if (resultDelete.Correct)
            {
                return Ok($"Empleado con el ID {IdEmpleado} fue eliminado con exito!");
            }
            else
            {
                return BadRequest(resultDelete.ErrorMessage);
            }

        }//FUNCIONANDO


        [HttpGet]
        [Route("GetEmpleadoById/{IdEmpleado}")]
        public IActionResult EmpleadosGetById(int IdEmpleado)
        {
            ML.Result resultGetById = _context.GetByIdEmpleado(IdEmpleado);

            if (resultGetById.Object != null && resultGetById.Correct)
            {
                return Ok(resultGetById.Object);

            }
            else
            {
                return NotFound($"Empleado con ID {IdEmpleado} no encontrado");
            }

        }//FUNCIONANDO


        [HttpPut]
        [Route("ActualizarEmpleado/{IdEmpleado}")]
        public IActionResult ActualizarEmpleado([FromBody] ML.Empleado Empleado, int IdEmpleado)
        {
            //ML.Empleado empleado1 = new ML.Empleado();
            Empleado.IdEmpleado = IdEmpleado;

            ML.Result resultUpdate = _context.UpdateEmpleado(Empleado);
            if (resultUpdate.Correct)
            {
                return Ok($"El Empleado con ID {IdEmpleado} se actualizo con Exito!");
            }
            else
            {
                return BadRequest(resultUpdate.ErrorMessage);
            }



        }//FUNCIONANDO

        [HttpGet]
        [Route("DepartamentosGetAll")]
        public IActionResult GetAllDepartamentos()
        {
            ML.Result resultGetAll = _contextDepartamento.GetAllDepartamentos();
            if (resultGetAll.Correct)
            {
                return Ok(resultGetAll.Objects);
            }
            else
            {
                return BadRequest(resultGetAll.ErrorMessage);
            }
        }


    }
}

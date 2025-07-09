using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioMigrationController : Controller
    {
        private readonly BL.Usuario _context;

        public UsuarioMigrationController(BL.Usuario context)
        {
            _context = context;
        }//INYECCION DE DEPENDENCIAS

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TablaGetAll()
        {
            ML.Usuario user = new ML.Usuario();
            user.Rol = new ML.Rol();
            user.Direccion ??= new ML.Direccion();
            user.Direccion.Colonia ??= new ML.Colonia();
            user.Direccion.Colonia.Municipio ??= new ML.Municipio();
            user.Direccion.Colonia.Municipio.Estado ??= new ML.Estado();

            ML.Result resultRol = _context.GetAllRol();
            if (resultRol.Correct)
            {
                user.Rol.Roles = resultRol.Objects;
            }


            ML.Result resultGetAll = _context.GetAll(user);

            return View(resultGetAll);
        }

        public IActionResult Delete(int IdUsuario)
        {
            ML.Result resultDelete = _context.Delete(IdUsuario);

            return RedirectToAction("TablaGetAll");
        }
    }
}
 
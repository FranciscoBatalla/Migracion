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
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result resultRol = _context.GetAllRol();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }


            ML.Result resultGetAll = _context.GetAll(usuario);


            if (resultGetAll.Objects != null)
            {
                foreach (ML.Usuario user in resultGetAll.Objects)
                {
                    user.Rol ??= new ML.Rol();
                    user.Direccion ??= new ML.Direccion();
                    user.Direccion.Colonia ??= new ML.Colonia();
                    user.Direccion.Colonia.Municipio ??= new ML.Municipio();
                    user.Direccion.Colonia.Municipio.Estado ??= new ML.Estado();
                }
            }

            return View(resultGetAll);
        }
    }
}

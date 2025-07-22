using DL;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace PL.Controllers
{
    public class UsuarioMigrationController : Controller
    {
        private readonly BL.Usuario _context;
        private readonly BL.Estado _contextEstado;
        private readonly BL.Rol _contextRol;
        private readonly BL.Municipio _contextMunicipio;
        private readonly BL.Colonia _contextColonia;
        public UsuarioMigrationController(BL.Usuario context, BL.Estado contextEstado, BL.Municipio contextmunicipio, BL.Colonia contextColonia, BL.Rol contextRol)
        {
            _context = context;
            _contextEstado = contextEstado;
            _contextRol = contextRol;
            _contextMunicipio = contextmunicipio;
            _contextColonia = contextColonia;
        }//INYECCION DE DEPENDENCIAS

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult TablaGetAll() 
        {
            ML.Usuario user = new ML.Usuario();
            user.Rol = new ML.Rol();
            user.Direccion ??= new ML.Direccion();
            user.Direccion.Colonia ??= new ML.Colonia();
            user.Direccion.Colonia.Municipio ??= new ML.Municipio();
            user.Direccion.Colonia.Municipio.Estado ??= new ML.Estado();

            ML.Result resultRol = _contextRol.GetAllRol();
            if (resultRol.Correct)
            {
                user.Rol.Roles = resultRol.Objects;
            }


            ML.Result resultGetAll = _context.GetAll(user);

            return View(resultGetAll);
        }//FUNCIONANDO GETALL

        [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result resultDelete = _context.Delete(IdUsuario);

            return RedirectToAction("TablaGetAll");
        }//FUNCIONANDO DELETE


        [HttpGet]
        public IActionResult Formulario(int IdUsuario)  
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


            //MOSTRAR ROLES EN DDL
            ML.Result resultRol = _contextRol.GetAllRol();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            //

            //MOSTRAR ESTADOS EN DDL
            ML.Result resultEstado = _contextEstado.GetAllEstado();
            if (resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }
            //


            //MOSTRAR ROLES, ESTADOS , MUNICIPOS, COLONIAS CUANDO SE HACE CON UN GETBYID
            if (IdUsuario > 0)
            {
                ML.Result resultId = _context.GetById(IdUsuario);
                usuario = (ML.Usuario)resultId.Object!;






                usuario.Rol!.Roles = resultRol.Objects;

                usuario.Direccion!.Colonia!.Municipio!.Estado!.Estados = resultEstado.Objects;




                //MUNICIPIOS
                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                {
                    ML.Result resultMunicipio = _contextMunicipio.GetByIdEstado(usuario.Direccion!.Colonia!.Municipio!.Estado!.IdEstado!.Value);
                    if (resultMunicipio.Correct)
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    }
                }

                //COLONIAS

                if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                {
                    ML.Result resultColonia = _contextColonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio.Value);
                    if (resultColonia.Correct)
                    {
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    }
                } 


            }

            return View(usuario); ;
        }

        [HttpPost]
        public IActionResult Formulario(ML.Usuario Usuario, IFormFile ImgInputFile)
        {

            if (ModelState.IsValid)
            {
                if (ImgInputFile != null && ImgInputFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ImgInputFile.CopyTo(memoryStream);
                        Usuario.Imagen = memoryStream.ToArray(); // Aquí se guarda la imagen como bytes
                        Usuario.ImagenBase64 = Convert.ToBase64String(Usuario.Imagen);
                    }
                }

                ML.Result resultRoles = _contextRol.GetAllRol();
                if (resultRoles.Correct)
                {
                    Usuario.Rol!.Roles = resultRoles.Objects;
                }

                ML.Result resultEstado = _contextEstado.GetAllEstado();

                if (resultEstado.Correct)
                {
                    Usuario.Direccion!.Colonia!.Municipio!.Estado!.Estados = resultEstado.Objects;
                }


                if (Usuario.IdUsuario > 0)
                {
                    ML.Result resultUpdate = _context.Update(Usuario);
                }
                else
                {
                    ML.Result resultAdd = _context.Add(Usuario);

                    return RedirectToAction("TablaGetAll");

                }
            }




            return View(Usuario);
        }//

        [HttpGet]
        public IActionResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result result = _contextMunicipio.GetByIdEstado(idEstado);

            return Json(result);


        }
        [HttpGet]
        public IActionResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result resultColonia = _contextColonia.ColoniaGetByIdMunicipio(IdMunicipio);
            return Json(resultColonia);
        }


    }
}

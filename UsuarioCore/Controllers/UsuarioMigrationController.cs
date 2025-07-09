using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioMigrationController : Controller
    {
        private readonly BL.Usuario _context;
        private readonly BL.Estado _contextEstado;
        private readonly BL.Rol _contextRol;

        public UsuarioMigrationController(BL.Usuario context, BL.Estado contextEstado, BL.Rol contextRol)
        {
            _context = context;
            _contextEstado = contextEstado;
            _contextRol = contextRol;
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


            ////MOSTRAR ROLES, ESTADOS , MUNICIPOS, COLONIAS CUANDO SE HACE CON UN GETBYID
            //if (IdUsuario > 0)
            //{
            //    ////ML.Result result = BL.Usuario.GetByIdEFSP(IdUsuario.Value);

            //    //UsuarioReference.UsuarioClient UsuarioByIdSOAP = new UsuarioReference.UsuarioClient();
            //    //var respuestaSOAP = UsuarioByIdSOAP.GetById(IdUsuario.Value);




            //    //usuario = GetById(IdUsuario.Value);//SOAP
            //    //usuario = (ML.Usuario)respuestaSOAP.Object;

            //    ML.Result resultId = GetByIdREST(IdUsuario.Value);
            //    usuario = (ML.Usuario)resultId.Object;


            //    usuario.Rol.Roles = resultRol.Objects;

            //    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            //    //MUNICIPIOS
            //    if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
            //    {
            //        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            //        if (resultMunicipio.Correct)
            //        {
            //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            //        }
            //    }

            //    //COLONIAS

            //    if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
            //    {
            //        ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            //        if (resultColonia.Correct)
            //        {
            //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
            //        }
            //    }


            return View(); ;
        }
    }
}

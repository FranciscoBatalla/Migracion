using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        private readonly DL.FbatallaProgramacionNcapasContext _context;

        public Usuario(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAll(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {


                var Nombre = new SqlParameter("@Nombre", Usuario.Nombre ?? (object)DBNull.Value);
                var ApellidoPaterno = new SqlParameter("@ApellidoPaterno", Usuario.ApellidoPaterno ?? (object)DBNull.Value);
                var ApellidoMaterno = new SqlParameter("@ApellidoMaterno", Usuario.ApellidoMaterno ?? (object)DBNull.Value);
                var IdRol = new SqlParameter("@IdRol", Usuario.Rol?.IdRol == 0 ? (object)DBNull.Value : Usuario.Rol.IdRol);


                var query = _context.UsuarioGetAllDTOs.FromSqlRaw("UsuarioGetAll @Nombre, @ApellidoPaterno, @ApellidoMaterno, @IdRol", Nombre, ApellidoPaterno, ApellidoMaterno, IdRol).ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


                        usuario.IdUsuario = item.IdUsuario;
                        usuario.UserName = item.UserName;
                        usuario.Nombre = item.Nombre;
                        usuario.ApellidoPaterno = item.ApellidoPaterno;
                        usuario.ApellidoMaterno = item.ApellidoMaterno;
                        usuario.Email = item.Email ?? "";
                        usuario.Password = item.Password ?? "";
                        usuario.Sexo = item.Sexo ?? "";
                        usuario.Telefono = item.Telefono ?? "";
                        usuario.Celular = item.Celular ?? "";

                        if (!string.IsNullOrEmpty(item.FechaNacimiento) && DateTime.TryParse(item.FechaNacimiento, out DateTime fecha))
                        {
                            usuario.FechaNacimiento = fecha;
                        }
                        else
                        {
                            usuario.FechaNacimiento = null;
                        }

                        usuario.CURP = item.CURP ?? "";
                        usuario.Status = item.Status ?? false;
                        usuario.Status = false;

                        usuario.Imagen = item.Imagen ?? new byte[0];

                        //usuario.Rol.IdRol = item.IdRol ?? 0;
                        usuario.Rol.Nombre = item.NombreRol ?? "";

                        //usuario.Direccion.IdDireccion = item.IdDireccion ?? 0;
                        usuario.Direccion.Calle = item.Calle ?? "";
                        usuario.Direccion.NumeroInterior = item.NumeroInterior ?? "";
                        usuario.Direccion.NumeroExterior = item.NumeroExterior ?? "";

                        //usuario.Direccion.Colonia.IdColonia = item.IdColonia ?? 0;
                        usuario.Direccion.Colonia.Nombre = item.NombreColonia ?? "";

                        //usuario.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio ?? 0;
                        usuario.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio ?? "";

                        //usuario.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado ?? 0;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado ?? "";


                        result.Objects.Add(usuario);
                    }

                    result.Correct = true;
                }
            }




            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//GETALL FUNCIONANDO
        public ML.Result GetAllRol()
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.RolGetAllDTOs.FromSqlRaw("RolGetAll").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Rol rol = new ML.Rol();
                        rol.IdRol = item.IdRol;
                        rol.Nombre = item.Nombre;

                        result.Objects.Add(rol);

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }// GETALLROL FUNCIONANDO

        public ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                var Id = new SqlParameter("@IdUsuario", IdUsuario);
                var query = _context.Database.ExecuteSqlRaw("UsuarioDelete @IdUsuario", Id);

                if (query > 0)
                {
                    result.Correct = true;
                } 
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }

    }
}

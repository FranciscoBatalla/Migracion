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
                var IdRol = new SqlParameter("@IdRol", Usuario.Rol?.IdRol > 0 ? Usuario.Rol.IdRol : (object)DBNull.Value);

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
                        Usuario.FechaNacimiento = item.FechaNacimiento;

                        //if (!string.IsNullOrEmpty(item.FechaNacimiento) && DateTime.TryParse(item.FechaNacimiento, out DateTime fecha))
                        //{
                        //    usuario.FechaNacimiento = fecha;
                        //}
                        //else
                        //{
                        //    usuario.FechaNacimiento = null;
                        //}

                        usuario.CURP = item.CURP ?? "";
                        usuario.Status = item.Status ?? false;
                        usuario.Status = false;

                        usuario.Imagen = item.Imagen ?? new byte[0];

                        usuario.Rol.IdRol = item.IdRol ?? 0;
                        usuario.Rol.Nombre = item.NombreRol ?? "";

                        usuario.Direccion.IdDireccion = item.IdDireccion ?? 0;
                        usuario.Direccion.Calle = item.Calle ?? "";
                        usuario.Direccion.NumeroInterior = item.NumeroInterior ?? "";
                        usuario.Direccion.NumeroExterior = item.NumeroExterior ?? "";

                        usuario.Direccion.Colonia.IdColonia = item.IdColonia ?? 0;
                        usuario.Direccion.Colonia.Nombre = item.NombreColonia ?? "";

                        usuario.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio ?? 0;
                        usuario.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio ?? "";

                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado ?? 0;
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
        }//GETALL FUNCIONANDO PERO SIN TRAER LA FECHA

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
        }//DELETE FUNCIONANDO

        public ML.Result Add(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();


            try
            {

                var UserName = new SqlParameter("@UserName", Usuario.UserName ?? (object)DBNull.Value);
                var Nombre = new SqlParameter("@Nombre", Usuario.Nombre ?? (object)DBNull.Value);
                var AP = new SqlParameter("@ApellidoPaterno", Usuario.ApellidoPaterno ?? (object)DBNull.Value);
                var AM = new SqlParameter("@ApellidoMaterno", Usuario.ApellidoMaterno ?? (object)DBNull.Value);
                var Email = new SqlParameter("@Email", Usuario.Email ?? (object)DBNull.Value);
                var Pass = new SqlParameter("@Password", Usuario.Password ?? (object)DBNull.Value);
                var sexo = new SqlParameter("@Sexo", Usuario.Sexo ?? (object)DBNull.Value);
                var Telefono = new SqlParameter("@Telefono", Usuario.Telefono ?? (object)DBNull.Value);
                var Celular = new SqlParameter("@Celular", Usuario.Celular ?? (object)DBNull.Value);
                var Date = new SqlParameter("@FechaNacimiento", Usuario.FechaNacimiento ?? (object)DBNull.Value);
                var Curp = new SqlParameter("@CURP", Usuario.CURP ?? (object)DBNull.Value);
                var IdRol = new SqlParameter("@IdRol", Usuario.Rol.IdRol);
                var Img = new SqlParameter("@Imagen", System.Data.SqlDbType.VarBinary);
                if (Usuario.Imagen != null)
                {
                    Img.Value = Usuario.Imagen;
                }
                var Calle = new SqlParameter("@Calle", Usuario.Direccion.Calle ?? (object)DBNull.Value);
                var NumeroInterior = new SqlParameter("@NumeroInterior", Usuario.Direccion.NumeroInterior ?? (object)DBNull.Value);
                var NumeroExterior = new SqlParameter("@NumeroExterior", Usuario.Direccion.NumeroExterior ?? (object)DBNull.Value);
                var IdColonia = new SqlParameter("@IdColonia", Usuario.Direccion.Colonia.IdColonia);



                var query = _context.Database.ExecuteSqlRaw("UsuarioAdd @UserName, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @Password, @Sexo, @Telefono, @Celular, @FechaNacimiento, @CURP, @IdRol, @Imagen, @Calle, @NumeroInterior, @NumeroExterior,@IdColonia", UserName, Nombre, AP, AM, Email, Pass, sexo, Telefono, Celular, Date, Curp, IdRol, Img, Calle, NumeroInterior, NumeroExterior, IdColonia);

                if (query > 0)
                {
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
        }//FUNCIONANDO

        public ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var Id = new SqlParameter("@IdUsuario", IdUsuario);
                var query = _context.UsuarioGetByIdDTOs.FromSqlRaw("UsuarioGetById @IdUsuario", Id).AsEnumerable().FirstOrDefault();


                if (query != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    usuario.IdUsuario = query.IdUsuario;
                    usuario.UserName = query.UserName;
                    usuario.Nombre = query.Nombre;
                    usuario.ApellidoPaterno = query.ApellidoPaterno;
                    usuario.ApellidoMaterno = query.ApellidoMaterno;
                    usuario.Email = query.Email;
                    usuario.Password = query.Password;
                    usuario.Sexo = query.Sexo;
                    usuario.Telefono = query.Telefono;
                    usuario.Celular = query.Celular;
                    usuario.Imagen = query.Imagen;
                    usuario.FechaNacimiento = query.Fecha;
                    usuario.CURP = query.CURP;

                    usuario.Rol.IdRol = query.IdRol;
                    usuario.Rol.Nombre = query.NombreRol;

                    usuario.Direccion.IdDireccion = query.IdDireccion ?? 0;
                    usuario.Direccion.Calle = query.Calle ?? "";
                    usuario.Direccion.NumeroInterior = query.NumeroInterior ?? "";
                    usuario.Direccion.NumeroExterior = query.NumeroExterior ?? "";

                    usuario.Direccion.Colonia.IdColonia = query.IdColonia ?? 0;
                    usuario.Direccion.Colonia.Nombre = query.NombreColonia ?? "";

                    usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio ?? 0;
                    usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio ?? "";

                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado ?? 0;
                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado ?? "";

                    result.Object = usuario;


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }


            return result;
            //}
        } //FUNCIONANDO PERO SIN TRAER  LA IMAGEN

        public ML.Result Update(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var Id = new SqlParameter("@IdUsuario", Usuario.IdUsuario);
                var UserName = new SqlParameter("@UserName", Usuario.UserName ?? (object)DBNull.Value);
                var Nombre = new SqlParameter("@Nombre", Usuario.Nombre ?? (object)DBNull.Value);
                var AP = new SqlParameter("@ApellidoPaterno", Usuario.ApellidoPaterno ?? (object)DBNull.Value);
                var AM = new SqlParameter("@ApellidoMaterno", Usuario.ApellidoMaterno ?? (object)DBNull.Value);
                var Email = new SqlParameter("@Email", Usuario.Email ?? (object)DBNull.Value);
                var Pass = new SqlParameter("@Password", Usuario.Password ?? (object)DBNull.Value);
                var sexo = new SqlParameter("@Sexo", Usuario.Sexo ?? (object)DBNull.Value);
                var Telefono = new SqlParameter("@Telefono", Usuario.Telefono ?? (object)DBNull.Value);
                var Celular = new SqlParameter("@Celular", Usuario.Celular ?? (object)DBNull.Value);
                var Date = new SqlParameter("@FechaNacimiento", Usuario.FechaNacimiento ?? (object)DBNull.Value);
                var Curp = new SqlParameter("@CURP", Usuario.CURP ?? (object)DBNull.Value);
                var IdRol = new SqlParameter("@IdRol", Usuario.Rol!.IdRol);
                var Img = new SqlParameter("@Imagen", System.Data.SqlDbType.VarBinary);
                if (Usuario.Imagen != null)
                {
                    Img.Value = Convert.FromBase64String(Usuario.ImagenBase64);
                }
                var Calle = new SqlParameter("@Calle", Usuario.Direccion!.Calle ?? (object)DBNull.Value);
                var NumeroInterior = new SqlParameter("@NumeroInterior", Usuario.Direccion.NumeroInterior ?? (object)DBNull.Value);
                var NumeroExterior = new SqlParameter("@NumeroExterior", Usuario.Direccion.NumeroExterior ?? (object)DBNull.Value);
                var IdColonia = new SqlParameter("@IdColonia", Usuario.Direccion!.Colonia!.IdColonia);


                var query = _context.Database.ExecuteSqlRaw("UsuarioUpdate  @IdUsuario,@UserName, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @Password, @Sexo, @Telefono, @Celular, @FechaNacimiento, @CURP, @IdRol, @Imagen, @Calle, @NumeroInterior, @NumeroExterior,@IdColonia",  Id,UserName, Nombre, AP, AM, Email, Pass, sexo, Telefono, Celular, Date, Curp, IdRol, Img, Calle, NumeroInterior, NumeroExterior, IdColonia);

                if (query > 0)
                {
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
        }//FUNCIONANDO
    }
}

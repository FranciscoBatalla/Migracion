using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        private readonly DL.FbatallaProgramacionNcapasContext _context;

        public Empleado(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }

        //METODO GETALL

        public ML.Result EmpleadosGetAll(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var nombre = new SqlParameter("@Nombre", Empleado.Nombre ?? (object)DBNull.Value);
                var AP = new SqlParameter("@ApellidoPaterno", Empleado.ApellidoPaterno ?? (object)DBNull.Value);
                var IdDepto = new SqlParameter("@IdDepartamento", Empleado.Departamento.IdDepartamento);



                var query = _context.EmpleadoGetAllDTO.FromSqlRaw("EmpleadoGetAll @Nombre, @ApellidoPaterno, @IdDepartamento", nombre, AP, IdDepto).ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.Departamento = new ML.Departamento();

                        empleado.IdEmpleado = item.IdEmpleado;
                        empleado.Nombre = item.Nombre;
                        empleado.ApellidoPaterno = item.ApellidoPaterno;
                        empleado.ApellidoMaterno = item.ApellidoMaterno;
                        empleado.FechaNacimiento = item.FechaNacimiento;
                        empleado.RFC = item.RFC;
                        empleado.NSS = item.NSS;
                        empleado.CURP = item.CURP;
                        empleado.FechaIngreso = item.FechaIngreso;
                        empleado.SalarioBase = item.SalarioBase;
                        empleado.NoFaltas = item.NoFaltas;
                        empleado.Departamento.IdDepartamento = item.IdDepartamento;
                        empleado.Departamento.Descripcion = item.Descripcion;

                        result.Objects.Add(empleado);
                    }
                    result.Correct = true;
                }

            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        } //FUNCIOANNDO

        //FIN METODO GETALL

        //METODO DELETE 
        public ML.Result DeleteEmpleado(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {

                var Id = new SqlParameter("@IdEmpleado", IdEmpleado);

                var query = _context.Database.ExecuteSqlRaw("EliminarEmpleado @IdEmpleado", Id);

                if (query > 0)
                {
                    result.Correct = true;
                }


            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }


            return result;
        }
        //FIN METODO DELETE 

        //METODO ADD
        public ML.Result AgregarEmpleado(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var Name = new SqlParameter("@Nombre", Empleado.Nombre ?? (object)DBNull.Value);
                var ApellidoPaterno = new SqlParameter("@ApellidoPaterno", Empleado.ApellidoPaterno ?? (object)DBNull.Value);
                var ApellidoMaterno = new SqlParameter("@ApellidoMaterno", Empleado.ApellidoMaterno ?? (object)DBNull.Value);
                var FechaNacimiento = new SqlParameter("@FechaNacimiento", Empleado.FechaNacimiento ?? (object)DBNull.Value);
                var RFC = new SqlParameter("@RFC", Empleado.RFC ?? (object)DBNull.Value);
                var NSS = new SqlParameter("@NSS", Empleado.NSS ?? (object)DBNull.Value);
                var CURP = new SqlParameter("@CURP", Empleado.CURP ?? (object)DBNull.Value);
                var FechaIngreso = new SqlParameter("@FechaIngreso", Empleado.FechaIngreso ?? (object)DBNull.Value);
                var SalarioBase = new SqlParameter("@SalarioBase", Empleado.SalarioBase ?? (object)DBNull.Value);
                var NoFaltas = new SqlParameter("@NoFaltas", Empleado.NoFaltas ?? (object)DBNull.Value);
                var IdDepartamento = new SqlParameter("@IdDepartamento", Empleado.Departamento!.IdDepartamento ?? (object)DBNull.Value);

                var query = _context.Database.ExecuteSqlRaw("AgregarEmpleado @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @RFC, @NSS, @CURP, @FechaIngreso,@IdDepartamento ,@SalarioBase, @NoFaltas",
                    Name, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, RFC, NSS, CURP, FechaIngreso, IdDepartamento, SalarioBase, NoFaltas);

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
                return result;
            }



            return result; ;
        }//FUNCIONANDO

        //FIN METODO ADD


        //METODO UPDATE
        public ML.Result UpdateEmpleado(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                //var IdEmpleado = new SqlParameter("@IdEmpleado", Empleado.IdEmpleado);
                //var Name = new SqlParameter("@Nombre", Empleado.Nombre ?? (object)DBNull.Value);
                //var ApellidoPaterno = new SqlParameter("@ApellidoPaterno", Empleado.ApellidoPaterno ?? (object)DBNull.Value);
                //var ApellidoMaterno = new SqlParameter("@ApellidoMaterno", Empleado.ApellidoMaterno ?? (object)DBNull.Value);
                //var FechaNacimiento = new SqlParameter("@FechaNacimiento", Empleado.FechaNacimiento ?? (object)DBNull.Value);
                //var RFC = new SqlParameter("@RFC", Empleado.RFC ?? (object)DBNull.Value);
                //var NSS = new SqlParameter("@NSS", Empleado.NSS ?? (object)DBNull.Value);
                //var CURP = new SqlParameter("@CURP", Empleado.CURP ?? (object)DBNull.Value);
                //var FechaIngreso = new SqlParameter("@FechaIngreso", Empleado.FechaIngreso ?? (object)DBNull.Value);
                //var SalarioBase = new SqlParameter("@SalarioBase", Empleado.SalarioBase ?? (object)DBNull.Value);
                //var NoFaltas = new SqlParameter("@NoFaltas", Empleado.NoFaltas ?? (object)DBNull.Value);
                //var IdDepartamento = new SqlParameter("@IdDepartamento", Empleado.Departamento.IdDepartamento);
                //                                                                                ^
                //ESTA MANERA TAMBIEN ES LA CORRECTA SOLO QUE PASA BIEN Y EN ORDEN LOS PARAMETROS |
                var query = _context.Database.ExecuteSqlRaw($"ActualizarEmpleado {Empleado.IdEmpleado},'{Empleado.Nombre}','{Empleado.ApellidoPaterno}','{Empleado.ApellidoMaterno}','{Empleado.FechaNacimiento}','{Empleado.RFC}','{Empleado.NSS}','{Empleado.CURP}','{Empleado.FechaIngreso}',{Empleado.Departamento.IdDepartamento},{Empleado.SalarioBase},{Empleado.NoFaltas}");

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
        } //FUNCIONANDO
        // FIN METODO UPDATE


        //METODO GETBYID
        public ML.Result GetByIdEmpleado(int IdEmpleado)
        {
            ML.Result result = new ML.Result();


            try
            {

                var Id = new SqlParameter("@IdEmpleado", IdEmpleado);
                var query = _context.EmpleadoGetByIdDTO.FromSqlRaw("EmpleadoGetById @IdEmpleado", Id).AsEnumerable().FirstOrDefault();

                if (query != null)
                {
                    ML.Empleado empleado = new ML.Empleado();
                    empleado.Departamento = new ML.Departamento();

                    empleado.IdEmpleado = query.IdEmpleado;
                    empleado.Nombre = query.Nombre;
                    empleado.ApellidoPaterno = query.ApellidoPaterno;
                    empleado.ApellidoMaterno = query.ApellidoMaterno;
                    empleado.FechaNacimiento = query.FechaNacimiento;
                    empleado.RFC = query.RFC;
                    empleado.NSS = query.NSS;
                    empleado.CURP = query.CURP;
                    empleado.FechaIngreso = query.FechaIngreso;
                    empleado.SalarioBase = query.SalarioBase;
                    empleado.NoFaltas = query.NoFaltas;
                    empleado.Departamento.IdDepartamento = query.IdDepartamento;
                    empleado.Departamento.Descripcion = query.NombreDepartamento;

                    result.Object = empleado;

                    result.Correct = true;
                }


            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }


            return result;
        }//FUNCIONANDO
        //FIN METODO GETBYID

    }
}

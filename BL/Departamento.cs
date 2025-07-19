using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {

        private readonly DL.FbatallaProgramacionNcapasContext _context;

        public Departamento(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result GetAllDepartamentos()
        {
            ML.Result result = new ML.Result();

            try
            {

                var query = _context.DepartamentoGetAllDTO.FromSqlRaw("DepartamentoGetAll").ToList();


                if(query.Count > 0)
                {

                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Departamento departamento = new ML.Departamento();
                        departamento.IdDepartamento = item.IdDepartamento;
                        departamento.Descripcion = item.NombreDepartamento;
                        result.Objects.Add(departamento);
                    }
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

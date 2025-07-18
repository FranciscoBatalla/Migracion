using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {

        private readonly DL.FbatallaProgramacionNcapasContext _context;

        public Municipio(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var Id = new SqlParameter("@IdEstado", IdEstado);

                var query = _context.MunicipioGetByIdEstadoDTOs.FromSqlRaw("MunicipioGetByIdEstado @IdEstado", Id).ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Municipio municipio = new ML.Municipio();
                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.Nombre = item.Nombre;

                        result.Objects.Add(municipio);
                    }

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
        }
    }
}

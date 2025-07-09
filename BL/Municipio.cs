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

                var query = _context.MunicipioGetByIdEstadoDTOs.FromSqlRaw("MunicipioGetByIdEstado @IdEstado", Id).AsEnumerable().FirstOrDefault();

                if (query != null)
                {
                    ML.Municipio municipio = new ML.Municipio();
                    municipio.IdMunicipio = query.IdMunicipio;
                    municipio.Nombre = query.Nombre;

                    result.Object = municipio;
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

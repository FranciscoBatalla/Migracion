using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {

        private readonly DL.FbatallaProgramacionNcapasContext _context;
        public Colonia(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {

                var IdMuni = new SqlParameter("@IdMunicipio", IdMunicipio);
                var query = _context.ColoniaGetByIdMunicipioDTOs.FromSqlRaw("ColoniaGetByIdMunicipio @IdMunicipio", IdMuni).ToList();

                if (query != null && query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.Municipio = new ML.Municipio();

                        colonia.IdColonia = item.IdColonia;
                        colonia.Nombre = item.Nombre;
                        colonia.Municipio.IdMunicipio = item.IdMunicipio;

                        result.Objects.Add(colonia);
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
        }
    }
}

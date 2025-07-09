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
                var query = _context.ColoniaGetByIdMunicipioDTOs.FromSqlRaw("ColoniaGetByIdMunicipio @IdMunicipio", IdMuni).AsEnumerable().FirstOrDefault();

                if(query != null)
                {
                    ML.Colonia colonia = new ML.Colonia();
                    colonia.Municipio = new ML.Municipio();

                    colonia.IdColonia = query.IdColonia; 
                    colonia.Nombre = query.Nombre;
                    colonia.Municipio.IdMunicipio = query.IdMunicipio;

                    result.Object = colonia;
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}

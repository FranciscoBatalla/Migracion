using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {

        private readonly DL.FbatallaProgramacionNcapasContext _context;

        public Estado(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result GetAllEstado()
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.EstadoGetAllDTOs.FromSqlRaw("EstadoGetAll").ToList();
                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Estado estado = new ML.Estado();

                        estado.IdEstado = item.IdEstado ?? 0;
                        estado.Nombre = item.Nombre;

                        result.Objects.Add(estado);
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

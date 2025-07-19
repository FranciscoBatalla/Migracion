using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class EmpleadoGetAllDTO
    {
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? RFC { get; set; }
        public string? NSS { get; set; }
        public string? CURP { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public decimal? SalarioBase { get; set; }
        public int? NoFaltas { get; set; }

        public int? IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
    }
}

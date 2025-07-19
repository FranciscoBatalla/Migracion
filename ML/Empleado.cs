using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int? IdEmpleado { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? ApellidoPaterno { get; set; } = null!;

        public string? ApellidoMaterno { get; set; } = null!;

        public DateTime? FechaNacimiento { get; set; }

        public string? RFC { get; set; }

        public string? NSS { get; set; }

        public string? CURP { get; set; }

        public DateTime? FechaIngreso { get; set; }
        public decimal? SalarioBase { get; set; }

        public int? NoFaltas { get; set; }
        public ML.Departamento? Departamento { get; set; }

    }
}

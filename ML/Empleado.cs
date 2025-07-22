using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Empleado
    {

        public int? IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Nombre no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "Solo se permiten letras")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido Paterno es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Apellido Paterno no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "Solo se permiten letras")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El campo Apellido Materno es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Apellido Maternos no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "Solo se permiten letras")]
        public string? ApellidoMaterno { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "RFC requerido")]
        [RegularExpression(@"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$", ErrorMessage = "RFC no válido")]
        public string? RFC { get; set; }

        //[Required(ErrorMessage = "NSS requerido")]
        public string? NSS { get; set; }

        [RegularExpression(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z0-9]{2}$", ErrorMessage = "Formato de CURP no válido")]
        public string? CURP { get; set; }

        public DateTime? FechaIngreso { get; set; }

        [Range(0, 999999, ErrorMessage = "Salario fuera de rango")]
        public Decimal? SalarioBase { get; set; }

        public int? NoFaltas { get; set; }
        public ML.Departamento? Departamento { get; set; }
        public List<object>? Empleados { get; set; }

    }
}

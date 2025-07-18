using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El userName es requerido")]
        [MaxLength(50, ErrorMessage = "El UserName no puede tener mas de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El UserName no acepta caracteres especiales ni espacios en blanco")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [MaxLength(50, ErrorMessage = "El nombre no puede tener mas de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre no puede tener mas de 50 carcateres y no acepta numeros")]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = "El Apellido Paterno es Requerido")]
        [MaxLength(50, ErrorMessage = "El Apellido Paterno no puede tener mas de 20 caracteres")]
        [RegularExpression(@"^[a-zA-Z]{1,20}$", ErrorMessage = "El Apellido Paterno no acepta numeros ni espacios")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El Apellido Materno es Requerido")]
        [MaxLength(50, ErrorMessage = "El Apellido Paterno no puede tener mas de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "El Apellido Materno no puede aceptar numeros ni espacios.")]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El Email es requerido")]
        [MaxLength(254, ErrorMessage = "El Email no puede tener mas de 254 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "No es un Email Valido")]
        [EmailAddress(ErrorMessage = "No es un Email Valido")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "El Password es requerido")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,10}$", ErrorMessage = "Debe tener letras, números, sin espacios y entre 8 y 10 caracteres")]
        [MaxLength(10, ErrorMessage = "El password no puede tener mas de 10 campos")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El Sexo es requerido")]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "El Telefono es requerido")]
        [MaxLength(20, ErrorMessage = "El numero de telefono no puede tener mas de 20 caracteres")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefono no valido")]
        public string?   Telefono { get; set; }

        [MaxLength(20, ErrorMessage = "El numero de Celular no puede tener mas de 20 caracteres")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Celular no valido")]
        public string? Celular { get; set; }

        public string? FechaNacimiento { get; set; }

        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "CURP invalido, intente de nuevo")]
        [MaxLength(18, ErrorMessage = "No puede ser mayor a 18 caracteres")]
        public string? CURP { get; set; }

        public ML.Rol? Rol { get; set; }
        public bool Status { get; set; }

        public List<object>?Usuarios { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<object>? Errores { get; set; }

        public byte[]? Imagen { get; set; }

        public string? ImagenBase64 { get; set; }

    }
}

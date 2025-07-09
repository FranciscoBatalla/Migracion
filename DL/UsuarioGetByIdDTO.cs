using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioGetByIdDTO
    {
        public int IdUsuario { get; set; }
        public string? UserName { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Sexo { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? CURP { get; set; }
        public byte[]? Imagen { get; set; }

        public int? IdRol { get; set; }
        public string? NombreRol { get; set; }

        public int? IdDireccion { get; set; }
        public string? Calle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }


        public int? IdColonia { get; set; }
        public string? NombreColonia { get; set; }




        public int? IdMunicipio { get; set; }
        public string? NombreMunicipio { get; set; }

        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; }

    }
}

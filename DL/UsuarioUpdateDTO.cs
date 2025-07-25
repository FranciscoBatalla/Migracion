﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioUpdateDTO
    {
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
        public int? IdRol { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Calle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }
        public int? IdColonia { get; set; }
    }
}

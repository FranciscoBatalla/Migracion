﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class UsuarioGetAllvw
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? FechaNacimiento { get; set; }

    public string? Curp { get; set; }

    public byte[]? Imagen { get; set; }

    public bool? Status { get; set; }

    public int IdRol { get; set; }

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

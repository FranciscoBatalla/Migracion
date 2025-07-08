using System;
using System.Collections.Generic;

namespace DL;

public partial class Libro
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? NumeroPagina { get; set; }

    public string? Autor { get; set; }

    public string? Editorial { get; set; }

    public DateOnly? FechaPublicacion { get; set; }
}

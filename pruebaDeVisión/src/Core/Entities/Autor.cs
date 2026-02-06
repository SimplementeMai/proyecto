using System;
using System.Collections.Generic;

namespace pruebaDeVisión.Core.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public int AnoNacimiento { get; set; }
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

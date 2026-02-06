using System;
using System.Collections.Generic;

namespace pruebaDeVisión.Core.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Editorial { get; set; } = string.Empty;
        public int AnoPublicacion { get; set; }
        public decimal Precio { get; set; }
        public ICollection<Autor> Autores { get; set; } = new List<Autor>();
        public string Genero { get; set; } = string.Empty;
        public string Subgenero { get; set; } = string.Empty;
    }
}

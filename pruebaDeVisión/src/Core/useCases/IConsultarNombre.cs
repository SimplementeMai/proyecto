using System.Collections.Generic;
using pruebaDeVisión.Core.Entities;

namespace pruebaDeVisión.Core.useCases
{
    public interface IConsultarNombre
    {
        List<Libro> Ejecutar(string palabraClave); // Method to get books containing a specific word in their Name
    }
}

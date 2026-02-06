using System.Collections.Generic;
using pruebaDeVisión.Core.Entities;

namespace pruebaDeVisión.Core.useCases
{
    public interface IConsultarAutorLibroRomance
    {
        List<Libro> Ejecutar(); // Method to get all books
    }
}

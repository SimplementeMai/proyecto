using System.Collections.Generic;
using pruebaDeVisión.Core.Entities;

namespace pruebaDeVisión.Core.useCases
{
    public interface IConsultarAutorFamoso
    {
        List<Libro> Ejecutar(); // Method to get all books
    }
}

using System.Collections.Generic;
using pruebaDeVisión.Core.Entities; // Assuming Libro is in Entities namespace

namespace pruebaDeVisión.Core.useCases
{
    public interface IConsultarLibros
    {
        List<Libro> Ejecutar(); // Renamed from the suggested 'ObtenerTodosLosArticulos'
    }
}

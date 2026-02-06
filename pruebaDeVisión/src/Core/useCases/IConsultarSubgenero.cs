using System.Collections.Generic;
using pruebaDeVisión.Core.Entities;

namespace pruebaDeVisión.Core.useCases
{
    public interface IConsultarSubgenero
    {
        List<Libro> EjecutarCiFi(); // Method to get books with Ci-Fi subgenre
    }
}

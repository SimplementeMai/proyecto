using System.Collections.Generic;
using System.Linq;
using pruebaDeVisión.Core.Entities;
using pruebaDeVisión.Core.useCases;
using System; // For Random and StringComparison

namespace pruebaDeVisión.Application
{
    public class ConsultarNombreService : IConsultarNombre
    {
        private readonly Random _random = new Random();

        public ConsultarNombreService()
        {
        }

        public List<Libro> Ejecutar(string palabraClave)
        {
            var allBooks = GenerateRandomBooks(50); // Generate a larger pool of books
            
            // Filter books to only include those whose Name contains the keyword.
            return allBooks.Where(libro => 
                libro.Nombre != null && 
                libro.Nombre.Contains(palabraClave, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private List<Libro> GenerateRandomBooks(int count)
        {
            var libros = new List<Libro>();
            string[] editoriales = { "Editorial Alfa", "Beta Libros", "Gamma Publicaciones", "Delta Ediciones", "Epsilon Casa Editorial" };
            string[] generosBase = { "Ficción", "Ciencia Ficción", "Fantasía", "Misterio", "Romance", "Terror", "Historia", "Biografía", "Ciencia", "Aventura", "Humor" }; // Added new genres
            string[] otrasPalabrasClaveNombre = { "Historia", "Física", "Química", "Literatura", "Aventura", "Misterio" }; // Removed "Algebra"

            string[] subgenerosFiccion = { "Comedia Negra", "Thriller Psicológico" };
            string[] subgenerosFantasia = { "Fantasía Épica" };
            string[] subgenerosHistoria = { "Historia Antigua" };

            // Ensure a few "Algebra" books are generated
            int algebraBookCount = Math.Min(count, 10); // Guarantee at least 10 books, or 'count' if less than 10
            for (int i = 1; i <= algebraBookCount; i++)
            {
                libros.Add(new Libro
                {
                    Id = i,
                    Nombre = $"Libro de Algebra Avanzada {i}",
                    ISBN = GenerateRandomISBN(),
                    Editorial = editoriales[_random.Next(editoriales.Length)],
                    AnoPublicacion = _random.Next(1900, DateTime.Now.Year + 1),
                    Precio = Math.Round((decimal)(_random.NextDouble() * 100), 2),
                    Autores = new List<Autor>(),
                    Genero = "Educación", // Specific genre for Algebra
                    Subgenero = "Matemáticas"
                });
            }

            // Generate remaining books randomly
            for (int i = algebraBookCount + 1; i <= count; i++)
            {
                string assignedGenero = generosBase[_random.Next(generosBase.Length)];
                string assignedSubgenero = string.Empty;

                switch (assignedGenero)
                {
                    case "Ficción":
                        if (subgenerosFiccion.Length > 0 && _random.Next(2) == 0)
                            assignedSubgenero = subgenerosFiccion[_random.Next(subgenerosFiccion.Length)];
                        break;
                    case "Fantasía":
                        if (subgenerosFantasia.Length > 0 && _random.Next(2) == 0)
                            assignedSubgenero = subgenerosFantasia[_random.Next(subgenerosFantasia.Length)];
                        break;
                    case "Historia":
                        if (subgenerosHistoria.Length > 0 && _random.Next(2) == 0)
                            assignedSubgenero = subgenerosHistoria[_random.Next(subgenerosHistoria.Length)];
                        break;
                }

                var libro = new Libro
                {
                    Id = i,
                    Nombre = $"Libro {otrasPalabrasClaveNombre[_random.Next(otrasPalabrasClaveNombre.Length)]} Aleatorio {i}",
                    ISBN = GenerateRandomISBN(),
                    Editorial = editoriales[_random.Next(editoriales.Length)],
                    AnoPublicacion = _random.Next(1900, DateTime.Now.Year + 1),
                    Precio = Math.Round((decimal)(_random.NextDouble() * 100), 2),
                    Autores = new List<Autor>(),
                    Genero = assignedGenero,
                    Subgenero = assignedSubgenero
                };
                libros.Add(libro);
            }

            return libros;
        }

        private string GenerateRandomISBN()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                sb.Append(_random.Next(0, 10));
            }
            return sb.ToString();
        }
    }
}

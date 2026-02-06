using System.Collections.Generic;
using System.Linq;
using pruebaDeVisión.Core.Entities;
using pruebaDeVisión.Core.useCases;
using System; // For Random

namespace pruebaDeVisión.Application
{
    public class ConsultarSubgeneroCiFiService : IConsultarSubgenero
    {
        private readonly Random _random = new Random();

        public ConsultarSubgeneroCiFiService()
        {
        }

        public List<Libro> EjecutarCiFi()
        {
            var allBooks = GenerateRandomBooks(30); // Generate a few more books to ensure we have some Ci-Fi
            
            // Filter books to only include those with "Ci-Fi" in their subgenre.
            return allBooks.Where(libro => libro.Subgenero != null && libro.Subgenero.Contains("Ci-Fi", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Libro> GenerateRandomBooks(int count)
        {
            var libros = new List<Libro>();
            string[] editoriales = { "Editorial Alfa", "Beta Libros", "Gamma Publicaciones", "Delta Ediciones", "Epsilon Casa Editorial" };
            string[] generosBase = { "Ficción", "Fantasía", "Misterio", "Romance", "Terror", "Historia", "Biografía", "Ciencia", "Aventura", "Humor" }; // Added new genres
            
            string[] subgenerosFiccion = { "Comedia Negra", "Thriller Psicológico" };
            string[] subgenerosFantasia = { "Fantasía Épica" };
            string[] subgenerosHistoria = { "Historia Antigua" };

            for (int i = 1; i <= count; i++)
            {
                string assignedGenero;
                string assignedSubgenero;

                if (i % 3 == 0) // Every 3rd book is Ci-Fi
                {
                    assignedGenero = "Ciencia Ficción";
                    assignedSubgenero = "Ci-Fi";
                }
                else
                {
                    assignedGenero = generosBase[_random.Next(generosBase.Length)];
                    assignedSubgenero = string.Empty;

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
                }

                var libro = new Libro
                {
                    Id = i,
                    Nombre = "Libro Subgénero Aleatorio " + i,
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

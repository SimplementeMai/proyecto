using System.Collections.Generic;
using System.Linq;
using pruebaDeVisión.Core.Entities;
using pruebaDeVisión.Core.useCases;
using System; // For Random

namespace pruebaDeVisión.Application
{
    public class ConsultarAutorLibroRomanceService : IConsultarAutorLibroRomance
    {
        private readonly Random _random = new Random();

        public ConsultarAutorLibroRomanceService()
        {
        }

        public List<Libro> Ejecutar()
        {
            var allBooks = GenerateRandomBooks(30); // Generate a few books
            
            // Filter books to only include those with "Romance" in their genre.
            return allBooks.Where(libro => libro.Genero != null && libro.Genero.Contains("Romance", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Libro> GenerateRandomBooks(int count)
        {
            var libros = new List<Libro>();
            string[] editoriales = { "Editorial Corazón", "Sueños de Papel", "Amor Eterno Ediciones" };
            string[] generosBase = { "Ficción", "Ciencia Ficción", "Fantasía", "Misterio", "Terror", "Historia", "Biografía", "Ciencia", "Aventura", "Humor", "Romance" }; // Include Romance
            
            string[] subgenerosFiccion = { "Comedia Negra", "Thriller Psicológico" };
            string[] subgenerosFantasia = { "Fantasía Épica" };
            string[] subgenerosHistoria = { "Historia Antigua" };

            for (int i = 1; i <= count; i++)
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
                    case "Romance": // Explicitly set a subgenre for Romance if desired
                        // For simplicity, let's keep subgenre empty for now, or add specific romance subgenres later
                        assignedSubgenero = "Romance Contemporáneo"; // Example subgenre
                        break;
                }

                var libro = new Libro
                {
                    Id = 200 + i, // Use a different ID range
                    Nombre = $"Historia de Amor {i}",
                    ISBN = GenerateRandomISBN(),
                    Editorial = editoriales[_random.Next(editoriales.Length)],
                    AnoPublicacion = _random.Next(1950, DateTime.Now.Year + 1), // More recent for romance
                    Precio = Math.Round((decimal)(_random.NextDouble() * 50), 2),
                    Autores = new List<Autor> { new Autor { Id = i, Nombre = "Autor Romántico", Apellido = $"Apellido {i}", Pais = "Ficticia", AnoNacimiento = _random.Next(1900, 1980) } },
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

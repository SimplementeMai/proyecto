using System.Collections.Generic;
using pruebaDeVisión.Core.Entities;
using pruebaDeVisión.Core.useCases;
using System; // For Random

namespace pruebaDeVisión.Application
{
    public class ConsultarAutorFamosoService : IConsultarAutorFamoso
    {
        private readonly Random _random = new Random();

        public ConsultarAutorFamosoService()
        {
        }

        public List<Libro> Ejecutar()
        {
            var libros = new List<Libro>();
            string[] editoriales = { "Editorial Fama", "Estrellas Literarias", "Clásicos Eternos" };
            string[] generos = { "Clásico", "Novela Histórica", "Biografía Famosa" };
            string[] autoresFamosos = { "Gabriel García Márquez", "Jane Austen", "William Shakespeare", "Miguel de Cervantes" };

            for (int i = 1; i <= 5; i++) // Generate 5 books from famous authors
            {
                string randomAuthor = autoresFamosos[_random.Next(autoresFamosos.Length)];
                var libro = new Libro
                {
                    Id = 100 + i, // Use a different ID range
                    Nombre = $"La Obra de {randomAuthor} - Volumen {i}",
                    ISBN = GenerateRandomISBN(),
                    Editorial = editoriales[_random.Next(editoriales.Length)],
                    AnoPublicacion = _random.Next(1800, 1950), // Older books for classic authors
                    Precio = Math.Round((decimal)(_random.NextDouble() * 50), 2),
                    Autores = new List<Autor> { new Autor { Id = i, Nombre = randomAuthor.Split(' ')[0], Apellido = randomAuthor.Split(' ').Last(), Pais = "Desconocido", AnoNacimiento = _random.Next(1700, 1850) } },
                    Genero = generos[_random.Next(generos.Length)],
                    Subgenero = "Clásico Moderno"
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pruebaDeVisión.Core.Entities;
using pruebaDeVisión.Core.useCases;

namespace pruebaDeVisión.Application
{
    public class ConsultarLibrosService : IConsultarLibros
    {
        private readonly Random _random = new Random();

        public ConsultarLibrosService()
        {
        }

        public List<Libro> Ejecutar()
        {
            var libros = new List<Libro>();
            string[] editoriales = { "Editorial Alfa", "Beta Libros", "Gamma Publicaciones", "Delta Ediciones", "Epsilon Casa Editorial" };
            string[] generos = { "Ficción", "Ciencia Ficción", "Fantasía", "Misterio", "Romance", "Terror", "Historia", "Biografía", "Ciencia", "Aventura", "Humor" };
            string[] subgenerosFiccion = { "Comedia Negra", "Thriller Psicológico" };
            string[] subgenerosFantasia = { "Fantasía Épica" };
            string[] subgenerosHistoria = { "Historia Antigua" };

            for (int i = 1; i <= 25; i++)
            {
                string randomGenero = generos[_random.Next(generos.Length)];
                string randomSubgenero = string.Empty;

                switch (randomGenero)
                {
                    case "Ficción":
                        if (subgenerosFiccion.Length > 0 && _random.Next(2) == 0) // 50% chance to assign a subgenre
                            randomSubgenero = subgenerosFiccion[_random.Next(subgenerosFiccion.Length)];
                        break;
                    case "Fantasía":
                        if (subgenerosFantasia.Length > 0 && _random.Next(2) == 0)
                            randomSubgenero = subgenerosFantasia[_random.Next(subgenerosFantasia.Length)];
                        break;
                    case "Historia":
                        if (subgenerosHistoria.Length > 0 && _random.Next(2) == 0)
                            randomSubgenero = subgenerosHistoria[_random.Next(subgenerosHistoria.Length)];
                        break;
                }

                var libro = new Libro
                {
                    Id = i,
                    Nombre = "Libro Aleatorio " + i,
                    ISBN = GenerateRandomISBN(),
                    Editorial = editoriales[_random.Next(editoriales.Length)],
                    AnoPublicacion = _random.Next(1900, DateTime.Now.Year + 1),
                    Precio = Math.Round((decimal)(_random.NextDouble() * 100), 2), // Random price between 0 and 100
                    Autores = new List<Autor>(), // Initialize as empty list for now
                    Genero = randomGenero,
                    Subgenero = randomSubgenero
                };
                libros.Add(libro);
            }

            return libros;
        }

        private string GenerateRandomISBN()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 13; i++) // ISBN-13 has 13 digits
            {
                sb.Append(_random.Next(0, 10));
            }
            return sb.ToString();
        }
    }
}

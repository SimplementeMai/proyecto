using System;
using System.Collections.Generic;
using System.Linq; // For Union and Take
using pruebaDeVisión.Core.useCases;
using pruebaDeVisión.Application;
using pruebaDeVisión.Core.Entities;

// --- 1. Generación de Datos ---
IConsultarLibros consultarLibrosGeneral = new ConsultarLibrosService();
List<Libro> librosGenerales = consultarLibrosGeneral.Ejecutar();

IConsultarSubgenero consultarLibrosCiFi = new ConsultarSubgeneroCiFiService();
List<Libro> librosCiFi = consultarLibrosCiFi.EjecutarCiFi();

IConsultarNombre consultarLibrosNombre = new ConsultarNombreService();
List<Libro> librosAlgebra = consultarLibrosNombre.Ejecutar("Algebra");

IConsultarAutorFamoso consultarAutorFamoso = new ConsultarAutorFamosoService();
List<Libro> librosAutorFamoso = consultarAutorFamoso.Ejecutar();

IConsultarAutorLibroRomance consultarLibrosRomance = new ConsultarAutorLibroRomanceService(); // New service
List<Libro> librosRomance = consultarLibrosRomance.Ejecutar(); // New list

// --- 2. Combinación de Listas y Orden Caótico ---
// Generar un tope fijo de 25 libros a mostrar
const int numeroTotalLibrosAMostrar = 25; // Fixed to 25 as per request

// Consolidar todos los libros candidatos
List<Libro> librosCombinadosPreShuffle = new List<Libro>();
librosCombinadosPreShuffle.AddRange(librosGenerales.Take(5)); // Tomar 5 generales
librosCombinadosPreShuffle.AddRange(librosCiFi.Take(10)); // Tomar 10 Ci-Fi
librosCombinadosPreShuffle.AddRange(librosAlgebra.Take(10)); // Tomar 10 Algebra
librosCombinadosPreShuffle.AddRange(librosAutorFamoso.Take(5)); // Tomar 5 de Autor Famoso
librosCombinadosPreShuffle.AddRange(librosRomance.Take(10)); // Tomar 10 de Romance

// Aplicar un orden caótico (Shuffle) a la lista completa antes de aplicar el tope
librosCombinadosPreShuffle.Shuffle();

// Aplicar el tope fijo a la lista ya mezclada
List<Libro> librosCombinados = librosCombinadosPreShuffle.Take(numeroTotalLibrosAMostrar).ToList();

// --- 3. Filtrado de Sub-listas (desde la lista ya caótica) ---
List<Libro> librosCiFiFiltrados = librosCombinados
    .Where(libro => libro.Subgenero.Equals("Ci-Fi", StringComparison.OrdinalIgnoreCase))
    .Take(25)
    .ToList();
List<Libro> librosAlgebraFiltrados = librosCombinados
    .Where(libro => libro.Nombre.Contains("Algebra", StringComparison.OrdinalIgnoreCase))
    .Take(25)
    .ToList();
List<Libro> librosRomanceFiltrados = librosCombinados // New filtered list for Romance
    .Where(libro => libro.Genero.Equals("Romance", StringComparison.OrdinalIgnoreCase) || libro.Subgenero.Equals("Romance Contemporáneo", StringComparison.OrdinalIgnoreCase))
    .Take(25)
    .ToList();

// --- 4. Visualización ---
DisplayBooks($"Lista Caótica Combinada (Total {numeroTotalLibrosAMostrar} libros: Generales, Ci-Fi, Algebra, Autor Famoso, Romance)", librosCombinados, ConsoleColor.Cyan);

Console.WriteLine("\n\n*****************************************************************************************************************************************************");
Console.WriteLine("A continuación, se presentan secciones ordenadas de elementos filtrados de la lista combinada (es decir, las consultas específicas).");
Console.WriteLine("*****************************************************************************************************************************************************");

DisplayBooks("Sub-lista: Libros de 'Ci-Fi'", librosCiFiFiltrados, ConsoleColor.Green);
DisplayBooks("Sub-lista: Libros de 'Algebra'", librosAlgebraFiltrados, ConsoleColor.Yellow);
DisplayBooks("Sub-lista: Libros de Autor Famoso", librosAutorFamoso, ConsoleColor.Magenta);
DisplayBooks("Sub-lista: Libros de Romance", librosRomanceFiltrados, ConsoleColor.Red); // New section for Romance

/// <summary>
/// Helper method to display a list of books in a formatted way.
/// </summary>
static void DisplayBooks(string title, List<Libro> books, ConsoleColor titleColor)
{
    Console.WriteLine();
    Console.ForegroundColor = titleColor;
    Console.WriteLine($"==================== {title.ToUpper()} ====================");
    Console.ResetColor();

    if (books.Any())
    {
        int count = 1;
        foreach (var libro in books)
        {
            Console.WriteLine($"Libro {count++} (Id: {libro.Id,-5}): Nombre: {libro.Nombre,-40} | Género: {libro.Genero,-20} | Subgénero: {(!string.IsNullOrEmpty(libro.Subgenero) ? libro.Subgenero : "N/A"),-15}");
            Console.WriteLine($"           ISBN: {libro.ISBN,-15} | Editorial: {libro.Editorial,-20} | Año: {libro.AnoPublicacion,-5} | Precio: {libro.Precio:C}");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ForegroundColor = titleColor;
        Console.WriteLine($"\nTotal de libros en esta sección: {books.Count}");
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine("No se encontraron libros para esta sección.");
    }
}

/// <summary>
/// Extension method to shuffle a list.
/// </summary>
public static class ListExtensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

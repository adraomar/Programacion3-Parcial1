namespace Problema1;

class Program
{
    public static void Main(string[] args)
    {
        // PASO 1: Carga de registros (Material)
        Console.WriteLine("\n\n=== PASO 1: CARGA DE REGISTROS ===");
        List<Material> registros = new List<Material>
        {
            new Libro("M001", "El Aleph", "Libro", 2000, "Borges, J.L.", 274),
            new Libro("M002", "Rayuela", "Libro", 1963, "Cortazar, J.", 600),
            new Revista("M003", "National Geo", "Revista", 2023, "Nat Geo Ed.", 45),
            new Libro("M004", "Cien años", "Libro", 1967, "García Marquez", 432),
            new Revista("M005", "Time Magazine", "Revista", 2024, "Time Inc.", 12),
            new Libro("M006", "Ficciones", "Libro", 1944, "Borges, J.L.", 186)
        };

        registros.ForEach(reg => Console.WriteLine(reg.ObtenerFicha()));

        // PASO 2: Carga de Registros (Prestamo)
        Console.WriteLine("\n\n=== PASO 2: CARGA DE REGISTROS ===");
        List<Prestamo> registros2 = new List<Prestamo>
        {
            new ("P001", "M001", "Socio: Ana Lopez", "Lectura recreativa", 0, new DateTime(2026, 4, 1)),
            new ("P002", "M001", "Socio: Luis Paz", "Investigacion", 150, new DateTime(2026, 4, 15)),
            new ("P003", "M002", "Socio: Ana Lopez", "Tarea universitaria", 0, new DateTime(2026, 4, 10)),
            new ("P004", "M003", "Socio: Mario Ruiz", "Informacion general", 0, new DateTime(2026, 3, 20)),
            new ("P005", "M004", "Socio: Luis Agregar un nuevo registroaz", "Lectura recreativa", 200, new DateTime(2026, 4, 5)),
            new ("P006", "M005", "Socio: Ana Lopez", "Investigacion", 0, new DateTime(2026, 4, 22)),
            new ("P007", "M006", "Socio: Mario Ruiz", "Tarea universitaria", 0, new DateTime(2026, 4, 25)),
            new ("P008", "M003", "Socio: Luis Paz", "Revision periodica", 100, new DateTime(2026, 2, 18))
        };

        // PASO 3: Agregar un nuevo registro
        Console.WriteLine("\n\n=== PASO 3: AGREGAR UN NUEVO REGISTRO ===");

        Libro nuevo = new Libro("M007", "Don Quijote", "Libro", 1605, "Cervantes, M.", 863);
        registros.Add(nuevo);

        Console.WriteLine("✔ Don Quijote agregado exitosamente.");
        Console.WriteLine(nuevo.ObtenerFicha());

        // PASO 4: Eliminar un registro
        Console.WriteLine("\n\n=== PASO 4: ELIMINAR UN REGISTRO ===");

        int eliminados = registros.RemoveAll(r => r.Nombre == "National Geo");
        Console.WriteLine("✔ National Geo eliminado del sistema");

        eliminados = registros.RemoveAll(r => r.Nombre == "Ciencia Hoy");
        if (eliminados == 0) Console.WriteLine("✘ No se encontró ningun registro con el nombre Ciencia Hoy");

        // PASO 5: Recorrido polimórfico
        Console.WriteLine("\n\n=== PASO 5: Recorrido polimórfico ===");

        // POLIMORFISMO: la lista es de tipo Material, pero en tiempo de ejecución
        // .NET invoca el ObtenerFicha() real de cada objeto (Libro o Revista).

        foreach (var r in registros)
        {
            Console.WriteLine(r.ObtenerFicha());

            if (r is Libro libro)
            {
                libro.AccionPropia();
            }
        }

        // PASO 6: Usar IPrestable
        Console.WriteLine("\n\n=== PASO 6: Usar IPrestable ===");

        var buscar = registros.First(r => r.Nombre == "El Aleph") as IPrestable;

        if (buscar != null)
        {
            buscar.Prestar("prueba");
            Console.WriteLine("✔ Prestar aplicado a El Aleph");

            buscar.Devolver();
            Console.WriteLine("✔ Devolver ejecutado para El Aleph");

            Console.WriteLine("Estado de El Aleph: " + buscar.ObtenerEstado());
        }
        else
        {
            Console.WriteLine("Material no encontrado o no prestable.");
        }

        // Parte C: Consultas LINQ
        Console.WriteLine("\n\n=== PARTE C: CONSULTAS LINQ ===");

        // Consulta 1: Materiales ordenados de mayor a menor por año
        Console.WriteLine("\n\n=== Consulta 1: Materiales ordenados de mayor a menor por año ===");
        var ordenados = registros.OrderByDescending(r => r.Anio);

        foreach (var ord in ordenados)
        {
            Console.WriteLine($"{ord.Nombre} - {ord.Anio}");
        }

        // Consulta 2: prestamos del socio "Socio: Ana Lopez" en abril de 2026
        Console.WriteLine("\n\n=== Consulta 2: prestamos del socio \"Socio: Ana Lopez\" en abril de 2026 ===");

        var prestamos = registros2.Where(p => p.NombreSocio == "Socio: Ana Lopez" && p.Fecha.Month == 4 && p.Fecha.Year == 2026);

        foreach (var p in prestamos)
        {
            Console.WriteLine($"{p.Motivo} | {p.IdPrestamo} | Importe: ${p.Multa}");
        }

        // Consulta 3: Total de multas por material
        Console.WriteLine("\n\n=== Consulta 3: Total de multas por material ===");

        var multas = registros2
            .GroupBy(p => p.IdMaterial)
            .Select(g => new
            {
                Id = g.Key,
                Total = g.Sum(x => x.Multa)
            })
            .Join(registros,
                  g => g.Id,
                  r => r.IdMaterial,
                  (g, r) => new { r.Nombre, g.Total })
            .OrderByDescending(x => x.Total);

        foreach (var m in multas)
        {
            Console.WriteLine($"{m.Nombre}: ${m.Total}");
        }

        // Consulta 4: Estadisticas generales
        Console.WriteLine("\n\n=== Consulta 4: Estadisticas generales ===");

        Console.WriteLine($"Total de materiales registrados: {registros.Count}");
        Console.WriteLine($"Cantidad de libros: {registros.Count(r => r is Libro)}");
        Console.WriteLine($"Cantidad de revistas: {registros.Count(r => r is Revista)}");
        Console.WriteLine($"Multa promedio: {registros2.Average(p => p.Multa)}");

        var maxMulta = registros2.OrderByDescending(p => p.Multa).First();
        Console.WriteLine($"Préstamo con mayor multa: {maxMulta.IdPrestamo} (${maxMulta.Multa})");
    }
}

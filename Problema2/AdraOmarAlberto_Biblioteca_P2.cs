namespace Problema2;

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

        // PASO 2: Carga de Registros (Prestamo)
        Console.WriteLine("\n\n=== PASO 2: CARGA DE REGISTROS ===");
        List<Prestamo> registros2 = new List<Prestamo>
        {
            new ("P001", "M001", "Socio: Ana Lopez", "Lectura recreativa", 0, new DateTime(2026, 4, 1)),
            new ("P002", "M001", "Socio: Luis Paz", "Investigacion", 150, new DateTime(2026, 4, 15)),
            new ("P003", "M002", "Socio: Ana Lopez", "Tarea universitaria", 0, new DateTime(2026, 4, 10)),
            new ("P004", "M003", "Socio: Mario Ruiz", "Informacion general", 0, new DateTime(2026, 3, 20)),
            new ("P005", "M004", "Socio: Luis Paz", "Lectura recreativa", 200, new DateTime(2026, 4, 5)),
            new ("P006", "M005", "Socio: Ana Lopez", "Investigacion", 0, new DateTime(2026, 4, 22)),
            new ("P007", "M006", "Socio: Mario Ruiz", "Tarea universitaria", 0, new DateTime(2026, 4, 25)),
            new ("P008", "M003", "Socio: Luis Paz", "Revision periodica", 100, new DateTime(2026, 2, 18))
        };

        // PARTE A: Tarea 1 - Generar historial completo de "El Aleph"
        Console.WriteLine("\n\n=== Tarea 1: HISTORIAL COMPLETO DE \"El Aleph\" ===");

        List<Prestamo> historial = new List<Prestamo>();
        decimal total = 0;

        for (int i = 0; i < registros2.Count; i++)
        {
            if (registros2[i].IdMaterial == "M001")
            {
                historial.Add(registros2[i]);
            }
        }

        foreach (var prestamo in historial)
        {
            Console.WriteLine($"{prestamo.IdPrestamo} | {prestamo.Fecha.Day}/{prestamo.Fecha.Month}/{prestamo.Fecha.Year} | {prestamo.Motivo} | Responsable: {prestamo.NombreSocio} | ${prestamo.Multa}");
            total += prestamo.Multa;
        }

        Console.WriteLine($"Total acumulado de \"El Aleph\": ${total}");

        // PARTE A: Tarea 2 - Tabla de costos base de todos los registros
        Console.WriteLine("\n\n=== Tarea 2: Tabla de costos base de todos los registros ===");
        int index = 0;

        while (index < registros.Count)
        {
            Console.WriteLine($"{registros[index].Nombre} ({registros[index].Categoria}) → Costo base: ${registros[index].CalcularCostoBase()}");
            index++;
        }

        // PARTE A: Tarea 3 - Reporte acumulado por responsable
        Console.WriteLine("\n\n=== Tarea 3: Reporte acumulado por responsable ===");
        string[] responsables = { "Socio: Ana Lopez", "Socio: Luis Paz", "Socio: Mario Ruiz" };
        int iResp = 0;
        decimal totalGeneral = 0m;

        Console.WriteLine("\n\n=== Reporte por responsable ===");

        do
        {
            int cantidad = 0;
            decimal totalGen = 0m;

            for (int i = 0; i < registros2.Count; i++)
            {
                if (registros2[i].NombreSocio == responsables[iResp])
                {
                    cantidad++;
                    totalGen += registros2[i].Multa;
                }
            }

            Console.WriteLine($"{responsables[iResp]} → {cantidad} registros | Total: ${totalGen}");
            totalGeneral += totalGen;
            iResp++;
        } while (iResp < responsables.Length);

        Console.WriteLine("============================");
        Console.WriteLine($"TOTAL GENERAL: ${totalGeneral}");

        // PARTE B: Recursividad, Arrays y Matrices
        Console.WriteLine("\n\n=== Parte B: Ejercicio 1 - Metoodo recursivo: BuscarMaterialPorId ===");

        var buscar = Busquedas.BuscarMaterialPorId(registros, "M004");

        if (buscar != null)
            Console.WriteLine($"Material encontrado: {buscar.ObtenerFicha()}");
        else
            Console.WriteLine("M004 no encontrado.");

        buscar = Busquedas.BuscarMaterialPorId(registros, "M999");

        if (buscar != null)
            Console.WriteLine($"Material encontrado: {buscar.ObtenerFicha()}");
        else
            Console.WriteLine("M999 no encontrado.");

        // PARTE B: Ejercicio 2 - Array: costos totales por elemento
        Console.WriteLine("\n\n=== Parte B: Ejercicio 2 - Array: costos totales por elemento ===");

        decimal[] costosPorMaterial = new decimal[registros.Count];

        for (int i = 0; i < registros.Count; i++)
        {
            decimal totalCosto = 0m;

            for (int j = 0; j < registros2.Count; j++)
            {
                if (registros[i].IdMaterial == registros2[j].IdMaterial)
                {
                    totalCosto += registros2[j].Multa;
                }
            }

            costosPorMaterial[i] = totalCosto;
        }

        decimal max = decimal.MinValue, min = decimal.MaxValue, suma = 0m;
        int cont = 0;
        string nombreMax = "", nombreMin = "";

        for (int i = 0; i < costosPorMaterial.Length; i++)
        {
            if (costosPorMaterial[i] > 0)
            {
                if (costosPorMaterial[i] > max)
                {
                    max = costosPorMaterial[i];
                    nombreMax = registros[i].Nombre;
                }

                if (costosPorMaterial[i] < min)
                {
                    min = costosPorMaterial[i];
                    nombreMin = registros[i].Nombre;
                }

                suma += costosPorMaterial[i];
                cont++;
            }
        }

        Console.WriteLine($"Mayor gasto: {nombreMax} → ${max}");
        Console.WriteLine($"Menor gasto: {nombreMin} → ${min}");
        Console.WriteLine($"Promedio: ${suma / cont}");

        // Parte B:Ejercicio 3 — Matriz: materials por responsables
        Console.WriteLine("\n\n=== Parte B: Ejercicio 3 - Matriz: materials por responsables ===");

        string[] resp = { "Socio: Ana Lopez", "Socio: Luis Paz", "Socio: Mario Ruiz" };
        decimal[,] matriz = new decimal[registros.Count, resp.Length];

        for (int i = 0; i < registros.Count; i++)
        {
            for (int j = 0; j < resp.Length; j++)
            {
                for (int k = 0; k < registros2.Count; k++)
                {
                    if (registros[i].IdMaterial == registros2[k].IdMaterial && registros2[k].NombreSocio == resp[j])
                    {
                        matriz[i, j] += registros2[k].Multa;
                    }
                }
            }
        }

        // Mostrar matriz
        Console.WriteLine("Material\tAna\tLuis\tMario");
        for (int i = 0; i < registros.Count; i++)
        {
            Console.Write($"{registros[i].Nombre}\t");

            for (int j = 0; j < resp.Length; j++)
            {
                Console.Write($"{matriz[i, j]}\t");
            }

            Console.WriteLine();
        }

        // Totales por responsable
        decimal[] totales = new decimal[resp.Length];

        for (int j = 0; j < resp.Length; j++)
        {
            for (int i = 0; i < registros.Count; i++)
            {
                totales[j] += matriz[i, j];
            }

            Console.WriteLine($"{resp[j]} → ${totales[j]}");
        }

        // Mayor recaudación
        decimal mayor = 0;
        string mejor = "";

        for (int i = 0; i < totales.Length; i++)
        {
            if (totales[i] > mayor)
            {
                mayor = totales[i];
                mejor = resp[i];
            }
        }

        Console.WriteLine($"Mayor recaudación: {mejor}");
    }
}

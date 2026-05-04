namespace Problema2;

class Libro : Material, IPrestable
{
    public string Autor { get; set; }
    public int NroPaginas { get; set; }

    private bool _estaDisponible = true;
    public bool EstaDisponible => _estaDisponible;

    public Libro(string id, string nombre, string categoria, int anio, string autor, int nroPaginas ) : base(id, nombre, categoria, anio)
    {
        Autor = autor;
        NroPaginas = nroPaginas;
    }

    public override decimal CalcularCostoBase()
    {
        // Costo base: tarifa fija $1500 + $0.50 por cada página
        return 1500m + (0.50m * NroPaginas);
    }

    public override string ObtenerFicha()
    {
        return base.ObtenerFicha() + $" | Autor: { Autor } | Páginas: { NroPaginas }";
    }

    public void AccionPropia()
    {
        Console.WriteLine("Pasando tiempo de calidad con una buena lectura...");
    }

    // Implementacion de Interfaz IPrestable
    public void Prestar(string detalle)
    {
        if(!EstaDisponible)
            throw new InvalidOperationException("El libro ya está prestado.");

        _estaDisponible = false;    

        Console.WriteLine("Libro prestado. Detalle: " + detalle);
    }

    public void Devolver()
    {
        if(EstaDisponible)
            throw new InvalidOperationException("El libro ya está disponible.");

        _estaDisponible = true;

        Console.WriteLine("Libro devuelvo correctamente!");    
    }
}
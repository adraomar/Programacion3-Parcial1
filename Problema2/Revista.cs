namespace Problema2;

class Revista : Material
{
    public string Editorial { get; set; }
    public int NroEdicion { get; set; }

    public Revista(string id, string nombre, string categoria, int anio, string editorial, int nroEdicion) : base(id, nombre, categoria, anio)
    {
        Editorial = editorial;
        NroEdicion = nroEdicion;
    }

    public override decimal CalcularCostoBase()
    {
        // Costo base fijo de $800 para revistas
        return 800m;
    }

    public override string ObtenerFicha()
    {
        return base.ObtenerFicha() + $" | Editorial: {Editorial} | Numero de edicion: {NroEdicion}";
    }
}
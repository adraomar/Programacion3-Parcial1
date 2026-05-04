namespace Problema1;

public abstract class Material
{
    private string _idMaterial;
    private string _nombre;
    private string _categoria;

    public string Nombre => _nombre;
    public string IdMaterial => _idMaterial;

    public string Categoria
    {
        get => _categoria;
        set 
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("La categoria no puede estar vacía.");

            _categoria = value;    
        }

    }

    public int Anio { get; set; }

    public Material(string id, string nombre, string categoria, int anio)
    {
        _idMaterial = id;
        _nombre = nombre;
        _categoria = categoria;
        Anio = anio;
    }

    public abstract decimal CalcularCostoBase();

    public virtual string ObtenerFicha()
    {
        return $"{IdMaterial} | {Nombre} | {Categoria} | Año: {Anio}";
    }
}
namespace Problema1;
public interface IPrestable
{
    bool EstaDisponible { get; }

    void Prestar(string detalle);
    void Devolver();

    public string ObtenerEstado()
    {
        return EstaDisponible ? "Disponible" : "Prestado";
    }
}
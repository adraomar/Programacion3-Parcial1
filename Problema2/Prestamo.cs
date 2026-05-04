namespace Problema2;
public record Prestamo(
    string IdPrestamo,
    string IdMaterial,
    string NombreSocio,
    string Motivo,
    decimal Multa,
    DateTime Fecha
);
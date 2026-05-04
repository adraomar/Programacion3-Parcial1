namespace Problema2;

public static class Busquedas
{
    public static Material BuscarMaterialPorId(List<Material> lista, string id, int index = 0)
    {
        if(index >= lista.Count)
            return null;

        if(lista[index].IdMaterial == id)
            return lista[index];    

        return BuscarMaterialPorId(lista, id, index + 1);
    }
}
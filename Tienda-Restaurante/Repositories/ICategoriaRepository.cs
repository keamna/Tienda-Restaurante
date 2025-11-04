namespace Tienda_Restaurante.Repositories
{
    public interface ICategoriaRepository 
    {
        Task AddCategoria(Categoria genre);
        Task UpdateCategoria(Categoria genre);
        Task<Categoria?> GetCategoriaById(int id);
        Task DeleteCategoria(Categoria genre);
        Task<IEnumerable<Categoria>> GetCategoria();
    }
}

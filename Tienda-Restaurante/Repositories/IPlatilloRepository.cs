namespace Tienda_Restaurante.Repositories
{
    public interface IPlatilloRepository
    {
        Task AddPlatillo(Platillo platillo);
        Task UpdatePlatillo(Platillo platillo);
        Task DeletePlatillo(Platillo platillo);
        Task<Platillo?> GetPlatilloById(int id);
        Task<IEnumerable<Platillo>> GetPlatillos();
    }
}

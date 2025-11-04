using Microsoft.EntityFrameworkCore;
using Tienda_Restaurante.Areas.Identity.Data;
using Tienda_Restaurante.Models;

namespace Tienda_Restaurante.Repositories
{
    public class PlatilloRepository : IPlatilloRepository
    {
        private readonly ApplicationDbContext _context;
        public PlatilloRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPlatillo(Platillo platillo)
        {
            _context.Platillos.Add(platillo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlatillo(Platillo platillo)
        {
            _context.Platillos.Update(platillo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlatillo(Platillo platillo)
        {
            _context.Platillos.Remove(platillo);
            await _context.SaveChangesAsync();
        }

        public async Task<Platillo?> GetPlatilloById(int id) => await _context.Platillos.FindAsync(id);

        public async Task<IEnumerable<Platillo>> GetPlatillos() => await _context.Platillos.Include(a => a.Categoria).ToListAsync();
    }
}


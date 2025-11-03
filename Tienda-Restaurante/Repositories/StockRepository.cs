using System.Collections;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Tienda_Restaurante.Areas.Identity.Data;
using Tienda_Restaurante.DTOs;

namespace Tienda_Restaurante.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByPlatilloId(int platilloId) => await
            _context.Stocks.FirstOrDefaultAsync(s => s.PlatilloId == platilloId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            var existingStock = await GetStockByPlatilloId(stockToManage.PlatilloId);
            if (existingStock is null)
            {
                var stock = new Stock { PlatilloId = stockToManage.PlatilloId, Cantidad = stockToManage.Cantidad };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Cantidad = stockToManage.Cantidad;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sterm = "")
        {
            var stocks = await (from platillo in _context.Platillos
                                join stock in _context.Stocks
                                on platillo.Id equals stock.PlatilloId
                                into platillo_stock
                                from platilloStock in platillo_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sterm) || platillo.PlatilloName.ToLower().Contains(sterm.ToLower())
                                select new StockDisplayModel
                                {
                                    PlatilloId = platillo.Id,
                                    PlatilloName = platillo.PlatilloName,
                                    Cantidad = platilloStock == null ? 0 : platilloStock.Cantidad
                                }
                               ).ToListAsync();
            return stocks;
        }
    }
}

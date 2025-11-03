using Tienda_Restaurante.DTOs;

namespace Tienda_Restaurante.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sterm = "");
        Task<Stock?> GetStockByPlatilloId(int platilloId);
        Task ManageStock(StockDTO stockToManage);
    }
}
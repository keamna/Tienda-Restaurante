using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda_Restaurante.Constants;

namespace Tienda_Restaurante.Controllers
{
    [Authorize(Roles =nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository) 
        {
            _stockRepository = stockRepository;
        }

        public async Task<IActionResult> Stock(string sterm = "")
        {
            var stocks = await _stockRepository.GetStocks(sterm);
            return View(stocks);
        }
    }
}

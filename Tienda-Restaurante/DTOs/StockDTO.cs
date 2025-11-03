using System.ComponentModel.DataAnnotations;

namespace Tienda_Restaurante.DTOs
{
    public class StockDTO
    {
        public int PlatilloId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 1")]

        public int Cantidad { get; set; }
    }
}

namespace Tienda_Restaurante.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int PlatilloId { get; set; }
        public int Cantidad { get; set; }
        public string? PlatilloName { get; set; }
    }
}

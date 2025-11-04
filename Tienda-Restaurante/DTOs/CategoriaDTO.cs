using System.ComponentModel.DataAnnotations;

namespace Tienda_Restaurante.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string CategoriaName { get; set; }
    }
}

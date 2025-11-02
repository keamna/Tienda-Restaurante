using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tienda_Restaurante1.Models;

namespace Tienda_Restaurante.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Platillo> Platillos { get; set; }
    public DbSet<Carrito> Carrito { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<DetalleCarrito> DetalleCarrito { get; set; }
    public DbSet<DetalleOrden> DetalleOrden { get; set; }
    public DbSet<Orden> Orden { get; set; }
    public DbSet<OrdenEstado> OrdenEstado { get; set; }

}

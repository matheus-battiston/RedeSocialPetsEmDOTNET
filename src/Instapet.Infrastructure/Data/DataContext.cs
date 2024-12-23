using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Usuario> Usuarios { get; set; } 
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<PedidoAmizade> PedidoAmizades { get; set; }
    public DbSet<UsuarioAmigo> UsuarioAmigos { get; set; }
    public DbSet<Curtida> Curtidas { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);


    }

}
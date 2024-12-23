using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class PostMap : BaseMap<Post>
{
    public PostMap() : base("tb_post"){}
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.UrlImagem).HasColumnName("urlImagem").HasColumnType("varchar(512)").IsRequired();
        builder.Property(x => x.Legenda).HasColumnName("legenda").HasColumnType("varchar(2000)");
        builder.Property(x => x.Horario).HasColumnName("horario").HasColumnType("timestamp").IsRequired();
        builder.Property(x => x.Privado).HasColumnName("privado").HasColumnType("bool").IsRequired();
        
        builder.Property(x => x.IdUsuario).HasColumnName("id_usuario").IsRequired();
        builder.HasOne(p => p.Usuario)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
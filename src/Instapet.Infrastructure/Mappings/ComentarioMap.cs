using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class ComentarioMap : BaseMap<Comentario>
{
    public ComentarioMap() : base("tb_comentario"){}
    public override void Configure(EntityTypeBuilder<Comentario> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Mensagem).HasColumnName("mensagem").HasColumnType("varchar(512)").IsRequired();
        builder.Property(x => x.Horario).HasColumnName("horario").HasColumnType("timestamp").IsRequired();
        
        builder.Property(x => x.IdUsuario).HasColumnName("id_usuario").IsRequired();
        builder.HasOne(x => x.Usuario).WithMany(x => x.Comentarios).HasForeignKey(x => x.IdUsuario).OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.IdPost).HasColumnName("id_post").IsRequired();
        builder.HasOne(x => x.Post).WithMany(x => x.Comentarios).HasForeignKey(x => x.IdPost).OnDelete(DeleteBehavior.Cascade);
        
    }
}
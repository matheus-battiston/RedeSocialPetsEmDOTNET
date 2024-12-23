using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class CurtidaMap : BaseMap<Curtida>
{
    public CurtidaMap() : base("tb_curtida"){}
    public override void Configure(EntityTypeBuilder<Curtida> builder)
    {
        base.Configure(builder);
        
        builder.HasOne(c => c.Post)
            .WithMany(p => p.Curtidas)
            .HasForeignKey(c => c.IdPost)
            .OnDelete(DeleteBehavior.Cascade); ;

        builder.HasOne(c => c.Usuario)
            .WithMany(u => u.Curtidas)
            .HasForeignKey(c => c.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
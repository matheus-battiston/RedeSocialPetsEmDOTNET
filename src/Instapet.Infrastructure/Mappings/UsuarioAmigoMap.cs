using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class UsuarioAmigoMap : BaseMap<UsuarioAmigo>
{
    public UsuarioAmigoMap() : base("tb_usuario_amigo"){}
    
    public override void Configure(EntityTypeBuilder<UsuarioAmigo> builder)
    {
        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Amigo)
            .WithMany()
            .HasForeignKey(x => x.AmigoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
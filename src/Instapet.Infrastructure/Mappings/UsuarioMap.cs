using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class UsuarioMap : BaseMap<Usuario>
{
    public UsuarioMap() : base("tb_usuario")
    {
    }

    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(255)").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(255)").IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Apelido).HasColumnName("apelido").HasColumnType("varchar(50)");
        builder.Property(x => x.DataNascimento).HasColumnName("dataNascimento").HasColumnType("date").IsRequired();
        builder.Property(x => x.CEP).HasColumnName("cpf").HasColumnType("varchar(8)").IsRequired();
        builder.Property(x => x.HashSenha).HasColumnName("senha").HasColumnType("varchar(128)").IsRequired();
        builder.Property(x => x.UrlFotoPerfil).HasColumnName("urlFotoPerfil").HasColumnType("varchar(512)")
            .IsRequired();
        
        builder.HasMany(x => x.Amigos)
            .WithMany()
            .UsingEntity<UsuarioAmigo>(
                j => j.HasOne(ua => ua.Amigo).WithMany(),
                j => j.HasOne(ua => ua.Usuario).WithMany()
            );
    }
}
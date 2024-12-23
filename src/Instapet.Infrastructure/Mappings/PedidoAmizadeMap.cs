using Instapet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instapet.Infrastructure.Mappings;

public class PedidoAmizadeMap : BaseMap<PedidoAmizade>
{
    public PedidoAmizadeMap() : base("tb_pedido_amizade"){}
    
    public override void Configure(EntityTypeBuilder<PedidoAmizade> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.RequerenteId).HasColumnName("requerente_id").IsRequired();
        builder.HasOne(x => x.Requerente).WithMany(x => x.PedidosAmizadeEnviados).HasForeignKey(x => x.RequerenteId);
        
        
        
        builder.Property(x => x.RequisitadoId).HasColumnName("requisitado_id").IsRequired();
        builder.HasOne(x => x.Requisitado).WithMany(x => x.PedidosAmizadeRecebidos).HasForeignKey(x => x.RequisitadoId);


    }
}
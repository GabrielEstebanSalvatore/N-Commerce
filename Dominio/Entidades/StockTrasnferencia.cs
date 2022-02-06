using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.MetaData;

namespace Dominio.Entidades
{
    [Table("Stock_Transferencia")]
    [MetadataType(typeof(IStockTransferencia))]
    public class StockTrasnferencia : EntidadBase
    {
        // Propiedades
        public long ArticuloId { get; set; }

        public long DepositoDonanteId { get; set; }

        public long DepositoReceptorId { get; set; }

        public decimal Cantidad { get; set; }

        public long UsuarioId { get; set; }

        public DateTime Fecha { get; set; }

        // Propiedades de Navegacion
        public virtual Usuario Usuario { get; set; }
        public virtual Articulo Articulo { get; set; }
        
        public virtual Stock DepositoDonante { get; set; }
   
        public virtual Stock DepositoReceptor { get; set; }

    }
}

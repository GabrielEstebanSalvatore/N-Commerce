using IServicio.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.StockTransferencia.DTOs
{
    public class StockTransferenciaServicioDto : DtoBase
    {
        public long ArticuloId { get; set; }
        public string Articulo { get; set; }

        public long DepositoDonanteId { get; set; }
        public string DepositoDonante { get; set; }

        public long DepositoReceptorId { get; set; }
        public string DepositoReceptor { get; set; }

        public decimal Cantidad { get; set; }

        public long UsuarioId { get; set; }
        public string UsuarioStr { get; set; }

        public DateTime Fecha { get; set; }
        public string FechaStr => Fecha.ToShortDateString();
    }
}

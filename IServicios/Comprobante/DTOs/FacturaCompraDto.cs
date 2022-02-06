using Aplicacion.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.Comprobante.DTOs
{
    public class FacturaCompraDto : ComprobanteDto
    {
        public Estado Estado { get; set; }

        public bool ArticuloEntregado { get; set; }

        public bool VieneVentas { get; set; }

        public bool vienePresupuesto { get; set; }

        public bool vieneCompras { get; set; }

        public long ProveedorId { get; set; }

        public DateTime FechaEntrega { get; set; }
        public int Numero { get; set; }

        public decimal Iva27 { get; set; }
        public decimal ImpInterno { get; set; }
        public decimal PercepcionTemp { get; set; }
        public decimal PercepcionPyP { get; set; }
        public decimal PercepcionIva { get; set; }
        public decimal PercepcionIB { get; set; }
    }
}

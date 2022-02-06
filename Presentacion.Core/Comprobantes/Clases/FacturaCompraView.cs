using Aplicacion.Constantes;
using IServicio.Persona.DTOs;
using IServicios.Proveedor.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.Core.Comprobantes.Clases
{
    public class FacturaCompraView
    {
        public FacturaCompraView()
        {
            if (Items == null)
                Items = new List<ItemView>();

            //ContadorItem = 0;
        }

        // Cabecera
        public int ContadorItem { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public EmpleadoDto Empleado { get; set; }
        public long UsuarioId { get; set; }
        public bool ArticuloEntregado { get; set; }
        public DateTime FechaEntrega { get; set; }

        public TipoComprobante TipoComprobante { get; set; }

        // Cuerpo
        public List<ItemView> Items { get; set; }

        // Pie
        public decimal Total => Items.Sum(x => x.SubTotal);

        public string TotalStr => Total.ToString("C");

        public decimal Iva21 { get; set; }
        public decimal Iva27 { get; set; }
        public decimal Iva105 { get; set; }

        public decimal ImpInterno { get; set; }
        public decimal PercepcionTemp { get; set; }
        public decimal PercepcionPyP{ get; set; }
        public decimal PercepcionIva { get; set; }
        public decimal PercepcionIB { get; set; }

    }
}

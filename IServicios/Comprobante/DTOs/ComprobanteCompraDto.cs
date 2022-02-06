using Aplicacion.Constantes;
using IServicio.BaseDto;
using System;
using System.Collections.Generic;

namespace IServicios.Comprobante.DTOs
{
    public class ComprobanteCompraDto : DtoBase
    {
        public ComprobanteCompraDto()
        {
            if (Items == null)
                Items = new List<DetalleComprobanteDto>();
            if (FormasDePagos == null)
                FormasDePagos = new List<FormaPagoDto>();
        }

        public long EmpleadoId { get; set; }
        public long UsuarioId { get; set; }
        public long ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int Numero { get; set; }
        public decimal Total { get; set; }

        public bool ArticuloEntregado { get; set; }
        public Estado Estado { get; set; }

        public decimal Iva21 { get; set; }
        public decimal Iva27 { get; set; }
        public decimal Iva105 { get; set; }

        public decimal ImpInterno { get; set; }
        public decimal PercepcionTemp { get; set; }
        public decimal PercepcionPyP { get; set; }
        public decimal PercepcionIva { get; set; }
        public decimal PercepcionIB { get; set; }
    
        public TipoComprobante TipoComprobante { get; set; }
        public List<DetalleComprobanteDto> Items { get; set; }
        public List<FormaPagoDto> FormasDePagos { get; set; }
    }
}

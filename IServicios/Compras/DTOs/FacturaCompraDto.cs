using Aplicacion.Constantes;
using IServicio.BaseDto;
using IServicios.Comprobante.DTOs;
using System;
using System.Collections.Generic;

namespace IServicios.Compras.DTOs
{
    public class FacturaCompraDto : DtoBase
    {
        public FacturaCompraDto()
        {
            if (Items == null)
                Items = new List<DetalleComprobanteCompraDto>();
        }
        // Propiedades
        public long ProveedorId { get; set; }

        public string RazonSocial { get; set; }

        public string CUIT { get; set; }

        public DateTime FechaEntrega { get; set; }

        public string FechaStr => FechaEntrega.ToShortDateString();

        public decimal Iva27 { get; set; }


        public decimal Total { get; set; }

        public decimal Iva21 { get; set; }

        public decimal Iva105 { get; set; }

        public decimal PrecepcionTemp { get; set; }

        public decimal PrecepcionPyP { get; set; }

        public decimal PrecepcionIva { get; set; }

        public decimal PrecepcionIB { get; set; }

        public decimal ImpInterno { get; set; }

        public Estado EstadoFactura { get; set; }

        public bool ArticuloEntregado{ get; set; }

        public List<DetalleComprobanteCompraDto> Items { get; set; }


    }
}

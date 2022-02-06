using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.Compras;
using IServicios.Compras.DTOs;
using Servicios.Base;
using Servicios.Comprobante;

namespace Servicios.Compras
{
    public class ComprasServicio : ComprobanteServicio, IComprasServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        DateTime _fecha;

        public ComprasServicio(IUnidadDeTrabajo unidadDeTrabajo)
              : base(unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

      
        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (FacturaCompraDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.FacturaCompraRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener la Factura");

            entidad.EstadoFactura = dto.EstadoFactura;
            entidad.FechaEntrega = dto.FechaEntrega;
            entidad.ImpInterno = dto.ImpInterno;
            entidad.Iva27 = dto.Iva27;
            entidad.Proveedor.CUIT = dto.CUIT;
            entidad.Iva105 = dto.Iva105;
            entidad.Iva21 = dto.Iva21;
            entidad.PrecepcionIB = dto.PrecepcionIB;
            entidad.PrecepcionIva = dto.PrecepcionIva;
            entidad.PrecepcionPyP = dto.PrecepcionPyP;
            entidad.PrecepcionTemp = dto.PrecepcionTemp;
            entidad.ProveedorId = dto.ProveedorId;
            entidad.Proveedor.RazonSocial = dto.RazonSocial;
            entidad.Total = dto.Total;
            entidad.EstaEliminado = dto.Eliminado;
            entidad.ArticuloEntregado = dto.ArticuloEntregado;

            _unidadDeTrabajo.FacturaCompraRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

       public FacturaCompraDto Obtener(long id)
       {
            return _unidadDeTrabajo.FacturaCompraRepositorio.Obtener(x=> x.Id == id, "Proveedor, DetalleComprobantes ")
                .Select(x => new FacturaCompraDto
                {
                    Id = x.Id,
                    EstadoFactura = x.EstadoFactura,
                    FechaEntrega = x.FechaEntrega,
                    ImpInterno = x.ImpInterno,
                    Iva27 = x.Iva27,
                    CUIT = x.Proveedor.CUIT,
                    Iva105 = x.Iva105,
                    Iva21 = x.Iva21,
                    PrecepcionIB = x.PrecepcionIB,
                    PrecepcionIva = x.PrecepcionIva,
                    PrecepcionPyP = x.PrecepcionPyP,
                    PrecepcionTemp = x.PrecepcionTemp,
                    ProveedorId = x.ProveedorId,
                    RazonSocial = x.Proveedor.RazonSocial,
                    Total = x.Total,
                    Eliminado = x.EstaEliminado,
                    ArticuloEntregado = x.ArticuloEntregado,
                    Items = x.DetalleComprobantes
                    .Select(d => new DetalleComprobanteCompraDto
                    {
                        Codigo = d.Codigo,
                        ArticuloId = d.ArticuloId,
                        Precio = d.Precio,
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        SubTotal = d.SubTotal,
                        ComprobanteId = d.ComprobanteId
                    }).ToList()

                }).FirstOrDefault();
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true, bool fechaActual = false)
        {
            Expression<Func<Dominio.Entidades.Compra, bool>> filtro = x =>
              x.Proveedor.RazonSocial.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }
    
            return _unidadDeTrabajo.FacturaCompraRepositorio.Obtener(filtro, "Proveedor, DetalleComprobantes ")
                 .Select(x => new FacturaCompraDto
                 {
                     Id = x.Id,
                     EstadoFactura = x.EstadoFactura,
                     
                     FechaEntrega = x.FechaEntrega,
                     ImpInterno = x.ImpInterno,
                     Iva27 = x.Iva27,
                     CUIT = x.Proveedor.CUIT,
                     Iva105 = x.Iva105,
                     Iva21 = x.Iva21,
                     PrecepcionIB = x.PrecepcionIB,
                     PrecepcionIva = x.PrecepcionIva,
                     PrecepcionPyP = x.PrecepcionPyP,
                     PrecepcionTemp = x.PrecepcionTemp,
                     ProveedorId = x.ProveedorId,
                     RazonSocial = x.Proveedor.RazonSocial,
                     Total = x.Total,
                     Eliminado = x.EstaEliminado,
                     ArticuloEntregado = x.ArticuloEntregado,
                     Items = x.DetalleComprobantes
                    .Select(d => new DetalleComprobanteCompraDto
                    {
                        Codigo = d.Codigo,
                        ArticuloId = d.ArticuloId,
                        Precio = d.Precio,
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        SubTotal = d.SubTotal,
                        ComprobanteId = d.ComprobanteId

                    }).ToList()

                 })
                 .OrderBy(x => x.FechaEntrega)
                 .ToList();
        }


        public IEnumerable<FacturaCompraDto> ObtenerCompras(string cadenaBuscar, bool mostrarTodos = true, bool ArticulosEntregados = true)
        {
            Expression<Func<Dominio.Entidades.Compra, bool>> filtro = x =>
              x.Proveedor.RazonSocial.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            if (!ArticulosEntregados)
            {
                filtro = filtro.And(x => !x.ArticuloEntregado);
            }

            var comprobantes = _unidadDeTrabajo.FacturaCompraRepositorio.Obtener(filtro, "Proveedor, DetalleComprobantes ")
                .Select(x => new FacturaCompraDto
                {
                    Id = x.Id,
                    EstadoFactura = x.EstadoFactura,
                    FechaEntrega = x.FechaEntrega,
                    ImpInterno = x.ImpInterno,
                    Iva27 = x.Iva27,
                    CUIT = x.Proveedor.CUIT,
                    Iva105 = x.Iva105,
                    Iva21 = x.Iva21,
                    PrecepcionIB = x.PrecepcionIB,
                    PrecepcionIva = x.PrecepcionIva,
                    PrecepcionPyP = x.PrecepcionPyP,
                    PrecepcionTemp = x.PrecepcionTemp,
                    ProveedorId = x.ProveedorId,
                    RazonSocial = x.Proveedor.RazonSocial,
                    Total = x.Total,
                    Eliminado = x.EstaEliminado,
                    ArticuloEntregado = x.ArticuloEntregado,
                    Items = x.DetalleComprobantes
                    .Select(d => new DetalleComprobanteCompraDto
                    {
                        Codigo = d.Codigo,
                        ArticuloId = d.ArticuloId,
                        Precio = d.Precio,
                        Descripcion =d.Descripcion,
                        Cantidad = d.Cantidad,
                        SubTotal = d.SubTotal,
                        ComprobanteId = d.ComprobanteId     

                    }).ToList()
                })
                .OrderBy(x => x.FechaEntrega)
                .ToList();

            return comprobantes;
        }
    }
}

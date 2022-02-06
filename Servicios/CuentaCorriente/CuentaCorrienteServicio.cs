using Aplicacion.Constantes;
using Dominio.Entidades;
using Dominio.UnidadDeTrabajo;
using IServicios.Comprobante;
using IServicios.Comprobante.DTOs;
using IServicios.Contador;
using IServicios.CuentaCorriente;
using IServicios.CuentaCorriente.DTOs;
using Servicios.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Servicios.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IContadorServicio _contadorServicio;
        private readonly ICtaCteComprobanteServicio _ctaCteComprobanteServicio;

        public CuentaCorrienteServicio(IUnidadDeTrabajo unidadDeTrabajo,
            IContadorServicio contadorServicio,
            ICtaCteComprobanteServicio ctaCteComprobanteServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _contadorServicio = contadorServicio;
            _ctaCteComprobanteServicio = ctaCteComprobanteServicio;
        }

        public IEnumerable<CuentaCorrienteDto> Obtener(long clienteId,DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda, bool estaEliminado)
        {
            var _fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);
            var _fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

            Expression<Func<MovimientoCuentaCorriente, bool>> filtro = x => x.ClienteId == clienteId;

            
            if (soloDeuda)
            {
                filtro = filtro.And(x => x.TipoMovimiento == TipoMovimiento.Egreso);
            }
            if (!estaEliminado)
            {
                filtro = filtro.And(x => x.EstaEliminado == false);
            }

            return _unidadDeTrabajo.CuentaCorrienteRepositorio.Obtener(filtro)
                .Select(x => new CuentaCorrienteDto
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    Descripcion = x.Descripcion,
                    Monto = (x.Monto * (int) x.TipoMovimiento),
                    EstaEliminado = x.EstaEliminado

                }).OrderBy(x => x.Fecha)
                .ToList();
                
        }

        public decimal ObtenerDeudaCliente(long clienteId)
        {
            var movimientos = _unidadDeTrabajo.CuentaCorrienteRepositorio.Obtener(x => !x.EstaEliminado && x.ClienteId == clienteId);

            return movimientos.Sum(x => x.Monto * (int)x.TipoMovimiento);
        }

        public void Pagar(CtaCteComprobanteDto ComprobanteDto)
        {
            try
            {
                _ctaCteComprobanteServicio.Insertar(ComprobanteDto);
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.SelectMany(v => v.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current + ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}. {Environment.NewLine}"));

              
                throw new Exception($"Ocurrio un error grave al grabar la Factura. Error: {error}");
            }
        }

        public void Eliminar(long id)
        {
            _unidadDeTrabajo.CuentaCorrienteRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        //-------------------------PROVEEDOR----------------------------/

        public void Pagar(CtaCteComprobanteProveedorDto ComprobanteDto)
        {
            try
            {
                _ctaCteComprobanteServicio.InsertarProveedor(ComprobanteDto);
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.SelectMany(v => v.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current + ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}. {Environment.NewLine}"));


                throw new Exception($"Ocurrio un error grave al grabar la Factura. Error: {error}");
            }
        }

        public decimal ObtenerDeudaProveedor(long proveedorId)
        {
            var movimientos = _unidadDeTrabajo.CuentaCorrienteProveedorRepositorio.Obtener(x => !x.EstaEliminado && x.ProveedorId == proveedorId);

            return movimientos.Sum(x => x.Monto * (int)x.TipoMovimiento);
        }

        public IEnumerable<CuentaCorrienteDto> ObtenerProveedor(long proveedorId, DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda, bool estaEliminado)
        {
            var _fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);
            var _fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

            Expression<Func<MovimientoCuentaCorrienteProveedor, bool>> filtro = x => x.ProveedorId == proveedorId;


            if (soloDeuda)
            {
                filtro = filtro.And(x => x.TipoMovimiento == TipoMovimiento.Egreso);
            }
            if (!estaEliminado)
            {
                filtro = filtro.And(x => x.EstaEliminado == false);
            }

            return _unidadDeTrabajo.CuentaCorrienteProveedorRepositorio.Obtener(filtro)
                .Select(x => new CuentaCorrienteDto
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    Descripcion = x.Descripcion,
                    Monto = (x.Monto * (int)x.TipoMovimiento),
                    EstaEliminado = x.EstaEliminado

                }).OrderBy(x => x.Fecha)
                .ToList();
        }
    }
}
using Aplicacion.Constantes;
using Dominio.Entidades;
using Dominio.UnidadDeTrabajo;
using IServicio.Configuracion;
using IServicios.Comprobante.DTOs;
using IServicios.Contador;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;

namespace Servicios.Comprobante
{
    public class Presupuesto : Comprobante
    {
        protected new readonly IUnidadDeTrabajo _unidadDeTrabajo;
        protected readonly IContadorServicio _contadorServicio;
        protected readonly IConfiguracionServicio _configuracionServicio;

        public Presupuesto()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
            _contadorServicio = ObjectFactory.GetInstance<IContadorServicio>();
            _configuracionServicio = ObjectFactory.GetInstance<IConfiguracionServicio>();
        }

        public override long Insertar(ComprobanteDto comprobante)
        {
            using (var tran = new TransactionScope())
            {
                try
                {
                    int numeroComprobante = 0;

                    var config = _configuracionServicio.Obtener();
                    if (config == null)
                        throw new Exception("Ocurrio un erorr al obtener la Configuración");

                    var cajaActual = _unidadDeTrabajo.CajaRepositorio.Obtener
                    (x => x.UsuarioAperturaId == comprobante.UsuarioId
                    && x.UsuarioCierreId == null).FirstOrDefault();

                    if (cajaActual == null)
                        throw new Exception("Ocurrio un error al obtener la Caja");
                    cajaActual.DetalleCajas = new List<DetalleCaja>();

                    var facturaDto = (FacturaDto)comprobante;
                    Dominio.Entidades.Factura _facturaNueva = new Dominio.Entidades.Factura();

                    numeroComprobante = _contadorServicio.ObtenerSiguienteNumeroComprobante(comprobante.TipoComprobante);

                    _facturaNueva = new Dominio.Entidades.Factura
                    {
                        ClienteId = facturaDto.ClienteId,
                        Descuento = facturaDto.Descuento,
                        EmpleadoId = facturaDto.EmpleadoId,
                        Estado = facturaDto.Estado,
                        Fecha = facturaDto.Fecha,
                        Iva105 = facturaDto.Iva105,
                        Iva21 = facturaDto.Iva21,
                        Numero = numeroComprobante,
                        SubTotal = facturaDto.SubTotal,
                        Total = facturaDto.Total,
                        PuestoTrabajoId = facturaDto.PuestoTrabajoId,
                        TipoComprobante = facturaDto.TipoComprobante,
                        UsuarioId = facturaDto.UsuarioId,
                        DetalleComprobantes = new List<DetalleComprobante>(),
                        Movimientos = new List<Movimiento>(),
                        FormaPagos = new List<FormaPago>(),
                        EstaEliminado = false
                    };

                    foreach (var item in facturaDto.Items)
                    {
                        _facturaNueva.DetalleComprobantes.Add(new DetalleComprobante
                        {
                            Cantidad = item.Cantidad,
                            ArticuloId = item.ArticuloId,
                            Iva = item.Iva,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Codigo = item.Codigo,
                            SubTotal = item.SubTotal
                        });
                    }

                    _unidadDeTrabajo.FacturaRepositorio.Insertar(_facturaNueva);
                    _unidadDeTrabajo.Commit();

                    tran.Complete();
                    return 0;
                }
                catch (DbEntityValidationException ex)
                {
                    var error = ex.EntityValidationErrors.SelectMany(v => v.ValidationErrors)
                        .Aggregate(string.Empty,
                            (current, validationError) =>
                                current +
                                ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}. {Environment.NewLine}"
                                ));

                    tran.Dispose();
                    throw new Exception($"Ocurrio un error grave al grabar la Factura. Error: {error}");
                }
            }
        }
    }
}
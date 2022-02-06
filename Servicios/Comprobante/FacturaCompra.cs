using Aplicacion.Constantes;
using Dominio.Entidades;
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
    public class FacturaCompra : Comprobante
    {
        private readonly IContadorServicio _contadorServicio;
        private readonly IConfiguracionServicio _configuracionServicio;

        public FacturaCompra()
       : base()
        {
            _contadorServicio = ObjectFactory.GetInstance<IContadorServicio>();
            _configuracionServicio = ObjectFactory.GetInstance<IConfiguracionServicio>();
        }

        public override long Insertar(ComprobanteCompraDto comprobante)
        {
            using (var tran = new TransactionScope())
            {
                try
                {
                    int numeroComprobante = 0;

                    var config = _configuracionServicio.Obtener();
                    
                    if (config == null) throw new Exception("Ocurrio un erorr al obtener la Configuración");
                    
                    var cajaActual = _unidadDeTrabajo.CajaRepositorio.Obtener
                                (x => x.UsuarioAperturaId == comprobante.UsuarioId 
                                   && x.UsuarioCierreId == null).FirstOrDefault();

                    if (cajaActual == null) throw new Exception("Ocurrio un error al obtener la Caja");

                    cajaActual.DetalleCajas = new List<DetalleCaja>();

                    var facturaCompraDto = (ComprobanteCompraDto)comprobante;
                    Dominio.Entidades.Compra _facturaCompraNueva = new Dominio.Entidades.Compra();

                    numeroComprobante = _contadorServicio.ObtenerSiguienteNumeroComprobante(comprobante.TipoComprobante);

                    _facturaCompraNueva = new Dominio.Entidades.Compra
                    {
                        Iva105 = facturaCompraDto.Iva105,
                        Iva21 = facturaCompraDto.Iva21,
                        Iva27 = facturaCompraDto.Iva27,
                        ImpInterno = facturaCompraDto.ImpInterno,
                        PrecepcionIB = facturaCompraDto.PercepcionIB,
                        PrecepcionIva = facturaCompraDto.PercepcionIva,
                        PrecepcionPyP = facturaCompraDto.PercepcionPyP,
                        PrecepcionTemp = facturaCompraDto.PercepcionTemp,

                        ArticuloEntregado = facturaCompraDto.ArticuloEntregado,
                        EstadoFactura = facturaCompraDto.Estado,
                        EmpleadoId = facturaCompraDto.EmpleadoId,
                        ProveedorId = facturaCompraDto.ProveedorId,
                        Fecha = facturaCompraDto.Fecha,
                        Numero = numeroComprobante,
                        Total = facturaCompraDto.Total,
                        TipoComprobante = facturaCompraDto.TipoComprobante,
                        UsuarioId = facturaCompraDto.UsuarioId,
                        FechaEntrega = facturaCompraDto.FechaEntrega,
                        DetalleComprobantes = new List<DetalleComprobante>(),
                        Movimientos = new List<Movimiento>(),
                        FormaPagos = new List<FormaPago>(),
                        EstaEliminado = false

                    };

                    if (facturaCompraDto.ArticuloEntregado == true)
                    {
                        foreach (var item in facturaCompraDto.Items)
                        {
                            var stockActual = _unidadDeTrabajo.StockRepositorio.Obtener(x =>
                            x.ArticuloId == item.ArticuloId && x.DepositoId == config.DepositoVentaId).FirstOrDefault();

                            if (stockActual == null)
                                throw new Exception("Ocurrio un error al obtener el Stock del Articulo");

                            stockActual.Cantidad += item.Cantidad;

                            _unidadDeTrabajo.StockRepositorio.Modificar(stockActual);
                            
                            _facturaCompraNueva.DetalleComprobantes.Add(new DetalleComprobante
                            {
                                Cantidad = item.Cantidad,
                                ArticuloId = item.ArticuloId,
                                Iva = item.Iva,
                                Descripcion = item.Descripcion,
                                Precio = item.Precio,
                                Codigo = item.Codigo,
                                SubTotal = item.SubTotal,

                            });
                        }
                    }
                    else
                    {
                        foreach (var item in facturaCompraDto.Items)
                        {
                            _facturaCompraNueva.DetalleComprobantes.Add(new DetalleComprobante
                            {
                                Cantidad = item.Cantidad,
                                ArticuloId = item.ArticuloId,
                                Iva = item.Iva,
                                Descripcion = item.Descripcion,
                                Precio = item.Precio,
                                Codigo = item.Codigo,
                                SubTotal = item.SubTotal,

                            });
                        }
                    }

                    _facturaCompraNueva.Movimientos.Add(new Movimiento
                    {
                        ComprobanteId = _facturaCompraNueva.Id,
                        CajaId = cajaActual.Id,
                        Fecha = facturaCompraDto.Fecha,
                        Monto = facturaCompraDto.Total,
                        UsuarioId = facturaCompraDto.UsuarioId,
                        TipoMovimiento = TipoMovimiento.Ingreso,
                        Descripcion = $"Fa- { facturaCompraDto.TipoComprobante.ToString() }-Nro { numeroComprobante }",
                        EstaEliminado = false


                    });

                    foreach (var fp in facturaCompraDto.FormasDePagos)
                    {
                        switch (fp.TipoPago)
                        {
                            case TipoPago.Efectivo:
                                _facturaCompraNueva.FormaPagos.Add(new FormaPago
                                {
                                    ComprobanteId = _facturaCompraNueva.Id,
                                    Monto = fp.Monto,
                                    TipoPago = fp.TipoPago,
                                    EstaEliminado = false
                                });
                                cajaActual.TotalSalidaEfectivo += fp.Monto;
                                cajaActual.DetalleCajas.Add(new DetalleCaja
                                {
                                    Monto = fp.Monto,
                                    TipoPago = TipoPago.Efectivo
                                });
                                break;

                            case TipoPago.Tarjeta:
                                var fpTarjeta = (FormaPagoTarjetaDto)fp;
                                _facturaCompraNueva.FormaPagos.Add(new FormaPagoTarjeta
                                {
                                    ComprobanteId = _facturaCompraNueva.Id,
                                    Monto = fpTarjeta.Monto,
                                    TipoPago = fpTarjeta.TipoPago,
                                    CantidadCuotas = fpTarjeta.CantidadCuotas,
                                    CuponPago = fpTarjeta.CuponPago,
                                    NumeroTarjeta = fpTarjeta.NumeroTarjeta,
                                    TarjetaId = fpTarjeta.TarjetaId,
                                    EstaEliminado = false
                                });
                                cajaActual.TotalSalidaTarjeta += fpTarjeta.Monto;
                                cajaActual.DetalleCajas.Add(new DetalleCaja
                                {
                                    CajaId = cajaActual.Id,
                                    Monto = fpTarjeta.Monto,
                                    TipoPago = TipoPago.TarjetaProveedor
                                });
                                break;

                            case TipoPago.Cheque:

                                var fpCheque = (FormaPagoChequeDto)fp;
                                _facturaCompraNueva.FormaPagos.Add(new FormaPagoCheque
                                {
                                    ComprobanteId = _facturaCompraNueva.Id,
                                    Cheque = new Cheque
                                    {
                                        BancoId = fpCheque.BancoId,
                                        ClienteId = fpCheque.ClienteId,
                                        FechaVencimiento =
                                        fpCheque.FechaVencimiento,
                                        Numero = fpCheque.Numero,
                                        EstaRechazado = false,
                                        EstaEliminado = false
                                    },
                                    Monto = fpCheque.Monto,
                                    TipoPago = TipoPago.ChequeProveedor
                                });
                                cajaActual.TotalSalidaCheque += fpCheque.Monto;
                                cajaActual.DetalleCajas.Add(new DetalleCaja
                                {
                                    Monto = fpCheque.Monto,
                                    TipoPago = TipoPago.ChequeProveedor
                                });
                                break;

                            case TipoPago.CtaCte:
                                var fpCtaCTe = (FormaPagoCtaCteDto)fp;
                                _facturaCompraNueva.FormaPagos.Add(new FormaPagoCtaCte
                                {

                                    Monto = fpCtaCTe.Monto,
                                    TipoPago = TipoPago.CtaCteProveedor,
                                    ClienteId = fpCtaCTe.ClienteId,
                                    EstaEliminado = false
                                });
                                _facturaCompraNueva.Movimientos.Add(new MovimientoCuentaCorrienteProveedor
                                {
                                    ComprobanteId = _facturaCompraNueva.Id,
                                    CajaId = cajaActual.Id,
                                    Fecha = facturaCompraDto.Fecha,
                                    Monto = fpCtaCTe.Monto,
                                    UsuarioId = facturaCompraDto.UsuarioId,
                                    TipoMovimiento =
                                    TipoMovimiento.Egreso,
                                    Descripcion = $"Fa- { facturaCompraDto.TipoComprobante.ToString() }-Nro { numeroComprobante }",       
                                    EstaEliminado = false
                                });

                                cajaActual.TotalSalidaCtaCte += fpCtaCTe.Monto;

                                cajaActual.DetalleCajas.Add(new DetalleCaja
                                {
                                    CajaId = cajaActual.Id,
                                    Monto = fpCtaCTe.Monto,
                                    TipoPago = TipoPago.CtaCteProveedor
                                });

                                break;
                        }
                    }
                    _unidadDeTrabajo.CajaRepositorio.Modificar(cajaActual);
                
                    _unidadDeTrabajo.FacturaCompraRepositorio.Insertar(_facturaCompraNueva);
           
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
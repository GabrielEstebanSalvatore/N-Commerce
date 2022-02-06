using Aplicacion.Constantes;
using IServicios.Comprobante.DTOs;
using IServicios.CuentaCorriente;
using IServicios.Proveedor.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Proveedor
{
    public partial class _00037_ProveedorPagoCtaCte : FormBase
    {
        private readonly ProveedorDto _proveedor;
        private readonly ICuentaCorrienteServicio _cuentaCorrienteServicio;
        public bool RealizoPago { get; internal set; }

        public _00037_ProveedorPagoCtaCte(ProveedorDto proveedorDto)
        {
            InitializeComponent();
            _proveedor = proveedorDto;
            _cuentaCorrienteServicio = ObjectFactory.GetInstance<ICuentaCorrienteServicio>();
            RealizoPago = false;
        }

        private void _00037_ProveedorPagoCtaCte_Load(object sender, System.EventArgs e)
        {
            if (_proveedor != null)
            {
                //TODO DEUDA PROVEEDOR

                var deuda = _cuentaCorrienteServicio.ObtenerDeudaProveedor(_proveedor.Id);
                nudMontoDeuda.Value = deuda >= 0 ? 0 : (deuda * -1);
                nudMontoDeuda.Select(0, nudMontoPagar.Text.Length);
                nudMontoPagar.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, System.EventArgs e)
        {
            nudMontoPagar.Value = 0;
            nudMontoDeuda.Select(0, nudMontoPagar.Text.Length);
            nudMontoPagar.Focus();
        }

        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            RealizoPago = false;
            Close();
        }

        private void btnPagar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (nudMontoPagar.Value > 0)
                {
                    if (nudMontoPagar.Value > nudMontoDeuda.Value)
                    {
                        var mensaje = "El monto que esta pagando es MAYOR al monto adeudado."
                            + Environment.NewLine
                            + "Desea realizar el pago?";

                        if (MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            return;

                    }

                    var comprobanteNuevo = new CtaCteComprobanteProveedorDto
                    {
                        ProveedorId = _proveedor.Id,
                        Descuento = 0,
                        SubTotal = nudMontoPagar.Value,
                        Total = nudMontoPagar.Value,
                        EmpleadoId = Identidad.EmpleadoId,
                        UsuarioId = Identidad.UsuarioId,
                        Fecha = DateTime.Now,
                        Iva105 = 0,
                        Iva21 = 0,
                        TipoComprobante = TipoComprobante.CuentaCorriente,
                        FormasDePagos = new List<FormaPagoDto>(),
                        Items = new List<DetalleComprobanteDto>(),
                        Eliminado = false
                    };
                    comprobanteNuevo.FormasDePagos.Add(new FormaPagoCtaCteDto
                    {
                       
                        ProveedorId = _proveedor.Id,
                        Monto = nudMontoPagar.Value,
                        TipoPago = TipoPago.CtaCte,
                        Eliminado = false
                    });


                    _cuentaCorrienteServicio.Pagar(comprobanteNuevo);
                    MessageBox.Show("Los datos se grabaron correctamente");
                    RealizoPago = true;
                    Close();


                }
                else
                {
                    MessageBox.Show("Por favor ingrese un monto mayor a 0");
                    nudMontoDeuda.Select(0, nudMontoPagar.Text.Length);
                    nudMontoPagar.Focus();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.SelectMany(v => v.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current +
                            ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}. {Environment.NewLine}"
                            ));

                throw new Exception($"Ocurrio un error grave al grabar la Factura. Error: {error}");
            }
        }
    }
}

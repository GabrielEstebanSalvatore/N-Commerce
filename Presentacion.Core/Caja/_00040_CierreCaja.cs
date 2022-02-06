using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using IServicios.Gasto;
using IServicios.Gasto.DTOs;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Caja
{
    public partial class _00040_CierreCaja : FormBase
    {
        private readonly long _cajaId;
        private readonly ICajaServicio _cajaServicio;
        private readonly IGastoServicio _gastoServicio;
        private CajaDto _caja;
        private List<GastoDto> _gastos;
        decimal gastos;
        public _00040_CierreCaja(long cajaId)
        {
            InitializeComponent();

            _cajaId = cajaId;
            _cajaServicio = ObjectFactory.GetInstance<ICajaServicio>();
            _gastoServicio = ObjectFactory.GetInstance<IGastoServicio>();
            CargarDatos(cajaId);

            this.Width = 350;
        }

        private void CargarDatos(long cajaId)
        {
            _caja = _cajaServicio.Obtener(cajaId);
            _gastos = (List< GastoDto >) _gastoServicio.Obtener(string.Empty);

            if (_caja == null )
            {
                MessageBox.Show("Ocurrió un error al obtener la caja");
            }

            txtCajaInicial.Text = _caja.MontoAperturaStr;
            lblFechaApertura.Text = _caja.FechaAperturaStr;

            var efectivo = _caja.Detalles
                .Where(x => x.TipoPago == TipoPago.Efectivo)
                .Sum(x => x.Monto)
                .ToString("C");

            var cheque = _caja.Detalles
                .Where(x => x.TipoPago == TipoPago.Cheque)
                .Sum(x => x.Monto)
                .ToString("C");

            var tarjeta = _caja.Detalles
                .Where(x => x.TipoPago == TipoPago.Tarjeta)
                .Sum(x => x.Monto)
                .ToString("C");

            var ctaCte = _caja.Detalles
                .Where(x => x.TipoPago == TipoPago.CtaCte)
                .Sum(x => x.Monto)
                .ToString("C");

            //-------------------------/

            foreach (var item in _gastos)
            {
                gastos += item.Importe;
            }
           

            txtVentasEfectivo.Text = efectivo;
            txtChequeVentas.Text = cheque;
            txtTarjetaVentas.Text = tarjeta;
            txtCtaCteVentas.Text = ctaCte;

            textBox10.Text = gastos.ToString("C");
            nudMontoCierre.Focus();

        }

        

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            var cerrarCaja = _cajaServicio.Obtener(_caja.Id);

            if (cerrarCaja == null) throw new Exception("No se pudo cerra la caja");



            //cerrarCaja.MontoCierre = nudMontoCierre.Value == 0 ? 0 : Convert.ToDecimal(nudMontoCierre.Value, CultureInfo.InvariantCulture);

            _cajaServicio.CerrarCaja(cerrarCaja);

            LimpiarControles(this);

            txtComprasEfectivo.Text = "";
            txtChequeCompras.Text = "";
            txtTarjetaCompras.Text = "";
            txtCtaCteCompra.Text = "";

        }

        private void btnVerDetalleVenta_Click(object sender, EventArgs e)
        {
            var fComprobantesCaja = new CompobantesCaja(_caja.Comprobantes);
            fComprobantesCaja.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

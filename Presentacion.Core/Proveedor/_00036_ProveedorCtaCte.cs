using IServicios.CuentaCorriente;
using IServicios.CuentaCorriente.DTOs;
using IServicios.Proveedor.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Proveedor
{
    public partial class _00036_ProveedorCtaCte : FormBase
    {
        private ProveedorDto _proveedorSeleccionado;
        private readonly ICuentaCorrienteServicio _cuentaCorrienteServicio;
        CuentaCorrienteDto _cuentaCorrienteSeleccionada;
        protected long? entidadId;

        public _00036_ProveedorCtaCte(ICuentaCorrienteServicio cuentaCorrienteServicio)
        {
            InitializeComponent();

            _cuentaCorrienteServicio = cuentaCorrienteServicio;
            _proveedorSeleccionado = null;
            dgvGrilla.DataSource = new List<CuentaCorrienteDto>();
            FormatearGrilla(dgvGrilla);
        }

        private void btnBuscarProveedor_Click(object sender, System.EventArgs e)
        {
            var fProveedorLookUp = ObjectFactory.GetInstance<ProveedorLookUp>();
            fProveedorLookUp.ShowDialog();

            if (fProveedorLookUp.EntidadSeleccionada != null)
            {
                _proveedorSeleccionado = (ProveedorDto)fProveedorLookUp.EntidadSeleccionada;

                txtRazonSocial.Text = _proveedorSeleccionado.RazonSocial;
                txtCelular.Text = _proveedorSeleccionado.Telefono;
                txtDni.Text = _proveedorSeleccionado.CUIT;

                CargarDatos();
            }
            else
            {
                txtRazonSocial.Clear();
                txtCelular.Clear();
                txtDni.Clear();
                _proveedorSeleccionado = null;

                dgvGrilla.DataSource = new List<CuentaCorrienteDto>();

                var fechaInicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                dtpFechaDesde.Value = fechaInicioMes;

                txtTotal.Text = 0.ToString("C");
            }
        }

        private void CargarDatos()
        {
            var resultado = _cuentaCorrienteServicio
            .ObtenerProveedor(_proveedorSeleccionado.Id, dtpFechaDesde.Value, dtpFechaHasta.Value, false, false);

            dgvGrilla.DataSource = resultado;

            FormatearGrilla(dgvGrilla);
            txtTotal.Text = resultado.Sum(x => x.Monto) >= 0
                ? resultado.Sum(x => x.Monto).ToString("C")
                : (resultado.Sum(x => x.Monto) * -1).ToString("C");

            txtTotal.BackColor = resultado.Sum(x => x.Monto) >= 0
            ? Color.DarkGreen
            : Color.DarkRed;
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";

            dgv.Columns["FechaStr"].Visible = true;
            dgv.Columns["FechaStr"].Width = 100;
            dgv.Columns["FechaStr"].HeaderText = "Fecha";

            dgv.Columns["HoraStr"].Visible = true;
            dgv.Columns["HoraStr"].Width = 100;
            dgv.Columns["HoraStr"].HeaderText = "Hora";

            dgv.Columns["MontoStr"].Visible = true;
            dgv.Columns["MontoStr"].Width = 100;
            dgv.Columns["MontoStr"].HeaderText = "Monto";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado == null) return;

            CargarDatos();
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado == null) return;

            dtpFechaDesde.MinDate = dtpFechaHasta.Value;
            if (dtpFechaDesde.Value > dtpFechaHasta.Value)
            {
                dtpFechaHasta.Value = dtpFechaDesde.Value;
            }
            CargarDatos();
        }

        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado == null) return;

            CargarDatos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado == null) return;

            CargarDatos();
        }

        private void btnRealizarPago_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado != null)
            {
                var fPagoDeuda = new _00037_ProveedorPagoCtaCte(_proveedorSeleccionado);
                fPagoDeuda.ShowDialog();

                if (fPagoDeuda.RealizoPago)
                {
                    CargarDatos();
                }
            }
        }


        private void btnCancelarPago_Click(object sender, EventArgs e)
        {
            if (_cuentaCorrienteSeleccionada == null)
            {
                MessageBox.Show("Ningun pago seleccionado");
                return;
            }

            try
            {
                _cuentaCorrienteServicio.Eliminar(_cuentaCorrienteSeleccionada.Id);
                MessageBox.Show("Pago cancelado");
            }
            catch
            {
                MessageBox.Show("No se pudo cancelar el pago");

                return;
            }
        }
        
        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            //entidadId = (long)dgvGrilla["Id", e.RowIndex].Value;

            _cuentaCorrienteSeleccionada = (CuentaCorrienteDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;
        }
    }
}

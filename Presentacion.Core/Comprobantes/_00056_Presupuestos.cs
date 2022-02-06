using Aplicacion.Constantes;
using IServicio.Configuracion;
using IServicio.Configuracion.DTOs;
using IServicio.ListaPrecio;
using IServicios.Comprobante;
using IServicios.Comprobante.DTOs;
using Presentacion.Core.FormaPago;
using PresentacionBase.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00056_Presupuestos : FormBase
    {
        private ComprobantePendienteDto comprobanteSeleccionado;
        private ConfiguracionDto _configuracion;

        private readonly IFacturaServicio _facturaServicio;
       
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IConfiguracionServicio _configuracionServicio;
       

        public _00056_Presupuestos(IFacturaServicio facturaServicio,
            IListaPrecioServicio listaPrecioServicio,
            IConfiguracionServicio configuracionServicio)
        {
            InitializeComponent();
            _facturaServicio = facturaServicio;
            _listaPrecioServicio = listaPrecioServicio;
            _configuracionServicio = configuracionServicio;

            _configuracion = _configuracionServicio.Obtener();

            comprobanteSeleccionado = null;
            cargarGrilla();

            PoblarComboBox(cmbTipoComprobante, Enum.GetValues(typeof(TipoComprobante)));
            cmbTipoComprobante.SelectedItem = TipoComprobante.Presupuesto;

          

           
        }

        private void cargarGrilla()
        {
            dgvGrillaPresupuestos.DataSource = _facturaServicio.ObtenerPresupuestos();

            FormatearGrilla(dgvGrillaPresupuestos);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Numero"].Visible = true;
            dgv.Columns["Numero"].Width = 80;
            dgv.Columns["Numero"].HeaderText = "Nro Comprobante";
            dgv.Columns["Numero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["ClienteApyNom"].Visible = true;
            dgv.Columns["ClienteApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ClienteApyNom"].HeaderText = "Cliente";

            dgv.Columns["Fecha"].Visible = true;
            dgv.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Fecha"].HeaderText = "Fecha";
           
            dgv.Columns["TipoComprobante"].Visible = true;
            dgv.Columns["TipoComprobante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["TipoComprobante"].HeaderText = "Tipo de Comprobante";


            dgv.Columns["MontoPagarStr"].Visible = true;
            dgv.Columns["MontoPagarStr"].Width = 150;
            dgv.Columns["MontoPagarStr"].HeaderText = "Total";
            dgv.Columns["MontoPagarStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void FormatearGrillaDetalle(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Artículo";

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 60;
            dgv.Columns["PrecioStr"].HeaderText = @"Precio";

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 60;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvGrillaPresupuestos_DoubleClick(object sender, EventArgs e)
        {
            comprobanteSeleccionado.TipoComprobante = (TipoComprobante)cmbTipoComprobante.SelectedValue;

            if (comprobanteSeleccionado.TipoComprobante == TipoComprobante.Presupuesto)
            {
                MessageBox.Show("Por favor cambie el tipo de comprobante");
                return;
            }

            var fFormaDePago = new _00044_FormaPago(comprobanteSeleccionado);

            fFormaDePago.ShowDialog();

            if (fFormaDePago.RealizoVenta)
            {
                MessageBox.Show("Los datos se grabaron correctamente");

                cargarGrilla();
            }

            cmbTipoComprobante.SelectedItem = TipoComprobante.Presupuesto;
            //var fFormaDePago = new _00050_Venta(comprobanteSeleccionado);
            //fFormaDePago.ShowDialog();


            //MessageBox.Show("Los datos se grabaron correctamente");
        }

        private void dgvGrillaPresupuestos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaPresupuestos.RowCount <= 0)
            {
                comprobanteSeleccionado = null;
                return;
            }

            comprobanteSeleccionado =
                (ComprobantePendienteDto)dgvGrillaPresupuestos.Rows[e.RowIndex].DataBoundItem;

            if (comprobanteSeleccionado != null)
            {
                
                dgvDetalles.DataSource = null;
                dgvDetalles.DataSource = comprobanteSeleccionado.Items.ToList();

                FormatearGrillaDetalle(dgvDetalles);
            }
        }

        private void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            FormatearGrilla(dgv);
        }

        private void _00056_Presupuestos_Load(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrillaPresupuestos, string.Empty);
        }
    }
}

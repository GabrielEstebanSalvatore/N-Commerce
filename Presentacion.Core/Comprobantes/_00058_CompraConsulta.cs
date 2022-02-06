using Aplicacion.Constantes;
using Dominio.UnidadDeTrabajo;
using IServicio.Configuracion;
using IServicios.Compras;
using IServicios.Compras.DTOs;

using PresentacionBase.Formularios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00058_CompraConsulta : FormBase
    {
        FacturaCompraDto EntidadSeleccionada;
        List<FacturaCompraDto> _facturas, _facturasDelDia = null;
        bool bandera = true;

        private long? entidadId;
        private readonly IComprasServicio _compraServicio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IConfiguracionServicio _configuracionServicio;

        public _00058_CompraConsulta(IComprasServicio comprasServicio,
            IUnidadDeTrabajo unidadDeTrabajo,
            IConfiguracionServicio configuracionServicio)
        {
            InitializeComponent();
            _compraServicio = comprasServicio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _configuracionServicio = configuracionServicio;
            EntidadSeleccionada = null;
            _facturas = new List<FacturaCompraDto>();
            _facturasDelDia = new List<FacturaCompraDto>();
        }

        private void _00058_CompraConsulta_Load(object sender, System.EventArgs e)
        {
            dgvGrillaComprasPendiente.DataSource = new FacturaCompraDto();
            dgvGrillaComprasEntrega.DataSource = new FacturaCompraDto();

            ActualizarDatos(dgvGrillaComprasPendiente, string.Empty);
            ActualizarDatos(dgvGrillaComprasEntrega, string.Empty);
        }

        private void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {

            if (dgv.Name == dgvGrillaComprasPendiente.Name)
            {

                var fechaActual = DateTime.Today.ToShortDateString();

                var comprobantes = _compraServicio
                    .ObtenerCompras(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar  : string.Empty,true,false);

                foreach (var item in comprobantes)
                {
                    var fechaComprobante = item.FechaEntrega.ToShortDateString();

                    if (fechaComprobante != fechaActual)
                    {
                        _facturas.Add(item);
                    }
                }

                if (comprobantes.Any() )
                {
                    dgvGrillaComprasPendiente.DataSource = _facturas;

                    FormatearGrilla(dgv);
                }

            }

            if (dgv.Name == dgvGrillaComprasEntrega.Name)
            {
                var fechaActual = DateTime.Today.ToShortDateString();

                var comprobantes = _compraServicio
                    .ObtenerCompras(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, true, false);

                foreach (var item in comprobantes)
                {
                    var fechaComprobante = item.FechaEntrega.ToShortDateString();

                    if (fechaComprobante == fechaActual)
                    {
                        _facturasDelDia.Add(item);
                    }
                }

                if (_facturas != null)
                {
                    dgvGrillaComprasEntrega.DataSource = _facturasDelDia;

                    FormatearGrilla(dgv);
                }
            }
          
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["RazonSocial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["RazonSocial"].HeaderText = "Razon Social";
            dgv.Columns["RazonSocial"].Visible = true;

            dgv.Columns["EstadoFactura"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["EstadoFactura"].HeaderText = "Estado";
            dgv.Columns["EstadoFactura"].Visible = true;

            dgv.Columns["CUIT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["CUIT"].HeaderText = "CUIT";
            dgv.Columns["CUIT"].Visible = true;

            dgv.Columns["FechaStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["FechaStr"].HeaderText = "Fecha Entrega";
            dgv.Columns["FechaStr"].Visible = true;

            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnArticulosRecibidos_Click(object sender, EventArgs e)
        {
            try
            {
                var config = _configuracionServicio.Obtener();
                if (config == null) throw new Exception("Ocurrio un erorr al obtener la Configuración");

                foreach (var item in EntidadSeleccionada.Items)
                {
                    if (EntidadSeleccionada.ArticuloEntregado == false)
                    {
                        var stockActual = _unidadDeTrabajo.StockRepositorio.Obtener(x =>
                        x.ArticuloId == item.ArticuloId && x.DepositoId == config.DepositoVentaId).FirstOrDefault();

                        if (stockActual == null)
                            throw new Exception("Ocurrio un error al obtener el Stock del Articulo");

                        stockActual.Cantidad += item.Cantidad;

                        _unidadDeTrabajo.StockRepositorio.Modificar(stockActual);
                    }
                }

                var comprobante = _compraServicio.Obtener(EntidadSeleccionada.Id);

                comprobante.ArticuloEntregado = true;

                _compraServicio. Modificar(comprobante);




            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.SelectMany(v => v.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current + ($"Property: " +
                            $"{validationError.PropertyName} Error: {validationError.ErrorMessage}. {Environment.NewLine}"));

                throw new Exception($"Ocurrio un error grave al grabar la Factura. Error: {error}");
            }
        }

        private void dgvGrillaComprasEntrega_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            
            if (dgvGrillaComprasEntrega.RowCount <= 0) return;

            entidadId = (long)dgvGrillaComprasEntrega["Id", e.RowIndex].Value;

            // Obtener el Objeto completo seleccionado
            EntidadSeleccionada = (FacturaCompraDto)dgvGrillaComprasEntrega.Rows[e.RowIndex].DataBoundItem;
        }

        private void dgvGrillaComprasPendiente_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaComprasPendiente.RowCount <= 0) return;

            entidadId = (long)dgvGrillaComprasPendiente["Id", e.RowIndex].Value;

            // Obtener el Objeto completo seleccionado
            EntidadSeleccionada = (FacturaCompraDto)dgvGrillaComprasPendiente.Rows[e.RowIndex].DataBoundItem;

            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            _facturas = null;
            _facturasDelDia = null;

            this.Close();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (EntidadSeleccionada == null)
            {
                MessageBox.Show("Seleccione una factura para ver su detalle");
                return;
            }
            var fFormaDePago = new _00059_CompraConsultaDetalle(EntidadSeleccionada);
            fFormaDePago.ShowDialog();

        }
    }
}

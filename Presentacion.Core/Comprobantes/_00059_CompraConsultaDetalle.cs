using Dominio.UnidadDeTrabajo;
using IServicios.Compras.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00059_CompraConsultaDetalle : FormBase
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
   

        public _00059_CompraConsultaDetalle(FacturaCompraDto factura)
        {
            InitializeComponent();

            lblCuit.Text = "CUIT: " + factura.CUIT;
            lblFechaDeEntrega.Text = "Fecha de Entrega: " + factura.FechaEntrega.ToString();
            lblRazonSocial.Text = "Razón Social: " + factura.RazonSocial;

            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
       
            dgvDetallesCompra.DataSource = factura.Items;

            FormatearGrilla(dgvDetallesCompra);


        }
        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Codigo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Codigo"].HeaderText = "Codigo Artículo";
            dgv.Columns["Codigo"].Visible = true;

            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Descripcion";
            dgv.Columns["Descripcion"].Visible = true;

            dgv.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
            dgv.Columns["Cantidad"].Visible = true;

            dgv.Columns["Precio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Precio"].HeaderText = "Precio";
            dgv.Columns["Precio"].Visible = true;

            dgv.Columns["SubTotal"].HeaderText = "SubTotal";
            dgv.Columns["SubTotal"].Visible = true;
            dgv.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

    }
}

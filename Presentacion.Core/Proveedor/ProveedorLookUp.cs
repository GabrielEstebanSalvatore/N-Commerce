using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IServicios.Proveedor;
using IServicios.Proveedor.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Proveedor
{
    public partial class ProveedorLookUp : FormLookUp
    {
        private readonly IProveedorServicio _proveedorServicio;
        public ProveedorLookUp(IProveedorServicio proveedorServicio)
        {
            InitializeComponent();
            _proveedorServicio = proveedorServicio;
            EntidadSeleccionada = null;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            var proveedores = (List<ProveedorDto>)_proveedorServicio
             .Obtener( !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, false);

            dgv.DataSource = proveedores;

            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["RazonSocial"].Width = 100;
            dgv.Columns["RazonSocial"].HeaderText = "RazonSocial";
            dgv.Columns["RazonSocial"].Visible = true;
            dgv.Columns["RazonSocial"].DisplayIndex = 0;

            dgv.Columns["CUIT"].Width = 100;
            dgv.Columns["CUIT"].HeaderText = "CUIT";
            dgv.Columns["CUIT"].Visible = true;
            dgv.Columns["CUIT"].DisplayIndex = 1;

            dgv.Columns["Telefono"].Width = 100;
            dgv.Columns["Telefono"].HeaderText = "Teléfono";
            dgv.Columns["Telefono"].Visible = true;
            dgv.Columns["Telefono"].DisplayIndex = 2;

            dgv.Columns["Direccion"].Width = 100;
            dgv.Columns["Direccion"].HeaderText = "Dirección";
            dgv.Columns["Direccion"].Visible = true;
            dgv.Columns["Direccion"].DisplayIndex = 3;
            dgv.Columns["Direccion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["Mail"].Width = 70;
            dgv.Columns["Mail"].HeaderText = "Mail";
            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].DisplayIndex = 4;
            dgv.Columns["Mail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public override void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            base.dgvGrilla_RowEnter(sender, e);

            var clienteSeleccionado = (ProveedorDto)_proveedorServicio.Obtener((long)entidadId);

            txtLegajo.Visible = false;
            lblLegajo.Visible = false;
            txtTelefono.Visible = false;
            lblTelefono.Visible = false;

            lblApellido.Text = "Razon Social";
            txtApellido.Text = clienteSeleccionado.RazonSocial;

            lblNombre.Text = "CUIT";
            txtNombre.Text = clienteSeleccionado.CUIT;
            
            lblDni.Text = "Dir.";
            txtDni.Text = clienteSeleccionado.Direccion;
            
            lblDomicilio.Text = "Teléfono";
            txtDomicilio.Text = clienteSeleccionado.Telefono;
            
            txtMail.Text = clienteSeleccionado.Mail;
            txtProvincia.Text = clienteSeleccionado.Provincia;
            txtDepartamento.Text = clienteSeleccionado.Departamento;
            txtLocalidad.Text = clienteSeleccionado.Localidad;



        }
    }
}

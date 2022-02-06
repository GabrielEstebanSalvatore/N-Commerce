using System.Windows.Forms;
using IServicios.Proveedor;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Proveedor
{
    public partial class _00015_Proveedor : FormConsulta
    {
        private readonly IProveedorServicio _proveedorServicio;
        public _00015_Proveedor(IProveedorServicio proveedorServicio)
        {
            InitializeComponent();
            _proveedorServicio = proveedorServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _proveedorServicio.Obtener(cadenaBuscar);
            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["RazonSocial"].Visible = true;
            dgv.Columns["RazonSocial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["RazonSocial"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["RazonSocial"].HeaderText = "RazonSocial";
            dgv.Columns["RazonSocial"].DisplayIndex = 1;

            dgv.Columns["Direccion"].Visible = true;
            dgv.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Direccion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Direccion"].HeaderText = @"Direccón";
            dgv.Columns["Direccion"].DisplayIndex = 2;

            dgv.Columns["CUIT"].Visible = true;
            dgv.Columns["CUIT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["CUIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["CUIT"].HeaderText = @"CUIT";
            dgv.Columns["CUIT"].DisplayIndex = 3;

            dgv.Columns["Telefono"].Visible = true;
            dgv.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Telefono"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Telefono"].HeaderText = @"Telefono";
            dgv.Columns["Telefono"].DisplayIndex = 4;

            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Mail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Mail"].HeaderText = @"Mail";
            dgv.Columns["Mail"].DisplayIndex = 5;

            dgv.Columns["Eliminado"].Visible = true;
            dgv.Columns["Eliminado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Eliminado"].Width = 100;
            dgv.Columns["Eliminado"].HeaderText = "Eliminado";
            dgv.Columns["Eliminado"].DisplayIndex = 6;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var form = new _00016_Abm_Proveedor(tipoOperacion, id);
            form.ShowDialog();
            btnActualizar.PerformClick();
            return form.RealizoAlgunaOperacion;
        }


    }
}

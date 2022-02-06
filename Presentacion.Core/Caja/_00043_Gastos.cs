using System.Windows.Forms;
using IServicios.Gasto;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Caja
{
    public partial class _00043_Gastos : FormConsulta
    {
        private readonly IGastoServicio _gastoServicio;
        public _00043_Gastos(IGastoServicio gastoServicio)
        {
            InitializeComponent();
            _gastoServicio = gastoServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _gastoServicio.Obtener(cadenaBuscar);
            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Gastos";

            dgv.Columns["ConceptoGastoStr"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ConceptoGastoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ConceptoGastoStr"].HeaderText = "Concepto";

            dgv.Columns["Importe"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Importe"].HeaderText = "Importe";

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var form = new _00044_Abm_Gastos(tipoOperacion, id);
            form.ShowDialog();

            btnActualizar.PerformClick();

            return form.RealizoAlgunaOperacion;
        }
    }
}

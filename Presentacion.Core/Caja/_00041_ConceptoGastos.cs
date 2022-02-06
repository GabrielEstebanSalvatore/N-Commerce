using System.Windows.Forms;
using IServicios.ConceptoGastos;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Caja
{
    public partial class _00041_ConceptoGastos : FormConsulta
    {
        private readonly IConceptoGastosServicio _conceptoGastosServicio;
        public _00041_ConceptoGastos(IConceptoGastosServicio conceptoGastosServicio)
        {
            InitializeComponent();

            _conceptoGastosServicio = conceptoGastosServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _conceptoGastosServicio.Obtener(cadenaBuscar);
            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = "Concepto de Gastos";


            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var form = new _00042_Abm_ConceptoGastos(tipoOperacion, id);
            form.ShowDialog();

            btnActualizar.PerformClick();

            return form.RealizoAlgunaOperacion;
        }
    }
}

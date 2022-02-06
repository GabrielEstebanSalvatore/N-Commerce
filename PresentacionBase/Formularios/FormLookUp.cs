using System;
using System.Windows.Forms;

namespace PresentacionBase.Formularios
{
    public partial class FormLookUp : FormBase
    {
        protected long? entidadId;
        public object EntidadSeleccionada;

        public FormLookUp()
        {
            InitializeComponent();

            entidadId = null;
        }

        private void FormLookUp_Load(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        public virtual void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            FormatearGrilla(dgv);
        }

        public virtual void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            entidadId = (long)dgvGrilla["Id", e.RowIndex].Value;

            // Obtener el Objeto completo seleccionado
            EntidadSeleccionada = dgvGrilla.Rows[e.RowIndex].DataBoundItem;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        public virtual void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public virtual void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        public virtual void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;

namespace PresentacionBase.Formularios
{
    public partial class FormConsulta : FormBase
    {
        protected long? entidadId;
        protected object EntidadSeleccionada;

        public FormConsulta()
        {
            InitializeComponent();

            entidadId = null;

            AsignarEvento_EnterLeave(this);
        }

        private void FormConsulta_Load(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        public virtual void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            FormatearGrilla(dgv);
        }

        public virtual bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            return false;
        }
                
        public virtual void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            entidadId = (long) dgvGrilla["Id", e.RowIndex].Value;

            // Obtener el Objeto completo seleccionado
            EntidadSeleccionada = dgvGrilla.Rows[e.RowIndex].DataBoundItem;

            
            btnModificar.Enabled = !(bool)dgvGrilla["Eliminado", e.RowIndex].Value;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (entidadId.HasValue) // Pregunto si tiene un valor
                {
                    if (EjecutarComando(TipoOperacion.Eliminar, entidadId))
                    {
                        ActualizarDatos(dgvGrilla, string.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un registro");
                }
            }
            else
            {
                MessageBox.Show("No hay registros Cargados");
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (entidadId.HasValue) // Pregunto si tiene un valor
                {
                    if (EjecutarComando(TipoOperacion.Modificar, entidadId))
                    {
                        ActualizarDatos(dgvGrilla, string.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un registro");
                }
            }
            else
            {
                MessageBox.Show("No hay registros Cargados");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (EjecutarComando(TipoOperacion.Nuevo))
            {

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Quita Ruido molesto enter
                btnBuscar.PerformClick(); // Hago un Click por Codigo
            }
        }
    }
}

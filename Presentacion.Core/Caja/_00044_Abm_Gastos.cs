using IServicios.ConceptoGastos;
using IServicios.Gasto;
using IServicios.Gasto.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;
using static Aplicacion.Constantes.ValidacionDatosEntrada;
namespace Presentacion.Core.Caja
{
    public partial class _00044_Abm_Gastos : FormAbm
    {
        IConceptoGastosServicio _conceptoGastosServicio;
        IGastoServicio _gastoServicio;

        public _00044_Abm_Gastos(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _conceptoGastosServicio = ObjectFactory.GetInstance<IConceptoGastosServicio>();
            _gastoServicio = ObjectFactory.GetInstance<IGastoServicio>();

            var conceptos = _conceptoGastosServicio.Obtener(string.Empty);

            PoblarComboBox(cmbConcepto, conceptos, "Descripcion", "Id");

            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
     
            };
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (GastoDto)_gastoServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;
                dtpFecha.Value = resultado.Fecha;
                cmbConcepto.SelectedValue = resultado.ConceptoGastoId;
                nudMontoPagar.Value = resultado.Importe;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else // Nuevo
            {
                btnEjecutar.Text = "Nuevo";
            }
        }
        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text)) return false;

            if (cmbConcepto.Items.Count <= 0) return false;

            if (nudMontoPagar.Value < 100) return false;

            return true;
        }
        public override bool VerificarSiExiste(long? id = null)
        {
            return _gastoServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new GastoDto();

            nuevoRegistro.Fecha = dtpFecha.Value;
            nuevoRegistro.Importe = nudMontoPagar.Value;
            nuevoRegistro.ConceptoGastoId = (long)cmbConcepto.SelectedValue;
            nuevoRegistro.Descripcion = txtDescripcion.Text;
            nuevoRegistro.Eliminado = false;

            _gastoServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new GastoDto
            {
               Fecha = dtpFecha.Value,
               Importe = nudMontoPagar.Value,
               ConceptoGastoId = (long)cmbConcepto.SelectedValue,
               Descripcion = txtDescripcion.Text,
               Eliminado = false
            };

            _gastoServicio.Insertar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _gastoServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);
            txtDescripcion.Focus();
        }

        private void btnNuevoConcepto_Click(object sender, System.EventArgs e)
        {
            var formNuevaMarca = new _00042_Abm_ConceptoGastos(TipoOperacion.Nuevo);
            formNuevaMarca.ShowDialog();

            if (formNuevaMarca.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbConcepto, _conceptoGastosServicio.Obtener(string.Empty),
                "Descripcion",
                "Id");
            }
        }
    }
}

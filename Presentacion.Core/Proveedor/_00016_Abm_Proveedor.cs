using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Provincia;
using IServicios.Proveedor;
using IServicios.Proveedor.DTOs;
using Presentacion.Core.CondicionIva;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;
using static Aplicacion.Constantes.ValidacionDatosEntrada;

namespace Presentacion.Core.Proveedor
{
    public partial class _00016_Abm_Proveedor : FormAbm
    {
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;
        private readonly IProveedorServicio _proveedorServicio;
        private readonly ICondicionIvaServicio _condicionIvaServicio;

        public _00016_Abm_Proveedor(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
            _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();
            _proveedorServicio = ObjectFactory.GetInstance<IProveedorServicio>();
            _condicionIvaServicio = ObjectFactory.GetInstance<ICondicionIvaServicio>();

            txtTelefono.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtCUIT.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtDomicilio.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
       
               
            };

            txtRazonSocial.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
       
            };

        }

        public override void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue)
            {

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            

                var entidad = (ProveedorDto)_proveedorServicio.Obtener(entidadId.Value);

                if (entidad == null)
                {
                    MessageBox.Show("Ocuriro un error al obtener el registro seleciconado");
                    Close();
                }

                txtRazonSocial.Text = entidad.RazonSocial;
                txtCUIT.Text = entidad.CUIT;           
                txtTelefono.Text = entidad.Telefono;
                txtDomicilio.Text = entidad.Direccion;
                txtMail.Text = entidad.Mail;
            


                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");
                cmbProvincia.SelectedValue = entidad.ProvinciaId;

                PoblarComboBox(cmbDepartamento, _departamentoServicio.ObtenerPorProvincia(entidad.ProvinciaId), "Descripcion", "Id");
                cmbDepartamento.SelectedValue = entidad.DepartamentoId;

                PoblarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorDepartamento(entidad.DepartamentoId), "Descripcion", "Id");
                cmbLocalidad.SelectedValue = entidad.LocalidadId;

                PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");
                cmbCondicionIva.SelectedValue = entidad.CondicionIvaId;
            }
            else
            {
                LimpiarControles(this);

                PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");

                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

                if (cmbProvincia.Items.Count > 0)
                {
                    PoblarComboBox(cmbDepartamento,
                        _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue)
                        , "Descripcion",
                        "Id");

                    if (cmbDepartamento.Items.Count > 0)
                    {
                        PoblarComboBox(cmbLocalidad,
                            _localidadServicio.ObtenerPorDepartamento((long)cmbProvincia.SelectedValue),
                            "Descripcion",
                            "Id");
                    }
                }
            }

        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);
        }

        public override void EjecutarComandoNuevo()
        {
            if (!ValidarEmail(txtMail.Text, error, txtMail))
            {
                MessageBox.Show("Formato de correo Iconrrecto");
                return;
            }

            var nuevoProveedor = new ProveedorDto
            {
                RazonSocial = txtRazonSocial.Text,
                CUIT = txtCUIT.Text,
                Telefono = txtTelefono.Text,
                ProvinciaId = (long) cmbProvincia.SelectedValue,
                DepartamentoId = (long) cmbDepartamento.SelectedValue,
                LocalidadId = (long) cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                Direccion = txtDomicilio.Text,
                CondicionIvaId = (long) cmbCondicionIva.SelectedValue,
                Eliminado = false
            };

            _proveedorServicio.Insertar(nuevoProveedor);

            LimpiarControles(this);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarProveedor = new ProveedorDto
            {
                Id = EntidadId.Value,
                RazonSocial = txtRazonSocial.Text,
                CUIT = txtCUIT.Text,
                Telefono = txtTelefono.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                Direccion = txtDomicilio.Text,
                CondicionIvaId = (long)cmbCondicionIva.SelectedValue,
                Eliminado = false
            };

            _proveedorServicio.Modificar(modificarProveedor);
        }

        public override void EjecutarComandoEliminar()
        {
            _proveedorServicio.Eliminar(EntidadId.Value);
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtCUIT.Text))
                return false;

            if (string.IsNullOrEmpty(txtTelefono.Text))
                return false;

            if (string.IsNullOrEmpty(txtMail.Text))
                return false;

            if (string.IsNullOrEmpty(txtTelefono.Text))
                return false;


            if (cmbProvincia.Items.Count <= 0)
                return false;

            if (cmbDepartamento.Items.Count <= 0)
                return false;

            if (cmbLocalidad.Items.Count <= 0)
                return false;

            if (cmbCondicionIva.Items.Count <= 0)
                return false;

            return true;
        }

        private void btnNuevaProvincia_Click(object sender, System.EventArgs e)
        {
            var formNuevaProvincia = new _00002_Abm_Provincia(TipoOperacion.Nuevo);
            formNuevaProvincia.ShowDialog();

            if (formNuevaProvincia.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbProvincia,
                    _provinciaServicio.Obtener(string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoDepartamento_Click(object sender, System.EventArgs e)
        {
            var formNuevoDepartamento = new _00004_Abm_Departamento(TipoOperacion.Nuevo);
            formNuevoDepartamento.ShowDialog();

            if (formNuevoDepartamento.RealizoAlgunaOperacion)
            {
                if (cmbProvincia.Items.Count > 0) // si tiene algo
                {
                    PoblarComboBox(cmbDepartamento,
                        _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue)
                        , "Descripcion",
                        "Id");
                }
            }
        }

        private void btnNuevaLocalidad_Click(object sender, System.EventArgs e)
        {
            var formNuevoDepartamento = new _00006_AbmLocalidad(TipoOperacion.Nuevo);
            formNuevoDepartamento.ShowDialog();
        }

        private void btnNuevaCondicionIva_Click(object sender, System.EventArgs e)
        {
            var formNuevoDepartamento = new _00014_Abm_CondicionIva(TipoOperacion.Nuevo);
            formNuevoDepartamento.ShowDialog();
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbProvincia.Items.Count <= 0) return;

            PoblarComboBox(cmbDepartamento,
                _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue), "Descripcion", "Id");

            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void cmbDepartamento_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }
    }
}

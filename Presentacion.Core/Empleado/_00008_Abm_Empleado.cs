using Aplicacion.Constantes;
using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicio.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using System.Linq;
using System.Drawing;
using static Aplicacion.Constantes.ValidacionDatosEntrada;
using System.Windows.Forms;
using static Aplicacion.Constantes.Imagen;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using Presentacion.Core.Departamento;

namespace Presentacion.Core.Empleado
{
    public partial class _00008_Abm_Empleado : FormAbm
    {
        private readonly IEmpleadoServicio _empleadoServicio; 
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;

        public _00008_Abm_Empleado(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _empleadoServicio = ObjectFactory.GetInstance<IEmpleadoServicio>();
            _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();

            txtTelefono.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtLegajo.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtDni.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtNombre.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtApellido.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtDomicilio.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
          
            };

        }

        public override void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue)
            {
                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);

                var entidad = (EmpleadoDto)_empleadoServicio.Obtener(typeof(EmpleadoDto), entidadId.Value);

                if (entidad == null)
                {
                    MessageBox.Show("Ocuriro un error al obtener el registro seleciconado");
                    Close();
                }

                txtLegajo.Text = entidad.Legajo.ToString();
                txtApellido.Text = entidad.Apellido;
                txtNombre.Text = entidad.Nombre;
                txtDni.Text = entidad.Dni;
                txtTelefono.Text = entidad.Telefono;
                txtDomicilio.Text = entidad.Direccion;

                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");
                cmbProvincia.SelectedValue = entidad.ProvinciaId;

                PoblarComboBox(cmbDepartamento, _departamentoServicio.ObtenerPorProvincia(entidad.ProvinciaId), "Descripcion", "Id");
                cmbDepartamento.SelectedValue = entidad.DepartamentoId;


                PoblarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorDepartamento(entidad.DepartamentoId), "Descripcion", "Id");
                cmbLocalidad.SelectedValue = entidad.LocalidadId;

                txtMail.Text = entidad.Mail;

                imgFoto.Image = Imagen.ConvertirImagen(entidad.Foto);
            }
            else
            {
                LimpiarControles(this);

                txtLegajo.Text = _empleadoServicio.ObtenerSiguienteLegajo().ToString();

                var provincias = _provinciaServicio.Obtener(string.Empty);

                PoblarComboBox(cmbProvincia,
                    provincias,
                    "Descripcion",
                    "Id");

                if (provincias.Any())
                {
                    var departamentos = _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                    PoblarComboBox(cmbDepartamento,
                        departamentos,
                        "Descripcion",
                        "Id");

                    if (departamentos.Any())
                    {
                        var localidades =
                            _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                        PoblarComboBox(cmbLocalidad,
                            localidades,
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

            var nuevoEmpleado = new EmpleadoDto
            {
                Apellido = txtApellido.Text,
                Legajo = int.Parse(txtLegajo.Text), 
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                Foto = Imagen.ConvertirImagen(imgFoto.Image),
                Eliminado = false
            };

            _empleadoServicio.Insertar(nuevoEmpleado);

            LimpiarControles(this);

            imgFoto.Image = Imagen.ImagenEmpleadoPorDefecto;

        }

        public override void EjecutarComandoModificar()
        {
            _empleadoServicio.Modificar(new EmpleadoDto
            {
                Id = EntidadId.Value,
                Legajo = int.Parse(txtLegajo.Text),
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                Foto = Imagen.ConvertirImagen(imgFoto.Image),
                Eliminado = false
            });
        }

        public override void EjecutarComandoEliminar()
        {
            _empleadoServicio.Eliminar(typeof(EmpleadoDto), EntidadId.Value);
            
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
                return false;
            if (string.IsNullOrEmpty(txtApellido.Text))
                return false;
            if (string.IsNullOrEmpty(txtDni.Text))
                return false;

            if (string.IsNullOrEmpty(txtTelefono.Text))
                return false;


            if (cmbProvincia.Items.Count <= 0)
                return false;

            if (cmbDepartamento.Items.Count <= 0)
                return false;

            return true;
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

        
         private void btnImagen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog getImage = new OpenFileDialog();

            getImage.InitialDirectory = @"C:\\";
            getImage.Filter = "Archivos de Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (getImage.ShowDialog() == DialogResult.OK)
            {
                imgFoto.ImageLocation = getImage.FileName;
            }
            else
            {
                MessageBox.Show("No se seleccionó ninguna imagen");
            }
        }

        private void btnNuevaLocalidad_Click(object sender, System.EventArgs e)
        {
            var formNuevoDepartamento = new _00006_AbmLocalidad(TipoOperacion.Nuevo);
            formNuevoDepartamento.ShowDialog();
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
    }
}

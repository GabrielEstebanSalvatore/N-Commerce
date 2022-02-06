using Aplicacion.Constantes;
using IServicio.Articulo;
using IServicio.Configuracion;
using IServicio.Configuracion.DTOs;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicios.Articulo.DTOs;
using IServicios.Comprobante;
using IServicios.Contador;
using IServicios.Precios;
using IServicios.Proveedor;
using IServicios.Proveedor.DTOs;
using Presentacion.Core.Articulo;
using Presentacion.Core.Comprobantes.Clases;
using Presentacion.Core.FormaPago;
using Presentacion.Core.Proveedor;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using static Aplicacion.Constantes.ValidacionDatosEntrada;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00053_Compra : FormBase
    {
        private ProveedorDto _proveedorSeleccionado;
        private EmpleadoDto _empleadoSeleccionado;
        private ConfiguracionDto _configuracion;
        private FacturaCompraView _factura;
        private ArticuloCompraDto _articuloSeleccionado;
        private ItemView _itemSeleccionado;

        private bool _permiteAgregarPorCantidad;
        private bool _articuloConPrecioAlternativo;
        //private bool _autorizaPermisoListaPrecio;
        private bool _cambiarCantidadConErrorPorValidacion;

        private readonly IProveedorServicio _proveedorServicio;
        private readonly IConfiguracionServicio _configuracionServicio;
        private readonly IContadorServicio _contadorServicio;
        private readonly IArticuloServicio _articuloServicio;
        private readonly IFacturaServicio _facturaServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IPrecioServicio _precioServicio;

        public _00053_Compra()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // -------------------------------------------- Servicios  

            _proveedorServicio = ObjectFactory.GetInstance<IProveedorServicio>();

            _configuracionServicio = ObjectFactory.GetInstance<IConfiguracionServicio>();

            _contadorServicio = ObjectFactory.GetInstance<IContadorServicio>();

            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();

            _facturaServicio = ObjectFactory.GetInstance<IFacturaServicio>();

            _empleadoServicio = ObjectFactory.GetInstance<IEmpleadoServicio>();

            _precioServicio = ObjectFactory.GetInstance<IPrecioServicio>();
            //----------------------------------------------

            _articuloSeleccionado = null;

            _factura = new FacturaCompraView();

            _configuracion = _configuracionServicio.Obtener();

            if (_configuracion == null)
            {
                MessageBox.Show("Antes de comenzar por favor cargue la configuracion del Sistema");
                Close();
            }
        }

        private void _00053_Compra_Load(object sender, System.EventArgs e)
        {
            CargarCabecera();
            CargarCuerpo();
            CargarPie();
        }

        private void CargarPie()
        {
            nudTotal.Value = _factura.Total;
        }

        private void CargarCuerpo()
        {
            dgvGrilla.DataSource = _factura.Items.ToList();
            FormatearGrilla(dgvGrilla);

        }

        private void CargarCabecera()
        {
            AsignarDatosProveedor(_proveedorSeleccionado);

            _empleadoSeleccionado = ObtenerEmpleadoPorDefecto();

            txtEmpleado.Text = _empleadoSeleccionado.ApyNom;

            lblFechaActual.Text = DateTime.Today.ToShortDateString();
        }

        private EmpleadoDto ObtenerEmpleadoPorDefecto()
        {
            return (EmpleadoDto)_empleadoServicio.Obtener(typeof(EmpleadoDto),
            Identidad.EmpleadoId);
        }

        private void btnBuscarProveedor_Click(object sender, System.EventArgs e)
        {
            var lookUpProveedor = ObjectFactory.GetInstance<ProveedorLookUp>();
            lookUpProveedor.ShowDialog();
            if (lookUpProveedor.EntidadSeleccionada != null)
            {
                _proveedorSeleccionado = (ProveedorDto)lookUpProveedor.EntidadSeleccionada;
                AsignarDatosProveedor((ProveedorDto)
                lookUpProveedor.EntidadSeleccionada);

            }
            else
            {
                _proveedorSeleccionado = ObtenerProveedor();
                AsignarDatosProveedor(_proveedorSeleccionado);
            }

        }

        private void AsignarDatosProveedor(ProveedorDto proveedor)
        {
            if (proveedor == null)
            {
                return;
            }

            txtCuit.Text = proveedor.CUIT;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtDomicilio.Text = proveedor.Direccion;
            txtTelefono.Text = proveedor.Telefono;
            txtCondicionIva.Text = proveedor.CondicionIva;
        }

        private ProveedorDto ObtenerProveedor()
        {
            var clientes = (List<ProveedorDto>)_proveedorServicio.Obtener(string.Empty);

            if (clientes.FirstOrDefault() == null)
            {
                MessageBox.Show("El cliente Consumidor Final no Existe");
                Close();
            }
            return clientes.FirstOrDefault();
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].Width = 100;
            dgv.Columns["CodigoBarra"].HeaderText = "Código";
            dgv.Columns["CodigoBarra"].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].HeaderText = "Articulo";
            dgv.Columns["Descripcion"].AutoSizeMode =
            DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Iva"].Visible = true;
            dgv.Columns["Iva"].Width = 100;
            dgv.Columns["Iva"].HeaderText = "Iva";
            dgv.Columns["Iva"].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 120;
            dgv.Columns["PrecioStr"].HeaderText = "Precio";
            dgv.Columns["PrecioStr"].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 120;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
            dgv.Columns["Cantidad"].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["SubTotalStr"].Visible = true;
            dgv.Columns["SubTotalStr"].Width = 120;
            dgv.Columns["SubTotalStr"].HeaderText = "Sub-Total";
            dgv.Columns["SubTotalStr"].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;
        }

        private bool AsignarArticuloAlternativo(string codigo)
        {
            //_articuloConPrecioAlternativo = true;

            //var codigoArticulo = codigo.Substring(0, codigo.IndexOf('*'));

            //if (!string.IsNullOrEmpty(codigoArticulo))
            //{
            //    _articuloSeleccionado = _articuloServicio.ObtenerPorCodigo(codigoArticulo, (long)cmbListaPrecio.SelectedValue, _configuracion.DepositoVentaId);

            //    if (_articuloSeleccionado != null)
            //    {
            //        var precioAlternativo = codigo.Substring(codigo.IndexOf('*') + 1);

            //        if (!string.IsNullOrEmpty(precioAlternativo))
            //        {
            //            if (decimal.TryParse(precioAlternativo, out decimal _precio))
            //            {
            //                _articuloSeleccionado.Precio = _precio;
            //                return true;
            //            }
            //            else
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}

            return false;
        }

        private void LimpiarParaNuevoItem()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtSubTotalLinea.Clear();
            nudPrecioUnitario.Value = 1;
            nudCantidad.Value = 1;
            nudCantidad.Enabled = false;
            _permiteAgregarPorCantidad = false;
            _articuloConPrecioAlternativo = false;

            _articuloSeleccionado = null;
            txtCodigo.Focus();
        }

        private void LimpiarParaNuevoFactura()
        {
            _factura = new FacturaCompraView();
            CargarCabecera();
            CargarCuerpo();
            CargarPie();
            LimpiarParaNuevoItem();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {

                    _articuloSeleccionado = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);


                    if (_articuloSeleccionado != null)
                    {
                        if (_permiteAgregarPorCantidad)
                        {
                            txtCodigo.Text = _articuloSeleccionado.CodigoBarra;
                            txtDescripcion.Text = _articuloSeleccionado.Descripcion;
                            nudPrecioUnitario.Value = _articuloSeleccionado.Precio;
                            nudCantidad.Focus();
                            nudCantidad.Select(0, nudCantidad.Text.Length);
                            return;
                        }
                        else
                        {
                            btnAgregar.PerformClick();
                        }
                    }
                    else
                    {
                        LimpiarParaNuevoItem();
                    }
                }
            }

            e.Handled = false;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
     
            switch (e.KeyValue)
            {
                // F1
                case 112:
                    ArticuloCompraDto currentArticulo = null;

                    currentArticulo = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);
                    if (currentArticulo == null)
                    {
                        txtDescripcion.Text = "";
                        txtSubTotalLinea.Text = "";
                    }
                    else
                    {
                        nudPrecioUnitario.Value = currentArticulo.Precio;
                        txtDescripcion.Text = currentArticulo.Descripcion;
                        txtSubTotalLinea.Text = (currentArticulo.Precio * nudCantidad.Value).ToString();
                    }

                    break;
                // F5
                case 116:
                    _permiteAgregarPorCantidad = !_permiteAgregarPorCantidad;
                    nudCantidad.Enabled = _permiteAgregarPorCantidad;
                    break;

                // F8
                case 119:

                    var lookUpArticulo = new ArticuloLookUp();
                    lookUpArticulo.ShowDialog();

                    if (lookUpArticulo.EntidadSeleccionada != null)
                    {
                        _articuloSeleccionado = (ArticuloCompraDto)lookUpArticulo.EntidadSeleccionada;

                        if (_permiteAgregarPorCantidad)
                        {
                            txtCodigo.Text = _articuloSeleccionado.CodigoBarra;
                            txtDescripcion.Text = _articuloSeleccionado.Descripcion;
                            nudPrecioUnitario.Text = _articuloSeleccionado.PrecioStr;
                            nudCantidad.Focus();
                            nudCantidad.Select(0, nudCantidad.Text.Length);
                            return;
                        }
                        else
                        {
                            btnAgregar.PerformClick();
                            LimpiarParaNuevoItem();
                        }
                    }
                    else
                    {
                        LimpiarParaNuevoItem();
                    }

                    break;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)//TODO - cambiar precio de articulos
        {
            if (_articuloSeleccionado != null)
            {
                using (var tran = new TransactionScope())
                {
                    try
                    {
                        ArticuloCompraDto articuloComparar = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);

                        if (_articuloSeleccionado.Precio != articuloComparar.Precio)
                        {
                            if (_configuracion.ModificaPrecioVentaDesdeCompra)
                            {
                                if (MessageBox.Show("Desea modificar el precio del artículo? ", "Atencion",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    var precioArticulo = nudPrecioUnitario.Value - _articuloSeleccionado.Precio;

                                    _precioServicio.ActualizarPrecio(precioArticulo, _articuloSeleccionado.Id);

                                    _articuloSeleccionado = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);
                                }
                            }
                        }

                        AgregarItem(_articuloSeleccionado, nudCantidad.Value);
                        tran.Complete();
                    }
                    catch (Exception exception)
                    {
                        tran.Dispose();
                        throw new Exception(exception.Message);
                    }

                }
            }
            else
            {

                using (var tran = new TransactionScope())
                {
                    _articuloSeleccionado = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);

                    try
                    {
                        ArticuloCompraDto articuloComparar = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);

                        if (_articuloSeleccionado.Precio != nudPrecioUnitario.Value)
                        {
                            if (_configuracion.ModificaPrecioVentaDesdeCompra)
                            {
                                if (MessageBox.Show("Desea modificar el precio del artículo? ", "Atencion",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    var precioArticulo =  nudPrecioUnitario.Value - _articuloSeleccionado.Precio;

                                    _precioServicio.ActualizarPrecio(precioArticulo, _articuloSeleccionado.Id);

                                    _articuloSeleccionado = _articuloServicio.ObtenerPorCodigoCompra(txtCodigo.Text, _configuracion.DepositoVentaId);
                                }
                            }
                        }

                        AgregarItem(_articuloSeleccionado, nudCantidad.Value);
                        LimpiarParaNuevoItem();
                        CargarCuerpo();
                        CargarPie();
                        tran.Complete();
                    }
                    catch (Exception exception)
                    {
                        tran.Dispose();
                        throw new Exception(exception.Message);
                    }
                }

            }
        }

        private void AgregarItem(ArticuloCompraDto articulo, decimal cantidad)
        {
           
           
            _factura.Items.Add(AsignarDatosItem(articulo, cantidad));
            
            
        }

        private ItemView AsignarDatosItem(ArticuloCompraDto articulo, decimal cantidad)
        {
            _factura.ContadorItem++;
            return new ItemView
            {
                Id = _factura.ContadorItem,
                Descripcion = articulo.Descripcion,
                Iva = articulo.Iva,
                Precio = articulo.Precio,
                CodigoBarra = articulo.CodigoBarra,
                Cantidad = cantidad,
                ArticuloId = articulo.Id,
                EsArticuloAlternativo = _articuloConPrecioAlternativo
            };
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (txtSubTotalLinea.Text != "")
            {
                var currentValue = decimal.Parse(txtSubTotalLinea.Text);

                txtSubTotalLinea.Text = (currentValue * nudCantidad.Value).ToString();
            }
        }

        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            

            if (MessageBox.Show($"Esta seguro de eliminar el item {_itemSeleccionado.Descripcion}",
                "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _factura.Items.Remove(_itemSeleccionado);
                LimpiarParaNuevoItem();
                CargarCuerpo();
                CargarPie();
            }
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                _itemSeleccionado = (ItemView)dgvGrilla.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                _itemSeleccionado = null;
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount <= 0)
            {
                MessageBox.Show("No hay ningun articulo seleccionado");
                return;
            }

            _factura.Proveedor = _proveedorSeleccionado;
          
            _factura.UsuarioId = Identidad.UsuarioId;
            _factura.Iva105 = nudIva105.Enabled == true ? nudIva105.Value : 0;
            _factura.Iva21 = nudIva21.Enabled == true ? nudIva21.Value : 0;
            _factura.Iva27 = nudIva27.Enabled == true ? nudIva27.Value : 0;
            _factura.ImpInterno = nudImpuestoInterno.Enabled == true ? nudImpuestoInterno.Value : 0;
            _factura.PercepcionTemp = nudPercepcionTemp.Enabled == true ? nudPercepcionTemp.Value : 0;
            _factura.PercepcionPyP = nudPercepcionPyP.Enabled == true ? nudPercepcionPyP.Value : 0;
            _factura.PercepcionIva = nudPercepcionIva.Enabled == true ? nudPercepcionIva.Value : 0;
            _factura.PercepcionIB = nudPercepcionIB.Enabled == true ? nudPercepcionIB.Value : 0;
            _factura.TipoComprobante = TipoComprobante.FacturaProveedor;
            _factura.Empleado = _empleadoSeleccionado;
            _factura.FechaEntrega = dtpFechaEntrega.Value;
            _factura.ArticuloEntregado = chkArticuloEntregado.Checked;

            var fFormaDePago = new _00044_FormaPago(_factura);
            fFormaDePago.ShowDialog();

            if (fFormaDePago.RealizoCompra)
            {
                LimpiarParaNuevoFactura();
                txtCodigo.Focus();
            }
        }

        private void chk27_CheckedChanged(object sender, EventArgs e)
        {
            nudIva27.Enabled = !nudIva27.Enabled;
        }

        private void chk21_CheckedChanged(object sender, EventArgs e)
        {
            nudIva21.Enabled = !nudIva21.Enabled;
        }

        private void chk105_CheckedChanged(object sender, EventArgs e)
        {
            nudIva105.Enabled = !nudIva105.Enabled;
        }

        private void chkImpuestoInterno_CheckedChanged(object sender, EventArgs e)
        {
            nudImpuestoInterno.Enabled = !nudImpuestoInterno.Enabled;
        }

        private void chkPercepcionTemp_CheckedChanged(object sender, EventArgs e)
        {
            nudPercepcionTemp.Enabled = !nudPercepcionTemp.Enabled;
        }

        private void chkPercepcionPyP_CheckedChanged(object sender, EventArgs e)
        {
            nudPercepcionPyP.Enabled = !nudPercepcionPyP.Enabled;
        }

        private void chkPercepcionIva_CheckedChanged(object sender, EventArgs e)
        {
            nudPercepcionIva.Enabled = !nudPercepcionIva.Enabled;
        }

        private void chkPercepcionIB_CheckedChanged(object sender, EventArgs e)
        {
            nudPercepcionIB.Enabled = !nudPercepcionIB.Enabled;
        }
    }
}
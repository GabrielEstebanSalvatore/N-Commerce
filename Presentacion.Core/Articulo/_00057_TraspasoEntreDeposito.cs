using Aplicacion.Constantes;
using Dominio.Entidades;
using Dominio.UnidadDeTrabajo;
using IServicio.Articulo;
using IServicio.Deposito;
using IServicios.Articulo.DTOs;
using IServicios.StockTransferencia;
using IServicios.StockTransferencia.DTOs;
using PresentacionBase.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00057_TraspasoEntreDeposito : FormBase
    {
        private readonly IDepositoSevicio _depositoSevicio;
        private readonly IArticuloServicio _articuloServicio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IStockTransferenciaServicio _stockTransferenciaServicio;


        ArticuloDto _articuloActual;
        Stock _stockDonante, _stockReceptor;
        int valorStockArticuloSeleccionado = 0;

        public _00057_TraspasoEntreDeposito(IDepositoSevicio depositoSevicio,
            IArticuloServicio articuloServicio,
            IUnidadDeTrabajo unidadDeTrabajo,
            IStockTransferenciaServicio stockTransferenciaServicio)
        {
            InitializeComponent();
            _depositoSevicio = depositoSevicio;
            _articuloServicio = articuloServicio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _stockTransferenciaServicio = stockTransferenciaServicio;

            _articuloActual = null;
            _stockDonante = null;
            _stockReceptor = null;

            var depositosDonante = _depositoSevicio.Obtener(string.Empty, false);

            PoblarComboBox(cmbDepositoDonante, depositosDonante,
                    "Descripcion",
                    "Id");

            var depositosReceptor = _depositoSevicio.Obtener(string.Empty, false);

            PoblarComboBox(cmbDepositoReceptor, depositosReceptor,
                 "Descripcion",
                 "Id");

            var articulos = _articuloServicio.Obtener(string.Empty);

            PoblarComboBox(cmbArticulo,
              articulos,
              "Descripcion",
              "Id");

            nudCantidadTransferir.Focus();

            //var ArticuloInicial = buscarStockArticulo(cmbDepositoDonante.SelectedValue);

            //nudCantidadActualDonante.Value = ArticuloInicial.Cantidad;

          
        }

        private void _00057_TraspasoEntreDeposito_Load(object sender, EventArgs e)
        {
            ActualizarDatos();

        }

        private void ActualizarDatos()
        {
            dgvTransferencias.DataSource = _stockTransferenciaServicio.Obtener(string.Empty);

            for (int i = 0; i < dgvTransferencias.ColumnCount; i++)
            {
                dgvTransferencias.Columns[i].Visible = false;

                dgvTransferencias.Columns[i].HeaderCell.Style.Alignment
                    = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvTransferencias.Columns["Articulo"].Visible = true;
            dgvTransferencias.Columns["Articulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["Articulo"].HeaderText = "Articulo";
            dgvTransferencias.Columns["Articulo"].DisplayIndex = 0;

            dgvTransferencias.Columns["Cantidad"].Visible = true;
            dgvTransferencias.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvTransferencias.Columns["Cantidad"].DisplayIndex = 1;

            dgvTransferencias.Columns["DepositoDonante"].Visible = true;
            dgvTransferencias.Columns["DepositoDonante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["DepositoDonante"].HeaderText = "Deposito Donante";
            dgvTransferencias.Columns["DepositoDonante"].DisplayIndex = 2;

            dgvTransferencias.Columns["DepositoReceptor"].Visible = true;
            dgvTransferencias.Columns["DepositoReceptor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["DepositoReceptor"].HeaderText = "Deposito Receptor ";
            dgvTransferencias.Columns["DepositoReceptor"].DisplayIndex = 3;

            dgvTransferencias.Columns["UsuarioStr"].Visible = true;
            dgvTransferencias.Columns["UsuarioStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["UsuarioStr"].HeaderText = "Usuario";
            dgvTransferencias.Columns["UsuarioStr"].DisplayIndex = 4;

            dgvTransferencias.Columns["FechaStr"].Visible = true;
            dgvTransferencias.Columns["FechaStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransferencias.Columns["FechaStr"].HeaderText = "Fecha";
            dgvTransferencias.Columns["FechaStr"].DisplayIndex = 5;

        }

        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmbArticulo_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var articuloActual = (ArticuloDto)_articuloServicio.Obtener((long)cmbArticulo.SelectedValue);

            var cantidadArticulo = _unidadDeTrabajo.StockRepositorio.Obtener(articuloActual.Id);

            nudCantidadActualDonante.Value = cantidadArticulo.Cantidad;

            nudCantidadTransferir.Value = 0;
            nudCantidadPosteriorDonante.Value = 0;

            buscarStockArticulo(cmbDepositoDonante.SelectedValue);

        }

        private void nudCantidadTransferir_ValueChanged(object sender, System.EventArgs e)
        {
            var articuloActual = (ArticuloDto)_articuloServicio.Obtener((long)cmbArticulo.SelectedValue);
            //verficiar 
            if ((articuloActual.StockActual - nudCantidadTransferir.Value) < 0)
            {
                MessageBox.Show("El monto a trasnferir no puede ser mayor a la cantidad actual del artículo");
                
                nudCantidadTransferir.Value = 0;

                return;
            }

            nudCantidadPosteriorDonante.Value = nudCantidadActualDonante.Value - nudCantidadTransferir.Value;

            nudCantidadPosteriorReceptor.Value = nudCantidadActualReceptor.Value + nudCantidadTransferir.Value;
     
        }

        private void btnTrasnferir_Click(object sender, System.EventArgs e)
        {
            if ((long)cmbDepositoDonante.SelectedValue == (long)cmbDepositoReceptor.SelectedValue)
            {
                MessageBox.Show("El depósito donante y receptor no pueden ser iguales"); ;

                return;
            }

            buscarStockArticulo(cmbDepositoDonante.SelectedValue);

            if ( (_stockDonante.Cantidad - nudCantidadTransferir.Value) < 0 )
            {
                MessageBox.Show("El monto a trasnferir no puede ser mayo a la cantidad actual del artículo"); ;
                
                nudCantidadTransferir.Value = 0;

                return;
            }

            var fecha = DateTime.Now;

            var entidadDepositoDonante =_unidadDeTrabajo.StockRepositorio.Obtener(_stockDonante.Id);
            if (entidadDepositoDonante == null) throw new Exception("Ocurrio un Error al Obtener el Artículo");

            entidadDepositoDonante.ArticuloId = _stockDonante.ArticuloId;
            entidadDepositoDonante.Cantidad = _stockDonante.Cantidad - nudCantidadTransferir.Value;
            entidadDepositoDonante.DepositoId = _stockDonante.DepositoId;
            entidadDepositoDonante.EstaEliminado = _stockDonante.EstaEliminado;

            _unidadDeTrabajo.StockRepositorio.Modificar(entidadDepositoDonante);
            _unidadDeTrabajo.Commit();


            var entidadDepositoReceptro = _unidadDeTrabajo.StockRepositorio.Obtener(_stockReceptor.Id);

            if (entidadDepositoReceptro == null) throw new Exception("Ocurrio un Error al Obtener el Artículo");


            entidadDepositoReceptro.ArticuloId = _stockReceptor.ArticuloId;
            entidadDepositoReceptro.Cantidad = _stockReceptor.Cantidad + nudCantidadTransferir.Value;
            entidadDepositoReceptro.DepositoId = _stockReceptor.DepositoId;
            entidadDepositoReceptro.EstaEliminado = _stockReceptor.EstaEliminado;
          

            _unidadDeTrabajo.StockRepositorio.Modificar(entidadDepositoReceptro);
            _unidadDeTrabajo.Commit();

            var fTrasnferenciaStock = new StockTransferenciaServicioDto
            {
                ArticuloId = _stockDonante.Id,
                Cantidad = nudCantidadTransferir.Value,
                DepositoDonanteId = _stockDonante.DepositoId,
                DepositoReceptorId = _stockReceptor.DepositoId,
                UsuarioId = Identidad.UsuarioId,
                Fecha = fecha

            };

            _stockTransferenciaServicio.Insertar(fTrasnferenciaStock);


            ActualizarDatos();
        }

        private void cmbDepositoReceptor_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var ArticuloInicial = buscarStockArticulo(cmbDepositoReceptor.SelectedValue);

            _stockReceptor = (Stock)_unidadDeTrabajo.StockRepositorio.Obtener(
                 x => x.ArticuloId == (long)cmbArticulo.SelectedValue &&
                x.DepositoId == (long)cmbDepositoReceptor.SelectedValue).FirstOrDefault();

            nudCantidadActualReceptor.Value = ArticuloInicial.Cantidad;
        }

        public Stock buscarStockArticulo(object cmbValue)
        {
            _stockDonante = (Stock)_unidadDeTrabajo.StockRepositorio.Obtener(
                 x => x.ArticuloId == (long)cmbArticulo.SelectedValue &&
                x.DepositoId == (long) cmbValue).FirstOrDefault();

            return _stockDonante;
        }

    }
}

using System.Collections.Generic;
using System.Windows.Forms;
using IServicio.Articulo;
using IServicios.Articulo.DTOs;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Articulo
{
    public partial class ArticuloLookUp : FormLookUp
    {
        private readonly IArticuloServicio _articuloServicio;
        private long _listaPrecioId;
        public ArticuloDto ArticuloSeleccionado => (ArticuloDto)EntidadSeleccionada;

        public ArticuloLookUp()
        {
            InitializeComponent();
            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();

        }
        public ArticuloLookUp(long listaPrecioId)
        {

            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
            _listaPrecioId = listaPrecioId;

            System.Windows.Forms.Control form = this;
   
            form.Width = form.Width - 217;

        }
    



        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            if (_listaPrecioId == 0 || _listaPrecioId == null)
            {
                var articulos = _articuloServicio.ObtenerLookUpCompra(cadenaBuscar);
                dgv.DataSource = articulos;
            }
            else
            {
                dgv.DataSource = (List<ArticuloVentaDto>)_articuloServicio.ObtenerLookUp(cadenaBuscar, _listaPrecioId);
            }
            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].Width = 80;
            dgv.Columns["CodigoBarra"].HeaderText = @"Codigo Barra";
            dgv.Columns["CodigoBarra"].DisplayIndex = 1;

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripcion";
            dgv.Columns["Descripcion"].DisplayIndex = 2;

            dgv.Columns["Stock"].Visible = true;
            dgv.Columns["Stock"].Width = 80;
            dgv.Columns["Stock"].HeaderText = @"Stock";
            dgv.Columns["Stock"].DisplayIndex = 3;
            dgv.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 80;
            dgv.Columns["PrecioStr"].HeaderText = @"Precio Publico";
            dgv.Columns["PrecioStr"].DisplayIndex = 4;
            dgv.Columns["PrecioStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}

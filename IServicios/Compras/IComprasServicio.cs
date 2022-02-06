using IServicio.BaseDto;
using IServicios.Compras.DTOs;
using IServicios.Comprobante;
using System.Collections.Generic;

namespace IServicios.Compras
{
    public interface IComprasServicio : IComprobanteServicio
    {
        IEnumerable<FacturaCompraDto> ObtenerCompras(string cadenaBuscar, bool mostrarTodos = true, bool ArticulosEntregados = true);

        FacturaCompraDto Obtener(long id);

        void Modificar(DtoBase dtoEntidad);
    }

}

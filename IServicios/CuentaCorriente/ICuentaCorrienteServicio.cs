using IServicios.Comprobante.DTOs;
using IServicios.CuentaCorriente.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.CuentaCorriente
{
    public interface ICuentaCorrienteServicio 
    {
        decimal ObtenerDeudaCliente(long clienteId);

        decimal ObtenerDeudaProveedor(long ProveedorId);

        IEnumerable<CuentaCorrienteDto> Obtener(long clienteId, DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda, bool estaEliminado);

        IEnumerable<CuentaCorrienteDto> ObtenerProveedor(long proveedorId, DateTime fechaDesde, DateTime fechaHasta, bool soloDeuda, bool estaEliminado);

        void Pagar(CtaCteComprobanteDto ComprobanteDto);

        void Pagar(CtaCteComprobanteProveedorDto ComprobanteDto);

        void Eliminar(long id);
    }
}

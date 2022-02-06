using Dominio.UnidadDeTrabajo;
using IServicios.Comprobante.DTOs;
using StructureMap;

namespace Servicios.Comprobante
{
    public class Comprobante
    {
        protected readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public Comprobante()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }
        public virtual long Insertar(ComprobanteDto comprobante)
        {
            return 0;
        }
        public virtual long InsertarProveedor(ComprobanteDto comprobante)
        {
            return 0;
        }

        public virtual long Insertar(ComprobanteCompraDto comprobante)
        {
            return 0;
        }

    }
}

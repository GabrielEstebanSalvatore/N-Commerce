using Aplicacion.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.Comprobante.DTOs
{
    public class PresupuestoDto : ComprobanteDto
    {
        public long ClienteId { get; set; }
        public Estado Estado { get; set; }
    }
}

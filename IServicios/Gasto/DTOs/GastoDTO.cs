using IServicio.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.Gasto.DTOs
{
    public class GastoDto : DtoBase
    {
        public DateTime Fecha { get; set; }
        public long ConceptoGastoId { get; set; }
        public string ConceptoGastoStr { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
    }
}

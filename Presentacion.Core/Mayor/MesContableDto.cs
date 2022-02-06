using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Mayor
{
    public class MesContableDto
    {
        public MesContableDto()
        {
            if (Mes == 0)
            {
                MesStr = "---";
            }
         
        }
        public int Mes { get; set; }
        public string MesStr { get; set; }
        public int Año { get; set; }
        public decimal totalIngreso { get; set; }
        public decimal totalEgreso { get; set; }
    }
}

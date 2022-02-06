using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServicios.Gasto
{
    public interface IGastoServicio : IServicio.Base.IServicio
    {
        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}

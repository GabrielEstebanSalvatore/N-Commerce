using Dominio.Entidades;
using Dominio.Repositorio;

namespace Infraestructura.Repositorio
{
    public class CtaCteProveedorRepositorio : Repositorio<CuentaCorrienteProveedor>, ICtaCteProveedorRepositorio
    {
        public CtaCteProveedorRepositorio(DataContext context)
            : base(context)
        {
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Dominio.MetaData
{
    public interface IFormaPagoCtaCteProveedor : IFormaPago
    {

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long ProveedorId { get; set; }
    }
}

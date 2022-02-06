using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.MetaData
{
    public interface IStockTransferencia
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long ArticuloId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long DepositoDonanteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long DepositoReceptorId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DefaultValue(0)]
        decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        long UsuarioId { get; set; }
    }
}

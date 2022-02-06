using Dominio.MetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("FormaPago_CtaCteProveedor")]
    [MetadataType(typeof(IFormaPagoCtaCteProveedor))]
    public class FormaPagoCtaCteProveedor : FormaPago
    {
        // Propiedades
        public long ProveedorId { get; set; }

        // Propiedades de Navegacion
        public virtual Proveedor Proveedor { get; set; }
    }
}

  

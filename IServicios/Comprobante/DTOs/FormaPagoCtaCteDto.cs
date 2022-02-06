namespace IServicios.Comprobante.DTOs
{
    public class FormaPagoCtaCteDto : FormaPagoDto
    {
        public long ClienteId { get; set; }

        public long ProveedorId { get; set; }
    }
}

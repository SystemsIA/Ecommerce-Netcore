#nullable disable

namespace Domain.Models
{
    public class Transacciones
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public long PedidoId { get; set; }
        public byte[] FechaTransaccion { get; set; }
        public byte[] Monto { get; set; }
        public string NumeroTarjeta { get; set; }

        public virtual Pedidos Pedido { get; set; }
    }
}

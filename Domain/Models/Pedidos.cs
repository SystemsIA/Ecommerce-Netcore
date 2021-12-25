using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Pedidos
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long ProductoId { get; set; }
        public byte[] Precio { get; set; }
        public long Cantidad { get; set; }
        public byte[] Subtotal { get; set; }
        public byte[] CreatedAt { get; set; }
        public byte[] UpdatedAt { get; set; }

        public virtual Productos Producto { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Transacciones Transacciones { get; set; }
    }
}

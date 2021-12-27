using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class Productos
    {
        public Productos()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        public long Id { get; set; }
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Decimal PrecioNormal { get; set; }
        public Decimal DescuentoPrecio { get; set; }
        public long? Cantidad { get; set; }
        public string ImagenPrincipal { get; set; }
        public bool Disponible { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
	public partial class Productos
	{
		public Productos()
		{
			Pedidos = new HashSet<Pedidos>();
		}

		public long Id { get; set; }
		public string Sku { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public byte[] PrecioNormal { get; set; }
		public byte[] DescuentoPrecio { get; set; }
		public long? Cantidad { get; set; }
		public string ImagenPrincipal { get; set; }
		public bool Disponible { get; set; }
		public byte[] CreatedAt { get; set; }
		public byte[] UpdatedAt { get; set; }

		public virtual ICollection<Pedidos> Pedidos { get; set; }
	}
}
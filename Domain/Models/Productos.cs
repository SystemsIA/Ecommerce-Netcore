using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domain.Models
{
	public partial class Productos
	{
		public Productos()
		{
			Imagenes = new HashSet<Imagenes>();
			Pedidos = new HashSet<Pedidos>();
		}

		public long Id { get; set; }
		public string Sku { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		[DataType(DataType.Currency)] public decimal PrecioNormal { get; set; }
		public decimal DescuentoPrecio { get; set; }
		public long? Cantidad { get; set; }
		public string ImagenPrincipal { get; set; }
		public bool Disponible { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<Imagenes> Imagenes { get; set; }
		public virtual ICollection<Pedidos> Pedidos { get; set; }
	}
}
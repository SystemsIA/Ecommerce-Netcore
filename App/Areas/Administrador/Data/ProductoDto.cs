using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Administrador.Data
{
	public class ProductoDto
	{
		public long Id { get; set; }
		public string Sku { get; set; }
		[Required] public string Nombre { get; set; }
		[Required] public string Descripcion { get; set; }
		[Required] public Decimal PrecioNormal { get; set; }
		[Required] public Decimal DescuentoPrecio { get; set; }
		[Required] public long? Cantidad { get; set; }
		[Required] public string ImagenPrincipal { get; set; }
	}
}
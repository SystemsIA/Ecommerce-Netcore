using App.Areas.Administrador.Data;

using Domain.Models;

namespace App.Areas.Administrador.Extensions
{
	public static class ProductoExtension
	{
		public static Productos AsProducto(this ProductoDto dto)
		{
			var p = new Productos
			{
				Id = dto.Id,
				Sku = dto.Sku,
				Nombre = dto.Nombre,
				Descripcion = dto.Descripcion,
				PrecioNormal = dto.PrecioNormal,
				DescuentoPrecio = dto.DescuentoPrecio,
				Cantidad = dto.Cantidad,
				ImagenPrincipal = dto.ImagenPrincipal,
				Disponible = dto.Disponible
			};
			return p;
		}
	}
}
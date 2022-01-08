#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using App.Areas.Administrador.Data;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace App.Areas.Administrador.Services
{
	public class ProductoServiceImpl : IProductoService
	{
		private readonly EcommerceDBContext _context;

		public ProductoServiceImpl(EcommerceDBContext context)
		{
			_context = context;
		}

		public async Task<Productos> CrearProducto(ProductoDto dto)
		{
			var newProducto = new Productos
			{
				Sku = Guid.NewGuid().ToString(),
				Nombre = dto.Nombre,
				Descripcion = dto.Descripcion,
				PrecioNormal = dto.PrecioNormal,
				DescuentoPrecio = dto.DescuentoPrecio,
				Cantidad = dto.Cantidad,
				Disponible = true,
				ImagenPrincipal = dto.ImagenPrincipal
			};

			await _context.Productos.AddAsync(newProducto);
			await _context.SaveChangesAsync();
			return newProducto;
		}

		public async Task<Productos> GetById(long id)
		{
			var producto = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			return producto;
		}

		public async Task<bool> DeleteProducto(long id)
		{
			var producto = await _context.Productos.FindAsync(id);
			try
			{
				_context.Productos.Remove(producto);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public Task<Productos> GetBySKU(string sku)
		{
			throw new System.NotImplementedException();
		}

		public async Task<ICollection<Productos>> GetAll(int? count)
		{
			var p = await _context.Productos.ToListAsync();
			return p.GetRange(0, count ?? 10);
		}
	}
}
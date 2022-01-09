using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using App.Areas.Administrador.Services;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace App.Areas.Public.Services
{
	public class CategoriaServiceImpl : ICategoriaService
	{
		private readonly EcommerceDBContext _context;
		private readonly IImagenService _imagenService;

		public CategoriaServiceImpl(EcommerceDBContext context, IImagenService imagenService)
		{
			_context = context;
			_imagenService = imagenService;
		}

		public async Task<ICollection<Categorias>> GetAll()
		{
			return await _context.Categorias.ToListAsync();
		}

		public async Task<Categorias> GetByNombre(string nombre)
		{
			var categoria =
				await _context.Categorias.FirstOrDefaultAsync(q => q.Nombre.ToLower().Contains(nombre.ToLower()));
			return categoria;
		}

		public async Task<ICollection<Productos>> GetProductosByCategoriaId(long id)
		{
			var categoriaProduct = await _context.ProductoCategorias.Where(q => q.CategoriaId.Equals(id))
				.ToListAsync();

			var productosList = categoriaProduct.Join(_context.Productos, cp => cp.ProductoId, p => p.Id, (cp, p) =>
					new Productos
					{
						Cantidad = p.Cantidad,
						Imagenes = _imagenService.GetListById(p.Id),
						Disponible = p.Disponible,
						Descripcion = p.Descripcion,
						Nombre = p.Nombre,
						Id = p.Id,
						DescuentoPrecio = p.DescuentoPrecio
					})
				.ToList();
			return productosList;
		}
	}
}
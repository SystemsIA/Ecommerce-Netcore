using System.Linq;
using System.Threading.Tasks;

using App.Areas.Administrador.Data;
using App.Areas.Administrador.Services;
using App.Attributes;

using Domain.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Administrador.Controllers
{
	[AuthorizeAdmin]
	[Area("Administrador")]
	public class ProductoController : Controller
	{
		private readonly EcommerceDBContext _context;
		private readonly IProductoService _productoService;
		private readonly IImagenService _imagenService;

		public ProductoController(EcommerceDBContext context, IProductoService productoService,
			IImagenService imagenService)
		{
			_context = context;
			_productoService = productoService;
			_imagenService = imagenService;
		}

		// GET: Administrador/Producto
		public async Task<IActionResult> Index()
		{
			return View(await _productoService.GetAll(null));
		}

		// GET: Administrador/Producto/Details/5
		public async Task<IActionResult> Details(long id)
		{
			var producto = await _productoService.GetById(id);
			var imagenes = await _imagenService.GetListById(id);

			if (imagenes.Count > 0)
			{
				producto.Imagenes = imagenes;
			}

			if (producto == null) return NotFound();

			return View(producto);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductoDto dto)
		{
			if (!ModelState.IsValid) return View();
			await _productoService.CrearProducto(dto);
			return RedirectToAction(nameof(Index));
		}

		// GET: Administrador/Producto/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null) return NotFound();

			var productos = await _context.Productos.FindAsync(id);
			if (productos == null) return NotFound();

			return View(productos);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id,
			[Bind(
				"Id,Sku,Nombre,Descripcion,PrecioNormal,DescuentoPrecio,Cantidad,ImagenPrincipal,Disponible")]
			Productos producto)
		{
			if (id != producto.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(producto);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductoExists(producto.Id))
						return NotFound();
					throw;
				}

				return RedirectToAction("Edit", new {id = producto.Id});
			}

			return View(producto);
		}

		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null) return NotFound();

			var productos = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productos == null) return NotFound();

			return View(productos);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var productos = await _context.Productos.FindAsync(id);
			_context.Productos.Remove(productos);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> SubirImagen(IFormFile imagen, int id)
		{
			await _imagenService.GuardarImagen(imagen, id);

			return RedirectToAction("Details", "Producto", new {id});
		}

		private bool ProductoExists(long id)
		{
			return _context.Productos.Any(e => e.Id == id);
		}
	}
}
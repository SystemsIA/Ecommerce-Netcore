using System.Threading.Tasks;

using App.Areas.Administrador.Data;
using App.Areas.Administrador.Extensions;
using App.Areas.Administrador.Services;
using App.Attributes;

using Domain.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Administrador.Controllers
{
	[AuthorizeAdmin]
	[Area("Administrador")]
	public class ProductoController : Controller
	{
		private readonly IProductoService _productoService;
		private readonly IImagenService _imagenService;

		public ProductoController(IProductoService productoService, IImagenService imagenService)
		{
			_productoService = productoService;
			_imagenService = imagenService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _productoService.GetAll(null));
		}

		public async Task<IActionResult> Details(long id)
		{
			var producto = await _productoService.GetById(id);
			var imagenes = await _imagenService.GetListByIdAsync(id);

			if (imagenes.Count > 0)
			{
				producto.Imagenes = imagenes;
			}

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

		public async Task<IActionResult> Edit(long id)
		{
			var producto = await _productoService.GetById(id);
			if (producto == null)
			{
				return NotFound();
			}

			return View(producto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, ProductoDto productoDto)
		{
			if (id != productoDto.Id) return NotFound();
			Productos productoUpdate;
			if (!ModelState.IsValid) return View(productoDto.AsProducto());
			productoUpdate = await _productoService.UpdateProducto(productoDto);
			return RedirectToAction("Edit", new {id = productoUpdate.Id});
		}

		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var producto = await _productoService.GetById(id);
			return View(producto);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			await _productoService.DeleteProducto(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> SubirImagen(IFormFile imagen, int id)
		{
			await _imagenService.GuardarImagen(imagen, id);

			return RedirectToAction("Details", "Producto", new {id});
		}

		// Request from Details product
		[HttpPost]
		public async Task<IActionResult> EliminarImagen(long idProducto, long idImagen)
		{
			var deleted = await _imagenService.EliminarImagen(idImagen);
			ViewBag.Message = deleted ? "Se elmino exitosamente" : "Sucedio un problema";

			return RedirectToAction("Details", "Producto", new {id = idProducto});
		}
	}
}
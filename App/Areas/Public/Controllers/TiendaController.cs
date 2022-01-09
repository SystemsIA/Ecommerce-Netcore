using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using App.Areas.Administrador.Services;
using App.Areas.Public.Services;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Public.Controllers
{
	[Area("Public")]
	[Route("tienda")]
	public class TiendaController : Controller
	{
		private readonly ICategoriaService _categoria;
		private readonly IProductoService _producto;

		public TiendaController(ICategoriaService categoria, IProductoService producto)
		{
			_categoria = categoria;
			_producto = producto;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("by/categoria")]
		public async Task<IActionResult> ProductosByCategoria([FromQuery(Name = "categoria")] string nombre)
		{
			ViewBag.Categorias = await _categoria.GetAll();
			if (string.IsNullOrEmpty(nombre))
			{
				return View(await _producto.GetAll(15));
			}

			ICollection<Productos> filterData;

			var categoria = await _categoria.GetByNombre(nombre);
			filterData = await _categoria.GetProductosByCategoriaId(categoria.Id);

			return View(filterData);
		}
	}
}
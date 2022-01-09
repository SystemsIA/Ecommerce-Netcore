using System;
using System.Threading.Tasks;

using App.Areas.Administrador.Services;
using App.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace App.Areas.Public.Controllers
{
	[Area("Public")]
	public class ProductoPublicController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IImagenService _imagenService;
		private readonly IProductoService _productoService;

		public ProductoPublicController(IConfiguration configuration,
			IImagenService imagenService, IProductoService productoService)
		{
			_configuration = configuration;
			_imagenService = imagenService;
			_productoService = productoService;
		}

		// GET: Public/Producto/Details/5
		[HttpGet("producto-detail/{id:long}")]
		public async Task<IActionResult> Details(long id)
		{
			var producto = await _productoService.GetById(id);
			var imagenes = await _imagenService.GetListByIdAsync(producto.Id);
			if (imagenes != null)
			{
				producto.Imagenes = imagenes;
			}

			var descuentoSol = (double) producto.PrecioNormal -
			                   (double) producto.DescuentoPrecio * (double) producto.PrecioNormal;
			var descuentoUsd = (double) producto.PrecioNormal / Currency.Dolar -
			                   (double) producto.DescuentoPrecio * ((double) producto.PrecioNormal / Currency.Dolar);
			ViewBag.PrecioNormalSol = Math.Round(producto.PrecioNormal, 2);
			ViewBag.PrecioNormalUSD = Math.Round((double) producto.PrecioNormal / Currency.Dolar, 2);

			ViewBag.DescuentoPrecioSol = Math.Round(descuentoSol, 2);
			ViewBag.PriceProductPaypal = Math.Round(descuentoUsd, 2);

			producto.DescuentoPrecio *= 100;
			return View(producto);
		}

		[Authorize]
		[Route("pagar/{id:long}")]
		public async Task<IActionResult> Payed(long id)
		{
			var producto = await _productoService.GetById(id);
			ViewBag.ClientId = _configuration["ClientId"];
			var descuentoUsd = (double) producto.PrecioNormal / Currency.Dolar -
			                   (double) producto.DescuentoPrecio * ((double) producto.PrecioNormal / Currency.Dolar);
			ViewBag.PriceProductPaypal = Math.Round(descuentoUsd, 2);
			var imagenes = await _imagenService.GetListByIdAsync(producto.Id);
			if (imagenes != null)
			{
				producto.Imagenes = imagenes;
			}

			producto.DescuentoPrecio *= 100;

			return View(producto);
		}
	}
}
using System;
using System.Threading.Tasks;

using App.Areas.Administrador.Services;

using Domain.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Areas.Public.Controllers
{
	[Area("Public")]
	public class ProductoPublicController : Controller
	{
		private readonly EcommerceDBContext _context;
		private readonly IConfiguration _configuration;
		private readonly IImagenService _imagenService;

		public ProductoPublicController(EcommerceDBContext context, IConfiguration configuration,
			IImagenService imagenService)
		{
			_context = context;
			_configuration = configuration;
			_imagenService = imagenService;
		}

		// GET: Public/Producto/Details/5
		[HttpGet("producto-detail/{id:long}")]
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null) return NotFound();

			var producto = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (producto == null) return NotFound();

			var imagen = await _imagenService.GetById(producto.Id);
			if (imagen != null)
			{
				ViewBag.ImagenPath = $"/imagenes/productos/{id}/{imagen.NombreImagen}";
			}

			var descuentoSol = (double) producto.PrecioNormal -
			                   (double) producto.DescuentoPrecio * (double) producto.PrecioNormal;
			var descuentoUSD = (double) producto.PrecioNormal / 3.95 -
			                   (double) producto.DescuentoPrecio * ((double) producto.PrecioNormal / 3.95);
			ViewBag.PrecioNormalSol = Math.Round(producto.PrecioNormal, 2);
			ViewBag.PrecioNormalUSD = Math.Round((double) producto.PrecioNormal / 3.95, 2);

			ViewBag.DescuentoPrecioSol = Math.Round(descuentoSol, 2);
			ViewBag.PriceProductPaypal = Math.Round(descuentoUSD, 2);

			return View(producto);
		}

		[Authorize]
		[Route("pagar/{id:long}")]
		public async Task<IActionResult> Payed(long id)
		{
			var producto = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			ViewBag.ClientId = _configuration["ClientId"];
			var descuentoUSD = (double) producto.PrecioNormal / 3.95 -
			                   (double) producto.DescuentoPrecio * ((double) producto.PrecioNormal / 3.95);
			ViewBag.PriceProductPaypal = Math.Round(descuentoUSD, 2);
			return View(producto);
		}
	}
}
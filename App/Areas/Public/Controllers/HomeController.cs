// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using System.Threading.Tasks;

using App.Areas.Administrador.Services;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Public.Controllers
{
	[Area("Public")]
	[Route("")]
	public class HomeController : Controller
	{
		private readonly IProductoService _productoService;

		public HomeController(IProductoService productoService)
		{
			_productoService = productoService;
		}

		[HttpGet, Route("")]
		public async Task<IActionResult> Index()
		{
			var results = await _productoService.GetAll(null);

			return View(results);
		}
	}
}
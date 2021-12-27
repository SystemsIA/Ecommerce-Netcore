// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Public.Controllers
{
    [Area("Public")]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly EcommerceDBContext _context;

        public HomeController(EcommerceDBContext context)
        {
            _context = context;
        }

        // GET
        // GET: Public/Producto
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var results = await _context.Productos.ToListAsync();

            return View(results.GetRange(0, 10));
        }
    }
}

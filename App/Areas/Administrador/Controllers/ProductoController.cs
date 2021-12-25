using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace App.Areas.Administrador.Controllers
{
	[Area("Administrador")]
	public class ProductoController : Controller
	{
		private readonly EcommerceDBContext _context;

		public ProductoController(EcommerceDBContext context)
		{
			_context = context;
		}

		// GET: Administrador/Producto
		public async Task<IActionResult> Index()
		{
			return View(await _context.Productos.ToListAsync());
		}

		// GET: Administrador/Producto/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productos = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productos == null)
			{
				return NotFound();
			}

			return View(productos);
		}

		// GET: Administrador/Producto/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Administrador/Producto/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind(
				"Sku,Nombre,Descripcion,PrecioNormal,DescuentoPrecio,Cantidad,ImagenPrincipal,Disponible")]
			Productos productos)
		{
			if (ModelState.IsValid)
			{
				_context.Add(productos);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(productos);
		}

		// GET: Administrador/Producto/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productos = await _context.Productos.FindAsync(id);
			if (productos == null)
			{
				return NotFound();
			}

			return View(productos);
		}

		// POST: Administrador/Producto/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id,
			[Bind(
				"Id,Sku,Nombre,Descripcion,PrecioNormal,DescuentoPrecio,Cantidad,ImagenPrincipal,Disponible,CreatedAt,UpdatedAt")]
			Productos productos)
		{
			if (id != productos.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(productos);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductosExists(productos.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction(nameof(Index));
			}

			return View(productos);
		}

		// GET: Administrador/Producto/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productos = await _context.Productos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productos == null)
			{
				return NotFound();
			}

			return View(productos);
		}

		// POST: Administrador/Producto/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var productos = await _context.Productos.FindAsync(id);
			_context.Productos.Remove(productos);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductosExists(long id)
		{
			return _context.Productos.Any(e => e.Id == id);
		}
	}
}
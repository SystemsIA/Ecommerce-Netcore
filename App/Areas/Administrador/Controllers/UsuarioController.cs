using System.Linq;
using System.Threading.Tasks;

using App.Attributes;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Administrador.Controllers
{
	
	
	[AuthorizeAdmin]
	[Area("Administrador")]
	public class UsuarioController : Controller
	{
		private readonly EcommerceDBContext _context;

		public UsuarioController(EcommerceDBContext context)
		{
			_context = context;
		}

		// GET: Administrador/Usuario
		public async Task<IActionResult> Index()
		{
			return View(await _context.Usuarios.ToListAsync());
		}

		// GET: Administrador/Usuario/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null) return NotFound();

			var usuarios = await _context.Usuarios
				.FirstOrDefaultAsync(m => m.Id == id);
			if (usuarios == null) return NotFound();

			return View(usuarios);
		}

		// GET: Administrador/Usuario/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Administrador/Usuario/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Email,Password,Nombre,Apellido,Active,Thumbnail,RegisteredAt,UpdatedAt")]
			Usuarios usuarios)
		{
			if (ModelState.IsValid)
			{
				_context.Add(usuarios);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(usuarios);
		}

		// GET: Administrador/Usuario/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null) return NotFound();

			var usuarios = await _context.Usuarios.FindAsync(id);
			if (usuarios == null) return NotFound();
			return View(usuarios);
		}

		// POST: Administrador/Usuario/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id,
			[Bind("Id,Email,Password,Nombre,Apellido,Active,Thumbnail,RegisteredAt,UpdatedAt")]
			Usuarios usuarios)
		{
			if (id != usuarios.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(usuarios);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UsuariosExists(usuarios.Id))
						return NotFound();
					throw;
				}

				return RedirectToAction(nameof(Index));
			}

			return View(usuarios);
		}

		// GET: Administrador/Usuario/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null) return NotFound();

			var usuarios = await _context.Usuarios
				.FirstOrDefaultAsync(m => m.Id == id);
			if (usuarios == null) return NotFound();

			return View(usuarios);
		}

		// POST: Administrador/Usuario/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var usuarios = await _context.Usuarios.FindAsync(id);
			_context.Usuarios.Remove(usuarios);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UsuariosExists(long id)
		{
			return _context.Usuarios.Any(e => e.Id == id);
		}
	}
}
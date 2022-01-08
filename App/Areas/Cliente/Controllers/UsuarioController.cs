using System.Threading.Tasks;

using App.Areas.Cliente.Data;
using App.Areas.Cliente.Services;

using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Cliente.Controllers
{
	[Area("Cliente")]
	[Controller]
	public class UsuarioController : Controller
	{
		private readonly IUserService _userService;

		public UsuarioController(IUserService userService)
		{
			_userService = userService;
		}

		[Route("registrarse")]
		public IActionResult Create()
		{
			return View();
		}

		[Route("registrarse"), HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UsuarioDto usuario)
		{
			if (await _userService.ExistUser(usuario.Email))
			{
				ViewData["message"] = "Este usuario ya existe";
				return View();
			}

			if (!ModelState.IsValid) return View(usuario);
			await _userService.CrearUsuario(usuario);
			return RedirectToAction("Login", "Cuenta");
		}
	}
}
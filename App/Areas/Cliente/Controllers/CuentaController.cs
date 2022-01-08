using System.Security.Claims;
using System.Threading.Tasks;

using App.Areas.Cliente.Data;
using App.Areas.Cliente.Services;
using App.Attributes;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Cliente.Controllers
{
	[Area("Cliente")]
	public class CuentaController : Controller
	{
		private string redirect = "";
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;

		public CuentaController(IUserService userService, IRoleService roleService)
		{
			_userService = userService;
			_roleService = roleService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (User is {Identity: {IsAuthenticated: true}})
				return RedirectToPage("/", new {area = ""});
			redirect = Request.Query["redirect"].ToString();
			ViewBag.redirect = redirect;
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLogin userLogin)
		{
			if (!string.IsNullOrEmpty(userLogin.Email) && string.IsNullOrEmpty(userLogin.Password))
			{
				return RedirectToAction("Login");
			}

			ClaimsIdentity identity = null;
			var isAuthenticated = false;
			if (!await _userService.ExistUser(userLogin.Email))
			{
				ViewBag.Message = "Este usuario no se encuentra registrado";
				return View();
			}

			var user = await _userService.GetUserByEmail(userLogin.Email);
			var rol = await _roleService.GetRoleById(user.RoleId);
			if (!_userService.VerifyCredentials(user.Password, userLogin.Password))
			{
				ViewBag.Message = "Por favor corrija su contraseña";
				return View();
			}

			if (!string.IsNullOrEmpty(userLogin.Email))
				identity = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Email, userLogin.Email),
					new Claim(ClaimTypes.Role, rol.Nombre),
					new Claim(ClaimTypes.Name, user.Nombre),
					new Claim(ClaimTypes.Surname, user.Apellido)
				}, CookieAuthenticationDefaults.AuthenticationScheme);
			isAuthenticated = true;

			if (!isAuthenticated) return View();
			if (identity == null) return RedirectToAction("Index", "Home");
			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			return rol.Nombre == AuthorizeRoles.Admin
				? RedirectToAction("Index", "Producto", new {area = "Administrador"})
				: !string.IsNullOrEmpty(redirect)
					? Redirect(redirect)
					: Redirect("/");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Cuenta");
		}
	}
}
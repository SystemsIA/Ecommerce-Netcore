using App.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Administrador.Controllers
{
	[AuthorizeAdmin]
	[Area("Administrador")]
	public class AdminHomeController : Controller
	{
		// GET
		public IActionResult Index()
		{
			return View();
		}
	}
}
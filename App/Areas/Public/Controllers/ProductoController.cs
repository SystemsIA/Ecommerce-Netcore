using System.Threading.Tasks;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Public.Controllers
{
    [Area("Public")]
    public class ProductoController : Controller
    {
        private readonly EcommerceDBContext _context;

        public ProductoController(EcommerceDBContext context)
        {
            _context = context;
        }

        // GET: Public/Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Public/Producto/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null) return NotFound();

            return View(productos);
        }
    }
}

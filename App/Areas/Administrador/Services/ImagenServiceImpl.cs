using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Administrador.Services
{
	public class ImagenServiceImpl : IImagenService
	{
		private readonly EcommerceDBContext _context;
		private readonly IWebHostEnvironment _environment;

		public ImagenServiceImpl(EcommerceDBContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}

		public async Task<bool> GuardarImagen(IFormFile imagen, long objectId)
		{
			if (imagen is not {Length: > 0}) return false;

			var pathBase = Path.Combine(_environment.WebRootPath, "imagenes\\");

			if (!Directory.Exists(pathBase))
			{
				Directory.CreateDirectory(pathBase);
			}

			var pathImagen = Path.Combine(_environment.WebRootPath, "imagenes\\productos\\", objectId + "\\");
			if (!Directory.Exists(pathImagen))
			{
				Directory.CreateDirectory(pathImagen);
			}

			var nameFile = Guid.NewGuid() + Path.GetExtension(imagen.FileName).ToLower();
			var relativePath = pathImagen + nameFile;
			await using var stream = new FileStream(relativePath, FileMode.Create);
			await imagen.CopyToAsync(stream);

			await _context.Imagenes.AddAsync(new Imagenes {NombreImagen = nameFile, ProductoId = objectId});
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<Imagenes> GetById(long id)
		{
			return await _context.Imagenes.FirstOrDefaultAsync(q => q.ProductoId == id);
		}

		public async Task<ICollection<Imagenes>> GetListById(long id)
		{
			return await _context.Imagenes.Where(q => q.ProductoId.Equals(id)).ToListAsync();
		}
	}
}
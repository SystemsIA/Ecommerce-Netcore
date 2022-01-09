using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Models;

using Microsoft.AspNetCore.Http;

namespace App.Areas.Administrador.Services
{
	public interface IImagenService
	{
		public Task<bool> GuardarImagen(IFormFile imagen, long objectId);
		public Task<Imagenes> GetById(long id);
		public Task<bool> EliminarImagen(long id);
		public Task<ICollection<Imagenes>> GetListByIdAsync(long id);
		public ICollection<Imagenes> GetListById(long id);
	}
}
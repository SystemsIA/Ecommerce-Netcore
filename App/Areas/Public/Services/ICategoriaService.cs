using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Models;

namespace App.Areas.Public.Services
{
	public interface ICategoriaService
	{
		public Task<ICollection<Categorias>> GetAll();
		public Task<Categorias> GetByNombre(string nombre);
		public Task<ICollection<Productos>> GetProductosByCategoriaId(long id);
	}
}
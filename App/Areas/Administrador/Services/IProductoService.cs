#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;

using App.Areas.Administrador.Data;

using Domain.Models;

namespace App.Areas.Administrador.Services
{
	public interface IProductoService
	{
		public Task<Productos> CrearProducto(ProductoDto dto);
		public Task<Productos> GetById(long? id);
		public Task<bool> DeleteProducto(long id);
		public Task<Productos> GetBySKU(string sku);
		public Task<Productos> UpdateProducto(ProductoDto dto);
		public Task<ICollection<Productos>> GetAll(int? count);
		public bool ProductoExists(long id);
	}
}
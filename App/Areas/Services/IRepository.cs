using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Areas.Services
{
	public interface IRepository<T>
	{
		public Task<T> GetById(int id);
		public Task<bool> ExistObject(string name);
		public Task<ICollection<T>> GetList();
		public Task<T> Save(T obj);
	}
}
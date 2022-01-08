using System.Threading.Tasks;

using Domain.Models;

namespace App.Areas.Cliente.Services
{
	public interface IRoleService
	{
		public Task<Roles> GetRoleById(long id);
	}
}
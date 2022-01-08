using System.Threading.Tasks;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace App.Areas.Cliente.Services
{
	public class RoleServiceImpl : IRoleService
	{
		public readonly EcommerceDBContext _context;

		public RoleServiceImpl(EcommerceDBContext context)
		{
			_context = context;
		}

		public async Task<Roles> GetRoleById(long id)
		{
			return await _context.Roles.FindAsync(id);
		}
	}
}
using System.Linq;
using System.Threading.Tasks;

using App.Areas.Cliente.Data;
using App.Attributes;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace App.Areas.Cliente.Services
{
	public class UserServiceImpl : IUserService
	{
		private readonly EcommerceDBContext _context;

		public UserServiceImpl(EcommerceDBContext context)
		{
			_context = context;
		}

		public async Task<Usuarios> CrearUsuario(UsuarioDto usuario)
		{
			var createdUser = new Usuarios
			{
				Apellido = usuario.Apellido,
				Email = usuario.Email,
				Nombre = usuario.Nombre,
				Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password),
				Thumbnail = $"https://ui-avatars.com/api/?name={usuario.Nombre}+{usuario.Apellido}&background=random"
			};
			var rolUser = await _context.Roles.FirstOrDefaultAsync(c => c.Nombre == AuthorizeRoles.User);
			var rolAdmin = await _context.Roles.FirstOrDefaultAsync(c => c.Nombre == AuthorizeRoles.Admin);

			createdUser.RoleId = usuario.Email.Contains("admin") ? rolAdmin.Id : rolUser.Id;

			_context.Add(createdUser);
			await _context.SaveChangesAsync();
			return createdUser;
		}

		public bool VerifyCredentials(string pass, string passDto)
		{
			return BCrypt.Net.BCrypt.Verify(passDto, pass);
		}

		public async Task<bool> ExistUser(string email)
		{
			var u = await _context.Usuarios.FirstOrDefaultAsync(q => q.Email == email);
			return u != null;
		}

		public async Task<Usuarios> GetUserByEmail(string email)
		{
			return await _context.Usuarios.FirstOrDefaultAsync(q => q.Email == email);
		}
	}
}
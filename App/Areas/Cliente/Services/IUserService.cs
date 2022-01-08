using System.Threading.Tasks;

using App.Areas.Cliente.Data;

using Domain.Models;

namespace App.Areas.Cliente.Services
{
	public interface IUserService
	{
		public Task<Usuarios> CrearUsuario(UsuarioDto usuario);
		public bool VerifyCredentials(string pass, string passDto);
		public Task<bool> ExistUser(string email);
		public Task<Usuarios> GetUserByEmail(string email);
	}
}
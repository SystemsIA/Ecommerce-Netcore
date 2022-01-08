using App.Areas.Administrador.Services;
using App.Areas.Cliente.Services;

using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
	public static class ServicesClassExtensions
	{
		public static IServiceCollection AddServicesClassApp(this IServiceCollection service)
		{
			service.AddScoped<IUserService, UserServiceImpl>();
			service.AddScoped<IRoleService, RoleServiceImpl>();
			service.AddScoped<IImagenService, ImagenServiceImpl>();
			service.AddScoped<IProductoService, ProductoServiceImpl>();

			return service;
		}
	}
}
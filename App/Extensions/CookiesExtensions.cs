using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
	public static class CookiesExtensions
	{
		public static IServiceCollection AddAuthenticateUserApp(this IServiceCollection services)
		{
			services.AddSession(options =>
			{
				options.Cookie.SameSite = SameSiteMode.Lax;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.IsEssential = true;
			});
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = _ => true;
				options.HttpOnly = HttpOnlyPolicy.Always;
				options.MinimumSameSitePolicy = SameSiteMode.Lax;
			});

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
			{
				config.LogoutPath = "/Cliente/Cuenta/login";
				config.LoginPath = "/Cliente/Cuenta/login";
				config.AccessDeniedPath = "/Error/Forbbiden";
			});

			return services;
		}
	}
}

using Microsoft.AspNetCore.Authorization;

namespace App.Attributes
{
	public class AuthorizeAdmin : AuthorizeAttribute
	{
		public AuthorizeAdmin()
		{
			Roles = AuthorizeRoles.Admin;
		}
	}
}
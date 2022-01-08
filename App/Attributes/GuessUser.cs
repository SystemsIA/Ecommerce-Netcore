using System;

using Microsoft.AspNetCore.Mvc;

namespace App.Attributes
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class GuessUser : RouteAttribute
	{
		public GuessUser(string template) : base(template)
		{
			
		}
		
	}
}
// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using App.Areas.Cliente.Data;

using Domain.Models;

namespace App.Areas.Cliente.Utils
{
	public static class Extensions
	{
		public static UsuarioDto AsDto(this Usuarios usuario)
		{
			return new UsuarioDto
			{
				Apellido = usuario.Apellido,
				Email = usuario.Email,
				Nombre = usuario.Nombre,
				Password = usuario.Password,
				Thumbnail = usuario.Thumbnail
			};
		}
	}
}
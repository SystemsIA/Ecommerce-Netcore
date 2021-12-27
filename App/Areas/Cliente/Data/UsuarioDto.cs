// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace App.Areas.Cliente.Data
{
    public class UsuarioDto
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Apellido { get; set; }
        [Required] public string Thumbnail { get; set; }
    }
}

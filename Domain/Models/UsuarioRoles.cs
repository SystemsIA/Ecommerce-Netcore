using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class UsuarioRoles
    {
        public long UsuarioId { get; set; }
        public long RolId { get; set; }

        public virtual Roles Rol { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}

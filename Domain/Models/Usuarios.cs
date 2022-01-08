using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class Usuarios
    {
        public Usuarios()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Active { get; set; }
        public string Thumbnail { get; set; }
        public long RolId { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}

using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
	public class Roles
	{
		public Roles()
		{
			Usuarios = new HashSet<Usuarios>();
		}

		public long Id { get; set; }
		public string Nombre { get; set; }
		public virtual ICollection<Usuarios> Usuarios { get; set; }
	}
}
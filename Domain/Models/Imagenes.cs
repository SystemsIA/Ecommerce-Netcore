using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Imagenes
    {
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public string NombreImagen { get; set; }

        public virtual Productos Producto { get; set; }
    }
}

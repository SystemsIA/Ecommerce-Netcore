using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Imagenes
    {
        public long ProductoId { get; set; }
        public string RutaImagen { get; set; }

        public virtual Productos Producto { get; set; }
    }
}

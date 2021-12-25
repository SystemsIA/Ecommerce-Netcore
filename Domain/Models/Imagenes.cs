#nullable disable

namespace Domain.Models
{
    public class Imagenes
    {
        public long ProductoId { get; set; }
        public string RutaImagen { get; set; }

        public virtual Productos Producto { get; set; }
    }
}

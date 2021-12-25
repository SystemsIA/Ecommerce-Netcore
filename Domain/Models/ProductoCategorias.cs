#nullable disable

namespace Domain.Models
{
    public class ProductoCategorias
    {
        public long CategoriaId { get; set; }
        public long ProductoId { get; set; }

        public virtual Categorias Categoria { get; set; }
        public virtual Productos Producto { get; set; }
    }
}

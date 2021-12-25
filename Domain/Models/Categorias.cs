#nullable disable

namespace Domain.Models
{
    public class Categorias
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long? ParentId { get; set; }
    }
}

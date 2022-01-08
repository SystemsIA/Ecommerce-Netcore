using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Categorias
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long? ParentId { get; set; }
    }
}

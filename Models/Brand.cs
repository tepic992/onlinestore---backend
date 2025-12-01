using System.Collections.Generic;

namespace OnlineStore.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }

        // Navigation
        public ICollection<Product>? Products { get; set; }
    }
}


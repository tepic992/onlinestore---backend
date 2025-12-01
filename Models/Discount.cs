using System;

namespace OnlineStore.Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal Percentage { get; set; } // e.g. 10 for 10%
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}


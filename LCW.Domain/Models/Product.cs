using System;
using System.Collections.Generic;

#nullable disable

namespace LCW.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            Offers = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool IsOfferable { get; set; }
        public bool IsSold { get; set; }
        public int UserId { get; set; }
        public decimal StartPrice { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string Description { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Color Color { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}

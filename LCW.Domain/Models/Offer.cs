using System;
using System.Collections.Generic;

#nullable disable

namespace LCW.Domain.Models
{
    public partial class Offer
    {
        public int Id { get; set; }
        public int LastUserId { get; set; }
        public int ProductId { get; set; }
        public decimal LastOfferPrice { get; set; }

        public virtual User LastUser { get; set; }
        public virtual Product Product { get; set; }
    }
}

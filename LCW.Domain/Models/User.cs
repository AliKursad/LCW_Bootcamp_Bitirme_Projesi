using System;
using System.Collections.Generic;

#nullable disable

namespace LCW.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Offers = new HashSet<Offer>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

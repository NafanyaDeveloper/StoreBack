using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Entities
{
    public class Customer: IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public string Address { get; set; }

        public int Discount { get; set; } = 0;

        public List<Order> Orders { get; set; } = new List<Order>();

        public Guid? UserId { get; set; }

        public User User { get; set; }
    }
}

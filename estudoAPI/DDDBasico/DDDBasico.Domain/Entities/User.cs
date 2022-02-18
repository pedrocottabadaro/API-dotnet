using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDBasico.Domain.Entities
{
    public class User
    {
        public int Id{ get; set; }
        public String? Username { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public String? email { get; set; }
        public int drink_counter { get; set; }
    }
}
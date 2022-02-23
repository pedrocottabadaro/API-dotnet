using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public String? UserName { get; set; }
        public String? email { get; set; }
        public int drink_counter { get; set; }
    }
}

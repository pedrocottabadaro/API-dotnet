using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.DTO
{
    public class JwtDTO : UserDTO
    {
        public String? Token { get; set; }
    }
}

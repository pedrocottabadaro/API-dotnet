using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public int Iduser { get; set; }
        public DateTime Data { get; set; }

        public int drink_amount { get; set; }

    }
}

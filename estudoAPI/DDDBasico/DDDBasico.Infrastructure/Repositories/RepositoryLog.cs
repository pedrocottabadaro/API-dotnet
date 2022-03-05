using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Infrastructure.Repositories
{
    public class RepositoryLog : RepositoryBase<Log>, IRepositoryLog 
    {
        private readonly ApplicationDbContext _context;

        public RepositoryLog(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Object GetRanking()
        {
       

            var currentDateTimeStart = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
            var currentDateTimeEnd = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
            var result= from u in _context.Users
                        join l in _context.Log on u.Id equals l.Iduser
                        where l.Data>=DateTime.Parse(currentDateTimeStart) && l.Data <= DateTime.Parse(currentDateTimeEnd)
                        group l by new{ l.Iduser,u.UserName}  into g
                        select new
                        {
                            username = g.Key.UserName,
                            drink_counter = g.Sum(log => log.drink_amount)
                        };

            return result;
        }

        public List<Log> GetUserLog(int IdUser)
        {
            return _context.Log.Where(log => log.Iduser == IdUser).ToList();
        }
    }
}

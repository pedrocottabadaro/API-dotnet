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

        public User GetRanking()
        {
            

        }

        public List<Log> GetUserLog(int IdUser)
        {
            return _context.Log.Where(log => log.Iduser == IdUser).ToList();
        }
    }
}

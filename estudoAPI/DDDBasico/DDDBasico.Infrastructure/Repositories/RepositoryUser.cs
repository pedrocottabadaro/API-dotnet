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
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser 
    {
        private readonly ApplicationDbContext _context;

        public RepositoryUser(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool checkUserExists(String username)
        {
            try
            {
                return _context.Users.Any(stored_users => stored_users.UserName == username.ToLower());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

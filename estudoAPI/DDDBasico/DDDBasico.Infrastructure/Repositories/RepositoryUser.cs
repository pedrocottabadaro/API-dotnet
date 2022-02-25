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

        public User checkUserExists(String email)
        {
            try
            {
                return _context.Users.FirstOrDefault(stored_users => stored_users.email == email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

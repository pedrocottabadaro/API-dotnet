using Microsoft.EntityFrameworkCore;
using DDDBasico.Domain.Entities;

namespace DDDBasico.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){

        }
        
        public DbSet<User>? Users{ get; set; }
    }
}
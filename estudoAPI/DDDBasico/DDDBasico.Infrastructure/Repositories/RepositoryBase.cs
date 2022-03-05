using DDDBasico.Domain.Interfaces;
using DDDBasico.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDBasico.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {

        private readonly ApplicationDbContext _context;
        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity obj)
        {

            _context.Set<TEntity>().Add(obj);
            _context.SaveChangesAsync();

        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChangesAsync();
        }

        public void Update(TEntity obj)
        {

            _context.Set<TEntity>().Update(obj);
            _context.SaveChangesAsync();
        }
    
        public IEnumerable<TEntity> GetAll()
        {
           return _context.Set<TEntity>().ToList();
           
        }

        public TEntity GetById(int id)
        {       
           return _context.Set<TEntity>().Find(id);       
          
        }

        public void Dispose()
        {
            /*throw new NotImplementedException();*/
        }
    }
}
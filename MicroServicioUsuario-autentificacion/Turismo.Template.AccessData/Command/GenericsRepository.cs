using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turismo.Template.AccessData.Context;
using Turismo.Template.Domain.Commands;

namespace Turismo.Template.AccessData.Command
{
    public class GenericsRepository : IRepositoryGeneric
    {
        private readonly DbContextGeneric _context;
        public GenericsRepository(DbContextGeneric dbContext)
        {
            _context = dbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteBy<T>(int id) where T : class
        {
            T entity = FindBy<T>(id);
            Delete<T>(entity);
        }

        public T FindBy<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Traer<T>() where T : class
        {
            List<T> query = _context.Set<T>().ToList();
            return query;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.AccessData.Command
{
    public class GenericsRepository : IRepositoryGeneric
    {
        protected DbContextGeneric Context;
        public GenericsRepository(DbContextGeneric contexto)
        {
            this.Context = contexto;
        }

        public void Add<T>(T entity) where T : class
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void DeleteById<T>(int id) where T : class
        {
            T entity = FindBy<T>(id);
            Delete<T>(entity);
        }



        public T FindBy<T>(int id) where T : class
        {
            return Context.Set<T>().Find(id);
        }

        public List<T> Traer<T>() where T : class
        {
            List<T> query = Context.Set<T>().ToList();
            return query;
        }

        public void Update<T>(T entity) where T : class
        {
            Context.ChangeTracker.Clear(); // tuve que agregar esto
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}

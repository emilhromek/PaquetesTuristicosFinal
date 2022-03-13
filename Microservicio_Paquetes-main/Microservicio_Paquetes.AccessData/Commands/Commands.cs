using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microservicio_Paquetes.Domain.Commands;

namespace Microservicio_Paquetes.AccessData.Commands
{
    public class Commands : ICommands
    {
        private readonly PaquetesDbContext _context;
        public Commands(PaquetesDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Agregar<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Borrar<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void BorrarPor<T>(int id) where T : class
        {
            T entity = FindBy<T>(id);
            Borrar<T>(entity);
        }

        private T FindBy<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public void Actualizar<T>(T entity) where T : class
        {
            //_context.ChangeTracker.Clear(); // tuve que agregar esto

            //_context.Set<T>().Attach(entity);
            _context.Set<T>().Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //Agregado por Emil
        public void ActualizarPor<T>(int id) where T : class
        {
            T entity = FindBy<T>(id);
            Actualizar<T>(entity);
        }
    }
}

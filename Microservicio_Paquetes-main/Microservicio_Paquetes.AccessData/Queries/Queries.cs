using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microservicio_Paquetes.Domain.Queries;

namespace Microservicio_Paquetes.AccessData.Queries
{
    public class Queries : IQueries
    {
        private readonly PaquetesDbContext _context;
        public Queries(PaquetesDbContext dbContext)
        {
            _context = dbContext;
        }

        public T EncontrarPor<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Traer<T>() where T : class
        {
            List<T> query = _context.Set<T>().ToList();
            return query;
        }
    }
}

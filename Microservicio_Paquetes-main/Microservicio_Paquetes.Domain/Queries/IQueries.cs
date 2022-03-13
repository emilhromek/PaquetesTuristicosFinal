using System;
using System.Collections.Generic;
using System.Text;


namespace Microservicio_Paquetes.Domain.Queries
{
    public interface IQueries
    {
        List<T> Traer<T>() where T : class;
        T EncontrarPor<T>(int id) where T : class;        
    }
}

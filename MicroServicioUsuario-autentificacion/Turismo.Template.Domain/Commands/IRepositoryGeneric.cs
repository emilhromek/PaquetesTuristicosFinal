using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.Commands
{
    public interface IRepositoryGeneric
    {
        void Add<T>(T entity) where T : class;
        List<T> Traer<T>() where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteBy<T>(int id) where T : class;
        T FindBy<T>(int id) where T : class;
    }
}

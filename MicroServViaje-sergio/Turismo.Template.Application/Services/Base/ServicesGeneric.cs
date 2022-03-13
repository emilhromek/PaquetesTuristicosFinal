using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turismo.Template.Domain.Commands;

namespace Turismo.Template.Application.Services.Base
{
    public class ServicesGeneric : IServicesGeneric
    {
        protected IRepositoryGeneric Repository;

        public ServicesGeneric(IRepositoryGeneric repository)
        {
            this.Repository = repository;

        }

        public void Add<T>(T entity) where T : class
        {
            Repository.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Repository.Delete<T>(entity);
        }

        public void DeleteById<T>(int id) where T : class
        {
            Repository.DeleteById<T>(id);
        }

        public T FindBy<T>(int id) where T : class
        {
            return Repository.FindBy<T>(id);
        }

        public List<T> Traer<T>() where T : class
        {
            return Repository.Traer<T>();
        }

        public void Update<T>(T entity) where T : class
        {
            Repository.Update<T>(entity);
        }

    }
}

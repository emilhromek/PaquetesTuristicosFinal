using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Application.Services
{
    public interface IMetodoPagoServices
    {
        MetodoPago Create(MetodoPagoDto pago);
        IEnumerable<MetodoPagoDtoById> getAll();
        MetodoPagoDtoById getId(int id);
        void deleteId(int id);
    }
    public class MetodoPagoServices : IMetodoPagoServices
    {
        private readonly IRepositoryGeneric _repository;
        public MetodoPagoServices(IRepositoryGeneric repository)
        {
            _repository = repository;
        }
        public MetodoPago Create(MetodoPagoDto pago)
        {
            var entity = new MetodoPago
            {
                Nombre = pago.Nombre,
                
            };
            _repository.Add<MetodoPago>(entity);
            return entity;
        }

        public void deleteId(int id)
        {
            _repository.DeleteBy<MetodoPago>(id);
        
        }

        public IEnumerable<MetodoPagoDtoById> getAll()
        {
            var metodos = _repository.Traer<MetodoPago>();
            List<MetodoPagoDtoById> listametodos = new List<MetodoPagoDtoById>();
            foreach(var mp in metodos)
            {
                MetodoPagoDtoById metodo = new MetodoPagoDtoById
                {
                    MetodoPagoId = mp.MetodoPagoId,
                    Nombre = mp.Nombre
                };
                listametodos.Add(metodo);
            }
            return listametodos;
        }

        public MetodoPagoDtoById getId(int id)
        {
            var metodo = _repository.FindBy<MetodoPago>(id);
            if (metodo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new MetodoPagoDtoById
            {
                MetodoPagoId = metodo.MetodoPagoId,
                Nombre = metodo.Nombre
            };
        }
    }
}

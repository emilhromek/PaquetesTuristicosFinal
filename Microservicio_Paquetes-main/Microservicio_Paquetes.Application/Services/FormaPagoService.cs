using System;
using System.Collections.Generic;
using System.Text;
using Microservicio_Paquetes.Domain.Entities;
using Microservicio_Paquetes.Domain.DTO;
using Microservicio_Paquetes.Domain.Commands;
using Microservicio_Paquetes.Domain.Queries;
using Microservicio_Paquetes.Domain.Responses;

namespace Microservicio_Paquetes.Application.Services
{
    public interface IFormaPagoService
    {
        public object GetFormaPagoId(int id);
        public object GetFormasPago();
    }

    public class FormaPagoService : IFormaPagoService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public FormaPagoService(ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }
        public object GetFormaPagoId(int id)
        {
            var formaPago = _queries.EncontrarPor<FormaPago>(id);

            if (formaPago == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Forma de pago con el id: " + id + " no encontrada."
                };
            }

            var output = new FormaPagoOutDto()
            {
                Id = formaPago.Id,
                Descripcion = formaPago.Descripcion,
            };

            return output;
        }

        public object GetFormasPago()
        {
            var formasPago = _queries.Traer<FormaPago>();

            if (formasPago.Count == 0)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No hay formas de pago."
                };
            }

            var listaOutput = new List<FormaPagoOutDto>();

            foreach(FormaPago x in _queries.Traer<FormaPago>())
            {
                listaOutput.Add(new FormaPagoOutDto { Id = x.Id, Descripcion = x.Descripcion });
            }

            return listaOutput;
        }
    }
}

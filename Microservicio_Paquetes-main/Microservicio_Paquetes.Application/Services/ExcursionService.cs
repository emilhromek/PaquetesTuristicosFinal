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
    public interface IExcursionService
    {
        public Response PostExcursion(ExcursionDto excursion);
        public object GetExcursionId(int id);
        public object GetExcursiones(string idDestino);
        public Response ActualizarExcursion(int Id, ExcursionDto excursion);
        public Response BorrarExcursion(int id);
    }

    public class ExcursionService : IExcursionService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public ExcursionService(ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public Response PostExcursion(ExcursionDto excursion)
        {
            if (excursion.Titulo.Length > 50)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "El nombre de la excursión supera los 50 caracteres.",
                };
            }

            if (excursion.Descripcion.Length > 50)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "La descripción de la excursión supera los 255 caracteres.",
                };
            }

            //Destino getDestino = _queries.EncontrarPor<Destino>(excursion.DestinoId);

            //if (getDestino == null)
            //{
            //    return new Response()
            //    {
            //        Code = "BAD_REQUEST",
            //        Message = "Destino con el id: " + excursion.DestinoId + " no existe."
            //    };
            //}

            Excursion nuevaExcursion = new Excursion()
            {
                Titulo = excursion.Titulo,
                Descripcion = excursion.Descripcion,
                Precio = excursion.Precio,
                Duracion = excursion.Duracion,
                DestinoId = excursion.DestinoId,
            };

            _commands.Agregar<Excursion>(nuevaExcursion);

            return new Response()
            {
                Code = "CREATED",
                Message = "Excursión con el id: " + nuevaExcursion.Id + " y título: '" + nuevaExcursion.Titulo + "' creada."
            };
        }

        public object GetExcursionId(int id)
        {
            var excursion = _queries.EncontrarPor<Excursion>(id);

            if (excursion == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Excursión con el id: " + id + " no encontrada."
                };
            }

            var destino = _queries.EncontrarPor<Destino>(excursion.DestinoId);

            var output = new ExcursionOutDto()
            {
                Id = excursion.Id,
                Titulo = excursion.Titulo,
                Descripcion = excursion.Descripcion,
                Precio = excursion.Precio,
                Duracion = excursion.Duracion,
                Destino = new DestinoOutDto {
                    Id = destino.Id,
                    Lugar = destino.Lugar,
                    Descripcion = destino.Descripcion,
                    Atractivo = destino.Atractivo,
                    Historia = destino.Historia,
                    Imagen = destino.Imagen,
                },
            };

            return output;
        }

        public object GetExcursiones(string idDestino)
        {
            var excursiones = _queries.Traer<Excursion>();

            var excursionesDestino = new List<ExcursionOutDto>();

            if (idDestino.Equals(""))
            {
                if (excursiones.Count == 0)
                {
                    return new Response()
                    {
                        Code = "NOT_FOUND",
                        Message = "No hay excursiones."
                    };
                }

                foreach (Excursion x in excursiones)
                {
                    var destino = _queries.EncontrarPor<Destino>(x.DestinoId);

                    var output = new ExcursionOutDto()
                    {
                        Id = x.Id,
                        Titulo = x.Titulo,
                        Descripcion = x.Descripcion,
                        Precio = x.Precio,
                        Duracion = x.Duracion,
                        Destino = new DestinoOutDto
                        {
                            Id = destino.Id,
                            Lugar = destino.Lugar,
                            Descripcion = destino.Descripcion,
                            Atractivo = destino.Atractivo,
                            Historia = destino.Historia,
                            Imagen = destino.Imagen,
                        },
                    };

                    excursionesDestino.Add(output);
                }

                return excursionesDestino;
            }

            if (_queries.EncontrarPor<Destino>(Int32.Parse(idDestino)) == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Destino con el id: " + idDestino + " no encontrado."
                };
            }      

            foreach (Excursion x in excursiones)
            {
                if (Int32.Parse(idDestino) == x.DestinoId)
                {
                    var destino = _queries.EncontrarPor<Destino>(Int32.Parse(idDestino));

                    var output = new ExcursionOutDto()
                    {
                        Id = x.Id,
                        Titulo = x.Titulo,
                        Descripcion = x.Descripcion,
                        Precio = x.Precio,
                        Duracion = x.Duracion,
                        Destino = new DestinoOutDto
                        {
                            Id = destino.Id,
                            Lugar = destino.Lugar,
                            Descripcion = destino.Descripcion,
                            Atractivo = destino.Atractivo,
                            Historia = destino.Historia,
                            Imagen = destino.Imagen,
                        },
                    };

                    excursionesDestino.Add(output);
                }
            }

            if (excursionesDestino.Count == 0)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No hay excursiones."
                };
            }

            return excursionesDestino;
        }

        public Response ActualizarExcursion(int id, ExcursionDto excursionDTO)
        {
            //Destino getDestino = _queries.EncontrarPor<Destino>(excursionDTO.DestinoId);

            //if (getDestino == null)
            //{
            //    return new Response()
            //    {
            //        Code = "BAD_REQUEST",
            //        Message = "Destino con el id: " + excursionDTO.DestinoId + " no existe."
            //    };
            //}

            var excursion = _queries.EncontrarPor<Excursion>(id);

            if (excursion == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Excursion con el id: " + id + " no existe."
                };
            }

            excursion.Titulo = excursionDTO.Titulo;
            excursion.Descripcion = excursionDTO.Descripcion;
            excursion.Precio = excursionDTO.Precio;
            excursion.Duracion = excursionDTO.Duracion;

            _commands.Actualizar<Excursion>(excursion);

            return new Response()
            {
                Code = "UPDATED",
                Message = "Excursión con el id: " + excursion.Id + " y título: '" + excursion.Titulo + "' actualizada."
            };
        }

        public Response BorrarExcursion (int id)
        {
            var check = _queries.EncontrarPor<Excursion>(id);

            if (check == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Excursion con el id: " + id + " no encontrado."
                };
            }

            _commands.Borrar<Excursion>(check);

            return new Response()
            {
                Code = "OK",
                Message = "Excursion con el id: " + id + " borrada."
            };
        }
    }
}

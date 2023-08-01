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
    public interface IDestinoService
    {
        public Response PostDestino(DestinoDto destino);
        public object GetDestinoId(int id);
        public object GetDestinos();
        public Response ActualizarDestino(int Id, DestinoDto destino);
        public Response BorrarDestino(int Id);

    }

    public class DestinoService : IDestinoService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public DestinoService(ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public Response PostDestino(DestinoDto destino)
        {            
            Destino nuevoDestino = new Destino()
            {
                Lugar = destino.Lugar,
                Descripcion = destino.Descripcion,
                Atractivo = destino.Atractivo,
                Historia = destino.Historia,
                HorasDeViaje = destino.HorasDeViaje,
                Imagen = destino.Imagen,
            };

            _commands.Agregar<Destino>(nuevoDestino);

            return new Response()
            {
                Code = "CREATED",
                Message = "Destino con el id: " + nuevoDestino.Id + " y nombre de lugar: '" + nuevoDestino.Lugar + "' creado."
            };
        }
        public object GetDestinoId(int id)
        {
            var destino = _queries.EncontrarPor<Destino>(id);

            if (destino == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Destino con el id: " + id + " no encontrado."
                };
            }

            var output = new DestinoOutDto()
            {
                Id = destino.Id,
                Lugar = destino.Lugar,
                Descripcion = destino.Descripcion,
                Atractivo = destino.Atractivo,
                Historia = destino.Historia,
                HorasDeViaje = destino.HorasDeViaje,
                Imagen = destino.Imagen,
            };

            return output;
        }

        public object GetDestinos()
        {
            var destinos = _queries.Traer<Destino>();

            if (destinos.Count == 0)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No hay destinos."
                };
            }

            var listaOutput = new List<DestinoOutDto>();

            foreach (Destino x in destinos)
            {
                var output = new DestinoOutDto()
                {
                    Id = x.Id,
                    Lugar = x.Lugar,
                    Descripcion = x.Descripcion,
                    Atractivo = x.Atractivo,
                    Historia = x.Historia,
                    HorasDeViaje = x.HorasDeViaje,
                    Imagen = x.Imagen,
                };

                listaOutput.Add(output);
            }

            return listaOutput;
        }

        public Response ActualizarDestino(int Id, DestinoDto destinoDTO)
        {
            var destino = _queries.EncontrarPor<Destino>(Id);

            if (destino == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Destino con el id: " + Id + " no encontrado."
                };
            }

            destino.Lugar = destinoDTO.Lugar;
            destino.Descripcion = destinoDTO.Descripcion;
            destino.Atractivo = destinoDTO.Atractivo;
            destino.Historia = destinoDTO.Historia;
            destino.HorasDeViaje = destinoDTO.HorasDeViaje;
            destino.Imagen = destinoDTO.Imagen;

            //Destino nuevoDestino = new Destino()
            //{
            //    Id = Id,
            //    Lugar = destino.Lugar,
            //    Descripcion = destino.Descripcion,
            //    Atractivo = destino.Atractivo,
            //    Historia = destino.Historia,
            //    HorasDeViaje = destino.HorasDeViaje,
            //    Imagen = destino.Imagen,
            //};

            _commands.Actualizar<Destino>(destino);

            return new Response()
            {
                Code = "UPDATED",
                Message = "Destino con el id: " + destino.Id + " y nombre de lugar: '" + destino.Lugar + "' actualizado."
            };
        }

        public Response BorrarDestino(int destinoId)
        {
            var check = _queries.EncontrarPor<Destino>(destinoId);

            if (check == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Destino con el id: " + destinoId + " no encontrado."
                };
            }

            _commands.Borrar<Destino>(check);

            // ok, este es el mas complicado de todos. al borrar un destino, hay que borrar tambien
            // todos los hoteles asociados
            // todos las excursiones
            // todos los paquetes, con sus tablas paqueteexcursion
            // hay que dejar huerfanas a las reservas
            // ademas hay que borrar todos los grupos y viajes

            //// borramos hoteles:

            //foreach(var hotel in _queries.Traer<Hotel>())
            //{
            //    if (hotel.DestinoId == destinoId)
            //    {
            //        _commands.Borrar(hotel);
            //    }
            //}

            //// borramos excursiones

            //foreach (var excursion in _queries.Traer<Excursion>())
            //{
            //    if (excursion.DestinoId == destinoId)
            //    {
            //        _commands.Borrar(excursion);
            //    }
            //}

            //// borramos paquetes y paqueteexcursion

            //foreach (var paquete in _queries.Traer<Paquete>())
            //{
            //    if (paquete.DestinoId == destinoId)
            //    {
            //        _commands.Borrar(paquete);

            //        foreach (var paqueteExcursion in _queries.Traer<PaqueteExcursion>())
            //        {
            //            if (paqueteExcursion.PaqueteId == paquete.Id)
            //            {
            //                _commands.Borrar(paqueteExcursion);
            //            }
            //        }

            //    }
            //}

            //// falta marca reservas y borrar grupos y viajes

            //foreach (var reserva in _queries.Traer<Reserva>())
            //{
            //    var destinoDeReserva = _queries.EncontrarPor<Paquete>(reserva.PaqueteId).DestinoId;

            //    if (destinoDeReserva == destinoId)
            //    {
            //        reserva.PaqueteId = 0;
            //        reserva.GrupoId = 0;
            //        reserva.ViajeId = 0;

            //        // faltaria alguna anotacion para dejar en claro los detalles del paquete borrado

            //        _commands.Actualizar<Reserva>(reserva);

            //        // no olvidar borrar todos los grupos y viajes asociados al paquete
            //    }
            //}








            return new Response()
            {
                Code = "OK",
                Message = "Destino con el id: " + destinoId + " borrado."
            };
        }
    }
}
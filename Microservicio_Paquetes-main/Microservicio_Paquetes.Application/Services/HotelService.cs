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
    public interface IHotelService
    {
        public Response PostHotel(HotelDto hotel);
        public object GetHotelId(int id);
        public object GetHoteles(string idDestino);
        public Response ActualizarHotel(int Id, HotelDto hotel);
        public Response BorrarHotel(int id);

    }

    public class HotelService: IHotelService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public HotelService (ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public Response PostHotel(HotelDto hotel)
        {
            Destino getDestino = _queries.EncontrarPor<Destino>(hotel.DestinoId);

            if (getDestino == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Destino con el id: " + hotel.DestinoId + " no existe."
                };
            }

            Hotel nuevoHotel = new Hotel()
            {
                Marca = hotel.Marca,
                Sucursal = hotel.Sucursal,
                Estrellas = hotel.Estrellas,
                Direccion = hotel.Direccion,
                DestinoId = hotel.DestinoId,
                Capacidad = hotel.Capacidad,
                Costo = hotel.Costo,
            };

            _commands.Agregar<Hotel>(nuevoHotel);

            return new Response()
            {
                Code = "OK",
                Message = "Hotel con el id: " + nuevoHotel.Id + ", marca: " + nuevoHotel.Marca + " y sucursal: " + nuevoHotel.Sucursal + " creado."
            };
        }

        public object GetHotelId(int id)
        {
            var hotel = _queries.EncontrarPor<Hotel>(id);

            if (hotel == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Hotel con el id: " + id + " no encontrado."
                };
            }

            var destino = _queries.EncontrarPor<Destino>(hotel.DestinoId);

            var output = new HotelOutDto()
            {
                Id = hotel.Id,
                Marca = hotel.Marca,
                Sucursal = hotel.Sucursal,
                Estrellas = hotel.Estrellas,
                Direccion = hotel.Direccion,
                DestinoId = hotel.DestinoId,
                Capacidad = hotel.Capacidad,
                Costo = hotel.Costo,
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

            return output;
        }

        public object GetHoteles(string idDestino)
        {
            var hoteles = _queries.Traer<Hotel>();

            if (idDestino.Equals(""))
            {
                var hotelesOut = new List<HotelOutDto>();

                if (hoteles.Count == 0)
                {
                    return new Response()
                    {
                        Code = "NOT_FOUND",
                        Message = "No hay hoteles."
                    };
                }

                foreach (Hotel x in hoteles)
                {
                    var destino = _queries.EncontrarPor<Destino>(x.DestinoId);
                    
                    var output = new HotelOutDto()

                    {
                        Id = x.Id,
                        Marca = x.Marca,
                        Sucursal = x.Sucursal,
                        Estrellas = x.Estrellas,
                        Direccion = x.Direccion,
                        DestinoId = x.DestinoId,
                        Capacidad = x.Capacidad,
                        Costo = x.Costo,
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

                    hotelesOut.Add(output);
                }

                return hotelesOut;
            }

            if (_queries.EncontrarPor<Destino>(Int32.Parse(idDestino)) == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Destino con el id: " + idDestino + " no encontrado."
                };
            }           

            if (hoteles.Count == 0)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No hay hoteles."
                };
            }            

            var hotelesDestino = new List<HotelOutDto>();

            foreach (Hotel x in hoteles)
            {
                if (Int32.Parse(idDestino) == x.DestinoId)
                {
                    var destino = _queries.EncontrarPor<Destino>(x.DestinoId);

                    var output = new HotelOutDto()
                    {
                        Id = x.Id,
                        Marca = x.Marca,
                        Sucursal = x.Sucursal,
                        Estrellas = x.Estrellas,
                        Direccion = x.Direccion,
                        DestinoId = x.DestinoId,
                        Capacidad = x.Capacidad,
                        Costo = x.Costo,
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

                    hotelesDestino.Add(output);
                }
            }

            return hotelesDestino;
        }

        public Response ActualizarHotel(int id, HotelDto hotelDTO)
        {
            Destino getDestino = _queries.EncontrarPor<Destino>(hotelDTO.DestinoId);

            if (getDestino == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Destino con el id: " + hotelDTO.DestinoId + " no existe."
                };
            }

            var hotel = _queries.EncontrarPor<Hotel>(id);

            hotel.Marca = hotelDTO.Marca;
            hotel.Sucursal = hotelDTO.Sucursal;
            hotel.Estrellas = hotelDTO.Estrellas;
            hotel.Direccion = hotelDTO.Direccion;
            hotel.Costo = hotelDTO.Costo;

            _commands.Actualizar<Hotel>(hotel);

            return new Response()
            {
                Code = "UPDATE",
                Message = "Hotel con el id: " + hotel.Id + ", marca: " + hotel.Marca + " y sucursal: " + hotel.Sucursal + " actualizado."
            };
        }

        public Response BorrarHotel(int id)
        {
            var check = _queries.EncontrarPor<Hotel>(id);

            if (check == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Hotel con el id: " + id + " no encontrado."
                };
            }

            _commands.BorrarPor<Hotel>(id);

            // borrar los paquetes que contengan el hotel

            foreach (var paquete in _queries.Traer<Paquete>())
            {
                if (paquete.HotelId == id)
                {
                    _commands.Borrar(paquete);

                    // asi como tambien las tablas PaqueteExcursion

                    foreach (var paqueteExcursion in _queries.Traer<PaqueteExcursion>())
                    {
                        if (paqueteExcursion.PaqueteId == paquete.Id)
                        {
                            _commands.Borrar(paqueteExcursion);
                        }
                    }

                    // no olvidemos dejar huerfanas a las reservas

                    foreach (var reserva in _queries.Traer<Reserva>())
                    {
                        if (reserva.PaqueteId == paquete.Id)
                        {
                            reserva.PaqueteId = 0;
                            reserva.GrupoId = 0;
                            reserva.ViajeId = 0;

                            // faltaria alguna anotacion para dejar en claro los detalles del paquete borrado

                            _commands.Actualizar<Reserva>(reserva);

                            // no olvidar borrar todos los grupos y viajes asociados al paquete
                        }
                    }


                }
            }   

            return new Response()
            {
                Code = "OK",
                Message = "Hotel con el id: " + id + " borrado."
            };

            // hay que borrar todos los paquetes que lo contengan, asi como tambien dejar huerfanas 
            // a las reservas, y tambien borrar todos los grupos y viajes que tengan asociado a ese paquete

        }

        // 
    }
}

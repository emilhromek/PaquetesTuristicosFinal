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
    public interface IPaqueteService
    {
        public Response PostPaquete(PaqueteDto paquete);
        public object GetPaqueteId(int id);
        public object GetPaquetes(string idDestino);
        public Response deletePaqueteId(int id);
    }

    public class PaqueteService: IPaqueteService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public PaqueteService (ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public Response PostPaquete(PaqueteDto paquete)
        {

            if (paquete.Descuento >= 100 && paquete.Descuento < 0)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "El descuento tiene que estar entre 0 y 99."
                };
            }


            Destino getDestino = _queries.EncontrarPor<Destino>(paquete.DestinoId);

            if (getDestino == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Destino con el id: " + paquete.DestinoId + " no existe."
                };
            }

            // check excursiones

            if (paquete.Excursiones.Count != 0)
            {
                foreach (int x in paquete.Excursiones)
                {
                    var excursionCheck = _queries.EncontrarPor<Excursion>(x);

                    if (excursionCheck.DestinoId != paquete.DestinoId)
                    {
                        return new Response()
                        {
                            Code = "BAD_REQUEST",
                            Message = "Las excursiones ingresadas para el paquete no coinciden con el destino. Eso pasa por no usar el front."
                        };
                    }
                }
            }
            else
            {

            }

            Paquete paqueteNuevo = new Paquete()
            {
                Nombre = paquete.Nombre,
                Descripcion = paquete.Descripcion,
                FechaArribo = paquete.FechaArribo,
                FechaPartida = paquete.FechaPartida,
                FechaSalida = paquete.FechaArribo.Subtract(new System.TimeSpan(getDestino.HorasDeViaje, 0, 0)),
                FechaLlegada = paquete.FechaPartida.Add(new System.TimeSpan(getDestino.HorasDeViaje, 0, 0)),
                Precio = paquete.Precio,
                Descuento = paquete.Descuento,
                TotalNoches = (paquete.FechaPartida - paquete.FechaArribo).Days,
                Prioridad = paquete.Prioridad,
                EmpleadoId = paquete.EmpleadoId,
                HotelId = paquete.HotelId,
                DestinoId = paquete.DestinoId,
                IdentificadorUnicoDePaquete = "P-FS" + paquete.FechaArribo.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "FL"
                + paquete.FechaPartida.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-" + Guid.NewGuid().ToString(),
            };                    

            _commands.Agregar<Paquete>(paqueteNuevo);

            foreach (int x in paquete.Excursiones)
            {
                PaqueteExcursion paqueteExcursion = new PaqueteExcursion()
                {
                    PaqueteId = paqueteNuevo.Id,
                    ExcursionId = x,
                };

                _commands.Agregar<PaqueteExcursion>(paqueteExcursion);
            }

            return new Response()
            {
                Code = "OK",
                Message = "Paquete con el id: " + paqueteNuevo.Id + " y título: " + paqueteNuevo.Nombre + " creado."
            };
        }

        public object GetPaqueteId(int id)
        {
            var paquete = _queries.EncontrarPor<Paquete>(id);

            if (paquete == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Paquete con el id: " + id + " no encontrado."
                };
            }

            var destino = _queries.EncontrarPor<Destino>(paquete.DestinoId);

            var hotel = _queries.EncontrarPor<Hotel>(paquete.HotelId);

            var listaExcursiones = new List<ExcursionOutDto>();

            foreach (var x in _queries.Traer<PaqueteExcursion>())
            {
                if (x.PaqueteId == id)
                {
                    var excursion = _queries.EncontrarPor<Excursion>(x.Id);

                    listaExcursiones.Add(new ExcursionOutDto
                    {
                        Id = excursion.Id,
                        Titulo = excursion.Titulo,
                        Descripcion = excursion.Descripcion,
                        Precio = excursion.Precio,
                        Duracion = excursion.Duracion,
                        DestinoId = excursion.DestinoId,
                    });
                }
            }

            var output = new PaqueteOutDto()
            {
                Id = paquete.Id,
                Nombre = paquete.Nombre,
                Descripcion = paquete.Descripcion,
                FechaSalida = paquete.FechaSalida.ToString(),
                FechaSalidaSinFormato = paquete.FechaSalida.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                FechaArribo = paquete.FechaArribo.ToString(),
                FechaArriboSinFormato = paquete.FechaArribo.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                FechaPartida = paquete.FechaPartida.ToString(),
                FechaPartidaSinFormato = paquete.FechaPartida.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                FechaLlegada = paquete.FechaLlegada.ToString(),
                FechaLlegadaSinFormato = paquete.FechaLlegada.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                Precio = paquete.Precio,
                Descuento = paquete.Descuento,
                TotalNoches = paquete.TotalNoches,
                Prioridad = paquete.Prioridad,
                DestinoId = paquete.DestinoId,
                HotelId = paquete.HotelId,
                IdentificadorUnicoDePaquete = paquete.IdentificadorUnicoDePaquete,
                Destino = new DestinoOutDto
                {
                    Id = destino.Id,
                    Lugar = destino.Lugar,
                    Descripcion = destino.Descripcion,
                    Atractivo = destino.Atractivo,
                    Historia = destino.Historia,
                    Imagen = destino.Imagen,
                },
                Hotel = new HotelOutDto
                {
                    Id = hotel.Id,
                    Marca = hotel.Marca,
                    Sucursal = hotel.Sucursal,
                    Estrellas = hotel.Estrellas,
                    Direccion = hotel.Direccion,
                    DestinoId = hotel.DestinoId,
                    Capacidad = hotel.Capacidad,
                    Costo = hotel.Costo,
                },

                ExcursionOutDtos = new List<ExcursionOutDto>(listaExcursiones),
            };   

                 return output;
        }
        
        public object GetPaquetes(string idDestino)
        {   
            var paquetesARenderizar = new List<Paquete>();

            var listaOutput = new List<object>();

            // Primer filtrado, por destino

            if (idDestino.Equals(""))
            {
                foreach (Paquete x in _queries.Traer<Paquete>())
                {
                    paquetesARenderizar.Add(x);
                }
            }

            if (!idDestino.Equals(""))
            {
                foreach (Paquete x in _queries.Traer<Paquete>())
                {
                    if (x.DestinoId == Int32.Parse(idDestino))
                    {
                        var paqueteAAgregar = _queries.EncontrarPor<Paquete>(x.Id);

                        if (!paquetesARenderizar.Contains(paqueteAAgregar))
                        {
                            paquetesARenderizar.Add(paqueteAAgregar);
                        }
                    }
                }
            }            

            foreach (Paquete x in paquetesARenderizar)
            {
                listaOutput.Add(GetPaqueteId(x.Id));
            }

            if (listaOutput.Count == 0)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No hay paquetes."
                };
            }

            return listaOutput;

        }

        public Response ActualizarPaquete(int id, PaqueteDto paqueteDTO)
        {
            var paquete = _queries.EncontrarPor<Paquete>(id);

            paquete.Nombre = paqueteDTO.Nombre;
            paquete.Descripcion = paqueteDTO.Descripcion;
            paquete.Precio = paqueteDTO.Precio;
            paquete.Descuento = paqueteDTO.Descuento;
            paquete.Prioridad = paqueteDTO.Prioridad;

            _commands.Actualizar<Paquete>(paquete);

            return new Response()
            {
                Code = "UPDATE",
                Message = "Paquete con el id: " + id + ", nombre: " + paquete.Nombre + " actualizado."
            };

        }

        public Response deletePaqueteId(int paqueteId)
        {
            Paquete paquete = _queries.EncontrarPor<Paquete>(paqueteId);

            if (paquete==null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "No existe ese paquete."
                };
            }

            _commands.BorrarPor<Paquete>(paqueteId);

            // hay que borrar todas las tablas PaqueteExcursion y a las reservas marcarlas como huerfanas
            // tambien que borrar todos los grupos y viajes asociados

            //borrado de PaqueteExcursion

            foreach (var paqueteExcursion in _queries.Traer<PaqueteExcursion>())
            {
                if(paqueteExcursion.PaqueteId == paqueteId)
                {
                    _commands.Borrar<PaqueteExcursion>(paqueteExcursion);
                }
            }

            // dejamos huerfanas a las reservas

            foreach (var reserva in _queries.Traer<Reserva>())
            {
                if (reserva.PaqueteId == paqueteId)
                {
                    reserva.PaqueteId = 0;
                    reserva.GrupoId = 0;
                    reserva.ViajeId = 0;

                    // faltaria alguna anotacion para dejar en claro los detalles del paquete borrado

                    _commands.Actualizar<Reserva>(reserva);
                }
            }

            // no olvidar de borrar todos los grupos y viajes asociados al paquete

            return new Response()
            {
                Code = "OK",
                Message = "Paquete borrado. Tambien se marcaron las reservas correspondientes"
            };
        }
        public Response convertirPaquetesEnHistoricos()
        {
            foreach (var paquete in _queries.Traer<Paquete>())
            {
                if (paquete.FechaSalida <= DateTime.Now)
                {
                    string excursiones = "";

                    foreach (PaqueteExcursion paqueteExcursion in _queries.Traer<PaqueteExcursion>())
                    {
                        excursiones = excursiones + _queries.EncontrarPor<Excursion>(paqueteExcursion.ExcursionId).Titulo + ". ";
                    }

                    var paqueteHistorico = new PaqueteHistorico
                    {
                        Nombre = paquete.Nombre,
                        Descripcion = paquete.Descripcion,
                        FechaSalida = paquete.FechaSalida,
                        FechaArribo = paquete.FechaArribo,
                        FechaPartida = paquete.FechaPartida,
                        FechaLlegada = paquete.FechaLlegada,
                        TotalNoches = paquete.TotalNoches,
                        Precio = paquete.Precio,
                        Empleado = paquete.EmpleadoId.ToString(),
                        Destino = "Lugar: " + _queries.EncontrarPor<Destino>(paquete.DestinoId).Lugar,
                        Hotel = "Hotel: " + _queries.EncontrarPor<Hotel>(paquete.DestinoId).Marca + ". Sucursal: " +
                        _queries.EncontrarPor<Hotel>(paquete.DestinoId).Sucursal,
                        Excursiones = excursiones,

                        // habria que agregar un contador de cuantos pasajeros viajaron y cuanto $ recaudo este paquete

                    };

                    _commands.Borrar(paquete);

                    _commands.Agregar(paqueteHistorico);

                }
            }

            return new Response
            {
                Code = "OK",
                Message = "Ok",
            };
        }

    }
}

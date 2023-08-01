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
    public interface IReservaService
    {
        public Response PostReserva(ReservaDto reserva);
        public object GetReservaId(int id);
        public object GetReservas(string PaqueteId, string PasajeroId);
        public Response PatchReservaPagadoId(int ReservaId);
        public Response PatchReservaGrupoId(int ReservaId, int GrupoId);        
        public Response UnpatchReservaGrupoId(int ReservaId);
        public Response PatchReservaViajeId(int ReservaId, int ViajeId);
        public Response UnpatchReservaViajeId(int ReservaId);
        public Response AsignarViajeAReservasSegunGrupo(int grupoId, int viajeId);
        public Response DesasignarViajeAReservasSegunGrupo(int grupoId);
    }

    public class ReservaService : IReservaService
    {
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public ReservaService(ICommands commands, IQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public Response PostReserva(ReservaDto reserva)
        {
            FormaPago getFormaPago = _queries.EncontrarPor<FormaPago>(reserva.FormaPagoId);

            if (getFormaPago == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Forma de pago con el id: " + reserva.FormaPagoId + " no existe."
                };
            }

            Paquete getPaquete = _queries.EncontrarPor<Paquete>(reserva.PaqueteId);

            if (getPaquete == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Paquete con el id: " + reserva.PaqueteId + " no existe."
                };
            }

            int precioTotalReserva = 0;

            Reserva nuevaReserva = new Reserva()
            {
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
            };

            var listaReservaExcursion = new List<ReservaExcursion>();

            //// Sumar el precio total pasajeros * paquete

            //precioTotalReserva = precioTotalReserva + nuevaReserva.Pasajeros * _queries.EncontrarPor<Paquete>(reserva.PaqueteId).Precio;

            //foreach (int x in reserva.ListaExcursiones)
            //{
            //    // Sumar el precio total pasajeros * cada una de las excursiones

            //    precioTotalReserva = precioTotalReserva + nuevaReserva.Pasajeros * _queries.EncontrarPor<Excursion>(x).Precio;

            //    ReservaExcursion reservaExcursion = new ReservaExcursion()
            //    {
            //        ReservaId = nuevaReserva.Id,
            //        ExcursionId = x,
            //    };

            //    listaReservaExcursion.Add(reservaExcursion);
            //}

            //nuevaReserva.PrecioTotal = precioTotalReserva;

            //nuevaReserva.ReservaExcursiones = listaReservaExcursion;

            _commands.Agregar<Reserva>(nuevaReserva);

            return new Response()
            {
                Code = "OK",
                Message = "Reserva con el id: " + nuevaReserva.Id + " creada."
            };
        }

        public object GetReservaId(int id)
        {
            var reserva = _queries.EncontrarPor<Reserva>(id);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "NOT_FOUND",
                    Message = "Reserva con el id: " + id + " no encontrada."
                };
            }

            List<object> listaExcursiones = new List<object>();

            foreach(ReservaExcursion x in _queries.Traer<ReservaExcursion>())
            {
                if (x.ReservaId == id)
                {
                    var excursion = _queries.EncontrarPor<Excursion>(x.ExcursionId);

                    listaExcursiones.Add(new {Id = x.ExcursionId, Título = excursion.Titulo, Destino = excursion.DestinoId});
                }
            }

            var output = new ReservaOutDto()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                PrecioTotal = reserva.PrecioTotal,
                Excursiones = listaExcursiones,
                GrupoId = reserva.GrupoId,
                ViajeId = reserva.ViajeId,
            };

            return output;
        }

        public object GetReservas(string PaqueteId, string PasajeroId)
        {
            var reservas = _queries.Traer<Reserva>();

            var reservasFiltradas = new List<Reserva>();

            // Primer filtrado, por Paquete

            if (PaqueteId == null) // no hay filtro
            {
                foreach (var x in reservas)
                {
                    reservasFiltradas.Add(x);
                }
            }

            if (!(PaqueteId == null))
            {
                //check si existe paquete

                var check = _queries.EncontrarPor<Paquete>(Int32.Parse(PaqueteId));

                if (check == null)
                {
                    return new Response()
                    {
                        Code = "BAD_REQUEST",
                        Message = "No existe ese paquete."
                    };
                }

                foreach (var x in reservas)
                {
                    if (x.PaqueteId == Int32.Parse(PaqueteId))
                    {
                        if (!reservasFiltradas.Contains(x))
                        {
                            reservasFiltradas.Add(x);
                        }                            
                    }
                }
            }

            // Segundo filtrado, por pasajero

            var reservasFiltradas2 = new List<Reserva>();

            if (PasajeroId == null)
            {
                foreach (var x in reservasFiltradas)
                {
                    if (!reservasFiltradas2.Contains(x))
                    {
                        reservasFiltradas2.Add(x);
                    }
                }
            }

            if (!(PasajeroId == null))
            {
                //check si existe pasajero

                //var check = _queries.EncontrarPor<Pasajero>(Int32.Parse(PasajeroId));

                //if (check == null)
                //{
                    //return new Response()
                    //{
                        //Code = "BAD_REQUEST",
                        //Message = "No existe ese paquete."
                    //};
                //}

                foreach (var x in reservasFiltradas)
                {
                    if (x.PasajeroId == Int32.Parse(PasajeroId))
                    {
                        if (!reservasFiltradas2.Contains(x))
                        {
                            reservasFiltradas2.Add(x);
                        }
                    }
                }
            }

            var output = new List<ReservaOutDto>();

            foreach (var x in reservasFiltradas2)
            {
                output.Add((ReservaOutDto) GetReservaId(x.Id));
            }

            return output;
        }

        public Response PatchReservaPagadoId(int ReservaId)
        {
            var reserva = _queries.EncontrarPor<Reserva>(ReservaId);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Error, no existe la reserva",
                };
            }

            bool pagado = reserva.Pagado;

            if (pagado == false)
            {
                pagado = true;
            }
            else
            {
                pagado = false;
            }

            Reserva reservaPatch = new Reserva()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                GrupoId = reserva.GrupoId,
                ViajeId = reserva.ViajeId,
            };

            _commands.Actualizar<Reserva>(reservaPatch);

            return new Response()
            {
                Code = "UPDATED",
                Message = "A la reserva " + ReservaId + " se asignó el estado de pagado: " + pagado,
            };

        }

        public Response PatchReservaGrupoId(int ReservaId, int GrupoId)
        {
            var reserva = _queries.EncontrarPor<Reserva>(ReservaId);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Error, no existe la reserva",
                };
            }

            Reserva reservaPatch = new Reserva()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                ViajeId = reserva.ViajeId,
                GrupoId = GrupoId,
            };

            _commands.Actualizar<Reserva>(reservaPatch);

            return new Response()
            {
                Code = "UPDATED",
                Message = "A la reserva " + ReservaId + " se le asignó el grupo " + GrupoId,
            };

        }        

        public Response UnpatchReservaGrupoId(int ReservaId)
        {
            var reserva = _queries.EncontrarPor<Reserva>(ReservaId);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Error, no existe la reserva",
                };
            }

            Reserva reservaPatch = new Reserva()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                ViajeId = reserva.ViajeId,
                GrupoId = 0,
            };

            _commands.Actualizar<Reserva>(reservaPatch);

            return new Response()
            {
                Code = "UPDATED",
                Message = "A la reserva " + ReservaId + " se le eliminó el grupo asignado",
            };

        }

        public Response PatchReservaViajeId(int ReservaId, int ViajeId)
        {
            var reserva = _queries.EncontrarPor<Reserva>(ReservaId);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Error, no existe la reserva",
                };
            }

            Reserva reservaPatch = new Reserva()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                GrupoId = reserva.GrupoId,
                ViajeId = ViajeId,
            };

            _commands.Actualizar<Reserva>(reservaPatch);

            return new Response()
            {
                Code = "UPDATE",
                Message = "A la reserva " + ReservaId + " se le asignó el viaje " + ViajeId,
            };

        }
        public Response UnpatchReservaViajeId(int ReservaId)
        {
            var reserva = _queries.EncontrarPor<Reserva>(ReservaId);

            if (reserva == null)
            {
                return new Response()
                {
                    Code = "BAD_REQUEST",
                    Message = "Error, no existe la reserva",
                };
            }

            Reserva reservaPatch = new Reserva()
            {
                Id = reserva.Id,
                Pasajeros = reserva.Pasajeros,
                Pagado = reserva.Pagado,
                PasajeroId = reserva.PasajeroId,
                FormaPagoId = reserva.FormaPagoId,
                PaqueteId = reserva.PaqueteId,
                GrupoId = reserva.GrupoId,
                ViajeId = 0,
            };

            _commands.Actualizar<Reserva>(reservaPatch);

            return new Response()
            {
                Code = "UPDATED",
                Message = "A la reserva " + ReservaId + " se le eliminó el viaje asignado",
            };

        }

        public Response AsignarViajeAReservasSegunGrupo(int grupoId, int viajeId)
        {
            var reservas = _queries.Traer<Reserva>();

            foreach (var x in reservas)
            {
                if (x.GrupoId == grupoId)
                {
                    Reserva reservaPatch = new Reserva()
                    {
                        Id = x.Id,
                        Pasajeros = x.Pasajeros,
                        Pagado = x.Pagado,
                        PasajeroId = x.PasajeroId,
                        FormaPagoId = x.FormaPagoId,
                        PaqueteId = x.PaqueteId,
                        GrupoId = x.GrupoId,
                        ViajeId = viajeId,
                    };

                    _commands.Actualizar<Reserva>(reservaPatch);
                }
            }

            return new Response()
            {
                Code = "OK",
                Message = "A todas las reservas que estan en el grupo " + grupoId + " se les asigno el viaje " + viajeId,
            };

        }

        public Response DesasignarViajeAReservasSegunGrupo(int grupoId)
        {
            var reservas = _queries.Traer<Reserva>();

            foreach (var x in reservas)
            {
                if (x.GrupoId == grupoId)
                {
                    Reserva reservaPatch = new Reserva()
                    {
                        Id = x.Id,
                        Pasajeros = x.Pasajeros,
                        Pagado = x.Pagado,
                        PasajeroId = x.PasajeroId,
                        FormaPagoId = x.FormaPagoId,
                        PaqueteId = x.PaqueteId,
                        GrupoId = x.GrupoId,
                        ViajeId = 0,
                    };

                    _commands.Actualizar<Reserva>(reservaPatch);
                }
            }

            return new Response()
            {
                Code = "OK",
                Message = "A todas las reservas que estan en el grupo " + grupoId + " se les desasigno el viaje",
            };

        }



    }
}

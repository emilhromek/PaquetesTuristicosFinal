using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.Application.Services
{
    public class CoordinadorService : ServicesGeneric, ICoordinadorService
    {
        private readonly ICoordinadorRepository repository;

        public CoordinadorService(ICoordinadorRepository _repository) : base(_repository)
        {
            this.repository = _repository;
        }

        public CoordinadorResponseDTO AddCoordinador(CoordinadorDTO coordinadorDTO)
        {
            var Coordinador = new Coordinador()
            {
                Nombre = coordinadorDTO.Nombre,   
                Apellido = coordinadorDTO.Apellido,
                Email = coordinadorDTO.Email,
                Agenda = coordinadorDTO.Agenda,
                Contacto = coordinadorDTO.Contacto,
            };

            this.Repository.Add(Coordinador);

            return new CoordinadorResponseDTO
            {
                CoordinadorId = Coordinador.CoordinadorId,
                Nombre = Coordinador.Nombre,
                Email = Coordinador.Email,
                Contacto = Coordinador.Contacto
            };
        }

        public CoordinadorResponseDTO ActualizarCoordinador(int id, CoordinadorDTO coordinadorDTO)
        {
            var coordinador = new Coordinador()
            {
                CoordinadorId = id,
                Nombre = coordinadorDTO.Nombre,
                Apellido = coordinadorDTO.Apellido,
                Email = coordinadorDTO.Email,
                Agenda = coordinadorDTO.Agenda,
                Contacto = coordinadorDTO.Contacto,
            };

            this.Repository.Update<Coordinador>(coordinador);

            return ToCoordinadorResponseDTO(coordinador);

        }

        public CoordinadorResponseDTO GetCoordinadorById(int id)
        {
            var coordinador = repository.FindBy<Coordinador>(id);
            if (coordinador == null)
                throw new Exception($"El coordinador id:{id} no existe");
            return ToCoordinadorResponseDTO(coordinador);
        }

        public List<CoordinadorResponseDTO> GetAll()
        {
            var lista_coordinador = new List<CoordinadorResponseDTO>();
            var coordinadores = repository.Traer<Coordinador>().ToList();
            foreach (var coordinador in coordinadores)
            {
                lista_coordinador.Add(ToCoordinadorResponseDTO(coordinador));
            }
            return lista_coordinador;
        }

        public List<CoordinadorResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal)
        {
            var lista_coordinadores = new List<CoordinadorResponseDTO>();
            var coordinadores = repository.Traer<Coordinador>().ToList();

            foreach (var coordinador in coordinadores)
            {
                if (checkSiRangoFechaEstaOcupada(coordinador.CoordinadorId, fechaInicial, fechaFinal) == false)
                {
                    lista_coordinadores.Add(ToCoordinadorResponseDTO(coordinador));
                }

            }
            return lista_coordinadores;
        }

        //public List<CoordinadorCompletoDTO> GetCompleto()
        //{
        //    List<CoordinadorCompletoDTO> coordinadoresCompletos = new List<CoordinadorCompletoDTO>();
        //    var coordinadores = this.Repository.Traer<Coordinador>();
        //    foreach (Coordinador coordinador in coordinadores)
        //    {
        //        coordinadoresCompletos.Add(repository.GetCoordinadorCompleto(coordinador));
        //    }
        //    return coordinadoresCompletos;
        //}

        //public void DeleteCoordinadorById(int id)
        //{
        //    var coordinadores = this.Repository.Traer<Domain.Entities.Coordinador>();

        //    foreach (var coordinador in coordinadores)
        //    {
        //        if (coordinador.CoordinadorId == id)
        //        {
        //            this.Repository.Delete(coordinador);
        //        }
        //    }
        //}

        public CoordinadorResponseDTO ToCoordinadorResponseDTO(Coordinador coordinador)
        {
            return new CoordinadorResponseDTO
            {
                CoordinadorId = coordinador.CoordinadorId,
                Nombre = coordinador.Nombre,
                Apellido = coordinador.Apellido,
                Email = coordinador.Email,
                Contacto = coordinador.Contacto,
                Agenda = coordinador.Agenda
            };
        }

        public void DeleteCoordinadorById(int coordinadorId)
        {
            this.Repository.DeleteById<Coordinador>(coordinadorId);

            // borrar toda la agenda

            foreach (var agenda in this.repository.Traer<AgendaCoordinador>())
            {
                if (agenda.CoordinadorId == coordinadorId)
                {
                    this.repository.Delete(agenda);
                }
            }

            // dejar sin chofer a todos los viajes asociados

            foreach (var viaje in this.repository.Traer<Viaje>())
            {
                if (viaje.CoordinadorId == coordinadorId)
                {
                    viaje.CoordinadorId = 0;

                    this.repository.Update(viaje);
                }
            }
        }

        public AgendaCoordinadorDTO agregarAgenda(AgendaCoordinadorDTO agendaDTO) // agrega una agenda para un Coordinador, especificando Coordinador id, fecha inicial y final
        {
            var agenda = new AgendaCoordinador()
            {
                CoordinadorId = agendaDTO.CoordinadorId,
                FechaInicial = agendaDTO.FechaInicial,
                FechaFinal = agendaDTO.FechaFinal,
                ViajeId = agendaDTO.ViajeId,
            };

            this.repository.Add<AgendaCoordinador>(agenda);

            return agendaDTO;
        }
        public List<AgendaCoordinadorResponseDTO> retornarAgenda(int CoordinadorId) // retornar toda la agenda de un Coordinador, segun Coordinadorid
        {
            var lista = this.repository.Traer<AgendaCoordinador>();

            var listaOutput = new List<AgendaCoordinadorResponseDTO>();

            foreach (var x in lista)
            {
                if (x.CoordinadorId == CoordinadorId)
                {
                    var agenda = new AgendaCoordinadorResponseDTO()
                    {
                        Id = x.Id,
                        CoordinadorId = x.CoordinadorId,
                        FechaInicial = x.FechaInicial,
                        FechaFinal = x.FechaFinal,
                        ViajeId = x.ViajeId,
                    };

                    listaOutput.Add(agenda);
                }
            }
            return listaOutput;
        }

        public List<AgendaCoordinadorResponseConFormatoDTO> retornarAgendaConFormato(int CoordinadorId) // retornar toda la agenda de un Coordinador, segun Coordinadorid
        {
            var lista = this.repository.Traer<AgendaCoordinador>();

            var listaOutput = new List<AgendaCoordinadorResponseConFormatoDTO>();

            foreach (var x in lista)
            {
                if (x.CoordinadorId == CoordinadorId)
                {
                    var agenda = new AgendaCoordinadorResponseConFormatoDTO()
                    {
                        Id = x.Id,
                        CoordinadorId = x.CoordinadorId,
                        FechaInicial = x.FechaInicial.ToShortDateString(),
                        FechaFinal = x.FechaFinal.ToShortDateString(),
                        ViajeId = x.ViajeId,
                    };

                    listaOutput.Add(agenda);
                }
            }
            return listaOutput;
        }

        public bool checkSiFechaEstaOcupada(int CoordinadorId, DateTime fecha) // chequea si para una fecha concreta, el Coordinador esta ocupado
        {
            var lista = retornarAgenda(CoordinadorId);

            foreach (var x in lista)
            {
                if (fecha <= x.FechaFinal && fecha >= x.FechaInicial)
                {
                    return true;
                }
                break;
            }
            return false;
        }

        public bool checkSiRangoFechaEstaOcupada(int coordinadorId, DateTime fechaInicial, DateTime fechaFinal)
        {
            var lista = retornarAgenda(coordinadorId);

            bool check1 = checkSiFechaEstaOcupada(coordinadorId, fechaInicial); // check fecha inicial

            bool check2 = checkSiFechaEstaOcupada(coordinadorId, fechaInicial); // check fecha final

            bool check3 = false;

            foreach (var x in lista) // check que el rango no tenga ninguna ocupacion intermedia
            {
                if (x.FechaInicial >= fechaInicial && x.FechaFinal <= fechaFinal)
                {
                    check3 = true;
                    break;
                }
            }

            if (check1 == false && check2 == false && check3 == false)
            {
                return false;
            }

            return true;

        }

        public AgendaCoordinadorResponseDTO retornarAgendaDeEsosDias(int CoordinadorId, DateTime fecha) // para una fecha concreta, retornar el rango de ocupacion en el que esta incluida
        {
            var lista = retornarAgenda(CoordinadorId);

            foreach (var x in lista)
            {
                if (fecha <= x.FechaFinal && fecha >= x.FechaInicial)
                {
                    return x;
                }
                break;
            }
            return null;
        }

        public void vaciarAgendaDeCoordinador(int CoordinadorId) // elimina toda la agenda de un chofer
        {
            foreach (var agenda in this.repository.Traer<AgendaCoordinador>())
            {
                if (agenda.CoordinadorId == CoordinadorId)
                {
                    this.repository.DeleteById<AgendaCoordinador>(agenda.Id);
                }
            }
        }

        public void eliminarAgendaSegunViajeId(int CoordinadorId, int viajeId) // eliminar una agenda segun Coordinadorid y viajeid
        {
            foreach (var agenda in this.repository.Traer<AgendaCoordinador>())
            {
                if (agenda.CoordinadorId == CoordinadorId && agenda.ViajeId == viajeId)
                {
                    this.repository.DeleteById<AgendaCoordinador>(agenda.Id);
                }
            }
        }

    }
}

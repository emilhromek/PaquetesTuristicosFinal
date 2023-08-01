using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.Chofer;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.Application.Services
{
    public class ChoferService : ServicesGeneric, IChoferService
    {
        private readonly IRepositoryGeneric repository;

        public ChoferService(IChoferRepository _repository) : base(_repository)
        {
            this.repository = _repository;
        }

        public ChoferResponseDTO AddChofer(ChoferDTO choferDTO)
        {
            var chofer = new Chofer()
            {
                Nombre = choferDTO.Nombre,
                Apellido = choferDTO.Apellido,
                Email = choferDTO.Email,
                Agenda = choferDTO.Agenda,
                Contacto = choferDTO.Contacto,
                Licencia = choferDTO.Licencia,
                Vencimiento = choferDTO.Vencimiento,
            };

            this.Repository.Add(chofer);

            return ToChoferResponseDTO(chofer);
        }

        public ChoferResponseDTO ActualizarChofer(int id, ChoferDTO choferDTO)
        {
            var chofer = new Chofer()
            {
                ChoferId = id,
                Nombre = choferDTO.Nombre,
                Apellido = choferDTO.Apellido,
                Email = choferDTO.Email,
                Agenda = choferDTO.Agenda,
                Contacto = choferDTO.Contacto,
                Licencia = choferDTO.Licencia,
                Vencimiento = choferDTO.Vencimiento,
            };

            this.Repository.Update<Chofer>(chofer);

            return ToChoferResponseDTO(chofer);

        }

        

        public void DeleteChoferById(int choferId)
        {
            var check = repository.FindBy<Chofer>(choferId);

            if (check == null)
                throw new Exception();

            repository.Delete(check);

            //// borrar toda la agenda

            //foreach (var agenda in this.repository.Traer<AgendaChofer>())
            //{
            //    if (agenda.ChoferId == choferId)
            //    {
            //        this.repository.Delete(agenda);
            //    }
            //}

            //// dejar sin chofer a todos los viajes asociados

            //foreach (var viaje in this.repository.Traer<Viaje>())
            //{
            //    if (viaje.Chofer1Id == choferId)
            //    {
            //        viaje.Chofer1Id = 0;

            //        this.repository.Update(viaje);
            //    }

            //    if (viaje.Chofer2Id == choferId)
            //    {
            //        viaje.Chofer2Id = 0;

            //        this.repository.Update(viaje);
            //    }
            //}
        }

        public ChoferResponseDTO GetChoferById(int id)
        {
            var chofer = repository.FindBy<Chofer>(id);
            if (chofer == null)
                throw new Exception($"El chofer id:{id} no existe");
            return ToChoferResponseDTO(chofer);
        }

        public List<ChoferResponseDTO> GetAll()
        {
            var lista_chofer = new List<ChoferResponseDTO>();
            var choferes = repository.Traer<Chofer>().ToList();
            foreach (var chofer in choferes)
            {
                lista_chofer.Add(ToChoferResponseDTO(chofer));
            }
            return lista_chofer;
        }

        //public List<ChoferResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal)
        //{
        //    var lista_chofer = new List<ChoferResponseDTO>();
        //    var choferes = repository.Traer<Chofer>().ToList();

        //    foreach (var chofer in choferes)
        //    {
        //        if (checkSiRangoFechaEstaOcupada(chofer.ChoferId, fechaInicial, fechaFinal) == false)
        //        {
        //            lista_chofer.Add(ToChoferResponseDTO(chofer));
        //        }
                
        //    }
        //    return lista_chofer;
        //}

        public ChoferResponseDTO ToChoferResponseDTO(Chofer chofer)
        {
            return new ChoferResponseDTO
            {
                ChoferId = chofer.ChoferId,
                Nombre = chofer.Nombre,
                Apellido = chofer.Apellido,
                Email = chofer.Email,
                Agenda = chofer.Agenda,
                Contacto = chofer.Contacto,
                Licencia = chofer.Licencia,
                Vencimiento = chofer.Vencimiento.ToShortDateString(),
            };
        }

        //public AgendaChoferDTO agregarAgenda(AgendaChoferDTO agendaDTO) // agrega una agenda para un chofer, especificando chofer id, fecha inicial y final
        //{
        //    var agenda = new AgendaChofer()
        //    {
        //        ChoferId = agendaDTO.ChoferId,
        //        FechaInicial = agendaDTO.FechaInicial,
        //        FechaFinal = agendaDTO.FechaFinal,
        //        ViajeId = agendaDTO.ViajeId,
        //    };

        //    this.repository.Add<AgendaChofer>(agenda);

        //    return agendaDTO;
        //}
        //public List<AgendaChoferResponseDTO> retornarAgenda(int ChoferId) // retornar toda la agenda de un chofer, segun choferid
        //{
        //    var lista = this.repository.Traer<AgendaChofer>();

        //    var listaOutput = new List<AgendaChoferResponseDTO>();

        //    foreach (var x in lista)
        //    {
        //        if (x.ChoferId == ChoferId)
        //        {
        //            var agenda = new AgendaChoferResponseDTO()
        //            {
        //                Id = x.Id,
        //                ChoferId = x.ChoferId,
        //                FechaInicial = x.FechaInicial,
        //                FechaFinal = x.FechaFinal,
        //                ViajeId = x.ViajeId,
        //            };

        //            listaOutput.Add(agenda);
        //        }
        //    }
        //    return listaOutput;
        //}

        //public List<AgendaChoferResponseConFormatoDTO> retornarAgendaConFormato(int ChoferId) // retornar toda la agenda de un chofer, segun choferid
        //{
        //    var lista = this.repository.Traer<AgendaChofer>();

        //    var listaOutput = new List<AgendaChoferResponseConFormatoDTO>();

        //    foreach (var x in lista)
        //    {
        //        if (x.ChoferId == ChoferId)
        //        {
        //            var agenda = new AgendaChoferResponseConFormatoDTO()
        //            {
        //                Id = x.Id,
        //                ChoferId = x.ChoferId,
        //                FechaInicial = x.FechaInicial.ToShortDateString(),
        //                FechaFinal = x.FechaFinal.ToShortDateString(),
        //                ViajeId = x.ViajeId,
        //            };

        //            listaOutput.Add(agenda);
        //        }
        //    }
        //    return listaOutput;
        //}

        //public bool checkSiFechaEstaOcupada(int choferId, DateTime fecha) // chequea si para una fecha concreta, el chofer esta ocupado
        //{
        //    var lista = retornarAgenda(choferId);

        //    foreach (var x in lista)
        //    {
        //        if (fecha <= x.FechaFinal && fecha >= x.FechaInicial)
        //        {
        //            return true;
        //        }
        //        break;
        //    }

        //    return false;
        //}

        //public bool checkSiRangoFechaEstaOcupada(int choferId, DateTime fechaInicial, DateTime fechaFinal) // chequea si para una fecha concreta, el chofer esta ocupado
        //{
        //    var lista = retornarAgenda(choferId);

        //    bool check1 = checkSiFechaEstaOcupada(choferId, fechaInicial); // check fecha inicial

        //    bool check2 = checkSiFechaEstaOcupada(choferId, fechaInicial); // check fecha final

        //    bool check3 = false;

        //    foreach (var x in lista) // check que el rango no tenga ninguna ocupacion intermedia
        //    {
        //        if (x.FechaInicial >= fechaInicial && x.FechaFinal <= fechaFinal)
        //        {
        //            check3 = true;
        //            break;
        //        }
        //    }

        //    if (check1 == false && check2 == false && check3 == false)
        //    {
        //        return false;
        //    }

        //    return true;

        //}

        //public AgendaChoferResponseDTO retornarAgendaDeEsosDias(int choferId, DateTime fecha) // para una fecha concreta, retornar el rango de ocupacion en el que esta incluida
        //{
        //    var lista = retornarAgenda(choferId);

        //    foreach (var x in lista)
        //    {
        //        if (fecha <= x.FechaFinal && fecha >= x.FechaInicial)
        //        {
        //            return x;
        //        }
        //    }
        //    return null;
        //}

        //public void vaciarAgendaDeChofer(int choferId) // elimina toda la agenda de un chofer
        //{
        //    foreach(var agenda in this.repository.Traer<AgendaChofer>())
        //    {
        //        if (agenda.ChoferId == choferId)
        //        {
        //            this.repository.DeleteById<AgendaChofer>(agenda.Id);
        //        }
        //    }
        //}

        //public void eliminarAgendaSegunViajeId(int choferId, int viajeId) // eliminar una agenda segun choferid y viajeid
        //{
        //    foreach (var agenda in this.repository.Traer<AgendaChofer>())
        //    {
        //        if (agenda.ChoferId == choferId && agenda.ViajeId == viajeId)
        //        {
        //            this.repository.DeleteById<AgendaChofer>(agenda.Id);
        //        }
        //    }
        //}

    }
}

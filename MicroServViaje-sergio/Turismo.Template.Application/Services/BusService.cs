using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Application.Services
{
    public class BusService : ServicesGeneric, IBusService
    {
        private readonly IBusRepository repository;
        public BusService(IBusRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public BusResponseDTO AddBus(BusDTO busDTO)
        {
            var bus = new Bus()
            {
                Numero = busDTO.Numero,   
                Patente = busDTO.Patente,
                Capacidad = busDTO.Capacidad,
                Observacion = busDTO.Observacion,
                EmpresaId = busDTO.EmpresaId
            };

            this.Repository.Add(bus);

            return ToBusRsponseDTO(bus);
        }

        public BusResponseDTO ActualizarBus(int id, BusDTO busDTO)
        {
            var bus = this.repository.FindBy<Bus>(id);

            if (bus == null)
                throw new Exception($"El bus id:{id} no existe");

            bus.Numero = busDTO.Numero;
            bus.Patente = busDTO.Patente;
            bus.Capacidad = busDTO.Capacidad;
            bus.Observacion = busDTO.Observacion;
            bus.EmpresaId = busDTO.EmpresaId;

            this.Repository.Update<Bus>(bus);

            return ToBusRsponseDTO(bus);
        }

        public BusResponseDTO GetBusById(int id)
        {
            var bus = repository.FindBy<Bus>(id);
            if (bus == null)
                throw new Exception($"El bus id:{id} no existe");
            return ToBusRsponseDTO(bus);
        }

        public void DeleteBusById(int busId)
        {
            var check = repository.FindBy<Bus>(busId);

            if (check == null)
                throw new Exception();

            repository.Delete(check);

            //// borrar toda la agenda

            //foreach (var agenda in this.repository.Traer<AgendaBus>())
            //{
            //    if (agenda.BusId == busId)
            //    {
            //        this.repository.Delete(agenda);
            //    }
            //}

            // dejar sin bus a todos los viajes asociados

            //foreach (var viaje in this.repository.Traer<Viaje>())
            //{
            //    if (viaje.BusId == busId)
            //    {
            //        viaje.BusId = 0;

            //        this.repository.Update(viaje);
            //    }
       // }
        }

        public List<BusResponseDTO> GetAll()
        {
            var lista_bus = new List<BusResponseDTO>();
            var buses = repository.Traer<Bus>().ToList();
            foreach (var bus in buses)
            {
                lista_bus.Add(ToBusRsponseDTO(bus));
            }
            return lista_bus;
        }

        //public List<BusResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal)
        //{
        //    var lista_bus = new List<BusResponseDTO>();
        //    var buses = repository.Traer<Bus>().ToList();

        //    foreach (var bus in buses)
        //    {
        //        if (checkSiRangoFechaEstaOcupada(bus.BusId, fechaInicial, fechaFinal) == false)
        //        {
        //            lista_bus.Add(ToBusRsponseDTO(bus));
        //        }

        //    }
        //    return lista_bus;
        //}

        public BusResponseDTO ToBusRsponseDTO(Bus bus)
        {
            var empresa = repository.FindBy<Empresa>(bus.EmpresaId);
            return new BusResponseDTO
            {
                BusId = bus.BusId,
                Numero = bus.Numero,
                Patente = bus.Patente,
                Capacidad = bus.Capacidad,
                Observacion = bus.Observacion,
                EmpresaId = bus.EmpresaId,
                Empresa = empresa.Nombre,
                EmpresaContacto = empresa.Contacto,
                EmpresaEmail = empresa.Email
            };
        }

        //public AgendaBusDTO agregarAgenda(AgendaBusDTO agendaDTO) // agrega una agenda para un Coordinador, especificando Coordinador id, fecha inicial y final
        //{
        //    var agenda = new AgendaBus()
        //    {
        //        BusId = agendaDTO.BusId,
        //        FechaInicial = agendaDTO.FechaInicial,
        //        FechaFinal = agendaDTO.FechaFinal,
        //        ViajeId = agendaDTO.ViajeId,
        //    };

        //    this.repository.Add<AgendaBus>(agenda);

        //    return agendaDTO;
        //}
        //public List<AgendaBusResponseDTO> retornarAgenda(int BusId) // retornar toda la agenda de un Bus, segun Busid
        //{
        //    var lista = this.repository.Traer<AgendaBus>();

        //    var listaOutput = new List<AgendaBusResponseDTO>();

        //    foreach (var x in lista)
        //    {
        //        if (x.BusId == BusId)
        //        {
        //            var agenda = new AgendaBusResponseDTO()
        //            {
        //               Id = x.Id,
        //                BusId = x.BusId,
        //                FechaInicial = x.FechaInicial,
        //                FechaFinal = x.FechaFinal,
        //                ViajeId = x.ViajeId,
        //            };

        //            listaOutput.Add(agenda);
        //        }
        //    }
        //    return listaOutput;
        //}

        //public List<AgendaBusResponseConFormatoDTO> retornarAgendaConFormato(int BusId) // retornar toda la agenda de un Bus, segun Busid
        //{
        //    var lista = this.repository.Traer<AgendaBus>();

        //    var listaOutput = new List<AgendaBusResponseConFormatoDTO>();

        //    foreach (var x in lista)
        //    {
        //        if (x.BusId == BusId)
        //        {
        //            var agenda = new AgendaBusResponseConFormatoDTO()
        //            {
        //                Id = x.Id,
        //                BusId = x.BusId,
        //                FechaInicial = x.FechaInicial.ToShortDateString(),
        //                FechaFinal = x.FechaFinal.ToShortDateString(),
        //                ViajeId = x.ViajeId,
        //            };

        //            listaOutput.Add(agenda);
        //        }
        //    }
        //    return listaOutput;
        //}

        //public bool checkSiFechaEstaOcupada(int BusId, DateTime fecha) // chequea si para una fecha concreta, el Bus esta ocupado
        //{
        //    var lista = retornarAgenda(BusId);

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

        //public bool checkSiRangoFechaEstaOcupada(int busId, DateTime fechaInicial, DateTime fechaFinal)
        //{
        //    var lista = retornarAgenda(busId);

        //    bool check1 = checkSiFechaEstaOcupada(busId, fechaInicial); // check fecha inicial

        //    bool check2 = checkSiFechaEstaOcupada(busId, fechaInicial); // check fecha final

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

        //public AgendaBusResponseDTO retornarAgendaDeEsosDias(int BusId, DateTime fecha) // para una fecha concreta, retornar el rango de ocupacion en el que esta incluida
        //{
        //    var lista = retornarAgenda(BusId);

        //    foreach (var x in lista)
        //    {
        //        if (fecha <= x.FechaFinal && fecha >= x.FechaInicial)
        //        {
        //            return x;
        //        }
        //    }
        //    return null;
        //}

        //public void vaciarAgendaDeBus(int BusId) // elimina toda la agenda de un Bus
        //{
        //    foreach (var agenda in this.repository.Traer<AgendaBus>())
        //    {
        //        if (agenda.BusId == BusId)
        //        {
        //            this.repository.DeleteById<AgendaBus>(agenda.Id);
        //        }
        //    }
        //}

        //public void eliminarAgendaSegunViajeId(int BusId, int viajeId) // eliminar una agenda segun Busid y viajeid
        //{
        //    foreach (var agenda in this.repository.Traer<AgendaBus>())
        //    {
        //        if (agenda.BusId == BusId && agenda.ViajeId == viajeId)
        //        {
        //            this.repository.DeleteById<AgendaBus>(agenda.Id);
        //        }
        //    }
        //}

    }
}

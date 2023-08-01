using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.DTO.ViajeResponseDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.Application.Services
{
    public class ViajeServices : ServicesGeneric, IViajeServices
    {
        private readonly IViajeRepository repository;
        public ViajeServices(IViajeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public ViajeResponseDTO AddViaje(ViajeDTO viajeDTO)
        {
            if (viajeDTO.TerminalOrigen1Id == viajeDTO.TerminalOrigen2Id || viajeDTO.TerminalOrigen1Id == viajeDTO.TerminalOrigen3Id ||
                viajeDTO.TerminalOrigen2Id == viajeDTO.TerminalOrigen3Id)
                throw new Exception("Hay un error: Hay terminales repetidas");
            var viaje = new Viaje()
            {
                FechaSalida = viajeDTO.FechaSalida,
                FechaVuelta = viajeDTO.FechaLlegada,
                TerminalOrigen1Id = viajeDTO.TerminalOrigen1Id,
                TerminalOrigen2Id = viajeDTO.TerminalOrigen2Id,
                TerminalOrigen3Id = viajeDTO.TerminalOrigen3Id,
                GrupoId = viajeDTO.GrupoId,
                BusId = viajeDTO.BusId,
                Chofer1Id = viajeDTO.Chofer1Id,
                Chofer2Id = viajeDTO.Chofer2Id,
                CoordinadorId = viajeDTO.CoordinadorId,
                IdentificadorUnicoDeViaje = "V-FS" + viajeDTO.FechaSalida.ToString() + "FL"
                + viajeDTO.FechaLlegada.ToString() + "-" + Guid.NewGuid().ToString(),
                //BusId =1,
                //Chofer1Id = 1,
                //Chofer2Id = 1         //a futuro: sembrar datos id1: "Sin designar"
            };      

            this.Repository.Add(viaje);

            // marcar grupo. esto se hace por si hay que actualizar el grupo, la idea es limitar
            // la cantidad de pasajeros que se le puede agregar, en funcion del bus

            //var grupo = this.repository.FindBy<Grupo>(viajeDTO.GrupoId);

            //grupo.BusId = viajeDTO.BusId;
            //grupo.CapacidadDeBus = this.repository.FindBy<Bus>(viajeDTO.BusId).Capacidad;

            //this.repository.Update(grupo);

            //// agregar agenda de chofer 1

            //var agendaChofer1 = new AgendaChofer()
            //{
            //    ChoferId = viajeDTO.Chofer1Id,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaChofer>(agendaChofer1);

            //// agregar agenda de chofer 2

            //var agendaChofer2 = new AgendaChofer()
            //{
            //    ChoferId = viajeDTO.Chofer2Id,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaChofer>(agendaChofer2);

            //// agregar agenda de coordinador

            //var agendaCoordinador = new AgendaCoordinador()
            //{
            //    CoordinadorId = viajeDTO.CoordinadorId,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaCoordinador>(agendaCoordinador);

            //// agregar agenda de coordinador

            //var agendaBus = new AgendaBus()
            //{
            //    BusId = viajeDTO.CoordinadorId,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaBus>(agendaBus);

            return ToViajeResponseDTO(viaje);
        }

        public ViajeResponseDTO ActualizarViaje(int id, ViajeDTO viajeDTO)
        {
            if (viajeDTO.TerminalOrigen1Id == viajeDTO.TerminalOrigen2Id || viajeDTO.TerminalOrigen1Id == viajeDTO.TerminalOrigen3Id ||
                viajeDTO.TerminalOrigen2Id == viajeDTO.TerminalOrigen3Id)
                throw new Exception("Hay un error: Hay terminales repetidas");

            if (this.repository.FindBy<Grupo>(viajeDTO.GrupoId).TotalPasajeros > this.repository.FindBy<Bus>(viajeDTO.BusId).Capacidad)
                throw new Exception("La cantidad de pasajeros supera a la capacidad del bus");

            var viaje = this.repository.FindBy<Viaje>(id);

            viaje.TerminalOrigen1Id = viajeDTO.TerminalOrigen1Id;
            viaje.TerminalOrigen2Id = viajeDTO.TerminalOrigen2Id;
            viaje.TerminalOrigen3Id = viajeDTO.TerminalOrigen3Id;
            viaje.GrupoId = viajeDTO.GrupoId;
            viaje.BusId = viajeDTO.BusId;
            viaje.Chofer1Id = viajeDTO.Chofer1Id;
            viaje.Chofer2Id = viajeDTO.Chofer2Id;
            viaje.CoordinadorId = viajeDTO.CoordinadorId;

            this.repository.Update(viaje);

            // al grupo marcarle la capacidad maxima

            var grupo = this.repository.FindBy<Grupo>(viajeDTO.GrupoId);

            grupo.BusId = viajeDTO.BusId;
            grupo.CapacidadDeBus = this.repository.FindBy<Bus>(viajeDTO.BusId).Capacidad;

            this.repository.Update(grupo);

            //// borrar agendas previas que estaban marcadas para este viaje

            //// de choferes

            //foreach (var agenda in this.repository.Traer<AgendaChofer>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaChofer>(agenda.Id);
            //    }
            //}

            //// de coordinadores

            //foreach (var agenda in this.repository.Traer<AgendaCoordinador>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaCoordinador>(agenda.Id);
            //    }
            //}

            //// de bus

            //foreach(var agenda in this.repository.Traer<AgendaBus>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaBus>(agenda.Id);
            //    }
            //}

            //// y ahora si, cargar agendas de nuevo
            //// agregar agenda de chofer 1

            //var agendaChofer1 = new AgendaChofer()
            //{
            //    ChoferId = viajeDTO.Chofer1Id,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaChofer>(agendaChofer1);

            //// agregar agenda de chofer 2

            //var agendaChofer2 = new AgendaChofer()
            //{
            //    ChoferId = viajeDTO.Chofer2Id,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaChofer>(agendaChofer2);

            //// agregar agenda de coordinador

            //var agendaCoordinador = new AgendaCoordinador()
            //{
            //    CoordinadorId = viajeDTO.CoordinadorId,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaCoordinador>(agendaCoordinador);

            //// agregar agenda de coordinador

            //var agendaBus = new AgendaBus()
            //{
            //    BusId = viajeDTO.CoordinadorId,
            //    FechaInicial = viajeDTO.FechaSalida,
            //    FechaFinal = viajeDTO.FechaLlegada,
            //    ViajeId = viaje.ViajeId,
            //};

            //this.repository.Add<AgendaBus>(agendaBus);

            return ToViajeResponseDTO(viaje);
        }

        public ViajeResponseDTO GetViajeById(int id)
        {
            Viaje viaje = repository.FindBy<Viaje>(id);
            if (viaje == null)
                throw new Exception($"El viaje id:{id} no existe");
            return ToViajeResponseDTO(viaje);
        }

        //public List<ViajeCompletoDTO> GetCompleto()
        //{
        //    List<ViajeCompletoDTO> viajesCompletos = new List<ViajeCompletoDTO>();
        //    var viajes = this.Repository.Traer<Viaje>();
        //    foreach (Viaje viaje in viajes)
        //    {
        //        viajesCompletos.Add(repository.GetViajeCompleto(viaje));
        //    }
        //    return viajesCompletos;
        //}

        public List<ViajeResponseDTO> GetAll()
        {
            var listaViajes = new List<ViajeResponseDTO>();
            var viajes = repository.Traer<Viaje>().ToList();
            foreach (var viaje in viajes)
            {
                listaViajes.Add(ToViajeResponseDTO(viaje));
            }
            return listaViajes;
        }

        public List<ViajeResponseDTO> GetBy(int GrupoId)
        {
            var listaViajes = new List<ViajeResponseDTO>();
            var viajes = repository.Traer<Viaje>().ToList();
            foreach (var viaje in viajes)
            {
                if(viaje.GrupoId == GrupoId)
                {
                    listaViajes.Add(ToViajeResponseDTO(viaje));
                }
            }
            return listaViajes;
        }

        public ViajeResponseDTO AddDesignacion(int viajeId, DesignacionDTO designacionDTO)
        {
            var viaje = repository.FindBy<Viaje>(viajeId);
            if (viaje == null)
                throw new Exception($"Viaje id:{viajeId} no existe");
            viaje.Chofer1Id = designacionDTO.Chofer1;
            viaje.Chofer2Id = designacionDTO.Chofer2;
            viaje.BusId = designacionDTO.BusId;
            viaje.CoordinadorId = designacionDTO.CoordinadorId;
            repository.Update<Viaje>(viaje);

            return ToViajeResponseDTO(viaje);
        }


        public ViajeResponseDTO ToViajeResponseDTO(Viaje viaje)
        {
            var viajeResponseDTO = new ViajeResponseDTO
            {
                ViajeId = viaje.ViajeId,
                FechaSalida = viaje.FechaSalida.ToShortDateString(),
                HoraSalida = viaje.FechaSalida.ToShortTimeString(),
                FechaSalidaSinFormato = viaje.FechaSalida.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                FechaLlegada = viaje.FechaVuelta.ToShortDateString(),
                HoraVuelta = viaje.FechaVuelta.ToShortTimeString(),
                FechaLlegadaSinFormato = viaje.FechaVuelta.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),                
                TerminalOrigen1Id = viaje.TerminalOrigen1Id,
                TerminalOrigen2Id = viaje.TerminalOrigen2Id,
                TerminalOrigen3Id = viaje.TerminalOrigen3Id,
                GrupoId = viaje.GrupoId,
                BusId = viaje.BusId,
                Chofer1Id = viaje.Chofer1Id,
                Chofer2Id = viaje.Chofer2Id,
                CoordinadorId = viaje.CoordinadorId,
            };
            if(viaje.BusId != 0)
            {
                var bus = repository.FindBy<Bus>(viaje.BusId);
                viajeResponseDTO.BusDescripcion = bus.Observacion;
                if (bus.EmpresaId != 0)
                {
                    viajeResponseDTO.BusDescripcion += " Emp:" + repository.FindBy<Empresa>(bus.EmpresaId).Nombre;
                }
                if(bus.Numero != "")
                {
                    viajeResponseDTO.BusDescripcion += " N°" + bus.Numero;
                }
                if(bus.Patente != "")
                {
                    viajeResponseDTO.BusDescripcion += " Pat:" + bus.Patente;
                }
            }
            else
            {
                viajeResponseDTO.BusDescripcion = "Sin designar";
            }
            if(viaje.Chofer1Id != 0)
            {
                var chofer1 = repository.FindBy<Chofer>(viaje.Chofer1Id);
                viajeResponseDTO.Chofer1 = chofer1.Nombre + " " + chofer1.Apellido;
                viajeResponseDTO.Contacto1 = chofer1.Contacto;
            }
            else
            {
                viajeResponseDTO.Chofer1 = "Sin designar";
                viajeResponseDTO.Contacto1 = "";

            }
            if(viaje.Chofer2Id != 0)
            {
                var chofer2 = repository.FindBy<Chofer>(viaje.Chofer2Id);
                viajeResponseDTO.Chofer2 = chofer2.Nombre + " " + chofer2.Apellido;
                viajeResponseDTO.Contacto2 = chofer2.Contacto;
            }
            else
            {
                viajeResponseDTO.Chofer2 = "Sin designar";
                viajeResponseDTO.Contacto2 = "";
            }
            if (viaje.TerminalOrigen1Id != 0)
            {
                var TerminalOrigen1 = repository.FindBy<Terminal>(viaje.TerminalOrigen1Id).Nombre;
                viajeResponseDTO.TerminalOrigen1 = TerminalOrigen1;
            }
            else
            {
                viajeResponseDTO.TerminalOrigen1 = "Sin designar";
            }
            if (viaje.TerminalOrigen2Id != 0)
            {
                var TerminalOrigen2 = repository.FindBy<Terminal>(viaje.TerminalOrigen2Id).Nombre;
                viajeResponseDTO.TerminalOrigen2 = TerminalOrigen2;
            }
            else
            {
                viajeResponseDTO.TerminalOrigen2 = "Sin designar";
            }
            if (viaje.TerminalOrigen3Id != 0)
            {
                var TerminalOrigen3 = repository.FindBy<Terminal>(viaje.TerminalOrigen3Id).Nombre;
                viajeResponseDTO.TerminalOrigen1 = TerminalOrigen3;
            }
            else
            {
                viajeResponseDTO.TerminalOrigen3 = "Sin designar";
            }
            return viajeResponseDTO;
        }


        //public Task<ViajeCompletoDTO> GetViajesById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ViajeCompletoDTO>> GetCursoByIdLista(List<int> idViajes)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ViajeCompletoDTO> GetViajeById(int idViaje)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ViajeCompletoDTO>> GetViajeByIdLista(List<int> idViajes)
        //{
        //    throw new NotImplementedException();
        //}

        public void DeleteViajeById(int id)
        {
            // borrar agenda de choferes

            //foreach (var agenda in this.repository.Traer<AgendaChofer>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaChofer>(agenda.Id);
            //    }
            //}

            // borrar agenda de coordinador

            //foreach (var agenda in this.repository.Traer<AgendaCoordinador>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaCoordinador>(agenda.Id);
            //        break; // ya que solo hay un coordinador asignado al viaje
            //    }
            //}

            // borrar agenda de bus

            //foreach (var agenda in this.repository.Traer<AgendaBus>())
            //{
            //    if (agenda.ViajeId == id)
            //    {
            //        this.repository.DeleteById<AgendaBus>(agenda.Id);
            //        break; // ya que solo hay un bus asignado al viaje
            //    }
            //}

            // si el viaje tiene un grupo asignado, quitarlo

            int grupoId = this.repository.FindBy<Viaje>(id).GrupoId;

            if (this.repository.FindBy<Viaje>(id).GrupoId != 0)
            {
                var grupo = this.repository.FindBy<Grupo>(grupoId);

                grupo.ViajeId = 0;

                this.repository.Update(grupo);
            }

            // borrar viaje

            this.Repository.DeleteById<Viaje>(id);

            // no olvidar quitarle el viaje a las reservas correspondientes

        }
    }
}

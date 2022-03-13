using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface IBusService : IServicesGeneric 
    {
        //void Delete(int id);
        void DeleteBusById(int id);
        BusResponseDTO AddBus(BusDTO busDTO);
        BusResponseDTO GetBusById(int id);
        List<BusResponseDTO> GetAll();
        public List<BusResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal);
        public BusResponseDTO ActualizarBus(int id, BusDTO busDTO);
        //Task<ViajeConBusTodoDTO> GetByIdConEmpresa(int id);
        public AgendaBusDTO agregarAgenda(AgendaBusDTO agendaDTO);
        public List<AgendaBusResponseDTO> retornarAgenda(int BusId);
        public List<AgendaBusResponseConFormatoDTO> retornarAgendaConFormato(int BusId);
        public bool checkSiFechaEstaOcupada(int BusId, DateTime fecha);
        public AgendaBusResponseDTO retornarAgendaDeEsosDias(int BusId, DateTime fecha);
        public void vaciarAgendaDeBus(int BusId);
        public void eliminarAgendaSegunViajeId(int BusId, int viajeId);
    }
}

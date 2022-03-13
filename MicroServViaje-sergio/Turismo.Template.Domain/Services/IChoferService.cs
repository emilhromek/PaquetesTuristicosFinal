using System;
using System.Collections.Generic;
using Turismo.Template.Domain.DTO.Chofer;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface IChoferService
    {
        ChoferResponseDTO AddChofer(ChoferDTO choferDTO);
        ChoferResponseDTO GetChoferById(int id);
        List<ChoferResponseDTO> GetAll();
        public List<ChoferResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal);
        void DeleteChoferById(int id);
        public ChoferResponseDTO ActualizarChofer(int id, ChoferDTO choferDTO);
        public AgendaChoferDTO agregarAgenda(AgendaChoferDTO agendaDTO);
        public List<AgendaChoferResponseDTO> retornarAgenda(int ChoferId);
        public List<AgendaChoferResponseConFormatoDTO> retornarAgendaConFormato(int ChoferId);
        public bool checkSiFechaEstaOcupada(int ChoferId, DateTime fecha);
        public AgendaChoferResponseDTO retornarAgendaDeEsosDias(int ChoferId, DateTime fecha);
        public void vaciarAgendaDeChofer(int ChoferId);
        public void eliminarAgendaSegunViajeId(int ChoferId, int viajeId);

    }
}

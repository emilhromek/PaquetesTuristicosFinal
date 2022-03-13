using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface ICoordinadorService : IServicesGeneric
    {
        CoordinadorResponseDTO AddCoordinador(CoordinadorDTO coordinadorDTO);
        CoordinadorResponseDTO GetCoordinadorById(int id);
        List<CoordinadorResponseDTO> GetAll();
        public List<CoordinadorResponseDTO> GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal);
        public void DeleteCoordinadorById(int id);
        public CoordinadorResponseDTO ActualizarCoordinador(int id, CoordinadorDTO coordinadorDTO);
        //List<CoordinadorCompletoDTO> GetCompleto();
        //void DeleteCoordinadorById(int id);
        public AgendaCoordinadorDTO agregarAgenda(AgendaCoordinadorDTO agendaDTO);
        public List<AgendaCoordinadorResponseDTO> retornarAgenda(int CoordinadorId);
        public List<AgendaCoordinadorResponseConFormatoDTO> retornarAgendaConFormato(int CoordinadorId);
        public bool checkSiFechaEstaOcupada(int CoordinadorId, DateTime fecha);
        public AgendaCoordinadorResponseDTO retornarAgendaDeEsosDias(int CoordinadorId, DateTime fecha);
        public void vaciarAgendaDeCoordinador(int CoordinadorId);
        public void eliminarAgendaSegunViajeId(int CoordinadorId, int viajeId);



    }
}

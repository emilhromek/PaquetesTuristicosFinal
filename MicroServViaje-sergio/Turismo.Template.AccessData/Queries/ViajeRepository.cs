using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.DTO.Chofer;
using Turismo.Template.Domain.DTO.ViajeResponseDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Turismo.Template.AccessData.Queries
{
    public class ViajeRepository : GenericsRepository, IViajeRepository
    {

        public ViajeRepository(DbContextGeneric contexto) : base(contexto)
        {
        }

        public ViajeDTO GetViajeById(Viaje viajeoriginal)
        {
            throw new NotImplementedException();
        }

        //public ViajeCompletoDTO GetViajeCompleto(Viaje c)
        //{
        //    throw new NotImplementedException();
        //}

        //public ViajeCompletoDTO GetViajeCompleto(Viaje viaje)
        //{
        //    BusDTO bus;
        //    ChoferDTO chofer;
        //    ViajeCompletoDTO viajeCompleto = new ViajeCompletoDTO()
        //    {
        //        FechaIda = viaje.FechaIda,
        //        FechaVuelta = viaje.FechaVuelta,
        //        Hora = viaje.Hora,
        //        Excursion = viaje.Excursion,
        //        CoordinadorId = viaje.CoordinadorId,
        //        TipoId = viaje.TipoId,
        //        TerminalId = viaje.TerminalId,
        //        Buses = viaje.Buses,

        //    };
        //    List<BusDTO> listaBuses = new List<BusDTO>();

        //    var lista = Context.Buses
        //        .Include(x => x.Viaje)
        //        .Include(x => x.Empresa)
        //        .Where(x => x.ViajeId == viaje.ViajeId)
        //        .ToList();


        //    viajeCompleto.Buses = listaPreguntas;
        //    return cuestionarioCompleto;
    }
    
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.DTO.Empresa;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class EmpresaRepository : GenericsRepository, IEmpresaRepository
    {
        private readonly DbContextGeneric context;

        public EmpresaRepository(DbContextGeneric contexto) : base(contexto)
        {
            this.context = contexto;
        }

        //public EmpresaCompletaDTO GetEmpresaCompleta(Empresa c)
        //{
        //    throw new NotImplementedException();
        //}

        //public EmpresaCompletaDTO GetEmpresaCompleta(Empresa empresa)
        //{
        //    BusDTO busDto;
        //    ViajeDTO viaje;
        //    EmpresaCompletaDTO empresaCompleta = new EmpresaCompletaDTO()
        //    {
        //        Nombre = empresa.Nombre,
        //        Email = empresa.Email,
        //        Contacto = empresa.Contacto
        //    };
        //    List<BusDTO> listaBus = new List<BusDTO>();

        //    var lista = Context.Buses
        //        .Include(x => x.Empresa)
        //        .Include(x => x.Viaje)
        //        .Where(x => x.EmpresaId == empresa.EmpresaId)
        //        .ToList();

        //    foreach (Bus bus in lista)
        //    {
        //        List<ViajeDTO> listaViajes = new List<ViajeDTO>();

        //        foreach (Viaje viaje1 in bus.Viaje)
        //        {
        //            viaje = new ViajeDTO()
        //            {
        //                FechaIda = respuestas.Descripcion,
        //                FechaVuelta = respuestas.Flag,
        //                Excursion,
        //                Hora,
        //                Descripcion,
        //                CoordinadorId,
        //                TerminalId,
        //                TipoId
        //            };
        //            listaViajes.Add(viaje);
        //        }
        //        busDto = new BusDTO()
        //        {
        //            Numero = preguntas.Descripcion,
        //            Patente = listaRespuestas,
        //            Capacidad = preguntas.Descripcion,
        //            EmpresaId = preguntas.Descripcion,
        //            ViajeId = preguntas.Descripcion,

        //        };
        //        listaBus.Add(busDto);
        //    }
        //    empresaCompleta.Buses = listaBus;
        //    return empresaCompleta;
        
    }
}

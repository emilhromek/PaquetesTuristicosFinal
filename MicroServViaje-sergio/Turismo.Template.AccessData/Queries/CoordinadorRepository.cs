using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class CoordinadorRepository : GenericsRepository, ICoordinadorRepository
    {

        public CoordinadorRepository(DbContextGeneric contexto) : base(contexto)
        {
        }

        public CoordinadorDTO GetCoordinadorById(Coordinador coordinadororiginal)
        {
            throw new NotImplementedException();
        }

        //public CoordinadorDTO GetCoordinadorById(Coordinador coordinadorOriginal)
        //{
        //    var CoordinadorDTO = new CoordinadorDTO
        //    {
        //        Nombre = coordinadorOriginal.Nombre,
        //        Apellido = coordinadorOriginal.Apellido,
        //        Email = coordinadorOriginal.Email,
        //        Agenda = coordinadorOriginal.Agenda,
        //        Contacto = coordinadorOriginal.Contacto,
        //        ViajeId = coordinadorOriginal.ViajeId
        //    };
        //}

        //public CoordinadorCompletoDTO GetCoordinadorCompleto(Coordinador v)
        //{
        //    throw new NotImplementedException();
        //}

    }
}

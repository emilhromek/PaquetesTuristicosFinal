using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO.Grupo;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;
using Turismo.Template.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Turismo.Template.Domain.Commands;
using System;

namespace Turismo.Template.Application.Services
{
    public class GrupoService : ServicesGeneric, IGrupoService
    {
        private readonly IGrupoRepository repository;
        public GrupoService(IGrupoRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public GrupoResponseDTO AddGrupo(GrupoDTO grupoDTO)
        {
            if (grupoDTO.BusId != 0 && grupoDTO.TotalPasajeros > repository.FindBy<Bus>(grupoDTO.BusId).Capacidad)
            {
                throw new Exception($"Este grupo tiene un bus asignado, se esta superando la capacidad");
            }

            if (grupoDTO.TotalPasajeros > 100)
            {
                throw new Exception($"El grupo no puede tener mas de 100 pasajeros, es es el limite maximo permitido");
            }

            var grupo = new Grupo()
            {
                TotalPasajeros = grupoDTO.TotalPasajeros,
                PaqueteId = grupoDTO.PaqueteId,
            };

            this.Repository.Add(grupo);

            return ToGrupoResponseDTO(grupo);
        }

        public GrupoResponseDTO GetGrupoById(int id)
        {
            var grupo = repository.FindBy<Grupo>(id);
            if (grupo == null)
                throw new Exception($"El grupo id:{id} no existe");
            return ToGrupoResponseDTO(repository.FindBy<Grupo>(id));
        }

        public void DeleteGrupoById(int id)
        {
            this.Repository.DeleteById<Grupo>(id);
        }

        public List<GrupoResponseDTO> GetAll()
        {
            var lista_grupo = new List<GrupoResponseDTO>();
            var grupos = repository.Traer<Grupo>().ToList();
            foreach (var grupo in grupos)
            {
                lista_grupo.Add(ToGrupoResponseDTO(grupo));
            }
            return lista_grupo;
        }

        public List<GrupoResponseDTO> GetAllPorPaquete(int PaqueteId)
        {
            var lista_grupo = new List<GrupoResponseDTO>();
            var grupos = repository.Traer<Grupo>().ToList();
            foreach (var grupo in grupos)
            {
                if (grupo.PaqueteId == PaqueteId)
                {
                    lista_grupo.Add(ToGrupoResponseDTO(grupo));
                }
            }
            return lista_grupo;
        }

        public GrupoResponseDTO ToGrupoResponseDTO(Grupo grupo)
        {
            //var coordinador = repository.FindBy<Coordinador>(grupo.CoordinadorId);
            return new GrupoResponseDTO
            {
                GrupoId = grupo.GrupoId,
                TotalPasajeros = grupo.TotalPasajeros,
                PaqueteId = grupo.PaqueteId,
                ViajeId = grupo.ViajeId,
                //NombreApe = coordinador.Nombre + " " + coordinador.Apellido,
                //Contacto = coordinador.Contacto,
            };

        }

        public GrupoResponseDTO PatchGrupoViaje(int id, int viajeId) // asignar viaje a un grupo
        {
            var grupo = repository.FindBy<Grupo>(id);

            if (grupo == null)
            {
                throw new Exception("No existe el grupo");
            }

            var nuevoGrupo = new Grupo()
            {
                GrupoId = id,
                TotalPasajeros = grupo.TotalPasajeros,
                PaqueteId = grupo.PaqueteId,
                ViajeId = viajeId,
            };

            this.Repository.Update<Grupo>(nuevoGrupo);

            return ToGrupoResponseDTO(nuevoGrupo);

        }

        public GrupoResponseDTO UnpatchGrupoViaje(int id) // desasignar viaje a un grupo
        {
            var grupo = repository.FindBy<Grupo>(id);

            if (grupo == null)
            {
                throw new Exception("No existe el grupo");
            }

            var nuevoGrupo = new Grupo()
            {
                GrupoId = id,
                TotalPasajeros = grupo.TotalPasajeros,
                PaqueteId = grupo.PaqueteId,
                ViajeId = 0,
            };

            this.Repository.Update<Grupo>(nuevoGrupo);

            return ToGrupoResponseDTO(nuevoGrupo);

        }
    }
}

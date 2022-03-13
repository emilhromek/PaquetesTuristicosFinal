using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO.Empresa;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.Application.Services
{
    public class EmpresaService : ServicesGeneric, IEmpresaService
    {
        private readonly IEmpresaRepository repository;
        public EmpresaService(IEmpresaRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public EmpresaResponseDTO  AddEmpresa(EmpresaDTO empresaDTO)
        {
            var empresa = new Empresa()
            {
                Nombre = empresaDTO.Nombre,
                Contacto = empresaDTO.Contacto,
                Email = empresaDTO.Email
            };

            this.Repository.Add(empresa);

            return ToEmpresaResponseDTO(empresa);
        }

        public EmpresaResponseDTO GetEmpresaById(int id)
        {
            var empresa = repository.FindBy<Empresa>(id);
            if (empresa == null)
                throw new Exception($"La empresa id:{id} no existe");
            return ToEmpresaResponseDTO(empresa);
        }

        //public List<EmpresaCompletaDTO> GetCompleto()
        //{
        //    List<EmpresaCompletaDTO> empresaCompletas = new List<EmpresaCompletaDTO>();
        //    var empresas = this.Repository.Traer<Empresa>();
        //    foreach (Empresa empresa in empresas)
        //    {
        //        empresaCompletas.Add(repository.GetEmpresaCompleta(empresa));
        //    }
        //    return empresaCompletas;
        //}

        public List<EmpresaResponseDTO> GetAll()
        {
            var lista_empresa = new List<EmpresaResponseDTO>();
            var empresas = repository.Traer<Empresa>().ToList();
            foreach (var empresa in empresas)
            {
                lista_empresa.Add(ToEmpresaResponseDTO(empresa));
            }
            return lista_empresa;
        }

        public void DeleteEmpresaById(int id)
        {
            repository.DeleteById<Empresa>(id);

            // borrar todos los buses de la empresa y sus agendas

            foreach (var bus in repository.Traer<Bus>())
            {
                if (bus.EmpresaId == id){
                    repository.DeleteById<Bus>(bus.BusId);

                    foreach(var agenda in repository.Traer<AgendaBus>())
                    {
                        if (agenda.BusId == bus.BusId)
                        {
                            repository.DeleteById<AgendaBus>(agenda.Id);
                        }
                    }

                    // dejar huerfanos a los viajes de los buses

                    foreach (var viaje in repository.Traer<Viaje>())
                    {
                        if (viaje.BusId == bus.BusId)
                        {
                            viaje.BusId = 0;

                            repository.Update(viaje);
                        }
                    }
                }
            }

        }

        public EmpresaResponseDTO ActualizarEmpresa(int id, EmpresaDTO empresaDTO)
        {
            var empresa = new Empresa()
            {
                EmpresaId = id,
                Nombre = empresaDTO.Nombre,
                Contacto = empresaDTO.Contacto,
                Email = empresaDTO.Email
            };

            this.Repository.Update<Empresa>(empresa);

            return ToEmpresaResponseDTO(empresa);

        }

        public EmpresaResponseDTO ToEmpresaResponseDTO(Empresa empresa)
        {
            return new EmpresaResponseDTO
            {
                EmpresaId = empresa.EmpresaId,
                Nombre = empresa.Nombre,
                Email = empresa.Email,
                Contacto = empresa.Contacto
            };
        }

    }
}

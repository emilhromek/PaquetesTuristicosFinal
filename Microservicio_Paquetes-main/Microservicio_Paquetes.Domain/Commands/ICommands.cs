using System;
using System.Collections.Generic;
using System.Text;


namespace Microservicio_Paquetes.Domain.Commands
{
    public interface ICommands
    {
        void Agregar<T>(T entity) where T : class;
        void Actualizar<T>(T entity) where T : class;
        void Borrar<T>(T entity) where T : class;
        void BorrarPor<T>(int id) where T : class;
        //Agregado por Emil
        void ActualizarPor<T>(int id) where T : class;
    }
}

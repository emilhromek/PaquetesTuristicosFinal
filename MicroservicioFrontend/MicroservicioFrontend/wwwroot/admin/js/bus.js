// Bus 
var msBus = `https://localhost:44385/api/bus/`
var msEmpresa = `https://localhost:44385/api/empresa/`
var msChofer = `https://localhost:44385/api/chofer/`

var bus = document.querySelector('#contenido')
pintarBus()
function pintarBus(){
    fetch(msBus, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            Bus(datos)
        })
}
function pintarEmpresa(){
    fetch(msEmpresa, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            empresa(datos)
        })
}

function pintarChofer(){
    fetch(msChofer, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            chofer(datos)
        })
}

function verAgendaBus(id){

    var formularioAgendaBus = document.getElementById('verAgendaBusForm');
    formularioAgendaBus.innerHTML = ''
    formularioAgendaBus.innerHTML = `
    
    <h5 class="card-title">Bus</h5>
    <div class="row justify-content-center align-items-center">
    <table class="table">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Fecha inicial</th>
            <th scope="col">Fecha final</th>
            <th scope="col">Viaje (#)</th>
          </tr>
        </thead>
        <tbody id="agendaBusCardBody">
        </tbody>
        </table>
    </div>   
      
    </div>`

    fetch(msBus + `retornarAgendaConFormato?BusId=` + id, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var tabla = document.querySelector('#agendaBusCardBody')
    for (let valor of datos)
        {
        tabla.innerHTML +=`
        <tr>
        <th scope="row">Agenda</th>
        <td>${valor.fechaInicial}</td>
        <td>${valor.fechaFinal}</td>
        <td>${valor.viajeId}</td>
        `    
        }

    })
}

//crear transporte 
function createBus(){  
  var formularioBusCargar = document.getElementById('cargarBusForm');
  var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
  var datos = new FormData(formularioBusCargar);
  console.log(datos)
  let jsonDataConvert = JSON.stringify(
      {
        numero: datos.get('numero'),
        patente: datos.get('patente'),     
        capacidad: new Number(datos.get('capacidad')),
        empresaId: new Number(datos.get('empresaId')),
        observacion: datos.get('observacion'),
      }               
  );
  console.log(jsonDataConvert)

  fetch(msBus, {
      method: 'POST',
      body: jsonDataConvert,
      headers: myHeaders,
      
  })
      .then(res => res.json())
      .then(datos => {
          console.log(datos)
          alert("Bus creado")
          location.reload()
      })
}

// modificar transporte
  
function actualizarBus(id){  
    var formularioBusEdit = document.getElementById('actualizarBusForm');
    fetch(msBus+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
      .then(res => res.json())
      .then(data => {
          console.log(data)
          formularioBusEdit.querySelector('.numero').value = data.numero,
          formularioBusEdit.querySelector('.patente').value = data.patente,
          formularioBusEdit.querySelector('.capacidad').value = data.capacidad,
          formularioBusEdit.querySelector('.observacion').value = data.observacion,
          formularioBusEdit.querySelector('.empresaId').innerHTML = `<option selected value="${data.empresaId}">${data.empresa}</option>`
      })

      var botonActualizar = document.getElementById('actualizarBus')
      botonActualizar.addEventListener('click', function(e){
          e.preventDefault();
          ejecutarActualizarBus(id)
      });

      function ejecutarActualizarBus(id){  
        var formularioBusActualizar = document.getElementById('actualizarBusForm');
        var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
        let datos = new FormData(formularioBusActualizar);
        
        let jsonDataConvert = JSON.stringify(
            {
                id: id,       
              numero: datos.get('numero'),
              patente: datos.get('patente'),     
              capacidad: new Number(datos.get('capacidad')),
              empresaId: new Number(datos.get('empresaId')),
              observacion: datos.get('observacion'),
            }               
        );
        console.log(jsonDataConvert)
    
        fetch(msBus+`?id=${id}`, {
            method: 'PUT',
            body: jsonDataConvert,
            headers: myHeaders,
            
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                alert("Bus actualizado")
                location.reload()
                
            })
    };
};

// eliminar transporte
function deleteBus(id){

    var botonEliminar = document.getElementById('eliminarBus');
        botonEliminar.addEventListener('click', function (e){
            e.preventDefault();
            ejecutarEliminarBus(id)
        });

        function ejecutarEliminarBus(id){
            fetch(msBus+`${id}`, {
                method: 'DELETE',
                headers: myHeaders,
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Bus eliminado")
                    location.reload()
                })
        }    
  };

// carga datos en pantalla 
function Bus(data) {
    bus.innerHTML = ''
    bus.innerHTML = `
    <div class="row">
    <h3>Bus</h3>
      </div>
<div class="row justify-content-center align-items-center">
<table id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Numero</th>
        <th scope="col">Patente</th>
        <th scope="col">Capacidad</th>
        <th scope="col">Empresa</th>
        <th scope="col">Telefono</th>
        <th scope="col">Email</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyViaje">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyViaje')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.busId}</th>
        <td>${valor.numero}</td>
        <td>${valor.patente}</td>
        <td>${valor.capacidad}</td>
        <td>${valor.empresa}</td>
        <td>${valor.empresaContacto}</td>
        <td>${valor.empresaEmail}</td>
        <td>
            <button type="button" class="btn btn-outline-warning" data-bs-toggle="modal" data-bs-target="#verAgendaBusModal" onclick="verAgendaBus(${valor.busId})">Ver agenda</button>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarBusModal" onclick="actualizarBus(${valor.busId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarBusModal" onclick="deleteBus(${valor.busId})">Borrar</button>
        </td>
      </tr>`
    }
}

function cargarEmpresasParaBuses(){
    fetch(msEmpresa, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect = document.querySelector('#selectEmpresaBus')
            for (let valor of datos)
            {
                menuSelect.innerHTML +=`<option value="${valor.empresaId}">${valor.nombre}</option>`
            }

        })
        
}

cargarEmpresasParaBuses();

function empresa(data) {
    bus.innerHTML = ''
    bus.innerHTML = `
    <div class="row">
    <h3>Empresas</h3>
      </div>

      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarEmpresaModal">Nueva empresa</button>  
      <!-- Modal -->
      <div class="modal fade" id="cargarEmpresaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nueva empresa</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="cargarEmpresaModal-body">
                      <form id="cargarEmpresaForm">
                      <p>Ingrese nombre:</p>                  
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3" required />
                          <p>Ingrese e-mail:</p>
                          <input type="email" name="email" placeholder="Ingrese email" class="form-control my-3" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createEmpresa()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>

      <div class="modal fade" id="actualizarEmpresaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar empresa</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="actualizarEmpresaModal-body">
                      <form id="actualizarEmpresaForm">
                          <p>Ingrese nombre:</p>                  
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3 nombre" required />
                          <p>Ingrese e-mail:</p>
                          <input type="text" name="email" placeholder="Ingrese email" class="form-control my-3 email" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3 contacto" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarEmpresa">Actualizar</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div> 

      <!-- Modal eliminar empresa -->
      <div class="modal fade" id="eliminarEmpresaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar destino</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar empresa?</p> 
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-danger" id="eliminarEmpresa">Eliminar</button>
                          </div>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>

<div class="row justify-content-center align-items-center">
<table id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">nombre</th>
        <th scope="col">email</th>
        <th scope="col">contacto</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyViaje">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyViaje')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.empresaId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.email}</td>
        <td>${valor.contacto}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarEmpresaModal" onclick="actualizarEmpresa(${valor.empresaId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarEmpresaModal" onclick="deleteEmpresa(${valor.empresaId})">Borrar</button>
        </td>
      </tr>`

    }
}

function chofer(data) {
    bus.innerHTML = ''
    bus.innerHTML = `
    <div class="row">
    <h3>Chofer</h3>
    </div>
      
      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarChoferModal">Nuevo chofer</button>  
      <!-- Modal -->
      <div class="modal fade" id="cargarChoferModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo chofer</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="cargarChoferModal-body">
                      <form id="cargarChoferForm">
                      <p>Ingrese nombre:</p>      
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3" required />
                          <p>Ingrese apellido:</p>
                          <input type="text" name="apellido" placeholder="Ingrese apellido" class="form-control my-3" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3" required />
                          <p>Ingrese e-mail:</p>
                          <input type="text" name="email" placeholder="Ingrese e-mail" class="form-control my-3" required />
                          <p>Ingrese licencia:</p>
                          <input type="text" name="licencia" placeholder="Ingrese licencia" class="form-control my-3" required />
                          <p>Ingrese vencimiento:</p>
                          <input type="text" name="vencimiento" placeholder="Ingrese vencimiento" class="form-control my-3" required />
                          <p>Ingrese agenda:</p>
                          <input type="text" name="agenda" placeholder="Ingrese agenda" class="form-control my-3" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createChofer()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal actualizar chofer-->
      <!-- Modal -->
      <div class="modal fade" id="actualizarChoferModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo chofer</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="actualizarChoferModal-body">
                      <form id="actualizarChoferForm">  
                      <p>Ingrese nombre:</p>                      
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3 nombre" required />
                          <p>Ingrese apellido:</p>
                          <input type="text" name="apellido" placeholder="Ingrese apellido" class="form-control my-3 apellido" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3 contacto" required />
                          <p>Ingrese e-mail:</p>
                          <input type="text" name="email" placeholder="Ingrese e-mail" class="form-control my-3 email" required />
                          <p>Ingrese licencia:</p>
                          <input type="text" name="licencia" placeholder="Ingrese licencia" class="form-control my-3 licencia" required />
                          <p>Ingrese vencimiento:</p>
                          <input type="text" name="vencimiento" placeholder="Ingrese vencimiento" class="form-control my-3 vencimiento" required />
                          <p>Ingrese agenda:</p>
                          <input type="text" name="agenda" placeholder="Ingrese agenda" class="form-control my-3 agenda" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarChofer">Actualizar</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal eliminar chofer -->
      <div class="modal fade" id="eliminarChoferModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar chofer</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar destino?</p> 
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-danger" id="eliminarChofer">Eliminar</button>
                          </div>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>

<div class="row justify-content-center align-items-center">
<table id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">nombre</th>
        <th scope="col">apellido</th>
        <th scope="col">contacto</th>
        <th scope="col">email</th>
        <th scope="col">licencia</th>
        <th scope="col">vencimiento</th>
        <th scope="col">agenda</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyViaje">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyViaje')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.choferId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.apellido}</td>
        <td>${valor.contacto}</td>
        <td>${valor.email}</td>
        <td>${valor.licencia}</td>
        <td>${valor.vencimiento}</td>
        <td>${valor.agenda}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarChoferModal" onclick="actualizarChofer(${valor.choferId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarChoferModal" onclick="deleteChofer(${valor.choferId})">Borrar</button>
        </td>
      </tr>`
    }
}

//crear chofer
function createChofer(){  
    var formularioChoferCargar = document.getElementById('cargarChoferForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    var datos = new FormData(formularioChoferCargar);
    console.log(datos)
    let jsonDataConvert = JSON.stringify(
        {
          nombre: datos.get('nombre'),
          apellido: datos.get('apellido'),     
          contacto: datos.get('contacto'),
          email: datos.get('email'),
          licencia: datos.get('licencia'),
          vencimiento: datos.get('vencimiento'),
          agenda: datos.get('agenda'),
        }               
    );
    console.log(jsonDataConvert)
  
    fetch(msChofer, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Chofer creado")
            location.reload()
        })
  }

  // modificar chofer
  
  function actualizarChofer(id){  
    var formularioChoferEdit = document.getElementById('actualizarChoferForm');
    fetch(msChofer+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
      .then(res => res.json())
      .then(data => {
          console.log(data)
          formularioChoferEdit.querySelector('.nombre').value = data.nombre,
          formularioChoferEdit.querySelector('.apellido').value = data.apellido,
          formularioChoferEdit.querySelector('.contacto').value = data.contacto,
          formularioChoferEdit.querySelector('.email').value = data.email,
          formularioChoferEdit.querySelector('.licencia').value = data.licencia,
          formularioChoferEdit.querySelector('.vencimiento').value = data.vencimiento,
          formularioChoferEdit.querySelector('.agenda').value = data.agenda
      })

      var botonActualizar = document.getElementById('actualizarChofer')
      botonActualizar.addEventListener('click', function(e){
          e.preventDefault();
          ejecutarActualizarChofer(id)
      });

      function ejecutarActualizarChofer(id){  
        var formularioChoferActualizar = document.getElementById('actualizarChoferForm');
        var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
        let datos = new FormData(formularioChoferActualizar);
        
        let jsonDataConvert = JSON.stringify(
            {
                id: id,       
                nombre: datos.get('nombre'),
                apellido: datos.get('apellido'),     
                contacto: datos.get('contacto'),
                email: datos.get('email'),
                licencia: datos.get('licencia'),
                vencimiento: datos.get('vencimiento'),
                agenda: datos.get('agenda'),
            }               
        );
        console.log(jsonDataConvert)
    
        fetch(msChofer+`?id=${id}`, {
            method: 'PUT',
            body: jsonDataConvert,
            headers: myHeaders,
            
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                alert("Chofer actualizado")
                location.reload()
                
            })
    };

};
  
  // eliminar chofer
  function deleteChofer(id){

    var botonEliminar = document.getElementById('eliminarChofer');
        botonEliminar.addEventListener('click', function (e){
            e.preventDefault();
            ejecutarEliminarChofer(id)
        });

        function ejecutarEliminarChofer(id){
            fetch(msChofer+`${id}`, {
                method: 'DELETE',
                headers: myHeaders,
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Chofer eliminado")
                    location.reload()
                })
        }      
    };

//crear empresa
function createEmpresa(){  
    var formularioEmpresaCargar = document.getElementById('cargarEmpresaForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    var datos = new FormData(formularioEmpresaCargar);
    console.log(datos)
    let jsonDataConvert = JSON.stringify(
        {
          nombre: datos.get('nombre'),
          email: datos.get('email'),     
          contacto: datos.get('contacto'),
        }               
    );
    console.log(jsonDataConvert)
  
    fetch(msEmpresa, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Empresa creada")
            location.reload()
        })
  }
  
  // modificar empresa
  
  function actualizarEmpresa(id){  
      var formularioEmpresaEdit = document.getElementById('actualizarEmpresaForm');
      fetch(msEmpresa+`${id}`, {
          method: 'GET',
          headers: myHeaders,
      })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            formularioEmpresaEdit.querySelector('.nombre').value = data.nombre,
            formularioEmpresaEdit.querySelector('.email').value = data.email,
            formularioEmpresaEdit.querySelector('.contacto').value = data.contacto
        })

        var botonActualizar = document.getElementById('actualizarEmpresa')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarEmpresa(id)
        });

        function ejecutarActualizarEmpresa(id){
            var formularioEmpresaActualizar = document.getElementById('actualizarEmpresaForm');
      var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
      let datos = new FormData(formularioEmpresaActualizar);
      
      let jsonDataConvert = JSON.stringify(
          {
              id: id,       
              nombre: datos.get('nombre'),
          email: datos.get('email'),     
          contacto: datos.get('contacto'),
          }               
      );
      console.log(jsonDataConvert)
  
      fetch(msEmpresa+`?id=${id}`, {
          method: 'PUT',
          body: jsonDataConvert,
          headers: myHeaders,
          
      })
          .then(res => res.json())
          .then(datos => {
              console.log(datos)
              alert("Empresa actualizada")
              location.reload()
              
          })
        }

  };
  
  // eliminar empresa
  function deleteEmpresa(id){
        var botonEliminar = document.getElementById('eliminarEmpresa');
        botonEliminar.addEventListener('click', function (e){
            e.preventDefault();
            ejecutarEliminarEmpresa(id)
        });

        function ejecutarEliminarEmpresa(id){
            fetch(msEmpresa+`${id}`, {
                method: 'DELETE',
                headers: myHeaders,
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Empresa eliminada")
                    location.reload()
                })

        }      
    };





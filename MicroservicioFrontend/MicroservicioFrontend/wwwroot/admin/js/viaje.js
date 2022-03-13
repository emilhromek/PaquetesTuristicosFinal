// Viaje 
// var bottomViaje = document.getElementById('bottomViaje');
var msViaje = `https://localhost:44385/api/viajes/`
var mstipoViaje = `https://localhost:44385/api/tipoviajes/`
var msterminal = `https://localhost:44385/api/terminal/`
var mscoordinadores = `https://localhost:44385/api/coordinadores/`
var msgrupos = `https://localhost:44385/api/grupos/`
var mschoferes = `https://localhost:44385/api/chofer/`
var msbus = `https://localhost:44385/api/bus/`

var mspaquete = `https://localhost:44341/api/Paquetes/`
var msreservas = `https://localhost:44341/api/reserva/`

var Viaje = document.querySelector('#contenido')
pintarViajes()
function pintarViajes(){
    fetch(msViaje, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            viajes(datos)
        })
}

function pintarTerminales(){
    fetch(msterminal, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            terminales(datos)
        })
}

function pintarCoordinadores(){
    fetch(mscoordinadores, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            coordinadores(datos)
        })
}

function pintarGrupos(){
    fetch(msgrupos, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            grupos(datos)
        })
}

// carga datos en pantalla 
function viajes(data) {


    function cargarPaquetesParaViajes(){
        fetch(mspaquete, {
            method: 'GET',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                var menuSelect = document.querySelector('#selectPaqueteViaje')
                for (let valor of datos)
                {
                    menuSelect.innerHTML +=`<option value="${valor.id}">${valor.nombre}</option>`
                }

            })
            
    }          

    cargarPaquetesParaViajes();

    Viaje.innerHTML = ''
    Viaje.innerHTML = `
    <div class="row">
    <h3>Viajes</h3>
      </div>
<div class="row justify-content-center align-items-center">
<table id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Salida</th>
        <th scope="col">Hola</th>
        <th scope="col">Tipo</th>
        <th scope="col">Origen</th>
        <th scope="col">Destino</th>
        <th scope="col">Paquete</th>
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
        <th scope="row">${valor.viajeId}</th>
        <td>${valor.fechaSalida}</td>
        <td>${valor.horaSalida}</td>
        <td>${valor.tipoViaje}</td>
        <td>${valor.terminalOrigen}</td>
        <td>${valor.terminalDestino}</td>
        <td>${valor.paqueteId}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#editarViajeModal" onclick="editViaje(${valor.viajeId})">Designar personal y bus</button>
            <button type="button" class="btn btn-outline-danger" onclick="deliteViaje(${valor.viajeId})">Borrar</button>
        </td>
      </tr>`
    }
}

//a medida que cambio el paquete, cambian los grupos para mostrar y los choferes

$('#selectPaqueteViaje').on('change', function (){    

    document.getElementById("selectGrupoViaje").innerHTML = ""; // alerta!!! tuve que usar esto porque con jquery no funcionaba

    var selectedItem2 = $('#selectPaqueteViaje').val();    

    fetch(mspaquete+ selectedItem2, {
        method: 'GET',
        headers: myHeaders
    })
    .then(res => res.json())
    .then(datos =>{

        var fechaDeArriboDelViaje = document.querySelector('#fechaSalida')

        fechaDeArriboDelViaje.value = datos.fechaSalidaSinFormato

        var fechaDeArriboDelViaje = document.querySelector('#fechaArribo')

        fechaDeArriboDelViaje.value = datos.fechaArriboSinFormato        
        
        var fechaDePartidaDelViaje = document.querySelector('#fechaPartida')

        fechaDePartidaDelViaje.value = datos.fechaPartidaSinFormato  

        var fechaDePartidaDelViaje = document.querySelector('#fechaLlegada')

        fechaDePartidaDelViaje.value = datos.fechaLlegadaSinFormato

        console.log(datos.fechaSalidaSinFormato)
        console.log(datos.fechaLlegadaSinFormato)
        
        cargarChoferes1ParaViajes(datos.fechaSalidaSinFormato, datos.fechaLlegadaSinFormato);        

    });    
    
    fetch(msgrupos+`porPaquete?PaqueteId=` + selectedItem2, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {

            var menuSelect = document.querySelector('#selectGrupoViaje')
            
            for (let valor of datos)
            {
                if (valor.viajeId == 0)
                {
                    menuSelect.innerHTML +=`<option value="${valor.grupoId}">Grupo # ${valor.grupoId} - Pasajeros: ${valor.totalPasajeros}</option>`
                }
                
            }

        });

    console.log(this.value,
                this.options[this.selectedIndex].value,
                $(this).find("option:selected").val(),);


});

$('#selectChofer1Viaje').on('change', function (){

    document.querySelector('#selectChofer2Viaje').innerHTML = '';

    var fechaSalida = $('#fechaSalida').val();
    var fechaLlegada = $('#fechaLlegada').val();
    var opcionElegidaChofer1 = $('#selectChofer1Viaje').val();

    fetch(mschoferes + `queEstenLibres?fechaInicial=` + fechaSalida + `&fechaFinal=` + fechaLlegada, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            
            let choferesSinChofer1 = datos.filter(chofer => chofer.choferId != opcionElegidaChofer1);

            var menuSelect2 = document.querySelector('#selectChofer2Viaje')

            console.log(choferesSinChofer1)

            for (let valor of choferesSinChofer1)
            {
                menuSelect2.innerHTML +=`<option value="${valor.choferId}"># ${valor.choferId} - ${valor.nombre} ${valor.apellido}</option>`
            }

        })

});


function cargarChoferes1ParaViajes(fechaSalida, fechaLlegada){    

    document.querySelector('#selectChofer1Viaje').innerHTML = '';
    document.querySelector('#selectChofer2Viaje').innerHTML = '';

    fetch(mschoferes + `queEstenLibres?fechaInicial=` + fechaSalida + `&fechaFinal=` + fechaLlegada, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect1 = document.querySelector('#selectChofer1Viaje')
            for (let valor of datos)
            {
                menuSelect1.innerHTML +=`<option value="${valor.choferId}"># ${valor.choferId} - ${valor.nombre} ${valor.apellido}</option>`
            }

            var opcionElegida = $('#selectChofer1Viaje').val();
            console.log(opcionElegida)
            
            let choferesSinChofer1 = datos.filter(chofer => chofer.choferId != opcionElegida);

            var menuSelect2 = document.querySelector('#selectChofer2Viaje')

            console.log(choferesSinChofer1)

            for (let valor of choferesSinChofer1)
            {
                menuSelect2.innerHTML +=`<option value="${valor.choferId}"># ${valor.choferId} - ${valor.nombre} ${valor.apellido}</option>`
            }

        })
        
}

function cargarTerminales1ParaViajes(){

    document.querySelector('#selectTerminal1Id').innerHTML = '<option selected disabled>Elija primer terminal</option>';
    document.querySelector('#selectTerminal2Id').innerHTML = '<option selected disabled>Elija segunda terminal (opcional)</option></option><option value="0">Sin designar</option';
    document.querySelector('#selectTerminal3Id').innerHTML = '<option selected disabled>Elija tercera terminal (opcional)</option></option><option value="0">Sin designar</option';

    fetch(msterminal, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect1 = document.querySelector('#selectTerminal1Id')
            for (let valor of datos)
            {
                menuSelect1.innerHTML +=`<option value="${valor.terminalId}"># ${valor.terminalId} - ${valor.nombre}</option>`
            }

        })
        
}

function cargarTerminales2paraViajes(){

    document.querySelector('#selectTerminal2Id').innerHTML = '<option selected disabled>Elija segunda terminal (opcional)</option><option value="0">Sin designar</option>';
    document.querySelector('#selectTerminal3Id').innerHTML = '<option selected disabled>Elija tercera terminal (opcional)</option><option value="0">Sin designar</option>';

    var opcionElegida = $('#selectTerminal1Id').val();
    console.log(opcionElegida)

    fetch(msterminal, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
    
    let terminalesSinTerminal1 = datos.filter(terminal => terminal.terminalId != opcionElegida);

    var menuSelect2 = document.querySelector('#selectTerminal2Id')

    console.log(terminalesSinTerminal1)

    for (let valor of terminalesSinTerminal1)
    {
        menuSelect2.innerHTML +=`<option value="${valor.terminalId}"># ${valor.terminalId} - ${valor.nombre}</option>`
    }
})
        
}

function cargarTerminales3paraViajes(){

    document.querySelector('#selectTerminal3Id').innerHTML = '<option selected disabled>Elija tercera terminal (opcional)</option><option value="0">Sin designar</option>';

    var opcionElegida = $('#selectTerminal1Id').val();
    var opcionElegida2 = $('#selectTerminal2Id').val();
    console.log(opcionElegida)

    fetch(msterminal, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
    
    let terminalesSinTerminal1 = datos.filter(terminal => terminal.terminalId != opcionElegida && terminal.terminalId != opcionElegida2);

    var menuSelect3 = document.querySelector('#selectTerminal3Id')

    console.log(terminalesSinTerminal1)

    for (let valor of terminalesSinTerminal1)
    {
        menuSelect3.innerHTML +=`<option value="${valor.terminalId}"># ${valor.terminalId} - ${valor.nombre}</option>`
    }
})
        
}

cargarTerminales1ParaViajes();

$('#selectTerminal1Id').on('change', function (){    

    cargarTerminales2paraViajes()
});

$('#selectTerminal2Id').on('change', function (){

    cargarTerminales3paraViajes()
});


//



// function cargarChoferes2ParaViajes(){
//     fetch(mschoferes, {
//         method: 'GET',
//         headers: myHeaders,
//     })
//         .then(res => res.json())
//         .then(datos => {
//             var menuSelect = document.querySelector('#selectChofer2Viaje')
//             for (let valor of datos)
//             {
//                 menuSelect.innerHTML +=`<option value="${valor.choferId}"># ${valor.choferId} -${valor.nombre} ${valor.apellido}</option>`
//             }

//         })
        
// }        

// cargarChoferes2ParaViajes();

function cargarCoordinadoresParaViajes(){
    fetch(mscoordinadores, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect = document.querySelector('#selectCoordinadorViaje')
            for (let valor of datos)
            {
                menuSelect.innerHTML +=`<option value="${valor.coordinadorId}"># ${valor.coordinadorId} -${valor.nombre} ${valor.apellido}</option>`
            }

        })
        
}

cargarCoordinadoresParaViajes();

function cargarBusesParaViajes(){
    fetch(msbus, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect = document.querySelector('#selectBusViaje')
            for (let valor of datos)
            {
                menuSelect.innerHTML +=`<option value="${valor.busId}"># ${valor.busId} - ${valor.numero} - Capacidad: ${valor.capacidad}</option>`
            }

        })
        
}

cargarBusesParaViajes();

// una vez elegido el grupo,  fijar horario de salida

// $('#selectGrupoViaje').on('change', function (){

//     document.getElementById("selectGrupoViaje").innerHTML = ""; // alerta!!! tuve que usar esto porque con jquery no funcionaba

//     var selectedItem2 = $('#selectPaqueteViaje').val();    
    
//     fetch(msgrupos+`porPaquete?PaqueteId=` + selectedItem2, {
//         method: 'GET',
//         headers: myHeaders,
//     })
//         .then(res => res.json())
//         .then(datos => {

//             var menuSelect = document.querySelector('#selectGrupoViaje')
            
//             for (let valor of datos)
//             {
//                 if(valor.viajeId == 0)
//                 {
//                     menuSelect.innerHTML +=`<option value="${valor.grupoId}">Grupo # ${valor.grupoId} - Pasajeros: ${valor.totalPasajeros}</option>`
//                 }                
                           
//             }

//         });

//     console.log(this.value,
//                 this.options[this.selectedIndex].value,
//                 $(this).find("option:selected").val(),);


// });




// carga datos en pantalla Terminal
function terminales(data) {
    Viaje.innerHTML = ''
    Viaje.innerHTML = `
    <div class="row">
    <h3>Terminales</h3>
      </div>

      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarTerminalModal">
      Nueva terminal
      </button> 

      <!-- Modal -->
      <div class="modal fade" id="cargarTerminalModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nueva terminal/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargarTerminalForm">    
                      <p>Ingrese terminal:</p>               
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3" required />
                          <p>Ingrese descripción</p>
                          <input type="text" name="descripcion" placeholder="Ingrese descripción" class="form-control my-3" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createTerminal()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal actualizar terminal -->
      <div class="modal fade" id="actualizarTerminalModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar terminal/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="actualizarTerminalForm">    
                      <p>Ingrese terminal:</p>               
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3 nombre" required />
                          <p>Ingrese descripción</p>
                          <input type="text" name="descripcion" placeholder="Ingrese descripción" class="form-control my-3 descripcion" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarTerminal">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>
      
      <!-- Modal eliminar terminal -->
      <div class="modal fade" id="eliminarTerminalModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar terminal</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar terminal?</p> 
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-danger" id="eliminarTerminal">Eliminar</button>
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
        <th scope="col">descripcion</th>
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
        <th scope="row">${valor.terminalId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.descripcion}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarTerminalModal" onclick="actualizarTerminal(${valor.terminalId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarTerminalModal" onclick="deleteTerminal(${valor.terminalId})">Borrar</button>
        </td>
      </tr>`
    }
}

// carga datos en pantalla Coordinadores
function coordinadores(data) {
    Viaje.innerHTML = ''
    Viaje.innerHTML = `
    <div class="row">
    <h3>Coordinadores</h3>
      </div>

      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarCoordinadorModal">
      Nuevo coordinador
      </button> 

      <!-- Modal -->
      <div class="modal fade" id="cargarCoordinadorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo coordinador/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargarCoordinadorForm">    
                      <p>Ingrese nombre:</p>               
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3" required />
                          <p>Ingrese apellido:</p>
                          <input type="text" name="apellido" placeholder="Ingrese apellido" class="form-control my-3" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3" required />
                          <p>Ingrese e-mail:</p>
                          <input type="text" name="email" placeholder="Ingrese e-mail" class="form-control my-3" required />
                          <p>Ingrese agenda:</p>
                          <input type="text" name="agenda" placeholder="Ingrese agenda" class="form-control my-3" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createCoordinador()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal actualizar coordinador -->
      <div class="modal fade" id="actualizarCoordinadorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo coordinador/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="actualizarCoordinadorForm">  
                      <p>Ingrese nombre:</p>                  
                          <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3 nombre" required />
                          <p>Ingrese apellido:</p>
                          <input type="text" name="apellido" placeholder="Ingrese apellido" class="form-control my-3 apellido" required />
                          <p>Ingrese contacto:</p>
                          <input type="text" name="contacto" placeholder="Ingrese contacto" class="form-control my-3 contacto" required />
                          <p>Ingrese e-mail:</p>
                          <input type="text" name="email" placeholder="Ingrese e-mail" class="form-control my-3 email" required />
                          <p>Ingrese agenda:</p>
                          <input type="text" name="agenda" placeholder="Ingrese agenda" class="form-control my-3 agenda" required />
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarCoordinador">Actualizar</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>     

      <!-- Modal eliminar coordinador -->
      <div class="modal fade" id="eliminarCoordinadorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar destino</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar coordinador?</p> 
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-danger" id="eliminarCoordinador">Eliminar</button>
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
        <th scope="row">${valor.coordinadorId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.apellido}</td>
        <td>${valor.contacto}</td>
        <td>${valor.email}</td>
        <td>${valor.agenda}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarCoordinadorModal" onclick="actualizarCoordinador(${valor.coordinadorId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deleteCoordinador(${valor.coordinadorId})">Borrar</button>
        </td>
      </tr>`

    }
}

// carga datos en pantalla Grupos
function grupos(data) {

    function cargarPaquetesParaGrupos(){
        fetch(mspaquete, {
            method: 'GET',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                var menuSelect = document.querySelector('#selectPaqueteGrupo')
                for (let valor of datos)
                {
                    menuSelect.innerHTML +=`<option value="${valor.id}">${valor.nombre}</option>`
                }

            })
            
    }          

    cargarPaquetesParaGrupos();   

    Viaje.innerHTML = ''
    Viaje.innerHTML = `
    <div class="row">
    <h3>Grupos</h3>
      </div>

      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarGrupoModal">
      Nuevo grupo
      </button> 

      <!-- Modal -->
      <div class="modal fade" id="cargarGrupoModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo grupo/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargarGrupoForm">         
                      <p>Elija el paquete:</p>   
                      <select class="form-select" name="idPaquete" id="selectPaqueteGrupo" aria-label="Default select example">
                            <option selected disabled>Elegir paquete</option>
                          </select>
                          <br>
                          </p>Elija las reservas que se agregan al grupo:</p>
                          <div class="col" id="elegirReservasParaElGrupo">
                                 
                        </div>
                        <div class="row">
                        <p>Total de pasajeros: </p>
                        <p id="totalDePasajeros">0</p>
                        </div>
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createGrupo()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal actualizar grupo-->
      <div class="modal fade" id="actualizarTerminalModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar terminal/a</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="actualizarGrupoModal-body">
                      
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>  

      <!-- Modal eliminar grupo -->
      <div class="modal fade" id="eliminarGrupoModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar destino</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar destino?</p> 
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-danger" id="eliminarGrupo">Eliminar</button>
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
        <th scope="col">Total de pasajeros</th>
        <th scope="col">Paquete</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyViaje">
    </tbody>
    </table>
</div>`

//a medida que cambio el paquete, cambian las reservas para mostrar

$('#selectPaqueteGrupo').on('change', function (){

    document.getElementById("elegirReservasParaElGrupo").innerHTML = ""; // alerta!!! tuve que usar esto porque con jquery no funcionaba

    var selectedItem2 = $('#selectPaqueteGrupo').val();
    
    fetch(msreservas+`?PaqueteId=` + selectedItem2, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {

            var checkboxesExcursiones = document.querySelector('#elegirReservasParaElGrupo')
            
            for (let valor of datos)
            {
                if(valor.grupoId == 0) // si la reserva tiene grupo = 0, entonces no esta asignada a ningun grupo
                    {
                        checkboxesExcursiones.innerHTML +=`<div class="form-check">
                        <input class="form-check-input reservacheck" name="idreserva" type="checkbox" value="${valor.id}" id="flexCheckDefault">
                        <label class="form-check-label" for="flexCheckDefault">
                        Reserva # ${valor.id}. Pasajeros: ${valor.pasajeros}
                        </label>
                        </div>`
                    }                
            }

        });

    console.log(this.value,
                this.options[this.selectedIndex].value,
                $(this).find("option:selected").val(),);


});

// actualizar el total de pasajeros a medida que hago clic en las reservas

$(document).on('change', '.reservacheck', function(){

    var checkboxesExcursiones = document.querySelector('#elegirReservasParaElGrupo')

    var checkboxes = checkboxesExcursiones.querySelectorAll('input[type=checkbox]:checked')

    let totalPasajeros = 0;

    for (var i = 0; i < checkboxes.length; i++) {
        
    
    fetch(msreservas + checkboxes[i].value, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)

            totalPasajeros = totalPasajeros + new Number(datos.pasajeros);
            document.getElementById('totalDePasajeros').innerHTML = new Number(totalPasajeros);
        })
    }
    
    if (checkboxes.length == 0)
    {
        document.getElementById('totalDePasajeros').innerHTML = 0;
    }


});


var tabla = document.querySelector('#tbodyViaje')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.grupoId}</th>
        <td>${valor.totalPasajeros}</td>
        <td>${valor.paqueteId}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarTerminalModal">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deleteGrupo(${valor.grupoId})">Borrar</button>
        </td>
      </tr>`

        // modal para actualizar grupo

        $("#actualizarGrupoModal-body").html(`<form id="actualizarGrupoForm">                   
        <input type="text" name="totalPasajeros" placeholder="Ingrese total de pasajeros" class="form-control my-3" required />
        <input type="text" name="coordinadorId" placeholder="Ingrese coordinador id" class="form-control my-3" required />
        <input type="text" name="paqueteId" placeholder="Ingrese paquete id" class="form-control my-3" required />
        
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            <button type="button" class="btn btn-primary" onclick="actualizarGrupo(${valor.grupoId})">Actualizar</button>
        </div>
    </form>`);
      
    }
}

//crear coordinador 
function createCoordinador(){  
    var formularioCoordinadorCargar = document.getElementById('cargarCoordinadorForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioCoordinadorCargar);
    
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            apellido: datos.get('apellido'),
            contacto: datos.get('contacto'),
            email: datos.get('email'),
            agenda: datos.get('agenda'),
        }               
    );
    console.log(jsonDataConvert)

    fetch(mscoordinadores, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Coordinador creado")
            location.reload()
            
        })
};

//actualizar coordinador

function actualizarCoordinador(id){
    var formularioCoordinadorEdit = document.getElementById('actualizarCoordinadorForm');
    fetch (mscoordinadores+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)            
            formularioCoordinadorEdit.querySelector('.nombre').value = data.nombre,
            formularioCoordinadorEdit.querySelector('.apellido').value = data.apellido,
            formularioCoordinadorEdit.querySelector('.contacto').value = data.contacto,
            formularioCoordinadorEdit.querySelector('.email').value = data.email,
            formularioCoordinadorEdit.querySelector('.agenda').value = data.agenda
        })

        var botonActualizar = document.getElementById('actualizarCoordinador')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarCoordinador(id)
        });

        function ejecutarActualizarCoordinador(id){  
            var formularioCoordinadorActualizar = document.getElementById('actualizarCoordinadorForm');
            var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
            let datos = new FormData(formularioCoordinadorActualizar);
            
            let jsonDataConvert = JSON.stringify(
                {
                    coordinadorId: id,
                    nombre: datos.get('nombre'),
                    apellido: datos.get('apellido'),
                    contacto: datos.get('contacto'),
                    email: datos.get('email'),
                    agenda: datos.get('agenda'),
                }               
            );
            console.log(jsonDataConvert)
        
            fetch(mscoordinadores+`?id=${id}`, {
                method: 'PUT',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Coordinador actualizado")
                    location.reload()
                    
                })
        };
};

// eliminar coordinador
function deleteCoordinador(id){
    fetch(mscoordinadores+`${id}`, {
      method: 'DELETE',
      headers: myHeaders,
  })
      .then(res => res.json())
      .then(datos => {
          console.log(datos)
          alert("Coordinador eliminado")
          location.reload()
      })
  };

  //crear terminal 
function createTerminal(){  
    var formularioTerminalCargar = document.getElementById('cargarTerminalForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioTerminalCargar);
    
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            descripcion: datos.get('descripcion'),
        }               
    );
    console.log(jsonDataConvert)

    fetch(msterminal, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Terminal creada")
            location.reload()
            
        })
};

//actualizar terminal

function actualizarTerminal(id){
    var formularioTerminalEdit = document.getElementById('actualizarTerminalForm');
    fetch (msterminal+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)            
            formularioTerminalEdit.querySelector('.nombre').value = data.nombre,
            formularioTerminalEdit.querySelector('.descripcion').value = data.descripcion
        })

        var botonActualizar = document.getElementById('actualizarTerminal')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarTerminal(id)
        });

        function ejecutarActualizarTerminal(id){  
            var formularioTerminalActualizar = document.getElementById('actualizarTerminalForm');
            var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
            let datos = new FormData(formularioTerminalActualizar);
            
            let jsonDataConvert = JSON.stringify(
                {
                    terminalId: id,
                    nombre: datos.get('nombre'),
                    descripcion: datos.get('descripcion'),
                }               
            );
            console.log(jsonDataConvert)
        
            fetch(msterminal+`?id=${id}`, {
                method: 'PUT',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Terminal actualizado")
                    location.reload()
                    
                })
        };
};


// eliminar terminal
function deleteTerminal(id){

    var botonEliminar = document.getElementById('eliminarTerminal')
    botonEliminar.addEventListener('click', function(e){
        e.preventDefault();
        ejecutarEliminarTerminal(id)
    });

    function ejecutarEliminarTerminal(id)
    {
        fetch(msterminal+`${id}`, {
            method: 'DELETE',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                alert("Terminal eliminado")
                location.reload()
            })
    }    
  };

//crear Viaje 
function createViaje(){  
    var formularioViajeCargar = document.getElementById('cargarViajeForm');
    var ViajeRespuestaCargar = document.getElementById('cargarViajeRespuesta');
    var datos = new FormData(formularioViajeCargar);

    let jsonDataConvert = JSON.stringify(
        {
            fechaSalida: datos.get('fechaSalida'),
            fechaLlegada: datos.get('fechaLlegada'),
            tipoViajeId: new Number(datos.get('tipoViajeId')),
            terminalOrigenId: new Number(datos.get('terminalOrigenId')),
            terminalDestinoId: new Number(datos.get('terminalDestinoId')),
            paqueteId: new Number(datos.get('paqueteId')),
            grupoId: new Number(datos.get('grupoId')),
            chofer1Id: new Number(datos.get('chofer1Id')),
            chofer2Id: new Number(datos.get('chofer2Id')),
            coordinadorId: new Number(datos.get('coordinador')),
            busId: new Number(datos.get('bus'))
        }               
    );
    console.log(jsonDataConvert)

    fetch(msViaje, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)

            // una vez creado el viaje, marcar al grupo correspondiente con ese viaje

            fetch(msgrupos + `asignarViaje?id=` + new Number(datos.grupoId) + `&viajeId=` + new Number(datos.viajeId), {
                method: 'PATCH',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Grupo marcado con el viaje" + datos.viajeId)
                    location.reload()
                    
                })

            // una vez creado el viaje, tambien marcar a todas las reservas correspondientes con ese viaje

            fetch(msreservas + `asignarViajeSegunGrupo?grupoId=` + new Number(datos.grupoId) + `&viajeId=` + new Number(datos.viajeId), {
                method: 'PATCH',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Reservas marcado con el viaje" + datos.viajeId)
                    location.reload()
                    
                })

            alert("Viaje creado")
            location.reload()
        })
}
// editar Viaje 
function editViaje(id){
    var formularioViajeEdit = document.getElementById('EditarViajeForm');
    var ViajeRespuestaEdit = document.getElementById('editViajeRespuesta');

    console.log(id);

    fetch(msViaje+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            formularioViajeEdit.querySelector('.selectPaqueteViaje').innerHTML = `<option selected value="${data.paqueteId}">${data.paqueteId}</option>`
            formularioViajeEdit.querySelector('.selectGrupoViaje').innerHTML = `<option selected value="${data.grupoId}">${data.grupoId}</option>`
            formularioViajeEdit.querySelector('.fechaSalida').value = data.fechaSalidaSinFormato,
            formularioViajeEdit.querySelector('.fechaLlegada').value = data.fechaLlegadaSinFormato,
            formularioViajeEdit.querySelector('.terminalOrigenId').value = data.terminalOrigenId,
            formularioViajeEdit.querySelector('.terminalDestinoId').value = data.terminalDestinoId
        })
        var bottomEdit = document.getElementById('editarViaje')
        bottomEdit.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarEdit(id)
        });
        function ejecutarEdit(id){
            var formularioViajeEdit = document.getElementById('editViajeForm');
            var datos = new FormData(formularioViajeEdit);
            let jsonDataConvertEdit = JSON.stringify(
                {
                    nombre: datos.get('chofer1Id'),
                    apellido: datos.get('chofer2Id'),
                    email: datos.get('coordinador'),
                    password: datos.get('bus')
                }               
            )
            
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Viaje editado")
                    location.reload()
                })

                fetch(msViaje+ `designaciones/${id}`, {
                    method: 'POST',
                    body: jsonDataConvertEdit,
                    headers: myHeaders,
                    
                })
                    .then(res => res.json())
                    .then(datos => {
                        console.log(datos)
                        alert("Designacion exitosa")
                        location.reload()
                    })
                }
                
    
                // //agregar agenda de chofer 1
                // var formularioViajeEdit = document.getElementById('editViajeForm');
                // var datos = new FormData(formularioViajeEdit);
                // let jsonDataConvertEdit = JSON.stringify(
                //     {
                //         fechaInicial: datos.get('fechaSalida'),
                //         fechaFinal: datos.get('fechaLlegada'),
                //         viajeId: document.getElementById('viajeId'),
                //         choferId: datos.get('selectChofer1Viaje')
                //     }               
                // );
                // fetch(mschoferes+ `agregarAgenda/`, {
                //     method: 'POST',
                //     body: jsonDataConvertEdit,
                //     headers: myHeaders,
                    
                // })
                // .then(res => res.json())
                //     .then(datos => {
                //         console.log(datos)
                //         alert("Agenda exitosa")
                //         location.reload()
                //     })  

                // //agregar agenda de chofer 2
                // var formularioViajeEdit = document.getElementById('editViajeForm');
                // var datos = new FormData(formularioViajeEdit);
                // let jsonDataConvertEdit = JSON.stringify(
                //     {
                //         fechaInicial: datos.get('fechaSalida'),
                //         fechaFinal: datos.get('fechaLlegada'),
                //         viajeId: document.getElementById('viajeId'),
                //         choferId: datos.get('selectChofer1Viaje')
                //     }               
                // );
                // fetch(mschoferes+ `agregarAgenda/`, {
                //     method: 'POST',
                //     body: jsonDataConvertEdit,
                //     headers: myHeaders,
                    
                // })
                // .then(res => res.json())
                //     .then(datos => {
                //         console.log(datos)
                //         alert("Agenda exitosa")
                //         location.reload()
                //     })  

                //     //agregar agenda de coordinador
                // var formularioViajeEdit = document.getElementById('editViajeForm');
                // var datos = new FormData(formularioViajeEdit);
                // let jsonDataConvertEdit = JSON.stringify(
                //     {
                //         fechaInicial: datos.get('fechaSalida'),
                //         fechaFinal: datos.get('fechaLlegada'),
                //         viajeId: document.getElementById(''),
                //         coordinadorId: datos.get('selectCoordinadorViaje')
                //     }               
                // );
                // fetch(mscoordinadores+ `agregarAgenda/`, {
                //     method: 'POST',
                //     body: jsonDataConvertEdit,
                //     headers: myHeaders,
                    
                // })
                // .then(res => res.json())
                //     .then(datos => {
                //         console.log(datos)
                //         alert("Agenda exitosa")
                //         location.reload()
                //     })  

                //         //agregar agenda de bus
                // var formularioViajeEdit = document.getElementById('editViajeForm');
                // var datos = new FormData(formularioViajeEdit);
                // let jsonDataConvertEdit = JSON.stringify(
                //     {
                //         fechaInicial: datos.get('fechaSalida'),
                //         fechaFinal: datos.get('fechaLlegada'),
                //         viajeId: document.getElementById('viajeId'),
                //         coordinadorId: datos.get('selectBusViaje')
                //     }               
                // );
                // fetch(msbus+ `agregarAgenda/`, {
                //     method: 'POST',
                //     body: jsonDataConvertEdit,
                //     headers: myHeaders,
                    
                // })
                // .then(res => res.json())
                //     .then(datos => {
                //         console.log(datos)
                //         alert("Agenda exitosa")
                //         location.reload()
                //     })  
            
            

  };

// eliminar Viaje 
function deliteViaje(id){
  fetch(msViaje+`${id}`, {
    method: 'DELETE',
    headers: myHeaders,
})
    .then(res => res.json())
    .then(datos => {
        console.log(datos)
        alert("Viaje eliminado")
        location.reload()
    })
};

function tipoViaje(id){
    fetch( mstipoViaje+`${id}?idTipoViaje=${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            
        })
}
function terminal(id){
    fetch( msterminal+`GetTerminalById/${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            return datos
        })
}

//crear grupo
function createGrupo(){  
    var formularioGrupoCargar = document.getElementById('cargarGrupoForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioGrupoCargar);

    var arrayReservas = []
    var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

    for (var i = 0; i < checkboxes.length; i++) {
    arrayReservas.push(new Number(checkboxes[i].value))
    }   
    
    let jsonDataConvert = JSON.stringify(
        {
            totalPasajeros: new Number(document.getElementById('totalDePasajeros').innerHTML),
            paqueteId: new Number(datos.get('idPaquete')),
        }               
    );
    console.log(jsonDataConvert)

    fetch(msgrupos, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)

            for (var i = 0; i < arrayReservas.length; i++)
            {
            fetch(msreservas + `asignarGrupo?id=` + arrayReservas[i] + `&grupoId=` + new Number(datos.grupoId), {
                method: 'PATCH',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Reservas marcadas con el grupo" + datos.GrupoId)
                    location.reload()
                    
                })
            };

            alert("Grupo creado con el id " + datos.GrupoId)
            location.reload()
            
        });
};

//actualizar grupo
function actualizarGrupo(id){  
    var formularioGrupoActualizar = document.getElementById('actualizarGrupoForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioGrupoActualizar);
    
    let jsonDataConvert = JSON.stringify(
        {
            grupoId: id,
            totalPasajeros: new Number(datos.get('totalPasajeros')),
            coordinadorId: datos.get('coordinadorId'),
            paqueteId: datos.get('paqueteId'),
        }               
    );
    console.log(jsonDataConvert)

    fetch(msgrupos+`?id=${id}`, {
        method: 'PUT',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Grupo actualizado")
            location.reload()
            
        })
};

// eliminar coordinador
function deleteGrupo(id){
    fetch(msgrupos+`${id}`, {
      method: 'DELETE',
      headers: myHeaders,
  })
      .then(res => res.json())
      .then(datos => {
          console.log(datos)
          alert("Grupo eliminado")
          location.reload()
      })
  };
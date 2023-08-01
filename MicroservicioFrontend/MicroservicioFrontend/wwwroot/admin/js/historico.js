// paquete 
// var bottompaquete = document.getElementById('bottompaquete');
var mspaquete = `https://localhost:44341/api/paquetes/`
var mstipopaquete = `https://localhost:44341/api/tipopaquetes/`
var msterminal = `https://localhost:44341/api/terminal/`
var mshoteles = `https://localhost:44341/api/hoteles/`
var msexcursion = `https://localhost:44341/api/Excursion/`
var msdestinos = `https://localhost:44341/api/destino/`
var msreservas = `https://localhost:44341/api/reserva/`
var msusuario = `https://localhost:44384/api/user/`
var msempleado = `https://localhost:44384/api/empleado/`
var mspasajero = `https://localhost:44384/api/pasajero/`
var msviajeshistoricos = ``
var msreservashistoricas = ``
var mspaqueteshistoricos = ``

var reservaHistorica = document.querySelector('#contenido')
function pintarReservaHistorica(){
    fetch(msreservashistoricas, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            reservasHistoricas(datos)
        })
}

function pintarReservaHistorica() {
    fetch(msreservashistoricas, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            reservasHistoricas(datos)
        })
}

function consultaUser(URL,id){
  fetch(URL+`${id}`, {
    method: 'GET',
    headers: myHeaders,
})
    .then(res => res.json())
    .then(datos => {
        // console.log(datos.email)
        pintarDatoUser(datos)
    })
    .catch(e => console.log(e));
}
function consultaPaquete(URL,id){
  fetch(URL+`${id}`, {
    method: 'GET',
    headers: myHeaders,
})
    .then(res => res.json())
    .then(paquete => {
        // console.log(datos.email)
        pintarDatoPaquete(paquete)
    })
    .catch(e => console.log(e));
}
function pintarDatoUser(datos){
  console.log(datos)
  document.getElementById('cliente').innerHTML = datos.email
}
function pintarDatoPaquete(paquete){
  console.log(paquete)
  document.getElementById('paquete').innerHTML = paquete.nombre
}

// carga datos en pantalla 
function reservasHistoricas(data) {
    reservaHistorica.innerHTML = ''
    reservaHistorica.innerHTML = `
    <div class="row">
    <h3>Reservas historicas</h3>
      </div>

     
<div class="row justify-content-center align-items-center">
<table  id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">pasajeros</th>
        <th scope="col">pagado</th>
        <th scope="col">pasajero</th>
        <th scope="col">formaPago</th>
        <th scope="col">paquete</th>
        <th scope="col">accion</th>
      </tr>
    </thead>
    <tbody id="tbodyreserva">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyreserva')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.pasajeros}</td>
        <td>${valor.pagado}</td>
        <td>${valor.pasajeroId}</td>
        <td>${valor.formaPagoId}</td>
        <td>${valor.paqueteId}</td>
        <td>
            <button type="button" class="btn btn-outline-warning" data-bs-toggle="modal" data-bs-target="#verpaqueteModal" onclick="verpaquete(${valor.id})">Ver</button>
            <button type="button" class="btn btn-outline-danger" onclick="delitepaquete(${valor.id})">Borrar</button>
        </td>
      </tr>`
    }
}

function paquetesHistoricos(data) {
    paqueteHistorico.innerHTML = ''
    paqueteHistorico.innerHTML = `
    <div class="row">
    <h3>Reservas historicas</h3>
      </div>

     
<div class="row justify-content-center align-items-center">
<table  id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">nombre</th>
        <th scope="col">destino</th>
        <th scope="col">hotel</th>
        <th scope="col">precio</th>
        <th scope="col">paquete</th>
        <th scope="col">accion</th>
      </tr>
    </thead>
    <tbody id="tbodyreserva">
    </tbody>
    </table>
</div>`
    var tabla = document.querySelector('#tbodyreserva')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.pasajeros}</td>
        <td>${valor.pagado}</td>
        <td>${valor.pasajeroId}</td>
        <td>${valor.formaPagoId}</td>
        <td>${valor.paqueteId}</td>
        <td>
            <button type="button" class="btn btn-outline-warning" data-bs-toggle="modal" data-bs-target="#verpaqueteModal" onclick="verpaquete(${valor.id})">Ver</button>
            <button type="button" class="btn btn-outline-danger" onclick="delitepaquete(${valor.id})">Borrar</button>
        </td>
      </tr>`
    }
}

// Ver paquete 
function verpaquetehistorico(id){
    var formulariopaqueteVer = document.getElementById('verpaqueteForm');
    var paqueteRespuestaEdit = document.getElementById('editpaqueteRespuesta');

    fetch(msreservas+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            pintarVerPaquete(data)
        })
        
  };

  function pintarVerPaquete(datos){
    var formulariopaqueteVer = document.getElementById('verpaqueteForm');
    formulariopaqueteVer.innerHTML = ''
    formulariopaqueteVer.innerHTML = `
    <div class="card-body">
      <h5 class="card-title destino">Paquete ${datos.id} </h5>
      <p class="card-text fechaSalida"><strong>Pasajeros: </strong> ${datos.pasajeros}</p>
      <p class="card-text descripcion"><strong>Pago: </strong> ${datos.pagado}</p>
      <p class="card-text fechaSalida"><strong>Cliente: </strong><div id="cliente"> ${consultaUser(msusuario,datos.pasajeroId)}</div></p>
      <p class="card-text fechaVuelta"><strong>Forma de Pago: </strong> ${datos.formaPagoId}</p>
      <p class="card-text fechaVuelta"><strong>Paquete: </strong> <div id="paquete"> ${datos.paqueteId}</div></p>
    </div>

1	Efectivo
2	Tarjeta de crédito
3	Tarjeta de débito
4	Mercado Pago
5	Pago Fácil
6	Bitcoin
 `
}
  

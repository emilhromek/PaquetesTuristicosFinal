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

var reserva = document.querySelector('#contenido')
pintarReserva()
function pintarReserva(){
    fetch(msreservas, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            reservas(datos)
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
function reservas(data) {
    reserva.innerHTML = ''
    reserva.innerHTML = `
    <div class="row">
    <h3>Reservas</h3>
      </div>

      
      <!-- Modal -->
      <div class="modal fade" id="cargarPaqueteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo Paquete</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargarpaqueteForm"> 
                        <div class="row">
                          <div class="col">
                          <input type="text" name="nombre" placeholder="Ingresa Nombre" class="form-control my-3" required />
                        </div>
                        <div class="col">
                          <input type="text" name="descripcion" placeholder="Ingresa descripcion" class="form-control my-3" required />
                        </div>
                        </div>                  
                          <div class="col">
                          <label class="form-label">fecha de salida</label>
                          <input type="datetime-local" name="fechasalida" placeholder="Ingresa fecha de salida" class="form-control my-3" required />
                        </div>
                        <div class="col">
                          <label class="form-label">fecha de vuelta</label>
                          <input type="datetime-local" name="fechavuelta" placeholder="Ingresa fecha de vuelta" class="form-control my-3" required />
                        </div>
                        <div class="row">
                          <div class="col">
                          <input type="number" name="precio" placeholder="Ingresa precio" class="form-control my-3" required />
                        </div>
                          <div class="col">
                          <input type="number" name="descuento" placeholder="Ingresa descuento" class="form-control my-3" required />
                        </div>
                        </div>
                          <div >
                              <label for="inputState" class="form-label">Estado</label>
                              <select id="paqueteEstadoId" class="form-select estado" required>
                                  <option selected value="">Seleccione Estado...</option>
                                  <option type="text" value="1" id="1">Activo 1</option>
                                  <option type="text" value="2" id="2">Cerrado 2</option>
                                  <option type="text" value="3" id="3">Bloqueado 3</option>
                                  <option type="text" value="3" id="4">Cancelado 4</option>
                              </select>
                          </div>
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createpaquete()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>
<div class="row justify-content-center align-items-center">
<table  id="regTable" class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">pasajeros</th>
        <th scope="col">pagado</th>
        <th scope="col">pasajeroId</th>
        <th scope="col">formaPagoId</th>
        <th scope="col">paqueteId</th>
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
//crear paquete 
function createpaquete(){  
    var formulariopaqueteCargar = document.getElementById('cargarpaqueteForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    var datos = new FormData(formulariopaqueteCargar);
    console.log(datos)
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            descripcion: datos.get('descripcion'),
            fechaSalida: datos.get('fechasalida'),
            fechaVuelta: datos.get('fechavuelta'),
            precio: new Number(datos.get('precio')),
            descuento: new Number(datos.get('descuento')),
            paqueteEstadoId: new Number(document.getElementById("paqueteEstadoId").value),
            prioridad: 1,
            listaDestinoHotelNochesPension:[[4,2,1,1]]
        }               
    );
    console.log(jsonDataConvert)

    fetch(mspaquete, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("paquete creado")
            // location.reload()
        })
}
// Ver paquete 
function verpaquete(id){
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
  

// eliminar paquete 
function delitepaquete(id){
  fetch(mspaquete+`${id}`, {
    method: 'DELETE',
    headers: myHeaders,
})
    .then(res => res.json())
    .then(datos => {
        console.log(datos)
        alert("paquete eliminado")
        location.reload()
    })
};
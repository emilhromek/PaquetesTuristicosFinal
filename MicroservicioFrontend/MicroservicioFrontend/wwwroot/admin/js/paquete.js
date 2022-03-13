// paquete 
// var bottompaquete = document.getElementById('bottompaquete');
var mspaquete = `https://localhost:44341/api/Paquetes/`
var mstipopaquete = `https://localhost:44341/api/tipopaquetes/`
var msterminal = `https://localhost:44341/api/terminal/`
var mshoteles = `https://localhost:44341/api/Hoteles/`
var msexcursion = `https://localhost:44341/api/Excursion/`
var msdestinos = `https://localhost:44341/api/Destino/`

var paquete = document.querySelector('#contenido')
pintarPaquetes()
function pintarPaquetes(){
    fetch(mspaquete, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            paquetes(datos)
        })
}

function pintarHoteles(){
    fetch(mshoteles, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            hoteles(datos)
        })
}

function pintarExcursiones(){
    fetch(msexcursion, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            excursiones(datos)
        })
}

function pintarDestinos(){
    fetch(msdestinos, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            destinos(datos)
        })
}
// carga datos en pantalla 
function paquetes(data) {

    function cargarDestinosParaPaquetes(){
        fetch(msdestinos, {
            method: 'GET',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                var menuSelect = document.querySelector('#selectPaqueteDestino')
                for (let valor of datos)
                {
                    menuSelect.innerHTML +=`<option value="${valor.id}">${valor.lugar}</option>`
                }

            })
            
    }          

    cargarDestinosParaPaquetes();    

    paquete.innerHTML = ''
    paquete.innerHTML = `
    <div class="row">
    <h3>Paquetes</h3>
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
                            <p>Ingrese nombre de paquete:</p>
                            <input type="text" name="nombre" placeholder="Ingrese nombre" class="form-control my-3" required />
                            </div>
                            <div class="col">
                            <p>Ingrese descripción:</p>
                          <input type="text" name="descripcion" placeholder="Ingrese descripción" class="form-control my-3" required />
                            </div>
                        </div>                  
                          <div class="col">
                          <label class="form-label">Fecha de arribo:</label>
                          <input type="datetime-local" name="fechaArribo" placeholder="Ingresa fecha de salida" class="form-control my-3" required />
                            </div>
                            <div class="col">
                          <label class="form-label">Fecha de partida:</label>
                          <input type="datetime-local" name="fechaPartida" placeholder="Ingresa fecha de partida" class="form-control my-3" required />
                            </div>
                        
                        <div class="row">
                          <div class="col">
                          <p>Ingrese precio:</p>
                          <input type="number" name="precio" placeholder="Ingrese precio" class="form-control my-3" required />
                          </div>
                          <div class="col">
                          <p>Ingrese descuento:</p>
                          <input type="number" name="descuento" placeholder="Ingrese descuento" min="0" max="99" class="form-control my-3" required />
                           </div>                        
                        </div>

                        <div class="row">
                          <div class="col">
                          <p>Elija destino:</p>
                          <select class="form-select" name="iddestino" id="selectPaqueteDestino" aria-label="Default select example">
                            <option selected>Elegir destino</option>
                          </select>
                          </div>
                          <div class="col">
                          <p>Elija hotel:</p>
                          <select class="form-select" name="idhotel" id="selectPaqueteHotel" aria-label="Default select example">
                            <option selected>Elegir hotel</option>
                          </select>                          
                           </div>                        
                        </div>

                        <div class="row">
                          <div class="col">
                          <p>Ingrese prioridad:</p>
                          <input type="number" name="prioridad" placeholder="Ingresa prioridad" class="form-control my-3" required />
                          </div>
                          <div class="col">
                          <p>Noches:</p>
                          <input type="number" name="totalnoches" placeholder="Ingresa noches" class="form-control my-3" readonly/>
                           </div>                        
                        </div>

                        <p>Elija excursiones:</p> 

                        <div class="col" id="elegirExcursionesDestino">
                                <div class="form-check">
                                    <input class="form-check-input" name="idexcursion" type="checkbox" value="" id="flexCheckDefault">
                                    <label class="form-check-label" for="flexCheckDefault">
                                    Excursion 1
                                    </label>
                                </div>
                                <div class="form-check">
                                    <input class="form-check-input" name="idexcursion" type="checkbox" value="" id="flexCheckDefault" checked>
                                    <label class="form-check-label" for="flexCheckDefault">
                                    Excursion 2
                                    </label>
                                </div>      
                        </div>

                        <br>

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
        <th scope="col">Nombre</th>
        <th scope="col">Salida</th>
        <th scope="col">Arribo</th>
        <th scope="col">Partida</th>
        <th scope="col">Llegada</th>
        <th scope="col">Total de noches</th>
        <th scope="col">Precio</th>
        <th scope="col">Descuento</th>
        <th scope="col">Prioridad</th>
        <th scope="col">Destino</th>
        <th scope="col">Hotel - Sucursal</th>
        <th scope="col">Acciones</th>
      </tr>
    </thead>
    <tbody id="tbodypaquete">
    </tbody>
    </table>
</div>`

// al elegir un destino, acomodar el formulario a los hoteles y excursiones correspondientes

$('#selectPaqueteDestino').on('change', function (){

    document.getElementById("selectPaqueteHotel").innerHTML = ""; // alerta!!! tuve que usar esto porque con jquery no funcionaba

    var selectedItem = $('#selectPaqueteDestino').val();

    fetch(mshoteles+`?idDestino=` + selectedItem, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {

            var menuSelect = document.querySelector('#selectPaqueteHotel')
            
            for (let valor of datos)
            {
                menuSelect.innerHTML +=`<option value="${valor.id}">${valor.marca} - ${valor.sucursal}</option>`
            }

        });

    document.getElementById("elegirExcursionesDestino").innerHTML = ""; // alerta!!! tuve que usar esto porque con jquery no funcionaba

    var selectedItem2 = $('#selectPaqueteDestino').val();
    
    fetch(msexcursion+`?idDestino=` + selectedItem2, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {

            var checkboxesExcursiones = document.querySelector('#elegirExcursionesDestino')
            
            for (let valor of datos)
            {
                checkboxesExcursiones.innerHTML +=`<div class="form-check">
                <input class="form-check-input" name="idexcursion" type="checkbox" value="${valor.id}" id="flexCheckDefault">
                <label class="form-check-label" for="flexCheckDefault">
                ${valor.titulo}
                </label>
                </div>`
            }

        });

    console.log(this.value,
                this.options[this.selectedIndex].value,
                $(this).find("option:selected").val(),);


  });



var tabla = document.querySelector('#tbodypaquete')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.nombre}</td>
        <td>${valor.fechaSalida}</td>
        <td>${valor.fechaArribo}</td>
        <td>${valor.fechaPartida}</td>
        <td>${valor.fechaLlegada}</td>
        <td>${valor.totalNoches}</td>
        <td>${valor.precio}</td>
        <td>${valor.descuento}</td>
        <td>${valor.prioridad}</td>
        <td># ${valor.destino.id} - ${valor.destino.lugar}</td>
        <td># ${valor.hotel.id} - ${valor.hotel.marca} - ${valor.hotel.sucursal}</td>
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
    
    var arrayExcursiones = []
    var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

    for (var i = 0; i < checkboxes.length; i++) {
    arrayExcursiones.push(new Number(checkboxes[i].value))
    }

    var datos = new FormData(formulariopaqueteCargar);
    console.log(datos)

    console.log(arrayExcursiones)

    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            descripcion: datos.get('descripcion'),
            fechaArribo: datos.get('fechaArribo'),
            fechaPartida: datos.get('fechaPartida'),
            precio: new Number(datos.get('precio')),
            descuento: new Number(datos.get('descuento')),
            prioridad: new Number(datos.get('precio')),            
            hotelId: new Number(datos.get('idhotel')),
            destinoId: new Number(datos.get('iddestino')),
            excursiones: arrayExcursiones,
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
            alert(JSON.stringify(datos, null, 4))
            // location.reload()
        })
}
// Ver paquete 
function verpaquete(id){
    var formulariopaqueteVer = document.getElementById('verpaqueteForm');
    var paqueteRespuestaEdit = document.getElementById('editpaqueteRespuesta');

    fetch(mspaquete+`${id}`, {
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
      <h5 class="card-title">Paquete # ${datos.id} - Nombre: ${datos.nombre}</h5>
      <p class="card-text"><strong>Descripción: </strong> ${datos.descripcion}</p>
      <p class="card-text"><strong>Fecha de salida (horario aproximado): </strong> ${datos.fechaSalida}</p>
      <p class="card-text"><strong>Fecha de arribo a destino (horario aproximado): </strong> ${datos.fechaArribo}</p>
      <p class="card-text"><strong>Fecha de partida desde destino (horario aproximado): </strong> ${datos.fechaPartida}</p>
      <p class="card-text"><strong>Fecha de llegada (horario aproximado: </strong> ${datos.fechaLlegada}</p>
        <p class="card-text"><strong>Total de noches: </strong> ${datos.totalNoches}</p>
        <p class="card-text"><strong>Precio base sin excursiones: </strong> ${datos.precio}</p>
        <p class="card-text"><strong>Descuento: </strong> ${datos.descuento} %</p>
        <p class="card-text"><strong>Prioridad de paquete: </strong> ${datos.prioridad}</p>
        <p class="card-text"><strong>Destino: </strong> ${datos.destino.lugar}</p>                
        <p class="card-text"><strong>Hotel: </strong> ${datos.hotel.marca} - ${datos.hotel.sucursal} </p>
        <p class="card-text"><strong>Identificador único de paquete: </strong> ${datos.identificadorUnicoDePaquete}</p>
    </div>
 `
 for (let valor of datos.listaDestinosDetalles) {
    console.log(valor)
    console.log(valor.destino.lugar)
    console.log(valor.hotel.marca)
    formulariopaqueteVer.innerHTML += `
    <h3>Noches: ${valor.noches}</h3>
    <div class="card-body">
      <h5 class="card-title destino">Destino ${valor.destino.id} ${valor.destino.lugar}</h5>
    <p class="card-text hotel">${valor.destino.descripcion}</p>
    <p class="card-text hotel">${valor.destino.historia}</p>
      
      </div>
      <div class="card-body">
      <h5 class="card-title destino">Hotel ${valor.hotel.id} ${valor.hotel.marca}</h5>
      <p class="card-text hotel">Pension ${valor.hotelPension.descripcion}</p>
      </div>
    `
}
  }

// eliminar paquete 
function delitepaquete(id){
  fetch(mspaquete+`?id=${id}`, {
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

function tipopaquete(id){
    fetch( mstipopaquete+`${id}?idTipopaquete=${id}`, {
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

function excursiones(data) {

    function cargarDestinosParaExcursiones(){
        fetch(msdestinos, {
            method: 'GET',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                var menuSelect = document.querySelector('#selectDestinoExcursion')
                for (let valor of datos)
                {
                    menuSelect.innerHTML +=`<option value="${valor.id}">${valor.lugar}</option>`
                }

            })
            
    }        

    cargarDestinosParaExcursiones(); 

    paquete.innerHTML = ''
    paquete.innerHTML = `
    <div class="row">
    <h3>Excursiones</h3>
      </div>
      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarExcursionModal">
      Nueva excursión
      </button>  
      <!-- Modal crear excursion -->
      <div class="modal fade" id="cargarExcursionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nueva excursión</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargarExcursionForm">
                      <p>Ingrese título:</p>                  
                          <input type="text" name="titulo" placeholder="Ingrese título" class="form-control my-3" required />
                          <p>Ingrese descripción:</p>
                          <textarea class="form-control" name="descripcion" placeholder="Ingrese descripión" rows="3"></textarea>
                          <br>
                          <div class="row">
                          <div class="col">
                          <p>Ingrese precio:</p>
                          <input type="number" name="precio" placeholder="Ingrese precio" class="form-control my-3" required />
                          </div>
                          <div class="col">
                          <p>Ingrese duración (horas):</p>
                          <input type="number" name="duracion" placeholder="Ingrese duracion" class="form-control my-3" required />
                          </div>
                          <div class="row">
                          <div class="col">
                          <p>Elija destino:</p>
                          <select class="form-select" name="destinoId" id="selectDestinoExcursion" aria-label="Default select example">
                            <option selected>Elija destino</option>
                          </select>
                          </div>
                          <div class="col">
                          </div>
                          </div>
                          </div>
                         
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createExcursion()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>

      <!-- Modal actualizar excursion -->
      <div class="modal fade" id="actualizarExcursionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar excursión</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="actualizarExcursionForm">
                      <p>Ingrese título:</p>                  
                          <input type="text" name="titulo" placeholder="Ingrese título" class="form-control my-3 titulo" required />
                          <p>Ingrese descripción:</p>
                          <textarea class="form-control descripcion" name="descripcion" placeholder="Ingrese descripción" rows="3"></textarea>
                          <br>
                          <div class="row">
                          <div class="col">
                          <p>Ingrese precio:</p>
                          <input type="number" name="precio" placeholder="Ingrese precio" class="form-control my-3 precio" required />
                          </div>
                          <div class="col">
                          <p>Ingrese duración (horas):</p>
                          <input type="number" name="duracion" placeholder="Ingrese duracion" class="form-control my-3 duracion" required />
                          </div>
                          <div class="row">
                          <div class="col">
                          <p>Elija destino:</p>
                          <select class="form-select destino" name="destinoId" id="selectDestinoExcursion" aria-label="Default select example">
                            <option selected>Elija destino</option>
                          </select>
                          </div>
                          <div class="col">
                          </div>
                          </div>
                          </div>
                         
                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarExcursion">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div> 

      <!-- Modal excursion hotel -->
      <div class="modal fade" id="eliminarExcursionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar excursion</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar excursion?</p>                    

                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="eliminarExcursion">Eliminar</button>
                          </div>
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
        <th scope="col">Titulo</th>
        <th scope="col">Descripcion</th>
        <th scope="col">Precio</th>
        <th scope="col">Duración (horas)</th>
        <th scope="col">Destino</th>
        <th scope="col">Acciones</th>
      </tr>
    </thead>
    <tbody id="tbodypaquete">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodypaquete')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.titulo}</td>
        <td>${valor.descripcion}</td>
        <td>${valor.precio}</td>
        <td>${valor.duracion}</td>
        <td># ${valor.destino.id} - ${valor.destino.lugar}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarExcursionModal" onclick="actualizarExcursion(${valor.id})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarExcursionModal" onclick="deleteexcursion(${valor.id})">Borrar</button>
        </td>
      </tr>`

    }
}

function hoteles(data) {

    paquete.innerHTML = ''
    paquete.innerHTML = `
    <div class="row">
    <h3>Hoteles</h3>
        </div>
        <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarHotelModal">
        Nuevo hotel
        </button>  
        <!-- Modal -->
        <div class="modal fade" id="cargarHotelModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Nuevo Hotel</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form id="cargarHotelForm">
                            <div class="row">
                                <div class="col">
                                    <p>Marca:</p>
                                    <input type="text" name="marca" placeholder="Ingrese marca" class="form-control my-3" required />
                                </div>
                                <div class="col">
                                    <p>Sucursal:<p>               
                                    <input type="text" name="sucursal" placeholder="Ingrese sucursal" class="form-control my-3" required />
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="col">
                                    <p>Estrellas:</p>
                                    <select class="form-select" name="estrellas" aria-label="Default select example">
                                        <option selected="true" disabled="disabled">Elija estrellas</option>  
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                    </select>
                                </div>
                                <div class="col">
                                    <p>Destino:<p>
                                    <select class="form-select" name="destinoId" id="selectDestinoHotel" aria-label="Default select example">
                                        <option selected="true" disabled="disabled">Elija destino</option>
                                    </select>                                    
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p>Dirección:</p>
                                    <input type="text" name="direccion" placeholder="Ingresa direccion" class="form-control my-3" required />
                                </div>
                                <div class="col">
                                    <p>Capacidad:<p>
                                    <input type="number" name="capacidad" placeholder="Ingrese capacidad" class="form-control my-3" required />                                  
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p>Costo por persona ($):</p>
                                    <input type="number" name="costo" placeholder="Ingrese costo" class="form-control my-3" required />
                                </div>
                                <div class="col">                             
                                </div>
                            </div> 
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                <button type="button" class="btn btn-primary" onclick="createHotel()">Crear</button>
                            </div>
                        </form>
                    </div>
      
                </div>
            </div>
        </div>

      <!-- Modal -->
      <div class="modal fade" id="actualizarHotelModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar hotel</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body" id="actualizarHotelModal-body">

                  <p>Está por actualizar el hotel</p>        
                  <form id="actualizarHotelForm">                   
                  <div class="row">
                                <div class="col">
                                    <p>Marca:</p>
                                    <input type="text" name="marca" placeholder="Ingrese marca" class="form-control my-3 marca" required />
                                </div>
                                <div class="col">
                                    <p>Sucursal:<p>               
                                    <input type="text" name="sucursal" placeholder="Ingrese sucursal" class="form-control my-3 sucursal" required />
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="col">
                                    <p>Estrellas:</p>
                                    <select class="form-select estrellas" name="estrellas" aria-label="Default select example">
                                        <option selected="true" disabled="disabled">Elija estrellas</option>  
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                    </select>
                                </div>
                                <div class="col">
                                    <p>Destino:<p>
                                    <select class="form-select destino" name="destino" id="selectDestinoHotelActualizar" aria-label="Default select example">
                                        
                                    </select>                                    
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p>Dirección:</p>
                                    <input type="text" name="direccion" placeholder="Ingresa direccion" class="form-control my-3 direccion" required />
                                </div>
                                <div class="col">
                                    <p>Capacidad:<p>
                                    <input type="number" name="capacidad" placeholder="Ingrese capacidad" class="form-control my-3 capacidad" required />                                  
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p>Costo por persona ($):</p>
                                    <input type="number" name="costo" placeholder="Ingrese costo" class="form-control my-3 costo" required />
                                </div>
                                <div class="col">                             
                                </div>
                            </div> 
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                <button type="button" id="actualizarHotel" class="btn btn-primary">Actualizar</button>
                            </div>
                  </form>
                  <div class="mt-3" id="cargarViajeRespuesta">
          
                  </div>
                      
                  </div>
      
              </div>
          </div>
      </div>   

      <!-- Modal eliminar hotel -->
      <div class="modal fade" id="eliminarHotelModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Eliminar hotel</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">

                      <p>¿Desea eliminar hotel?</p>                

                          
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="eliminarHotel">Eliminar</button>
                          </div>
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
        <th scope="col">Marca</th>
        <th scope="col">Sucursal</th>
        <th scope="col">Estrellas</th>
        <th scope="col">Dirección</th>
        <th scope="col">Destino</th>
        <th scope="col">Capacidad</th>
        <th scope="col">Costo</th>
        <th scope="col">Acciones</th>
      </tr>
    </thead>
    <tbody id="tbodypaquete">
    </tbody>
    </table>
</div>`

function cargarDestinosParaHoteles(){
    fetch(msdestinos, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var menuSelect = document.querySelector('#selectDestinoHotel')
            for (let valor of datos)
            {
                menuSelect.innerHTML +=`<option value="${valor.id}">${valor.lugar}</option>`
            }

        })
        
}

cargarDestinosParaHoteles();

var tabla = document.querySelector('#tbodypaquete')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.marca}</td>
        <td>${valor.sucursal}</td>
        <td>${valor.estrellas}</td>
        <td>${valor.direccion}</td>
        <td># ${valor.destino.id} - ${valor.destino.lugar}</td>
        <td>${valor.capacidad}</td>
        <td>${valor.costo}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarHotelModal" onclick="actualizarHotel(${valor.id})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarHotelModal" onclick="deletehotel(${valor.id})">Borrar</button>
        </td>
      </tr>`
    }
}

function destinos(data) {
    paquete.innerHTML = ''
    paquete.innerHTML = `
    <div class="row">
    <h3>Destinos</h3>
      </div>

      <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#cargarDestinoModal">
      Nuevo Destinos
      </button> 

      <!-- Modal crear destino-->
      <div class="modal fade" id="cargarDestinoModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Nuevo Destino</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="cargardestinoForm">
                      <div class="row">
                      <div class="col">
                      <p>Ingrese lugar:</p>
                      <input type="text" name="lugar" placeholder="Ingrese lugar" class="form-control my-3" required />
                      </div>
                      </div>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese descripción:</p>
                      <textarea class="form-control" name="descripcion" placeholder="Ingrese descripción" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese atractivos:</p>
                      <textarea class="form-control" name="atractivo" placeholder="Ingrese atractivos" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese historia:</p>
                      <textarea class="form-control" name="historia" placeholder="Ingrese historia" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese horas de viaje aproximadas:</p>
                      <input type="number" name="horasdeviaje" placeholder="Ingrese horas de viaje" class="form-control my-3" min="0" max="50" required />
                      </div>
                      </div>
                      <p>Ingrese imagen:</p>

                          <input type="text" name="imagen" placeholder="Ingresa imagen" class="form-control my-3" required />
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" onclick="createdestino()">Crear</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal actualizar destino -->
      <div class="modal fade" id="actualizarDestinoModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Actualizar destino</h5>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <form id="actualizarDestinoForm">
                      <div class="row">
                      <div class="col">
                      <p>Ingrese lugar:</p>
                      <input type="text" name="lugar" placeholder="Ingrese lugar" class="form-control my-3 lugar" required />
                      </div>
                      </div>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese descripción:</p>
                      <textarea class="form-control descripcion" name="descripcion" placeholder="Ingrese descripción" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese atractivos:</p>
                      <textarea class="form-control atractivo" name="atractivo" placeholder="Ingrese atractivos" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese historia:</p>
                      <textarea class="form-control historia" name="historia" placeholder="Ingrese historia" rows="3"></textarea>
                      </div>
                      </div>
                      <br>
                      <div class="row">
                      <div class="col">
                      <p>Ingrese horas de viaje aproximadas:</p>
                      <input type="number" name="horasdeviaje" placeholder="Ingrese horas de viaje" class="form-control my-3 horasdeviaje" min="0" max="50" required />
                      </div>
                      </div>
                      <p>Ingrese imagen:</p>

                          <input type="text" name="imagen" placeholder="Ingresa imagen" class="form-control my-3 imagen" required />
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                              <button type="button" class="btn btn-primary" id="actualizarDestino">Actualizar</button>
                          </div>
                      </form>
                      <div class="mt-3" id="cargarViajeRespuesta">
      
                      </div>
                  </div>
      
              </div>
          </div>
      </div>    

      <!-- Modal eliminar destino -->
      <div class="modal fade" id="eliminarDestinoModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                              <button type="button" class="btn btn-danger" id="eliminarDestino">Eliminar</button>
                          </div>
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
        <th scope="col">lugar</th>
        <th scope="col">descripcion</th>
        <th scope="col">atractivo</th>
        <th scope="col">historia</th>
        <th scope="col">imagen</th>
        <th scope="col">accion</th>
      </tr>
    </thead>
    <tbody id="tbodypaquete">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodypaquete')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.id}</th>
        <td>${valor.lugar}</td>
        <td>${valor.descripcion}</td>
        <td>${valor.atractivo}</td>
        <td>${valor.historia}</td>
        <td><img class="img-responsive img-thumbnail" src="${valor.imagen}" style="height: 100px; width: 100px" alt=""></td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#actualizarDestinoModal" onclick="actualizarDestino(${valor.id})">Editar</button>
            <button type="button" class="btn btn-outline-danger" data-bs-toggle="modal" data-bs-target="#eliminarDestinoModal" onclick="delitedestino(${valor.id})">Borrar</button>
        </td>
      </tr>`
    }
}

//crear destino 
function createdestino(){  
    var formularioDestinoCargar = document.getElementById('cargardestinoForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioDestinoCargar);
    let jsonDataConvert = JSON.stringify(
        {
            lugar: datos.get('lugar'),
            descripcion: datos.get('descripcion'),
            atractivo: datos.get('atractivo'),
            historia: datos.get('historia'),
            horasDeViaje: datos.get('horasdeviaje'),
            imagen: datos.get('imagen'),

        }               
    );
    console.log(jsonDataConvert)

    fetch(msdestinos, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Destino creado")
            location.reload()
            
        })
}

//actualizar destino

function actualizarDestino(id){
    var formularioDestinoEdit = document.getElementById('actualizarDestinoForm');
    fetch (msdestinos+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)            
            formularioDestinoEdit.querySelector('.lugar').value = data.lugar,
            formularioDestinoEdit.querySelector('.descripcion').value = data.descripcion,
            formularioDestinoEdit.querySelector('.atractivo').value = data.atractivo,
            formularioDestinoEdit.querySelector('.historia').value = data.historia,
            formularioDestinoEdit.querySelector('.horasdeviaje').value = data.horasDeViaje,
            formularioDestinoEdit.querySelector('.imagen').value = data.imagen
        })

        var botonActualizar = document.getElementById('actualizarDestino')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarDestino(id)
        });

        function ejecutarActualizarDestino(id){  
            var formularioDestinoActualizar = document.getElementById('actualizarDestinoForm');
            let datos = new FormData(formularioDestinoActualizar);
            console.log(datos);            
            let jsonDataConvert = JSON.stringify(
                {
                    id: id,
                    lugar: datos.get('lugar'),
                    descripcion: datos.get('descripcion'),
                    atractivo: datos.get('atractivo'),
                    historia: datos.get('historia'),
                    horasDeViaje: datos.get('horasdeviaje'),
                    imagen: datos.get('imagen')
                }               
            );
            console.log(jsonDataConvert)
        
            fetch(msdestinos+`?id=${id}`, {
                method: 'PUT',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Destino actualizado")
                    location.reload()
                    
                })
        }
};

// eliminar destino 
function delitedestino(id){
    var botonEliminar = document.getElementById('eliminarDestino')
    botonEliminar.addEventListener('click', function(e){
        e.preventDefault();
        ejecutarEliminarDestino(id)
    });
    function ejecutarEliminarDestino(id){
        fetch(msdestinos+`?Id=${id}`, {
            method: 'DELETE',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                alert("Destino eliminado")
                location.reload()
            })
    }
};


function ListarHoteles(){
    fetch('https://localhost:44341/api/hoteles/', {
        method: 'GET',
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
        })
}
var bottomNuevo = document.getElementById('nuevoPaquete')
bottomNuevo.addEventListener('click', function(e){
    e.preventDefault();
    ListarHoteles()
});


//crear Hotel 
function createHotel(){  
    var formularioHotelCargar = document.getElementById('cargarHotelForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioHotelCargar);
    
    let jsonDataConvert = JSON.stringify(
        {
            marca: datos.get('marca'),
            sucursal: datos.get('sucursal'),
            estrellas: new Number(datos.get('estrellas')),
            direccion: datos.get('direccion'),
            capacidad: new Number(datos.get('capacidad')),
            costo: new Number(datos.get('costo')),
            destinoId: new Number(datos.get('destinoId')),
        }               
    );
    console.log(jsonDataConvert)

    fetch(mshoteles, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Hotel creado")
            location.reload()
            
        })
};

//actualizar Hotel

function actualizarHotel(id){
    var formularioHotelEdit = document.getElementById('actualizarHotelForm');
    fetch (mshoteles+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)            
            formularioHotelEdit.querySelector('.marca').value = data.marca,
            formularioHotelEdit.querySelector('.sucursal').value = data.sucursal,
            formularioHotelEdit.querySelector('.estrellas').value = data.estrellas,
            formularioHotelEdit.querySelector('.direccion').value = data.direccion,
            formularioHotelEdit.querySelector('.capacidad').value = data.capacidad,
            formularioHotelEdit.querySelector('.costo').value = data.costo,            
            formularioHotelEdit.querySelector('.destino').innerHTML = `<option selected value="${data.destinoId}">${data.destino.lugar}</option>`
        })

        var botonActualizar = document.getElementById('actualizarHotel')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarHotel(id)
        });

        function ejecutarActualizarHotel(id){  
            var formularioHotelActualizar = document.getElementById('actualizarHotelForm');
            var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
            let datos = new FormData(formularioHotelActualizar);
            console.log(datos);            
            let jsonDataConvert = JSON.stringify(
                {
                    id: id,
                    marca: datos.get('marca'),
                    sucursal: datos.get('sucursal'),
                    estrellas: new Number(datos.get('estrellas')),
                    direccion: datos.get('direccion'),
                    capacidad: new Number(datos.get('capacidad')),
                    costo: new Number(datos.get('costo')),
                    destinoId: new Number(datos.get('destino')),
                }               
            );
            console.log(jsonDataConvert)
        
            fetch(mshoteles+`?id=${id}`, {
                method: 'PUT',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Hotel actualizado")
                    location.reload()
                    
                })
        }
};



// eliminar hotel
function deletehotel(id){

    var botonEliminar = document.getElementById('eliminarHotel')
    botonEliminar.addEventListener('click', function(e){
        e.preventDefault();
        ejecutarEliminarHotel(id)
    });

    function ejecutarEliminarHotel(id)
    {    
    fetch(mshoteles+`?Id=${id}`, {
      method: 'DELETE',
      headers: myHeaders,
  })
      .then(res => res.json())
      .then(datos => {
          console.log(datos)
          alert("Hotel eliminado")
          location.reload()
      })
    };
  };

  //crear excursion
function createExcursion(){  
    var formularioExcursionCargar = document.getElementById('cargarExcursionForm');
    var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
    let datos = new FormData(formularioExcursionCargar);
    
    let jsonDataConvert = JSON.stringify(
        {
            titulo: datos.get('titulo'),
            descripcion: datos.get('descripcion'),
            precio: new Number(datos.get('precio')),
            destinoId: new Number(datos.get('destinoId')),
            duracion: new Number(datos.get('duracion')),
        }               
    );
    console.log(jsonDataConvert)

    fetch(msexcursion, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Excursion creada")
            location.reload()
            
        })
};

//actualizar excursion 
function actualizarExcursion(id){
    var formularioExcursionEdit = document.getElementById('actualizarExcursionForm');
    fetch (msexcursion+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)            
            formularioExcursionEdit.querySelector('.titulo').value = data.titulo,
            formularioExcursionEdit.querySelector('.descripcion').value = data.descripcion,
            formularioExcursionEdit.querySelector('.precio').value = data.precio,
            formularioExcursionEdit.querySelector('.duracion').value = data.duracion,   
            formularioExcursionEdit.querySelector('.destino').innerHTML = `<option selected value="${data.destinoId}">${data.destino.lugar}</option>`
        })

        var botonActualizar = document.getElementById('actualizarExcursion')
        botonActualizar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarActualizarExcursion(id)
        });

        function ejecutarActualizarExcursion(id){  
            var formularioExcursionActualizar = document.getElementById('actualizarExcursionForm');
            var paqueteRespuestaCargar = document.getElementById('cargarpaqueteRespuesta');
            let datos = new FormData(formularioExcursionActualizar);
            console.log(datos);            
            let jsonDataConvert = JSON.stringify(
                {
                    id: id,
                    titulo: datos.get('titulo'),
                    descripcion: datos.get('descripcion'),
                    precio: new Number(datos.get('precio')),
                    destinoId: new Number(datos.get('destinoId')),
                    duracion: new Number(datos.get('duracion')),
                }               
            );
            console.log(jsonDataConvert)
        
            fetch(msexcursion+`?id=${id}`, {
                method: 'PUT',
                body: jsonDataConvert,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("Excursion actualizado")
                    location.reload()
                    
                })
        }
};

// eliminar excursion
function deleteexcursion(id){
    var botonEliminar = document.getElementById('eliminarExcursion')
    botonEliminar.addEventListener('click', function(e){
        e.preventDefault();
        ejecutarEliminarExcursion(id)
    });
    function ejecutarEliminarExcursion(id){
        fetch(msexcursion+`?Id=${id}`, {
            method: 'DELETE',
            headers: myHeaders,
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                alert("Excursion eliminada")
                location.reload()
            })

    }    
  };
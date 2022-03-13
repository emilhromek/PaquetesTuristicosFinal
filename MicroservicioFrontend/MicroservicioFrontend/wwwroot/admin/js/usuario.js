// Usuario 



// var bottomUsuario = document.getElementById('bottomUsuario');
var msusuario = `https://localhost:44384/api/user/`
var msempleado = `https://localhost:44384/api/empleado/`
var mspasajero = `https://localhost:44384/api/pasajero/`

var usuario = document.querySelector('#contenido')
var empleado = document.querySelector('#contenidoEmpleado')
var pasajero = document.querySelector('#contenidoPasajero')


function limpiar(tabla){
    tabla.innerHTML = ''
}
pintar()
function pintar(){
    fetch(msusuario, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(usuario)
            limpiar(empleado)
            limpiar(pasajero)
            usuarios(datos,usuario)
        })
}
// carga datos en pantalla 
function usuarios(data,tabla) {
    
    tabla.innerHTML = ''
    tabla.innerHTML = `
    <div class="row">
    <h3>Usuarios</h3>
      </div>
<div class="row justify-content-center align-items-center">
<table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Nombre</th>
        <th scope="col">Apellido</th>
        <th scope="col">Email</th>
        <th scope="col">Roll</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyUsuario">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyUsuario')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.userId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.apellido}</td>
        <td>${valor.email}</td>
        <td>${valor.roll.nombre}</td>
        <td>
            <button type="button" class="btn btn-outline-warning"  data-bs-toggle="modal" data-bs-target="#AgregarDatosModal" onclick="agregarEmpleadoPasajero(${valor.userId})">Agregar</button>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#editarUsuarioModal" onclick="editUsuario(${valor.userId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deliteUsuario(${valor.userId})">Borrar</button>
        </td>
      </tr>`
    }
}
//crear Usuario 
function createUsuario(){  
    var formularioUsuarioCargar = document.getElementById('cargarUsuarioForm');
    var usuarioRespuestaCargar = document.getElementById('cargarUsuarioRespuesta');
    var datos = new FormData(formularioUsuarioCargar);
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            apellido: datos.get('apellido'),
            email: datos.get('email'),
            password: datos.get('password'),
            roll: new Number(document.getElementById("roll").value)
        }               
    );
    console.log(jsonDataConvert)

    fetch(msusuario, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("usuario creado")
            location.reload()
        })
}

//crear Psajero 
function createPasajero(){  
    var formularioPasajeroCargar = document.getElementById('cargarPasajeroForm');
    var usuarioRespuestaCargar = document.getElementById('cargarUsuarioRespuesta');
    var datos = new FormData(formularioPasajeroCargar);
    let jsonDataConvert = JSON.stringify(
        {
            userId: new Number(datos.get('id')),
            dni: new Number(datos.get('dni')),
            telefono: new Number(datos.get('telefono')),
            fechaNacimiento: datos.get('fechaNacimiento'),
        }               
    );
    console.log(jsonDataConvert)

    fetch(mspasajero, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Pasajero creado")
            location.reload()
        })
}

//crear Empleado 
function createEmpleado(){  
    var formularioEmpleadoCargar = document.getElementById('cargarEmpleadoForm');
    var usuarioRespuestaCargar = document.getElementById('cargarUsuarioRespuesta');
    var datos = new FormData(formularioEmpleadoCargar);
    let jsonDataConvert = JSON.stringify(
        {       
            dni: new Number(datos.get('dni')),
            telefono: new Number(datos.get('telefono')),
            fechaNacimiento: datos.get('fechaNacimiento'),
            legajo: new Number(datos.get('legajo')),
            sueldo: new Number(datos.get('sueldo')),
            userId: new Number(datos.get('id')),
        }               
    );
    console.log(jsonDataConvert)

    fetch(msempleado, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            console.log(datos)
            alert("Empleado creado")
            location.reload()
        })
}
// editar usuario 
function editUsuario(id){
    var formularioUsuarioEdit = document.getElementById('editUsuarioForm');
    var usuarioRespuestaEdit = document.getElementById('editUsuarioRespuesta');

    fetch(msusuario+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            formularioUsuarioEdit.querySelector('.id').value = id
            formularioUsuarioEdit.querySelector('.nombre').value = data.nombre
            formularioUsuarioEdit.querySelector('.apellido').value = data.apellido
            formularioUsuarioEdit.querySelector('.email').value = data.email
            formularioUsuarioEdit.querySelector('.password').value = data.password

        })
        var bottomEdit = document.getElementById('editarUsuario')
        bottomEdit.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarEdit(id)
        });
        function ejecutarEdit(id){
            var formularioUsuarioEdit = document.getElementById('editUsuarioForm');
            var datos = new FormData(formularioUsuarioEdit);
            let jsonDataConvertEdit = JSON.stringify(
                {
                    nombre: datos.get('nombre'),
                    apellido: datos.get('apellido'),
                    email: datos.get('email'),
                    password: datos.get('password'),
                    roll: new Number(document.getElementById("editroll").value)
                }               
            );
            fetch(msusuario+`${id}`, {
                method: 'PUT',
                body: jsonDataConvertEdit,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("usuario editado")
                    location.reload()
                })
            }
  };

// eliminar usuario 
function deliteUsuario(id){
  fetch(msusuario+`${id}`, {
    method: 'DELETE',
    headers: myHeaders,
})
    .then(res => res.json())
    .then(datos => {
        console.log(datos)
        alert("usuario eliminado")
        location.reload()
    })
};
// empleado 
function empleados() {
    fetch(msempleado, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(empleado)
            limpiar(pasajero)
            tablaEmpleado(datos,usuario)
        })
}
function tablaUsuarios(data,tabla) {
    
    tabla.innerHTML = ''
    tabla.innerHTML = `
    <div class="row">
    <h3>Usuarios</h3>
      </div>
<div class="row justify-content-center align-items-center">
<table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Nombre</th>
        <th scope="col">Apellido</th>
        <th scope="col">Email</th>
        <th scope="col">Roll</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyUsuario">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyUsuario')
    for (let valor of data) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.userId}</th>
        <td>${valor.nombre}</td>
        <td>${valor.apellido}</td>
        <td>${valor.email}</td>
        <td>${valor.roll.nombre}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#editarUsuarioModal" onclick="editUsuario(${valor.userId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deliteUsuario(${valor.userId})">Borrar</button>
        </td>
      </tr>`
    }
}
function tablaEmpleado(datos,tabla){

    tabla.innerHTML = ''
    tabla.innerHTML = `
    <div class="row">
    <h3>Empleado</h3>
      </div>
<table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">DNI</th>
        <th scope="col">Telefono</th>
        <th scope="col">Cumpleaños</th>
        <th scope="col">Legajo</th>
        <th scope="col">Sueldo</th>
        <th scope="col">Email</th>
        <th scope="col">Accion</th>
      </tr>
    </thead>
    <tbody id="tbodyEmpleado">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyEmpleado')
    for (let valor of datos) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.empleadoId}</th>
        <td>${valor.dni}</td>
        <td>${valor.telefono}</td>
        <td>${valor.fechaNacimiento}</td>
        <td>${valor.legajo}</td>
        <td>${valor.sueldo}</td>
        <td>${valor.user.email}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#editarUsuarioModal" onclick="editEmpleado(${valor.empleadoId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deliteEmpleado(${valor.empleadoId})">Borrar</button>
        </td>
      </tr>
    `
    }
}
// Pasajero 
function pasajeros() {
    fetch(mspasajero, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(empleado)
            limpiar(pasajero)
            tablaPasajero(datos,usuario)
        })
}
function tablaPasajero(datos,tabla){

    tabla.innerHTML = ''
    tabla.innerHTML = `
    <div class="row">
    <h3>Pasajero</h3>
      </div>
<table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">DNI</th>
        <th scope="col">Telefono</th>
        <th scope="col">Cumpleaños</th>
        <th scope="col">Email</th>
        <th scope="col">accion</th>
      </tr>
    </thead>
    <tbody id="tbodyPasajero">
    </tbody>
    </table>
</div>`
var tabla = document.querySelector('#tbodyPasajero')
    for (let valor of datos) {
        tabla.innerHTML += `
        <tr>
        <th scope="row">${valor.pasajeroId}</th>
        <td>${valor.dni}</td>
        <td>${valor.telefono}</td>
        <td>${valor.fechaNacimiento}</td>
        <td>${valor.user.email}</td>
        <td>
            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#editarUsuarioModal" onclick="editEmpleado(${valor.pasajeroId})">Editar</button>
            <button type="button" class="btn btn-outline-danger" onclick="deliteEmpleado(${valor.pasajeroId})">Borrar</button>
        </td>
      </tr>
    `
    }
}
function agregarEmpleadoPasajero(id){
    var agregarDatos = document.getElementById('agregarDatosForm');
    fetch(msusuario+`${id}`, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            agregarDatos.querySelector('.id').value = id
            agregarDatos.querySelector('.email').value = data.email
        })
        var bottomAgregar = document.getElementById('agregarDatos')
        bottomAgregar.addEventListener('click', function(e){
            e.preventDefault();
            ejecutarAgregar(id)
        });
        function ejecutarAgregar(id){
            var agregarDatos = document.getElementById('agregarDatosForm');
            var datos = new FormData(agregarDatos);
            let jsonDataConvertAgregar = JSON.stringify(
                {
                    dni: new Number(datos.get('dni')),
                    telefono: new Number(datos.get('telefono')),
                    fechaNacimiento: datos.get('fechaNacimiento'),
                    legajo: new Number(datos.get('legajo')),
                    sueldo: new Number(datos.get('sueldo')),
                    userId: new Number(datos.get('id')),
                }               
            );
            fetch(msempleado, {
                method: 'POST',
                body: jsonDataConvertAgregar,
                headers: myHeaders,
                
            })
                .then(res => res.json())
                .then(datos => {
                    console.log(datos)
                    alert("datos agregados")
                    location.reload()
                })
            }
}
var buscador = document.getElementById('buscarByEmail');
buscador.addEventListener('submit', function (e) {
    e.preventDefault()
    var email = document.querySelector("#emailId").value
    if(email === ""){
        limpiar(empleado)
        limpiar(pasajero)
        limpiar(usuario)
        pintar()
    }else{
    fetch(msusuario+`?email=${email}`, {
        method: 'GET',
        headers: myHeaders,    
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(usuario)
            tablaUsuarios(datos,usuario)
        })
        fetch(msempleado+`?email=${email}`, {
        method: 'GET',
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(empleado)
            tablaEmpleado(datos,empleado)
        })
        fetch(mspasajero+`?email=${email}`, {
        method: 'GET',
        headers: myHeaders,
        
    })
        .then(res => res.json())
        .then(datos => {
            limpiar(pasajero)
            tablaPasajero(datos,pasajero)
        })

    }
})

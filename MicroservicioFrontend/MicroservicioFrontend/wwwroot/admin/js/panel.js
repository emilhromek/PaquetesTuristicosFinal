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
var msViaje = `https://localhost:44385/api/viajes/`
var mstipoViaje = `https://localhost:44385/api/tipoviajes/`
var msterminal = `https://localhost:44385/api/terminal/`
var mscoordinadores = `https://localhost:44385/api/coordinadores/`
var msBus = `https://localhost:44385/api/bus/`
var msEmpresa = `https://localhost:44385/api/empresa/`
var msChofer = `https://localhost:44385/api/chofer/`

var usuarios = document.querySelector('#usuarios')
var reservas = document.querySelector('#reservas')
var pasajeros = document.querySelector('#pasajeros')
var paquetes = document.querySelector('#paquetes')
var hoteles = document.querySelector('#hoteles')
var destinos = document.querySelector('#destinos')
var transportes = document.querySelector('#transportes')
var choferes = document.querySelector('#choferes')

function contarUsuarios(){
    fetch(msusuario, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    usuarios.innerHTML=`${suma}`
    })
}

contarUsuarios()

function contarReservas(){
    fetch(msreservas, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    reservas.innerHTML=`${suma}`
    })
}
function contarPasajeros(){
    fetch(mspasajero, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    pasajeros.innerHTML=`${suma}`
    })
}
function contarPaquetes(){
    fetch(mspaquete, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    paquetes.innerHTML=`${suma}`
    })
}
function contarHoteles(){
    fetch(mshoteles, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    hoteles.innerHTML=`${suma}`
    })
}
function contarDestinos(){
    fetch(msdestinos, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    destinos.innerHTML=`${suma}`
    })
}

function contarTransportes(){
    fetch(msBus, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    transportes.innerHTML=`${suma}`
    })
}

function contarChoferes(){
    fetch(msChofer, {
        method: 'GET',
        headers: myHeaders,
    })
        .then(res => res.json())
        .then(datos => {
            var suma = 0    
    for(let su of datos){
      suma = suma + 1
    }
    choferes.innerHTML=`${suma}`
    })
}

contarReservas()
contarPasajeros()
contarPaquetes()
contarHoteles()
contarDestinos()
contarTransportes()
contarChoferes()
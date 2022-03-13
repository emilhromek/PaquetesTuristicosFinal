var pagina = "../"

// var bottomPanel = document.getElementById('bottomPanel');
// var bottomReservas = document.getElementById('bottomReservas');
// var bottomPaquetes = document.getElementById('bottomPaquetes');
// var bottomViajes = document.getElementById('bottomViajes');
// var bottomTransporte = document.getElementById('bottomTransporte');
// var bottomUsuario = document.getElementById('bottomUsuario');


function login(){
    window.location.href = 'admin/login.html'
}
// validacion de login 
function validarLogin(){
    if(localStorage.getItem('token') != null ){
        console.log(localStorage.getItem('token'))
    }else{
        window.location.href = 'login.html'
    }
}
validarLogin()
// cabecera 
myHeaders = new Headers({
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    "Authorization": "Bearer "+localStorage.getItem('token') ,
    credentials: 'include',
  });
// active 
function buttomActive(){
    
}
function salir(){
    localStorage.clear()
    window.location.href = "../login.html";
}

// usuario 
// bottomUsuario.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "usuario/index.html");
// })
// // paquete 
// bottomPaquetes.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "paquete/index.html");
// })
// // viaje 
// bottomViajes.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "viaje/index.html");
// })
// // reserva 
// bottomReservas.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "reserva/index.html");
// })
// // transporte 
// bottomTransporte.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "transporte/index.html");
// })
// // panel 
// bottomPanel.addEventListener('click', function (e) {
//     e.preventDefault()
//     location.assign(pagina + "panel/index.html");
// })

var empleado = document.querySelector('#empleado')
empleado.append(localStorage.getItem('nombre'))
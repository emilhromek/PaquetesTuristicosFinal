const params = new URLSearchParams(window.location.search);
const contenedor = window.document.getElementById("contenedorPaquetes");
const id = params.get("destinoId");
const lugar = params.get("lugar");

let URL= "https://localhost:44341/api/Paquetes";

if(params.has("destinoId")) {
  contenedor
  .innerHTML = `<h5><br>Viendo paquetes del destino <i>${lugar}</i></h5>`;
  // URL += "/porDestino?destinoId=" + id;
  URL += "/?idDestino=" + id;
};


let prom = fetch(URL);
console.log(URL);
console.log(prom);
prom.then(res => {
  if(res.ok) {
    console.log(res);
    const json = res.json();
    return json;
  }
  else {
    console.log("no hay paquetes");
    contenedor.innerHTML = `<h3>No hay Paquetes para el destino <i>${lugar}</i></h3>`;
  };
}).then(json => {
    console.log(json);
    for(let i=0; i < json.length; i++) {
        console.log(json[i].lugar);
    }
    renderizarPaquetes(json);
});

renderInformacionDeUsuario()

function renderizarPaquetes(paquetes){
    let contenedor = document.getElementById("contenedorPaquetes");
    for(let i=0; i < paquetes.length; i++) {
        paquete = paquetes[i];
        contenedor.innerHTML +=
        `<div class="card mt-4 paquete" style="width: 18rem;">
        <img src=${paquete.destino.imagen} class="card-img-top" alt="Img paquete...">
        <div class="card-body">
          <h5 class="card-title">${paquete.nombre}</h5>
          <p class="card-text">${paquete.descripcion}</p> 
        </div>
        <ul class="list-group list-group-flush">
          <li class="list-group-item">Salida el ${paquete.fechaSalida}</li>
          <li class="list-group-item">${paquete.totalNoches} noches</li>
          <li class="list-group-item">$${paquete.precio}</li>
          <li class="list-group-item" style="color: brown">Descuento ${paquete.descuento}%</li>
        </ul>
        <div class="card-body">
          <a class="card-link" href="https://localhost:44327/Home/PaqueteInfo?paqueteId=${paquete.id}">Mas información</a>
          <a class="card-link" href="https://localhost:44327/Home/Reserva?paqueteId=${paquete.id}&nombrePaquete=${paquete.nombre}">Reservar YA</a>
        </div>
      </div>`
    };
};


//Redessociales
document.getElementById('contacto').onclick = RedesSociales;
function RedesSociales(event){
    $('html,body').animate(
        {
            scrollTop: $('#social').offset().top
        }
    );
}
function login(){
  // window.location.href = "http://localhost/paquetes/Frontend/admin/login.html";
  window.location.href = "Home/Login";
}

//renderizar seccion de login-usuario

function renderInformacionDeUsuario() {
    let usuario = document.getElementById("infoUsuario");
    if (!(typeof localStorage.getItem('nombre') !== 'undefined' && localStorage.getItem('nombre') !== null)) {
        usuario.innerHTML = `<a href='#'>Iniciar sesion</a>`
    }    

    else {
        var nombre = localStorage.getItem('nombre')
        usuario.innerHTML = `<a href='#'>${nombre} - Cerrar sesion</a>`
    }
        
        

    
}
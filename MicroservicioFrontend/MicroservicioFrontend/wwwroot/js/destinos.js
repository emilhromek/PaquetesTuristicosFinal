let URL = "https://localhost:44341/api/Destino"
let prom = fetch(URL);
console.log(prom);
prom.then(res => {
    console.log(res);
    const json = res.json();
    console.log(json);
    console.log(res);
    return json;
}).then(json => {
    console.log(json);
    for(let i=0; i < json.length; i++) {
        console.log(json[i].lugar);
    }
    renderizarDestinos(json);
});

function renderizarDestinos(destinos){
    let contenedor = document.getElementById("contenedorDestinos");
    for(let i=0; i < destinos.length; i++) {
        destino = destinos[i];
        contenedor.innerHTML +=
        `<div class="card mt-4 paquete" style="width: 18rem;">
        <img src="${destino.imagen}" class="card-img-top" alt="Img destino...">
        <div class="card-body">
          <h5 class="card-title">${destino.lugar}</h5>
          <p class="card-text">${destino.descripcion}</p>
        </div>
        <ul class="list-group list-group-flush">
          <li class="list-group-item">${destino.atractivo}</li>
          <li class="list-group-item">${destino.historia}</li>
        </ul>
        <div class="card-body">
          <a href="index.html?destinoId=${destino.id}&lugar=${destino.lugar}" class="card-link">Paquetes</>
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

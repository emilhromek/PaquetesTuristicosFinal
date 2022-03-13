var login =document.querySelector('#loginForm')

var tokenl
myHeaders = new Headers({
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    "Authorization": "Bearer "+tokenl ,
    credentials: 'include',
  });
login.addEventListener('submit', function (e) {
    e.preventDefault();
    var datos = new FormData(login);
    console.log(datos.get('email')); 
    let jsonDataConvert = JSON.stringify(
        {
            email: datos.get('email'),
            password: datos.get('password'),

        }               
    );
    // console.log(jsonDataConvert)
    fetch(`https://localhost:44384/api/User/authenticate`, {
        method: 'POST',
        body: jsonDataConvert,
        headers: myHeaders,
    })
        .then(res => {          
        if(res.status == 200){  
            console.log(res)    
            window.location.href = "../admin";
        }else{
            alert("Usuario o Contraseña incorrecta")
        }
        return res.json()

    }) .then(datos => {
        guardar(datos)       
    }).catch(error => console.log(error));
})
function guardar(dato){
    console.log(dato)
    localStorage.setItem('username',dato.username)
    localStorage.setItem('apellido',dato.apellido)
    localStorage.setItem('nombre',dato.nombre)
    localStorage.setItem('token',dato.token)
    localStorage.setItem('id',dato.id)
}

// function test(){
//     console.log(tokenl)
//     var test = localStorage.getItem('username')
//     var test2 = localStorage.getItem('token')
//     console.log(test)
//     console.log(test2)
// }

function salir(){
    localStorage.clear()
    window.location.href = "../login.html";
}
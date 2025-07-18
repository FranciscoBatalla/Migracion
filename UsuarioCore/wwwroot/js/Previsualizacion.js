
//PREVIZUALIZAR IMG
var CargarArchivo = function (input) {

    var img = input.target.files[0];

    if (!Validar(img.name)) {
        return 0;
    }

    var reader = new FileReader()

    reader.onload = function () {

        var output = $('#imgInput')[0];

        output.src = reader.result;
    }
    reader.readAsDataURL(img)
}

function Validar(imgName) {

    let extensions = ["jpg", "jpeg", "png"];
    if (imgName == null) {
        alert("No se subio tu archivo")
        return false;
    }
    let imgExtensions = imgName.split('.').pop().toLowerCase();

    if (!extensions.includes(imgExtensions)) {
        alert("No es un archivo valido");
        return false;
    }
    return true;
}
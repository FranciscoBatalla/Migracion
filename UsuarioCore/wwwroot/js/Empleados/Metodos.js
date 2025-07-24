let modo = "agregar";

$(document).ready(function () {
    cargarEmpleados();
    cargarDepartamentos();


    $('#btnAgregar').click(function () {
        modo = "agregar";
        limpiarFormulario();
        $('#modalEmpleado').modal('show');
    });//cierre btnagregar




    $('#btnGuardar').click(function () {
        let empleado = {
            IdEmpleado: parseInt($('#IdEmpleado').val()) /*? $('IdEmpleado').val() : 0*/,
            Nombre: $('#Nombre').val(),
            ApellidoPaterno: $('#ApellidoPaterno').val(),
            ApellidoMaterno: $('#ApellidoMaterno').val(),
            FechaNacimiento: $('#FechaNacimiento').val(),
            RFC: $('#RFC').val(),
            NSS: $('#NSS').val(),
            CURP: $('#CURP').val(),
            FechaIngreso: $('#FechaIngreso').val(),
            SalarioBase: $('#SalarioBase').val(),
            NoFaltas: parseInt($('#NoFaltas').val()),
            Departamento: {
                IdDepartamento: $('#IdDepartamento').val()
            }
        };

        const metodo = {
            type: (modo === "agregar") ? 'POST' : 'PUT',
            url: (modo === "agregar") ? EndpointAgregar : EndpointEditarEmpleado + "/" + empleado.IdEmpleado,
            data: JSON.stringify(empleado),
            contentType: 'application/json',

            success: function () {
                $('#modalEmpleado').modal('hide');
                cargarEmpleados();
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: (modo === "agregar") ? 'Empleado agregado correctamente' : 'Empleado actualizado correctamente',
                    timer: 2000,
                    showConfirmButton: false
                });
            },
            error: function (e) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error al guardar',
                    text: 'Revisa que los datos estén correctos o intenta más tarde',
                });
                console.log("Error", e.responseText);
            }
        };

        $.ajax(metodo);
    });

});//document.ready FUNCIONANDO



function cargarEmpleados() {

    let empleado = {
        Nombre: "",
        ApellidoPaterno: "",
        ApellidoMaterno: "",
        Departamento: {
            IdDepartamento: 0
        }
    }// fin de empleado

    $.ajax({
        type: 'POST',
        url: EndpointGetAll,
        data: JSON.stringify(empleado),
        contentType: 'application/json',
        success: function (data) {


            let tbody = $('#tblEmpleados tbody');
            tbody.empty();
            data.forEach(e => {

                const formatoFechaNacimiento = formatearFecha(e.fechaNacimiento);
                const formatoFechaIngreso = formatearFecha(e.fechaIngreso);

                console.log("Original:", e.fechaNacimiento, "| Formateada:", formatoFechaNacimiento);

                tbody.append(`
                        <tr>
                        <td>
                            <button class="btn btn-warning btn-sm" onclick="editar(${e.idEmpleado})">Editar</button>
                        </td>
                            <td>${e.nombre}</td>
                            <td>${e.apellidoPaterno}</td>
                            <td>${e.apellidoMaterno}</td>
                            <td>${formatoFechaNacimiento}</td>
                            <td>${e.rfc}</td>
                            <td>${e.nss}</td>
                            <td>${e.curp}</td>
                            <td>${formatoFechaIngreso}</td>
                            <td>${e.salarioBase}</td>
                            <td>${e.noFaltas}</td>
                            <td>${e.departamento.descripcion}</td>
                            <td>
                                <button class=" btn btn-danger btn-sm" onclick="eliminar(${e.idEmpleado})">Eliminar</button>
                            </td>
                        </tr>
                        
                    `);



            });



        },
        error: function (e) {
            console.log("Ocurrio un error en la solicitud", e)
        },
    }); //FIN AJAX
} //FUNCIONANDO GETALL
function cargarDepartamentos() {
    $.get(EndpointDepartamentoGetAll, function (data) {
        let ddl = $('#IdDepartamento');
        ddl.empty();
        ddl.append(`<option value="">Selecciona un departamento</option>`);
        data.forEach(d => {
            ddl.append(`<option value="${d.idDepartamento}">${d.descripcion}</option>`);
        });
    });
}//FUNCINANDO GETALL DEPARTAMENTOS

function limpiarFormulario() {
    $('#IdEmpleado').val('');
    $('#Nombre').val('');
    $('#ApellidoPaterno').val('');
    $('#ApellidoMaterno').val('');
    $('#FechaNacimiento').val('');
    $('#RFC').val('');
    $('#NSS').val('');
    $('#CURP').val('');
    $('#FechaIngreso').val('');
    $('#SalarioBase').val('');
    $('#NoFaltas').val('');
    $('#IdDepartamento').val('');
}//FUNCIONANDO
function editar(idEmpleado) {
    Swal.fire({
        title: 'Cargando datos...',
        didOpen: () => {
            Swal.showLoading();
        },
        allowOutsideClick: false
    });
    $.ajax({
        type: 'GET',
        url: EndpointEmpleadoGetById + `/${idEmpleado}`,
        success: function (data) {
            Swal.close();
            $('#IdEmpleado').val(data.idEmpleado);
            $('#Nombre').val(data.nombre);
            $('#ApellidoPaterno').val(data.apellidoPaterno);
            $('#ApellidoMaterno').val(data.apellidoMaterno);
            $('#FechaNacimiento').val(data.fechaNacimiento.substring(0, 10));
            $('#RFC').val(data.rfc);
            $('#NSS').val(data.nss);
            $('#CURP').val(data.curp);
            $('#FechaIngreso').val(data.fechaIngreso.substring(0, 10));
            $('#SalarioBase').val(data.salarioBase);
            $('#NoFaltas').val(data.noFaltas);
            $('#IdDepartamento').val(data.departamento.idDepartamento);

            modo = "editar";
            $('#modalTitle').text('Editar Empleado');
            $('#modalEmpleado').modal('show');
        },
        error: function (xhr) {
            console.error("Error al obtener el empleado:", xhr.responseText);
        }
    });
}//FUNCIONANDO



function eliminar(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción no se puede deshacer",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: EndpointEliminarEmpleado + `/${id}`,
                contentType: 'application/json',
                success: function () {
                    cargarEmpleados();

                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'El empleado ha sido eliminado exitosamente',
                        timer: 2000,
                        showConfirmButton: false
                    });
                },
                error: function (e) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error al eliminar',
                        text: 'No se pudo eliminar el empleado, intenta más tarde'
                    });
                    console.log("Error", e.responseText);
                }
            });
        }
    });
}//FUNCIONANDO

//}












//$(document).on('click', '.eliminarbtn', function () {

//    var id = $(this).data('id');

//    if (confirm("Estas seguro de eliminar este restaurante?")) {
//        $.ajax({
//            url: EndpointEliminarEmpleado.replace("{IdEmpleado}", id),
//            method: "DELETE",
//            success: function () {
//                alert("Empleado eliminado correctamente");
//                location.reload();
//            },
//            error: function () {
//                alert("Ocurrio un error");
//            }
//        })
//    }


//})

//function formatearFecha(fechaStr) {
//    if (!fechaStr) return "";

//    ```
//const fecha = new Date(fechaStr);
//const dia = ("0" + fecha.getDate()).slice(-2);
//const mes = ("0" + (fecha.getMonth() + 1)).slice(-2);
//const año = fecha.getFullYear();

//return `${ dia } /${mes}/${ año } `;

//```
//}


function formatearFecha(fechaISO) {
    if (!fechaISO) return '';

    const fecha = new Date(fechaISO);
    const dia = String(fecha.getDate()).padStart(2, '0');
    const mes = String(fecha.getMonth() + 1).padStart(2, '0');
    const anio = fecha.getFullYear();

    return `${dia}/${mes}/${anio}`;
}





function validarLetras(input, event) {
    var regex = /^[a-zA-Z\s]+$/;
    var letra = event.key;



    //console.log("KEYPRESS", regex.test(letra))
    //console.log("BLUR", regex.test(blur))

    if (event.type === "keypress") {


        if (regex.test(letra)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }
    } else if (event.type === "blur") {

        var valor = $(input).val();

        if (regex.test(valor)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            //event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }

    }


}


//FIN SOLO LETRAS


//LETRAS Y NUMEROS

function validarLetrasyNumeros(input, event) {
    var regex = (/^[a-zA-Z0-9]+$/);

    var char = event.key;
    if (event.type == "keypress") {
        if (regex.test(char)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }
    } else if (event.type === "blur") {

        var valor = $(input).val();

        if (regex.test(valor)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }

    }

}

//FIN SOLO LETRAS Y NUMEROS



//VALIDAR CURP

function ValidarCURP(input, event) {

    var regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;

    var char = input.value.toUpperCase();


    //console.log(char)
    //console.log(regex.test(char))

    if (regex.test(char)) {
        $(input).removeClass('border border-danger').addClass('is-valid');
        $(input).addClass('border border-success').removeClass('is-invalid')
    } else {
        //event.preventDefault();
        $(input).addClass('border border-danger').removeClass('is-valid');
        $(input).removeClass('border border-success').addClass('is-invalid');
    }
}
//FIN VALIDAR CURP


function validarSoloNumeros(input, event) {

    var regex = /^\d+(\.\d{1,2})?$/;

    var char = event.key;

    if (event.type === "keypress") {

        if (regex.test(char)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }
    } else if (event.type === "blur") {

        var valor = $(input).val();
        if (regex.test(valor)) {
            $(input).removeClass('border border-danger').addClass('is-valid');
            $(input).addClass('border border-success').removeClass('is-invalid')
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger').removeClass('is-valid');
            $(input).removeClass('border border-success').addClass('is-invalid');
        }
    }


}

function validarRFC(input, event) {
    var regex = /^[0-9]*$/;
    var char = event.key;

    if (event.type === "keypress") {
        var valorActual = $(input).val();

        if (regex.test(char) && valorActual.length < 13) {
            $(input).removeClass('border border-danger is-invalid')
                .addClass('border border-success is-valid');
        } else {
            event.preventDefault();
            $(input).addClass('border border-danger is-invalid')
                .removeClass('border border-success is-valid');
        }

    } else if (event.type === "blur") {
        var valor = $(input).val();

        if (regex.test(valor) && valor.length <= 13) {
            $(input).removeClass('border border-danger is-invalid')
                .addClass('border border-success is-valid');
        } else {
            $(input).addClass('border border-danger is-invalid')
                .removeClass('border border-success is-valid');
        }
    }
}

function ValidarConParent(input, event) {

    var padre = $(input).closest('div');
    var span = padre.find('span');

    const regex = /^[A-Za-z0-9\s]+$/g;
    var letra = event.key;

    if (regex.test(letra)) {
        $(input).removeClass("border border-danger").addClass('border border-success');
        $(span).attr("hidden", "hidden");
    } else {
        event.preventDefault();
        $(span).removeAttr('hidden').text("No se aceptan carcateres especiales.").css("color", "red");
        $(input).addClass('border border-danger').removeClass('border border-success');
    }
}


function ValidarDepartamento(drop, event) {
    var index = drop.selectedIndex;
    var valor = $(drop).val();

    if (event.type === "change") {
        if (index !== 0) {
            $(drop).removeClass('border border-danger is-invalid')
                .addClass('border border-success is-valid');
        } else {
            $(drop).removeClass('border border-success is-valid')
                .addClass('border border-danger is-invalid');
        }
    } else if (event.type === "blur") {
        if (index === 0 || valor === "") {
            $(drop).removeClass('border border-success is-valid')
                .addClass('border border-danger is-invalid');
        } else {
            $(drop).removeClass('border border-danger is-invalid')
                .addClass('border border-success is-valid');
        }
    }
}



function validarFecha(input, event) {
    var valor = $(input).val();

    if (!valor) {
        event.preventDefault();
        $(input).addClass('border border-danger is-invalid')
            .removeClass('border border-success is-valid');
        return;
    }

    // Validar que no sea fecha futura (opcional)
    var fechaActual = new Date();
    var fechaIngresada = new Date(valor);

    if (fechaIngresada > fechaActual) {
        $(input).addClass('border border-danger is-invalid')
            .removeClass('border border-success is-valid');
    } else {
        $(input).removeClass('border border-danger is-invalid')
            .addClass('border border-success is-valid');
    }
}


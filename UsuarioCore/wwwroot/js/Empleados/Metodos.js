let modo = "agregar";

$(document).ready(function () {
    cargarEmpleados();
    cargarDepartamentos();


    $('#btnAgregar').click(function () {
        limpiarFormulario();
        $('#modalEmpleado').modal('show');
    });//cierre btnagregar




    $('#btnGuardar').click(function () {
        let empleado = {
            IdEmpleado: parseInt( $('#IdEmpleado').val()) /*? $('IdEmpleado').val() : 0*/,
            Nombre: $('#Nombre').val(),
            ApellidoPaterno: $('#ApellidoPaterno').val(),
            ApellidoMaterno: $('#ApellidoMaterno').val(),
            FechaNacimiento: $('#FechaNacimiento').val(),
            RFC: $('#RFC').val(),
            NSS: $('#NSS').val(),
            CURP: $('#CURP').val(),
            FechaIngreso: $('#FechaIngreso').val(),
            SalarioBase: $('#SalarioBase').val(),
            NoFaltas: parseInt( $('#NoFaltas').val()),
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
            },
            error: function (e) {
                console.log("Error", e.responseText)
            }
        };

        $.ajax(metodo);
    });

});//document.ready



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

                //const formatoFecha = formatearFecha(e.fechaNacimiento);
                //const formatoFechaIngreso = formatearFecha(e.fechaIngreso);


                tbody.append(`
                        <tr>
                        <td>
                            <button class="btn btn-warning btn-sm" onclick="editar(${e.idEmpleado})">Editar</button>
                        </td>
                            <td>${e.nombre}</td>
                            <td>${e.apellidoPaterno}</td>
                            <td>${e.apellidoMaterno}</td>
                            <td>${e.fechaNacimiento}</td>
                            <td>${e.rfc}</td>
                            <td>${e.nss}</td>
                            <td>${e.curp}</td>
                            <td>${e.fechaIngreso}</td>
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
}
function editar(idEmpleado) {
    $.ajax({
        type: 'GET',
        url: EndpointEmpleadoGetById + `/${idEmpleado}`,
        success: function (data) {
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
}



function eliminar(id) {
    //if (confirm('¿Deseas eliminar este empleado?')) {
    //    $.get(EndpointEliminarEmpleado + `/${id}`, function () {
    //        cargarEmpleados();
    //    });


        $.ajax({
            type: 'DELETE',
            url: EndpointEliminarEmpleado + `/${id}`,
            data: 'JSON',
            contentType: 'application/json',
            success: function () {
                $('#modalEmpleado').modal('hide');
                cargarEmpleados();
            },
            error: function (e) {
                console.log("Error", e.responseText)
            },
        });
    }
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


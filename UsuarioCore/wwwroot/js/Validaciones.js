function manejarValidacion(input, event, regex) {
    const $input = $(input);
    const valor = $input.val();
    const esKeyPress = event.type === "keypress";
    const esBlur = event.type === "blur";

    if (esKeyPress && !regex.test(event.key)) {
        event.preventDefault();
        return;
    }

    if ((esBlur || esKeyPress) && regex.test(valor)) {
        $input.removeClass('is-invalid border-danger').addClass('is-valid border-success');
    } else if (esBlur) {
        $input.removeClass('is-valid border-success').addClass('is-invalid border-danger');
    }
}

// Validación de solo letras
function validarLetras(input, event) {
    const regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
    manejarValidacion(input, event, regex);
}

// Validación de letras con código ASCII
function validaLetrasConASCII(input, event) {
    const keyCode = event.keyCode;
    const esValida = (keyCode > 64 && keyCode < 91) ||  // A-Z
        (keyCode > 96 && keyCode < 123) || // a-z
        keyCode == 32 ||                   // Espacio
        (keyCode >= 192 && keyCode <= 255); // Caracteres acentuados

    if (event.type === "keypress" && !esValida) {
        event.preventDefault();
    }

    const $input = $(input);
    if (esValida) {
        $input.removeClass('is-invalid border-danger').addClass('is-valid border-success');
    } else if (event.type === "blur") {
        $input.removeClass('is-valid border-success').addClass('is-invalid border-danger');
    }
}

// Validación de solo números con ASCII
function validaLNumerosConASCII(input, event) {
    const keyCode = event.keyCode;
    const esNumero = (keyCode > 47 && keyCode < 58); // 0-9

    if (event.type === "keypress" && !esNumero) {
        event.preventDefault();
    }

    const $input = $(input);
    if (esNumero) {
        $input.removeClass('is-invalid border-danger').addClass('is-valid border-success');
    } else if (event.type === "blur") {
        $input.removeClass('is-valid border-success').addClass('is-invalid border-danger');
    }
}

// Validación de letras y números
function validarLetrasyNumeros(input, event) {
    const regex = /^[a-zA-Z0-9]+$/;
    manejarValidacion(input, event, regex);
}

// Validación de email
function validarEmail(input, event) {
    const email = $(input).val();
    const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    const isValid = regex.test(email);

    if (event.type === "blur") {
        if (email === "") {
            $(input).removeClass('is-valid is-invalid');
        } else if (isValid) {
            $(input).removeClass('is-invalid').addClass('is-valid');
            // Si es el email principal, validar también el de confirmación
            if (input.id === "inputEmail") {
                ValidarMismoEmail(document.getElementById('inputConfirmEmail'), event);
            }
        } else {
            $(input).removeClass('is-valid').addClass('is-invalid');
        }
    }
}


// Validación de coincidencia de emails
function ValidarMismoEmail(input, event) {
    const email = $('#inputEmail').val();
    const confirmEmail = $('#inputConfirmEmail').val();
    const $input = $(input);

    if (email === confirmEmail && email !== "") {
        $input.removeClass('is-invalid border-danger').addClass('is-valid border-success');
    } else {
        $input.removeClass('is-valid border-success').addClass('is-invalid border-danger');
    }
}

// Validación de CURP
function ValidarCURP(input, event) {
    const regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;
    manejarValidacion(input, event, regex);
}

// Validación de teléfono (10 dígitos)
function ValidarTelefono(input, event) {
    const regex = /^\d{10}$/;
    manejarValidacion(input, event, regex);
}

// Validación de celular (10 dígitos)
function ValidarCelular(input, event) {
    ValidarTelefono(input, event); // Misma validación que teléfono
}

// Validación de solo números
function validarSoloNumeros(input, event) {
    const regex = /^[0-9]*$/;
    manejarValidacion(input, event, regex);
}

// Validación de calle
function validarCalle(input, event) {
    const regex = /^[A-Za-z0-9\s]+$/;
    manejarValidacion(input, event, regex);
}

// Validación con mensaje en span
function ValidarConParent(input, event) {
    const $padre = $(input).closest('div');
    const $span = $padre.find('span');
    const regex = /^[A-Za-z0-9\s]+$/;
    const valor = $(input).val();

    if (regex.test(valor)) {
        $(input).removeClass('border-danger').addClass('border-success');
        $span.attr("hidden", true);
    } else {
        $(input).removeClass('border-success').addClass('border-danger');
        $span.removeAttr('hidden').text("No se aceptan caracteres especiales.").css("color", "red");
    }
}

// Validación de contraseña
function validarPassword(input, event) {
    const password = $(input).val();
    // Requisitos: 8-10 caracteres, sin espacios
    const isValid = password.length >= 8 && password.length <= 10 && !/\s/.test(password);

    if (event.type === "blur") {
        if (password === "") {
            $(input).removeClass('is-valid is-invalid');
        } else if (isValid) {
            $(input).removeClass('is-invalid').addClass('is-valid');
            // Si es la contraseña principal, validar también la confirmación
            if (input.id === "inputPassword") {
                validarMismoPassword(document.getElementById('inputConfirmPassword'), event);
            }
        } else {
            $(input).removeClass('is-valid').addClass('is-invalid');
        }
    }
}

// Validación de coincidencia de contraseñas
function validarMismoPassword(input, event) {
    const password = $('#inputPassword').val();
    const confirmPassword = $(input).val();

    if (event.type === "blur") {
        if (confirmPassword === "") {
            $(input).removeClass('is-valid is-invalid');
        } else if (password === confirmPassword && password !== "") {
            $(input).removeClass('is-invalid').addClass('is-valid');
        } else {
            $(input).removeClass('is-valid').addClass('is-invalid');
        }
    }
}

// Validación de rol seleccionado
function ValidarRol(drop, event) {
    const valor = $(drop).val();
    const $drop = $(drop);

    if (valor && valor !== "") {
        $drop.removeClass('is-invalid border-danger').addClass('is-valid border-success');
    } else {
        $drop.removeClass('is-valid border-success').addClass('is-invalid border-danger');
    }
}

// Validación de fecha
function validarFecha(input, event) {
    const regex = /^([1-2][0-9]|3[0-1]|0?[1-9])([-\.\/ ])(1[0-2]|0?[1-9])(\2)([\d]{4}|[\d]{2})$/;
    manejarValidacion(input, event, regex);
}

// Prevenir acciones de copiar/pegar en emails
$(document).ready(function () {
    $("#inputEmail, #inputConfirmEmail").on('paste copy', function (e) {
        e.preventDefault();
        alert('Esta acción está prohibida');
    });
});

// Validación de formulario Bootstrap
(function () {
    'use strict';
    const forms = document.querySelectorAll('.needs-validation');

    Array.from(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
})();

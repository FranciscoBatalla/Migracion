    //const urlMunicipios = '@Url.Action("MunicipioGetByIdEstado", "UsuarioMigration")';
    //const urlColonias = '@Url.Action("ColoniaGetByIdMunicipio", "UsuarioMigration")';

    $(document).ready(function() {
        $('#ddlEstado').on('change', FillMunicipios);
    $('#ddlMunicipio').on('change', FillColonias);
    });

    function FillMunicipios() {
        var idEstado = $('#ddlEstado').val(); 
    console.log("Estado seleccionado:", idEstado);

    if (!idEstado) {
        $('#ddlMunicipio').empty().append('<option value="">Selecciona un Municipio</option>');
    $('#ddlColonia').empty().append('<option value="">Selecciona una Colonia</option>');
    return;
        }

    $.ajax({
        url: urlMunicipios,
    dataType: 'json',
    method: 'GET',
    data: {idEstado: idEstado },
    success: function (result) {
        console.log("Respuesta municipios:", result);

    var $ddlMunicipio = $("#ddlMunicipio");
    $ddlMunicipio.empty().append('<option value="">Selecciona un Municipio</option>');

                if (result.correct && result.objects && result.objects.length > 0) { 
        $.each(result.objects, function (i, municipio) { 
            $ddlMunicipio.append(
                $('<option></option>')
                    .val(municipio.idMunicipio) 
                    .text(municipio.nombre) 
            );
        });
                } else {
        console.warn("No se encontraron municipios");
                }

    $('#ddlColonia').empty().append('<option value="">Selecciona una Colonia</option>');
            },
    error: function (xhr, status, error) {
        console.error("Error al cargar municipios:", error);
    alert("Error al cargar municipios. Ver consola para detalles.");
            }
        });
    }

    function FillColonias() {
        var idMunicipio = $('#ddlMunicipio').val();
    console.log("Municipio seleccionado:", idMunicipio);

    if (!idMunicipio) {
        $('#ddlColonia').empty().append('<option value="">Selecciona una Colonia</option>');
    return;
        }

    $.ajax({
        url: urlColonias,
    dataType: 'json',
    method: 'GET',
    data: {idMunicipio: idMunicipio }, 
    success: function (result) {
        console.log("Respuesta colonias:", result);

    var $ddlColonia = $("#ddlColonia");
    $ddlColonia.empty().append('<option value="">Selecciona una Colonia</option>');

                if (result.correct && result.objects && result.objects.length > 0) {
        $.each(result.objects, function (i, colonia) {
            $ddlColonia.append(
                $('<option></option>')
                    .val(colonia.idColonia) 
                    .text(colonia.nombre) 
            );
        });
                } else {
        console.warn("No se encontraron colonias");
                }
            },
    error: function (xhr, status, error) {
        console.error("Error al cargar colonias:", error);
    alert("Error al cargar colonias. Ver consola para detalles.");
            }
        });
    }

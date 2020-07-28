var api = "https://localhost:44325/api/Categories/";
$(document).ready(function () {
    // Si la pagina se carga mostrar los registros en la etiqueta "Table"
    
    mostrarCategorias();
    $(document.body).on('click', 'a', function (e) {
        e.preventDefault();
        // Al hace un click sobre la clave primaria de un registro de la tabla, buscar
        // por la clave primaria y mostrar el registro seleccionado en los TextBox.
        buscarCategoria($(this).attr("href"));
    });
});

// Recuperar los datos de la tabla Categories y enlazarlos a la etiqueta "Table".
function mostrarCategorias() {
    // Recuperar los datos y llamar el método con el prefijo "Get" de la Web API.
    $.getJSON(api, function (datos) {
        var filas = "";
        $.each(datos, function (clave, valor) {
            // Con cada fila recuperada construir cada celda y formar
            // una fila con los datos, con lo cual se agrega celdas y filas a la
                filas += "<tr><td><a href=" + api + valor.CategoryID + ">"
                + valor.CategoryID + "</a></td>";
            filas += "<td>" + valor.ShortName + "</td>";
            filas += "<td>" + valor.LongName + "</td></tr>";
        });
        // Buscar la etiqueta con el id: "categories" dentro de > tbody tr
        $('#categories > tbody tr').remove();// Remover
        $('#categories > tbody').append(filas); // Agregar
        limpiarTodo();//Llamar al metodo: "limpiarTodo"
    })
        .fail(mostrarError);// Si falla la llamada invocar al metodo: "mostrarError"
};
// Buscar una categoria en base a la clave primaria.
function buscarCategoria(href) {
    $('#message').html("Buscando...");
    // Llamar al metodo con el prefijo "Get" de la Web API que pasa como
    // parametro el atributo "href".
    $.getJSON(href, function (datos) {
        // Poner los datos en los TextBox
        $('#id').val(datos.CategoryID);
        // Deshabilitar (Bloquear) el TextBox que tiene la clave primaria, para
        // que no se pueda ingresar datos en este TextBox.
        $("#id").attr("disabled", "disabled");
        // Poner los datos en los TextBox
        $('#short').val(datos.ShortName);
        // Poner los datos en los TextBox
        $('#long').val(datos.LongName);
        // Limpiar el TextBox
        $('#message').text("");
    })
        .fail(mostrarError); // Si falla la llamada invocar al metodo: "mostrarError"
};

// Usar AJAX para llamar el metodo con el prefijo "Post" de la Web API.
function insertarCategoria() {
    // Mostrar un texto en el TextBox
    $('#message').text("Insertando...");
    // Usar AJAX
    $.ajax({
        type: 'POST', // Metodo con el prefijo "Post" de la Web API.
        url: api, // Direccion de la URL
        data: $('#form1').serialize(),
        dataType: "json", // Formato JSon
        success: mostrarCategorias, // Llamar al metodo
        error: mostrarError // Llamar al metodo
    });
};
//Usar AJAX para llamar el método con el prefijo "Put" de la Web API.
function actualizarCategoria() {
    $('#message').text("Actualizando...");
    $.ajax({
        type: 'PUT', // Metodo con el prefijo "Put" de la Web API.
        url: api + $('#id').val(), // Direccion de la URL mas la clave primaria
        data: $('#form1').serialize(),
        dataType: "json", // Formato JSon
        success: mostrarCategorias, // Llamar al metodo
        error: mostrarError // Llamar al metodo
    });
};
// Usar AJAX para llamar el metodo con el prefijo "Delete" de la Web API.
function eliminarCategoria() {
    $('#message').text("Eliminando...");
    $.ajax({
        type: 'DELETE', // Metodo con el prefijo "Delete" de la Web API.
        url: api + $('#id').val(), // Direccion de la URL mas la clave primaria
        data: $('#form1').serialize(),
        dataType: "json", // Formato JSon
        success: mostrarCategorias, // Llamar al metodo
        error: mostrarError // Llamar al metodo
    });
};
// Mostrar en mensaje de error si no se puede conectar con el servidor.
function mostrarError(solicitud, estado, error) {
    try {
        // Capturar el error y mostrar el usuario en un TextBox
        var respuesta = JSON.parse(solicitud.responseText);
        $('#message').text(respuesta.ExceptionMessage);
    } catch (e) {
        // Mostrar un mensaje en el TextBox con el id=message
        $('#message').text("Ocurrio un error - no es posible conectar con el servidor.");
 }
};
//Borrar todos los datos que se muestran en los controles (TextBox)
function limpiarTodo() {
    $('#id').val("");
    // Deshabilitar el TextBox
    $("#id").removeAttr("disabled");
    $('#short').val("");
    $('#long').val("");
    $('#message').text("");
};


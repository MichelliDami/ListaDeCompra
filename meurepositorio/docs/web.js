// Espera o DOM carregar
$(document).ready(function () {
    // Intercepta o submit do formulário
    $("#form-inserir").submit(function (event) {
        event.preventDefault(); // impede envio tradicional

        // Verifica se o formulário é válido segundo o Bootstrap
        let form = this;
        if (!form.checkValidity()) {
            event.stopPropagation();
            $(form).addClass('was-validated');
            return; // não envia se inválido
        }

        // Prepara os dados para enviar
        let dataForm = {
            nome: $('#nome').val(),
            prioridade: parseInt($('#prioridade').val()),
            localItem: $('#categoria').val(),
            quantidade: parseInt($('#quantidade').val())
        };

        // Envia os dados via AJAX
        $.ajax({
            url:"https://localhost:7076/api/ListaCompra/inserir",
            method: "POST",
            contentType : "application/json",
            data: JSON.stringify(dataForm),
            beforeSend: function () {
                $("button[type='submit']").prop('disabled', true);
            },
            success: function (resposta) {
                // Sucesso: limpa o form e ordena a lista
                $('#form-inserir')[0].reset();
                $('#form-inserir').removeClass('was-validated');
                getOrdenar(); // sua função
            },
            complete: function () {
                $("button[type='submit']").prop('disabled', false);
            },
            error: function (response) {
                console.log(response)
                alert("Erro ao inserir.");
            }
        });
    });

    getOrdenar()

    
});

function deletar(id){
    $.ajax({
        url:"https://localhost:7076/api/ListaCompra/deletar/"+id,
        method: "DELETE",
        contentType : "application/json",
        success: function (response) {
            getOrdenar()
        },
        error: function (response) {
            console.log(response)
            alert("Erro ao listar.");
        }
    });
}

function getOrdenar(){
        $.ajax({
            url:"https://localhost:7076/api/ListaCompra/ordenados",
            method: "GET",
            contentType : "application/json",
            success: function (response) {
                console.log(response)

                $('#data-server').html('')

                $.each(response, function(index, item){
                    let html = ''
                    html += '<tr>'
                    html +=       '<th scope="row">'
                    html +=           item.nome
                    html +=      '</th>'
                    html +=        '<td class="text-center">'+item.localItem+'</td>'
                    html +=        '<td class="text-center">'+item.quantidade+'</td>'
                    html +=        '<td class="text-center">'
                    html +=            '<span class="badge badge-'+ (item.prioridade == 1 ? 'success' : item.prioridade == 2 ? 'warning' : 'danger') +' w-100">'+ (item.prioridade == 1 ? 'Baixa' : item.prioridade == 2 ? 'Média' : 'Alta') +'</span>'
                    html +=        '</td>'
                    html +=        '<td class="text-center">'
                    html +=            '<button class="btn btn-danger px-2 py-1 btn-sm" onclick="deletar(\'' + item.id + '\')"> '
                    html +=        '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">'
                    html +=            '<path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5" />'
                    html +=       '</svg>'
                    html +=    '</button>'
                    html += '</td>'
                    html +=    '</tr>'
                    $('#data-server').append(html)
                })
            },
            error: function (response) {
                console.log(response)
                alert("Erro ao listar.");
            }
        });
    }










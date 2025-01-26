(function () {

    var paginaAtual = 1;
    var totalPagina = 1;

    var botaoPesquisarOrcamento = document.getElementById("btnPesquisarOrcamento");
    var inputPesquisaNumero = document.getElementById("inputPesquisarOrcamento");
    var inputPesquisaCliente = document.getElementById("inputPesquisarOrcamentoCliente");
    var tabela = document.getElementsByTagName("tbody");

    var tableBody = document.querySelector("#tabela-Index tbody");

    botaoPesquisarOrcamento.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisaNumero.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });
    inputPesquisaCliente.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var nomeCliente = inputPesquisaCliente.value;
        var numeroOrcamento = inputPesquisaNumero.value;

        $.ajax({
            type: "GET",
            url: "/orcamento/PesquisarOrcamentoIndex",
            data: { nome: nomeCliente, numeroOrcamento: numeroOrcamento, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";


                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.IdOrcamento + "</td>" +
                            "<td>" + element.Cliente + "</td>" +
                            "<td>" + element.Telefone + "</td>" +
                            "<td>" + element.ValorFinal + "</td>" +
                            "<td>" +
                            "<a href='/orcamento/orcamentopdf?idOrcamento=" + element.IdOrcamento + "'>" +
                            "<i class= 'bi bi-file-earmark-binary-fill' style='font-size:25px; color:black' data-toggle='tooltip' data-placement='top' title='Orcamento'></i>" +
                            "</a>" +
                            "<a href='/orcamento/editar?idOrcamento=" + element.IdOrcamento + "'>" +
                            "<i class= 'fbi bi-arrow-counterclockwise' style='font-size:25px; color:black'  data-toggle='tooltip' data-placement='top' title='Alterar Os'></i>" +
                            "</a>" +
                            "</td>" +
                            "</tr>";
                    });
                }
                else {

                    totalPagina = 1;
                    Notiflix.Notify.failure('Sem resultados para essa pesquisa');
                }

                tableBody.innerHTML = list;

            }
        });
    };

})();
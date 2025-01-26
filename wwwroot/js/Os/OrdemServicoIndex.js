(function () {

    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var botaoPesquisarOsIndex = document.getElementById("botaoPesquisarOs");
    var inputPesquisaIndex = document.getElementById("inputPesquisa");
    var inputPesquisaNumero = document.getElementById("inputPesquisaNumero");


    //(function () {
    //    var pag = tabelaIndexOs.getAttribute("data-totalPagina");
    //    totalPagina = parseInt(pag);

    //})();

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }

    setaEsquerda.addEventListener("click", function () {
        paginaAtual--;
        if (paginaAtual < 0)
            paginaAtual = 1;

        Pesquisar();
    });

    setaDireita.addEventListener("click", function () {

        paginaAtual++;
        if (paginaAtual > totalPagina)
            paginaAtual = totalPagina;

        Pesquisar();
            
    });
    botaoPesquisarOsIndex.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisaIndex.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var nomeCliente = inputPesquisaIndex.value;
        var numeroOs = inputPesquisaNumero.value;

        $.ajax({
            type: "GET",
            url: "/Os/PesquisarOsIndex",
            data: { nome: nomeCliente, numeroOs: numeroOs, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";
                

                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.NumeroOs + "</td>" +
                            "<td>" + element.Cliente + "</td>" +
                            "<td>" + element.DataAbertura + "</td>" +
                            "<td>" +
                            "<a href='/os/Relatorio?idOs=" + element.idOs + "'>" +
                            "<i class='bi bi-file-earmark-spreadsheet-fill' style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Detalhe da Os'></i>" +
                            "</a>" +
                            "<a href='/os/Editar?idOs=" + element.idOs + "'>" +
                            "<i class='bi bi-gear-fill' style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Alterar Os'></i>" +
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
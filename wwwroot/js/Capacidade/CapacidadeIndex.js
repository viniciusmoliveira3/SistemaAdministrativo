(function () {

    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var botaoPesquisarCapacidade = document.getElementById("btnPesquisarCapacidade");
    var inputPesquisarCapacidade = document.getElementById("inputPesquisarCapacidade");


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
    botaoPesquisarCapacidade.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisarCapacidade.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var capacidade = inputPesquisarCapacidade.value;

        $.ajax({
            type: "GET",
            url: "/Capacidade/PesquisarCapacidadeIndex",
            data: { capacidade: capacidade, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";


                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.IdCapacidade + "</td>" +
                            "<td>" + element.Capacidade + "</td>" +
                            "<td>" +
                            "<a href='/Capacidade/Detalhe?id=" + element.idCapacidade + "'>" +
                            "<i class= 'fa - solid fa-eye fa-lg' data-toggle='tooltip' data-placement='top' title='Detalhe da capacidade'></i>" +
                            "</a>" +
                            "<a href='/Capacidade/Editar?id=" + element.idCapacidade + "'>" +
                            "<i class='fa - solid fa-gear fa-lg' data-toggle='tooltip' data-placement='top' title='Alterar capacidade'></i>" +
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
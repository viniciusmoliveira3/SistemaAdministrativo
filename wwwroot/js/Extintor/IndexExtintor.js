(function () {


    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var botaoPesquisarExtintor = document.getElementById("btnPesquisarExtintor");
    var inputPesquisaNumeroCilindro = document.getElementById("inputPesquisaExtintor");


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
    botaoPesquisarExtintor.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisaNumeroCilindro.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var numeroCilindro = inputPesquisaNumeroCilindro.value;
        
        $.ajax({
            type: "GET",
            url: "/Extintor/PesquisarExtintorIndex",
            data: { numeroExtintor: numeroCilindro, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";


                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.idExtintor + "</td>" +
                            "<td>" + element.NumeroCilindro + "</td>" +
                            "<td>" + element.MarcaExtintor + "</td>" +
                            "<td>" + element.MateriaPrima + "</td>" +
                            "<td>" + element.Capacidade + "</td>" +
                            "<td>" + element.ProximoEnsaioHisdrostatico + "</td>" +
                            "<td>" +
                            "<a href='/Extintor/Detalhe?id=" + element.idExtintor + "'>" +
                            "<i class= 'bi bi-info-square-fill' style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Detalhe do extintor'></i>" +
                            "</a>" +
                            "<a href='/Extintor/Editar?id=" + element.idExtintor + "'>" +
                            "<i class='bi bi-gear-fill' style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Alterar extintor'></i>" +
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
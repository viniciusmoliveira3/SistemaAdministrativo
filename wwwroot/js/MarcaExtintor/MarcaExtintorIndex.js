(function () {

    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var btnPesquisarMarcaExtintor = document.getElementById("btnPesquisarMarcaExtintor");
    var inputPesquisarMarcaExtintor = document.getElementById("inputPesquisarMarcaExtintor");


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
    btnPesquisarMarcaExtintor.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisarMarcaExtintor.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var marca = inputPesquisarMarcaExtintor.value;

        $.ajax({
            type: "GET",
            url: "/MarcaExtintor/PesquisarMarcaExtintorIndex",
            data: { marca: marca, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";


                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.Id + "</td>" +
                            "<td>" + element.Nome + "</td>" +
                            "<td>" +
                            "<a href='/MarcaExtintor/Detalhe?id=" + element.Id + "'>" +
                            "<i class= 'fa - solid fa-eye fa-lg' data-toggle='tooltip' data-placement='top' title='Detalhe da marca de extintor'></i>" +
                            "</a>" +
                            "<a href='/MarcaExtintor/Editar?id=" + element.Id + "'>" +
                            "<i class='fa - solid fa-gear fa-lg' data-toggle='tooltip' data-placement='top' title='Alterar marca de extintor'></i>" +
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
(function () {

    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var btnPesquisarFornecedor = document.getElementById("btnPesquisarFornecedor");
    var inputPesquisarFornecedor = document.getElementById("inputPesquisarFornecedor");


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
    btnPesquisarFornecedor.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisarFornecedor.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var fornecedor = inputPesquisarFornecedor.value;

        $.ajax({
            type: "GET",
            url: "/Fornecedor/PesquisarFornecedorIndex",
            data: { fornecedor: fornecedor, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";


                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {

                        list += "<tr>" +
                            "<td>" + element.Id + "</td>" +
                            "<td>" + element.NomeFantasia + "</td>" +
                            "<td>" + element.Telefone + "</td>" +
                            "<td>" + element.Email + "</td>" +
                            "<td>" +
                            "<a href='/Fornecedor/Detalhe?id=" + element.Id + "'>" +
                            "<i class= 'fa - solid fa-eye fa-lg' data-toggle='tooltip' data-placement='top' title='Detalhe do fornecedor'></i>" +
                            "</a>" +
                            "<a href='/Fornecedor/Editar?id=" + element.Id + "'>" +
                            "<i class='fa - solid fa-gear fa-lg' data-toggle='tooltip' data-placement='top' title='Alterar fornecedor'></i>" +
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
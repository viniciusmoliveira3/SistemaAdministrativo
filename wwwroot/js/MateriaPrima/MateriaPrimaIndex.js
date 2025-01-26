(function () {

    var paginaAtual = 1;
    var totalPagina = 1;
    var tableBody = document.querySelector("#tabela-Index tbody");
    var setaEsquerda = document.getElementById("seta-esquerda");
    var setaDireita = document.getElementById("seta-direita");
    var tabelaIndexOs = document.getElementById("tabela-Index");
    Notiflix.Notify.init({});

    const notify = document.getElementById("notify");

    var btnPesquisarMateria = document.getElementById("btnPesquisarMateria");
    var inputPesquisarMateria = document.getElementById("inputPesquisarMateria");


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
    btnPesquisarMateria.addEventListener("click", function (evt) {
        Pesquisar();
    });

    inputPesquisarMateria.addEventListener("keypress", function (evt) {
        if (evt.key === 13) {

            Pesquisar();
        }
    });


    function Pesquisar() {
        var materia = inputPesquisarMateria.value;

        $.ajax({
            type: "GET",
            url: "/MateriaPrima/PesquisarMateriaPrimaIndex",
            data: { materia: materia, paginaAtual: paginaAtual },
            dataType: "json",
            success: function (json) {
                var list = "";
                

                if (json.length > 0) {

                    totalPagina = parseInt(json.TotalPagina);

                    json.forEach(function (element) {
                        var ativo = element.ativo == true ? "Em uso" : "Não usado";
                        var data = new Date(element.data);
                        var dataFormatada = data.toLocaleDateString('pt-BR', { timeZone: 'UTC' });

                        list += "<tr>" +
                            "<td>" + element.id + "</td>" +
                            "<td>" + element.nome + "</td>" +
                            "<td>" + element.loteInterno + "</td>" +
                            "<td>" + element.quantidadeAtual + "</td>" +
                            "<td>" + dataFormatada + "</td>" +
                            "<td>" + ativo + "</td>" +
                            "<td>" +
                            "<a href='/MateriaPrima/Detalhe?id=" + element.id + "'>" +
                            "<i class= 'bi bi-info-square-fill' style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Detalhe da matéria prima'></i>" +
                            "</a>" +
                            "<a href='/MateriaPrima/Editar?id=" + element.id + "'>" +
                            "<i class='bi bi-gear-wide'  style='font-size:23px;color:black' data-toggle='tooltip' data-placement='top' title='Alterar matéria prima'></i>" +
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
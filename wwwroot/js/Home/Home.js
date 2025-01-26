(function () {

    Notiflix.Notify.init({});
    const notify = document.getElementById("notify");

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "errorEtiquetas")
            Notiflix.Notify.failure(notify.innerText);
        if (notify.getAttribute("data-type") === "errorRelatorio")
            Notiflix.Notify.failure(notify.innerText);

    }
    var modalCliente = document.getElementById("RelatorioExtintorCliente");
    var modalBuscarExtintorCliente = new bootstrap.Modal(modalCliente);
    var botaoModalExtintorCliente = document.getElementById("botaoModalExtintorCliente");
    var inputCliente = document.getElementById("clienteBusca");
    var inputData = document.getElementById("Data");
    var botaoEnviarExtintorCliente = document.getElementById("btnRelatorioExtintorCliente");
    var idCliente = document.getElementById("IdCliente");


    var botaoEnviarExtintorSelo = document.getElementById("btnRelatorioExtintorSelo");
    var modalSelo = document.getElementById("RelatorioExtintorSelo");
    var modalBuscarExtintorSelo = new bootstrap.Modal(modalSelo);
    var botaoModalExtintorSelo = document.getElementById("botaoModalExtintiorSelo");
    var inputSelo = document.getElementById("Selo");

    var modalLote = document.getElementById("RelatorioExtintorLote");
    var modalBuscarextintiorLote = new bootstrap.Modal(modalLote);
    var botaoModalExtintiorLote = document.getElementById("botaoModalExtintorLote");
    var inputMateriaPrima = document.getElementById("TipoCarga");
    var inputLote = document.getElementById("Lote");
    var botaoEnviarExtintorLote = document.getElementById("btnRelatorioExtintorLote");

    var modalEtiqueta = document.getElementById("etiquetas");
    var modalImprimirEtiqueta = new bootstrap.Modal(modalEtiqueta);
    var botaoModalEtiquetas = document.getElementById("botaoModalEtiquetas");
    var inputNumeroOs = document.getElementById("numeroOs");
    var inputDataEtiqueta = document.getElementById("dataEtiqueta");
    var botaoEnviarEtiqueta = document.getElementById("btnEtiquetas");


    var modalRelExtintor = document.getElementById("RelatorioPorExtintor");
    var modalRelatorioExtintor = new bootstrap.Modal(modalRelExtintor);
    var botaoModalRelatorioExtintor = document.getElementById("botaoModalPorExtintor");
    var inputNumeroExtintiorRelatorio = document.getElementById("nuemroExtintorCilindo");
    var botaoEnviarRelatorioExtintor = document.getElementById("btnRelatorioExtintor");


    var modalEtiquetaIndividual = document.getElementById("etiquetasIndividual");
    var modalImprimirEtiquetaIndividual = new bootstrap.Modal(modalEtiquetaIndividual);
    var botaoModalEtiquetasIndividual = document.getElementById("botaoModalEtiquetasIndividual");
    var inputNumeroExtintior = document.getElementById("numeroExtintor");
    var botaoEnviarEtiquetaIndividual = document.getElementById("btnEtiquetasIndividual");
    var inputDataEtiquetaIdividual = document.getElementById("dataEtiquetaIndividual");

    var modalEtiquetaTemporaria = document.getElementById("etiquetasTemporaria");
    var modalImprimirEtiquetaTemporaria = new bootstrap.Modal(modalEtiquetaTemporaria);
    var botaoModalEtiquetaTemp = document.getElementById("btnEtiquetasTem");
    var botaoModalEtiquetasTemporariaModal = document.getElementById("botaoModalEtiquetasTemporaria");
    var inputMateriaTemporaria = document.getElementById("TipoCargaTempo");
    var inputMesTemporaria = document.getElementById("mes");
    var inputProximoAno = document.getElementById("anoProximaManu");
    var inputAnoManut3 = document.getElementById("dataEtiquetaTemporaria");
    var inputManutencao = document.getElementById("manutencao");
    var inputQtd = document.getElementById("qtd");


    $("#Lote").mask("00/00");


    botaoModalEtiquetasTemporariaModal.addEventListener("click", function () {
        modalImprimirEtiquetaTemporaria.show();
    });
    function limparModalRelatorioExtintor() {
        inputNumeroExtintiorRelatorio.value = "";

    }
    botaoModalRelatorioExtintor.addEventListener("click", function () {
        limparModalRelatorioExtintor();
        modalRelatorioExtintor.show();
    });
    function limparModalEtiquetaIndividual() {
        inputNumeroExtintior.value = "";

    }
    botaoModalEtiquetasIndividual.addEventListener("click", function () {
        limparModalEtiquetaIndividual();
        modalImprimirEtiquetaIndividual.show();
    });
    function limparModalEtiqueta() {
        inputNumeroOs.value = "";
        inputDataEtiqueta.value = "";
    }
    botaoModalEtiquetas.addEventListener("click", function () {
        limparModalEtiqueta();
        modalImprimirEtiqueta.show();
    });
    function limparModalExtintorLote() {
        inputLote.value = "";
        $("#Lote").mask("00/00");
    }
    botaoModalExtintiorLote.addEventListener("click", function () {
        limparModalExtintorLote();
        modalBuscarextintiorLote.show();
    });
    function limparModalExtintorSelo() {
        inputSelo.value = "";
    };

    botaoModalExtintorSelo.addEventListener("click", function () {
        limparModalExtintorSelo();
        modalBuscarExtintorSelo.show();

    });
    function limparModalExtintorCliente() {
        inputCliente.value = "";
        idCliente.value = 0;
        inputData.value = "";
    };

    botaoModalExtintorCliente.addEventListener("click", function () {

        limparModalExtintorCliente();
        modalBuscarExtintorCliente.show();
    });



    $("#clienteBusca").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Relatorio/BuscarExtintiorClienteAutoComplete",
                dataType: "json",
                data: {
                    nome: request.term
                },
                success: function (data) {
                    response(data);
                    console.log(data)
                }
            });
        },
        minLength: 4,
        autoFocus: true,
        response: function (event, ui) {
            if (ui.content.length < 1) {
                ui.content.push({
                    id: 0,
                    label: 'Nenhum cliente encontrado.',
                });
            }
        },
        select: async function (event, ui) {
            if (ui.item && ui.item.id != 0) {

                $.ajax({
                    url: "/os/BuscarCliente",
                    type: "GET",
                    data: {
                        idCliente: ui.item.id, nome: "", autocomplete: false
                    },
                    dataType: "json",
                    success: async function (json) {
                        $("#IdCliente").val(ui.item.id);
                        //console.log(json)
                        //window.location.href = "/relatorio/RelatorioExtintorCliente?idCliente=" + json.Id;
                    }
                });
            }
        }
    });

    botaoEnviarExtintorCliente.addEventListener("click", function () {
        window.location.href = "/relatorio/RelatorioExtintorCliente?idCliente=" + idCliente.value + "&data=" + inputData.value;
    });
    botaoEnviarExtintorSelo.addEventListener("click", function () {
        window.location.href = "/relatorio/RelatorioExtintorSelo?selo=" + inputSelo.value;
    });
    botaoEnviarExtintorLote.addEventListener("click", function () {
        window.location.href = "/relatorio/RelatorioExtintorLote?carga=" + inputMateriaPrima.value + "&lote=" + inputLote.value;

    });
    botaoEnviarEtiqueta.addEventListener("click", function () {
        window.location.href = "/etiquetas/GerarEtiquetas?numeroOs=" + inputNumeroOs.value + "&data=" + inputDataEtiqueta.value;
    });
    botaoEnviarEtiquetaIndividual.addEventListener("click", function () {
        window.location.href = "/etiquetas/GerarEtiquetasPorExtintor?numeroExtintor=" + inputNumeroExtintior.value + "&data=" + inputDataEtiquetaIdividual.value;
    });
    botaoEnviarRelatorioExtintor.addEventListener("click", function () {
        window.location.href = "/relatorio/RelatorioExtintor?numeroExtintor=" + inputNumeroExtintiorRelatorio.value;
    });
    botaoModalEtiquetaTemp.addEventListener("click", function () {
        var url = "/etiquetas/GerarEtiquetasTemporaria?NomeMateria=" + inputMateriaTemporaria.value + "&mes=" + inputMesTemporaria.value + "&anoProxima=" + inputProximoAno.value + "&ano=" + inputAnoManut3.value + "&manutencao=" + inputManutencao.value + "&qtd=" + inputQtd.value;
        window.open(url, '_blank')
        window.focus();

    });
})();
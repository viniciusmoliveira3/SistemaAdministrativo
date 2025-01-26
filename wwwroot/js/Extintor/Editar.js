(function () {
    var botaoCadastrar = document.getElementById("botaoCadastrar");
    var botaoEditar = document.getElementById("botaoEditar");
    var numeroCilindro = document.getElementById("NumeroCilindro");
    var anoFabricacao = document.getElementById("AnoFabricacao");
    var marcaExtintor = document.getElementById("IdMarcaExtintor");
    var nbr = document.getElementById("NBR");
    var tipoCarga = document.getElementById("IdTipoCarga");
    var capacidade = document.getElementById("IdCapacidade");
    const notify = document.getElementById("notify");
    var ensaioHidrosticos = document.getElementById("EnsaioHidrostatico");
    var proximoEnsaio = document.getElementById("ProximoEnsaioHisdrostatico");

    Notiflix.Notify.init({});


    //Mask
    $("#Lote").mask("00/00");
    $("#CapacExtintora").mask("00:AA");

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }
  
    botaoEditar.addEventListener("click", function (evt) {

        if (numeroCilindro.value == "") {
            Notiflix.Notify.failure('Digite o número do cilindro');
            evt.preventDefault()
        }

        if (anoFabricacao.value == "") {
            Notiflix.Notify.failure('Digite a data de fabricação');
            evt.preventDefault()
        }

        if (marcaExtintor.value == 0) {
            Notiflix.Notify.failure('Selecione uma marca de extintor');
            evt.preventDefault()
        }
        if (nbr.value == "") {
            Notiflix.Notify.failure('Digite a NBR');
            evt.preventDefault()
        }
        if (tipoCarga.value == 0) {
            Notiflix.Notify.failure('Selecione um tipo de carga');
            evt.preventDefault()
        }
        if (capacidade.value == 0) {
            Notiflix.Notify.failure('Selecione uma capacidade');
            evt.preventDefault()
        }
    });

    ensaioHidrosticos.addEventListener("blur", function () {
        var anoAdiciionado5anos = parseInt(ensaioHidrosticos.value);
        anoAdiciionado5anos += 5;
        proximoEnsaio.value = anoAdiciionado5anos;
    });
})();
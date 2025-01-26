(function () {
    var botaoCadastrar = document.getElementById("botaoCadastrar");
    var botaoEditar = document.getElementById("botaoEditar");
    var nome = document.getElementById("Nome");
    var quantidade = document.getElementById("Quantidade");
    const notify = document.getElementById("notify");
    var botaoAtivo = document.getElementById("Ativo");
    
    Notiflix.Notify.init({});

    $("#LoteInterno").mask("00/00")

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }
   
    botaoCadastrar.addEventListener("click", function (evt) {

        if (nome.value == "") {
            Notiflix.Notify.failure('Digite o nome da matéria prima');
            evt.preventDefault()
        }
        if (quantidade.value == "") {
            Notiflix.Notify.failure('Digite a quantidade');
            evt.preventDefault()
        }

    });
    botaoAtivo.addEventListener("click", function () {
        var materiaPrima = document.getElementById("Nome").value;
        $.ajax({
            type: "GET",
            url: "/materiaprima/VerficarLoteAtivoExistente",
            data: { materiaPrima: materiaPrima },
            dataType: "json",
            success: function (json) {
                if (json.length >= 1) {
                    json.forEach(function (evt) {
                        if (evt.ativo == true) {
                            Notiflix.Notify.failure(`Já existe um lote dessa materia prima ativa: Lote:${evt.lote}, data: ${evt.data}`);
                        }
                    });
                }

            },
            error: function (msg) {
                Notiflix.Notify.warning('Não existe nenhuma materia prima com esse lote em uso!');
            }
        });

    });
})();
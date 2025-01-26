(function () {

    const notify = document.getElementById("notify");
    var botaoCadastrar = document.getElementById("botaoCadastrar");
    var botaoEditar = document.getElementById("btnEditar");
    var nome = document.getElementById("Nome");
    var data = document.getElementById("Data");
    var quantidade = document.getElementById("Quantidade");
    var botaoAtivo = document.getElementById("Ativo");


    $("#Lote").mask("00/00");


    Notiflix.Notify.init({});

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }

    botaoEditar.addEventListener("click", function (evt) {

        if (nome.value == "") {
            Notiflix.Notify.failure('Digite o nome do componente');
            evt.preventDefault()
        }

        if (data.value == "") {
            Notiflix.Notify.failure('Digite a data');
            evt.preventDefault()
        }

        if (quantidade.value == "") {
            Notiflix.Notify.failure('Digite a quantidade');
            evt.preventDefault()
        }
    });
    botaoAtivo.addEventListener("change", function () {
        var componente = document.getElementById("Nome").value;

        $.ajax({
            type: "GET",
            url: "/Componente/VerficarLoteAtivoExistente",
            data: { componente: componente },
            dataType: "json",
            success: function (json) {
                if (json.length >= 1) {
                    json.forEach(function (evt) {
                        if (evt.ativo == true) {
                            Notiflix.Notify.failure(`Já existe um lote desse compononente ativo: Lote:${evt.lote}, data: ${evt.data}`);
                        }
                    });
                }

            },
            error: function (msg) {
                Notiflix.Notify.warning('Não existe nenhum componente com esse lote em uso!');
            }
        });

    });


})();
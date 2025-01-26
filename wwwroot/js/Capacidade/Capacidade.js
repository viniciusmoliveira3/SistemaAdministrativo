(function () {

    const notify = document.getElementById("notify");
    var botaoCadastrar = document.getElementById("btnCadastrar");
    var botaoEditar = document.getElementById("btn-editar");
    var capacidadeCarga = document.getElementById("CapacidadeCarga");

    Notiflix.Notify.init({});

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }

    botaoCadastrar.addEventListener("click", function (evt) {

        if (capacidadeCarga.value == "") {
            Notiflix.Notify.failure('Digite a capacidade');
            evt.preventDefault()
        }

    });
    botaoEditar.addEventListener("click", function (evt) {

        if (capacidadeCarga.value == "") {
            Notiflix.Notify.failure('Digite a capacidade');
            evt.preventDefault()
        }

    });

})();
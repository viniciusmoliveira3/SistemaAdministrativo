(function () {
    var botaoCadastrar = document.getElementById("botaoCadastrar");
    var botaoEditar = document.getElementById("botaoEditar");
    var marca = document.getElementById("Nome");
    const notify = document.getElementById("notify");
 

    Notiflix.Notify.init({});

    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }

    botaoEditar.addEventListener("click", function (evt) {

        if (marca.value == "") {
            Notiflix.Notify.failure('Digite o nome da marca de extintor');
            evt.preventDefault()
        }
    });

    botaoCadastrar.addEventListener("click", function (evt) {

        if (marca.value == "") {
            Notiflix.Notify.failure('Digite o nome da marca de extintor');
            evt.preventDefault()
        }

    });


})();
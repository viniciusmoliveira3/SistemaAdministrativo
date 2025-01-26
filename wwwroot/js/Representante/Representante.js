(function () {
    var botaoCadastrar = document.getElementById("botaoCadastrar");
    var botaoEditar = document.getElementById("botaoEditar");
    var nome = document.getElementById("NomeFantasia");
    var telefone = document.getElementById("Telefone");
    const notify = document.getElementById("notify");
  
    const cep = document.getElementById("CEP");

    Notiflix.Notify.init({});

    cep.addEventListener('blur', e => {
        const value = cep.value.replace(/[^0-9]+/, '');
        const url = `https://viacep.com.br/ws/${value}/json/`;


        fetch(url)
            .then(response => response.json())
            .then(json => {

                if (json.logradouro) {

                    document.getElementById('EnderecoSocial').value = json.logradouro;
                    document.getElementById('Bairro').value = json.bairro
                    document.getElementById('Cidade').value = json.localidade;
                    document.getElementById('Uf').value = json.uf;
                }

            });



    });


    if (notify != undefined) {
        if (notify.getAttribute("data-type") === "success")
            Notiflix.Notify.success(notify.innerText);
        if (notify.getAttribute("data-type") === "error")
            Notiflix.Notify.failure(notify.innerText);
    }



    //Mask

    $("#CEP").mask("00000-000");
    $("#CNPJ").mask("00.000.000/0000-00");
    $("#InscricaoEstadual").mask("000.000.000.000");
    $("#Telefone").mask("(00)90000-0000");


    //Retirando a mascara

    $("button").click(function () {
        $("#CEP").unmask();
        $("#CNPJ").unmask();
        $("#InscricaoEstadual").unmask();
        $("#Telefone").unmask();
    });

    botaoEditar.addEventListener("click", function (evt) {

        if (nome.value == "") {
            Notiflix.Notify.failure('Digiteo nome do representante');
            evt.preventDefault()
        }
        if (telefone.value == "") {
            Notiflix.Notify.failure('Digite o telefone do representante');
            evt.preventDefault()
        }
        
    });

    botaoCadastrar.addEventListener("click", function (evt) {

        if (nome.value == "") {
            Notiflix.Notify.failure('Digite o nome do representante');
            evt.preventDefault()
        }
        if (telefone.value == "") {
            Notiflix.Notify.failure('Digite o telefone do representante');
            evt.preventDefault()
        }

    });


})();






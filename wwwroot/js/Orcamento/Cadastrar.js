(function () {
    Notiflix.Notify.init({});
    const notify = document.getElementById("notify");
    //replace(".", "").replace(",", "."));
    //$("#ValorTercerizado").val(valorTercerizado.toLocaleString("pt-br", { minimumFractionDigits: 2 }));
    var valorTotalUniversal = 0;
    var valorUnitariolUniversal = 0;
    var valorTotalFinal = 0;
    var materiaPrimaNome = "";

    var btnEnviarProduto = document.getElementById("btnEnviarProduto");

    var botaoModal = document.getElementById("novaOrdem-btn");
    var modal = document.getElementById("modalProduto");
    var modalProduto = new bootstrap.Modal(modal);

    var nomeProduto = document.getElementById("Nome");
    var idMateriaPrima = document.getElementById("IdMateriaPrima"); 
    var quantidade = document.getElementById("Quantidade");
    var valorUnitario = document.getElementById("ValorUnitario");
    var valorTotal = document.getElementById("ValorTotal");

    var tabela = document.querySelector("#tabela-produto tbody");
    //formatMoney()
    var botaoAddProduto = document.getElementById("btAddProduto");

    var botaoGerarOrcamento = document.getElementById("btnEnviarProduto");
    var cliente = document.getElementById("Cliente");
    var vendedor = document.getElementById("Vendedor");
    var formulario = document.getElementById("formulario");

    //Mask
    $("#Cep").mask("00000-000");
    $("#Cnpj").mask("00.000.000/0000-00");
    $("#Cpf").mask("000.000.000-00");
    $("#ValorUnitario").mask('000.000,00', { reverse: true });
    $("#ValorTotal").mask('000.000,00', { reverse: true });


    var cep = document.getElementById("Cep");
    cep.addEventListener('blur', e => {
        const value = cep.value.replace(/[^0-9]+/, '');
        const url = `https://viacep.com.br/ws/${value}/json/`;

        fetch(url)
            .then(response => response.json())
            .then(json => {

                if (json.logradouro) {

                    document.getElementById('Endereco').value = json.logradouro;
                    document.getElementById('Bairro').value = json.bairro
                    document.getElementById('Cidade').value = json.localidade;
                    document.getElementById('Estado').value = json.uf;
                }
                else {
                    Notiflix.Notify.failure('CEP não encontrado');
                }

            });
    });

    botaoModal.addEventListener("click", function () {
        limparModalAddProduto();
        modalProduto.show();
    });
    function limparModalAddProduto() {
        nomeProduto.value = "";
        idMateriaPrima.value = 0;
        quantidade.value = "";
        valorUnitario.value = "";
        valorTotal.value = "";
        materiaPrimaNome = "";
    }

    idMateriaPrima.addEventListener("change", function () {
        var index = this.selectedIndex;
        if (index != -1)
            materiaPrimaNome = this.options[index].innerText;
        else
            materiaPrimaNome = "";
    });
    function indexarLinhasProduto() {
        var linhas = document.querySelectorAll("#tabela-produto > tbody > tr");
        var linhasInput1 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(1)");
        var linhasInput2 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(2)");
        var linhasInput3 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(3)");
        var linhasInput4 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(4)");
        var linhasInput5 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(5)");

        var count = 0;

        linhas.forEach(function (element) {
            element.setAttribute("data-index", count)

            count++;
        });
        count = 0;

        linhasInput1.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].IdMateriaPrima");

            count++;
        });
        count = 0;
        linhasInput2.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].Nome");

            count++;
        });
        count = 0;
        linhasInput3.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].Quantidade");

            count++;
        });
        count = 0;
        linhasInput4.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].ValorUnitario");

            count++;
        });
        count = 0;
        linhasInput5.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].ValorTotal");

            count++;
        });
        count = 0;
    }

    valorUnitario.addEventListener("change", function () {
        valorUnitariolUniversal = parseFloat(this.value.replaceAll(".", "").replace(",", ".")) > 0 ? parseFloat(this.value.replaceAll(".", "").replace(",", ".")) : 0 ;
        var quantidadeTotal = parseInt(quantidade.value) > 0 ? parseInt(quantidade.value) : 0;


        if (isNaN(quantidadeTotal))
            quantidadeTotal = 0;
        if (isNaN(valorUnitariolUniversal))
            valorUnitariolUniversal = 0;

        valorTotalUniversal = valorUnitariolUniversal * quantidadeTotal;
        valorTotalUniversal = formatMoney(valorTotalUniversal);
        valorTotal.value = formatMoney(valorTotalUniversal);
        valorUnitariolUniversal = formatMoney(valorUnitariolUniversal);

    });


    botaoAddProduto.addEventListener("click", function () {
        var list = tabela.innerHTML;
        if (nomeProduto.value != "" && quantidade.value != "" && valorUnitario != "" && valorTotal.value != "") {
            var valorFormatadoUnitario = parseFloat(valorUnitariolUniversal.replaceAll('.', '').replace(',', '.'));
            var valorFormatadoTotal = parseFloat(valorTotalUniversal.replaceAll('.', '').replace(',', '.'));
            list += "<tr>" + 
                "<td>" + nomeProduto.value + "</td>" +
                "<td>" + materiaPrimaNome + 
                "<input type='hidden' name='OrcamentoProduto[].IdMateriaPrima' value='" + idMateriaPrima.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].Nome' value='" + nomeProduto.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].Quantidade' value='" + quantidade.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorUnitario' value='" + valorFormatadoUnitario + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorTotal' value='" + valorFormatadoTotal + "'/>" +
                "</td>" +
                "<td>" + quantidade.value + "</td>" +
                "<td>" + valorUnitariolUniversal + "</td>" +
                "<td>" + formatMoney(valorTotal.value) + "</td>" +
                 "</tr>"

            tabela.innerHTML = list;
            Notiflix.Notify.success('Produto adicionado com sucesso!!!');
            limparModalAddProduto();
            indexarLinhasProduto()
        }
        else {
            Notiflix.Notify.failure('Preencha os campos corretamente!');
        }
    });

    botaoGerarOrcamento.addEventListener("click", function (evt) {
        if (cliente.value != "" && vendedor.value != "") {
            $("#Cnpj").unmask();
            formulario.submit()

        }
        else {
            Notiflix.Notify.failure('Preencha os campos obrigatorios!');
            evt.preventDefault();
        }
        $("#Cnpj").mask("00.000.000/0000-00");

    });
    function formatMoney(amount) {
        try {
            var decimal = "00";
            var aux = "";

            amount = amount.toLocaleString("pt-BR");
            aux = amount.split(",");
            decimal = aux[1] === undefined ? "0" : aux[1];
            decimal = decimal.length === 1 ? decimal + "0" : decimal;
            amount = aux[0] + "," + decimal;

            return amount;
            //decimalCount = Math.abs(decimalCount);
            //decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

            //const negativeSign = amount < 0 ? "-" : "";

            //let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
            //let j = (i.length > 3) ? i.length % 3 : 0;

            //return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
        } catch (e) {
            console.log(e);
        }
    }


})();
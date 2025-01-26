(function () {
    Notiflix.Notify.init({});
    const notify = document.getElementById("notify");
    //replace(".", "").replace(",", "."));
    //$("#ValorTercerizado").val(valorTercerizado.toLocaleString("pt-br", { minimumFractionDigits: 2 }));
    var valorTotalUniversal = 0;
    var valorUnitariolUniversal = 0;
    var valorTotalFinal = 0;
    var materiaPrimaNome = "";
    var materiaPrimaNomeNovo = "";

    var linhaGlobal = 0;

    var botaoAlterarOrcamento = document.getElementById("btnEnviarProduto");

    var botaoModal = document.getElementById("novaOrdem-btn");
    var modal = document.getElementById("modalProduto");
    var modalProduto = new bootstrap.Modal(modal);

    var modalAlterar = document.getElementById("modalProdutoAlterar");
    var modalProdutoAlterar = new bootstrap.Modal(modalAlterar);
    var botaoApagarProduto = document.getElementById("btnApagarProduto");

    var modalExcluir = document.getElementById("ModalExclusaoProduto");
    var modalExcluirProduto = new bootstrap.Modal(modalExcluir);

    var nomeProduto = document.getElementById("NomeEditar");
    var idMateriaPrima = document.getElementById("IdMateriaPrimaEditar");
    var quantidade = document.getElementById("QuantidadeEditar");
    var valorUnitario = document.getElementById("ValorUnitarioEditar");
    var valorTotal = document.getElementById("ValorTotalEditar");


    var nomeProdutoNovo = document.getElementById("Nome");
    var idMateriaPrimaNovo = document.getElementById("IdMateriaPrima");
    var quantidadeNovo = document.getElementById("Quantidade");
    var valorUnitarioNovo = document.getElementById("ValorUnitario");
    var valorTotalNovo = document.getElementById("ValorTotal");

    var tabela = document.querySelector("#tabela-produto tbody");
    var botaoAddProduto = document.getElementById("btAddProduto");

    var botaoGerarOrcamento = document.getElementById("btnEnviarProduto");
    var cliente = document.getElementById("Cliente");
    var vendedor = document.getElementById("Vendedor");
    var formulario = document.getElementById("formulario");

    var btnEditarOrcamentoProduto = document.getElementById("btnAlterarProduto");

    //Mask
    $("#Cep").mask("00000-000");
    $("#Cnpj").mask("00.000.000/0000-00");
    $("#Cpf").mask("000.000.000-00");
    $("#ValorUnitarioEditar").mask('000.000,00', { reverse: true });
    $("#ValorTotalEditar").mask('000.000,00', { reverse: true });
    $("#ValorUnitario").mask('000.000,00', { reverse: true });
    $("#ValorTotal").mask('000.000,00', { reverse: true });

    //Funcoes gerais
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
    tabela.addEventListener("click", function (evt) {

        if (evt.target.tagName == "I") {
            var linha = evt.target.closest("tr");
            var index = linha.getAttribute("data-index");
            if (isNaN(index))
                index = null;

            linhaGlobal = parseInt(index);
        }

    });
    tabela.addEventListener("click", function (evt) {

        if (evt.target.tagName == "I") {
            if (evt.target.classList.contains("bi-arrow-counterclockwise")) {
                limparModalAlterar()
                var linha = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "'] > td .bi-trash-fill");
                var json = linha.getAttribute("data-json");
                json = JSON.parse(json);

                nomeProduto.value = json.Nome;
                idMateriaPrima.value = json.IdMateriaPrima;
                quantidade.value = json.Quantidade;
                valorUnitario.value = json.ValorUnitario;
                valorTotal.value = json.ValorTotal;


                modalProdutoAlterar.show()
            }
            if (evt.target.classList.contains("bi-trash-fill"))
            {
                modalExcluirProduto.show();
            }
        }

    });

    botaoApagarProduto.addEventListener("click", function () {

        var linha = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "'] > td .bi-trash-fill");
        var json = linha.getAttribute("data-json");
        json = JSON.parse(json);

        if (linhaGlobal != null && linhaGlobal >= 0) {
            if (json.IdOrcamentoProduto == 0) {
                var linhaRemovida = tabela.querySelector("tbody > tr[data-index='" + linhaGlobal + "']");
                if (linhaRemovida) {
                    linhaRemovida.parentNode.removeChild(linhaRemovida);
                    indexarLinhasProduto();
                    modalExcluirProduto.hide();

                    Notiflix.Notify.success('Produto deletado com sucesso!!!');
                }
            }
            else {

                var idOrcamentoProduto = json.IdOrcamentoProduto;

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { idOrcamentoProduto: idOrcamentoProduto },
                    url: "/orcamento/deletarorcamentoproduto",
                    success: function (json) {
                        if (json.sucesso == true) {
                            var linhaRemovida = tabela.querySelector("tbody > tr[data-index='" + linhaGlobal + "']");

                            if (linhaRemovida) {
                                linhaRemovida.parentNode.removeChild(linhaRemovida);
                                indexarLinhasProduto();
                                modalExcluirProduto.hide();

                                Notiflix.Notify.success('Produto deletado com sucesso!!!');

                            }
                        }
                     
                    },
                    error: function () {
                        Notiflix.Notify.failure('Erro ao deletar!!!');

                    }

                });

            }
          
        }
    });
    btnEditarOrcamentoProduto.addEventListener("click", function () {

        alterarProdutoOrcamento();
    });
    function indexarLinhasProduto() {
        var linhas = document.querySelectorAll("#tabela-produto > tbody > tr");
        var linhasInput1 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(1)");
        var linhasInput2 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(2)");
        var linhasInput3 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(3)");
        var linhasInput4 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(4)");
        var linhasInput5 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(5)");
        var linhasInput6 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(6)");
        var linhasInput7 = document.querySelectorAll("#tabela-produto > tbody > tr > td:nth-child(2) > input:nth-child(7)");

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
            element.setAttribute("name", "OrcamentoProduto[" + count + "].IdOrcamentoProduto");

            count++;
        });
        count = 0;
        linhasInput3.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].Nome");

            count++;
        });
        count = 0;
        linhasInput4.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].Quantidade");

            count++;
        });
        count = 0;
        linhasInput5.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].ValorUnitario");

            count++;
        });
        count = 0;
        linhasInput6.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].ValorTotal");

            count++;
        });
        count = 0;
        linhasInput7.forEach(function (element) {
            element.setAttribute("name", "OrcamentoProduto[" + count + "].IdOrcamento");

            count++;
        });
        count = 0;
    }
  
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
    // Alterar produto
    function alterarProdutoOrcamento() {
        var nomeAlterar = nomeProduto.value;
        var idMateriaPrimaAlterar = idMateriaPrima.value;
        var quantidadeAlterar = quantidade.value;
        var valorUnitarioAlterar = valorUnitariolUniversal;
        var valorTotalAlterar = valorTotal.value;
        var list = "";
        var linha = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "'] > td > .bi-trash-fill");
        var linhaAlterar = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "']");
        var json = linha.getAttribute("data-json");
        json = JSON.parse(json);
        var indexMateriaPrima = 0;
        indexMateriaPrima = idMateriaPrima.selectedIndex;
        materiaPrimaNome = idMateriaPrima.options[indexMateriaPrima].innerText;

        idMateriaPrimaAlterar = isNaN(parseInt(idMateriaPrimaAlterar)) ? 0 : parseInt(idMateriaPrimaAlterar);
        quantidadeAlterar = isNaN(parseInt(quantidadeAlterar)) ? 0 : parseInt(quantidadeAlterar);

        json.Nome = nomeAlterar;
        if (json.IdMateriaPrima != undefined)
            json.IdMateriaPrima = idMateriaPrimaAlterar;
        json.Quantidade = quantidadeAlterar;
        json.ValorUnitario = valorUnitarioAlterar;
        json.ValorTotal = valorTotalUniversal;

        if (nomeAlterar != "" && quantidadeAlterar > 0 && valorUnitarioAlterar != "" && valorTotalAlterar != "") {
            var valorFormatadoUnitario = parseFloat(valorUnitariolUniversal.replaceAll('.', '').replace(',', '.'));
            var valorFormatadoTotal = parseFloat(valorTotalUniversal.replaceAll('.', '').replace(',', '.'));
            list += "<td>" + nomeProduto.value + "</td>" +
                "<td>" + materiaPrimaNome +
                "<input type='hidden' name='OrcamentoProduto[].IdMateriaPrima' value='" + idMateriaPrimaAlterar + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].IdOrcamentoProduto' value='" + json.IdOrcamentoProduto + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].Nome' value='" + nomeAlterar + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].Quantidade' value='" + quantidadeAlterar + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorUnitario' value='" + valorFormatadoUnitario + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorTotal' value='" + valorFormatadoTotal + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].IdOrcamento' value='" + json.IdOrcamento + "'/>" +
                "</td>" +
                "<td>" + quantidadeAlterar + "</td>" +
                "<td>" + valorUnitariolUniversal + "</td>" +
                "<td>" + valorTotalUniversal + "</td>" +
                "<td>" +
                "<i class='bi bi-arrow-counterclockwise' style='font-size: 25px; color:black'></i>" +
                "<i class='bi bi-trash-fill' data-json='" + JSON.stringify(json) + "' style='font-size:25px; color:black'></i>" +
                "</td>";

            linhaAlterar.innerHTML = list;
            Notiflix.Notify.success('Produto alterado com sucesso!!!');
            indexarLinhasProduto()
            limparModalAlterar();
            modalProdutoAlterar.hide();
        }
        else {
            Notiflix.Notify.failure('Preencha os campos corretamente!');

        }

    }
    valorUnitario.addEventListener("change", function () {
        valorUnitariolUniversal = parseFloat(this.value.replaceAll(".", "").replace(",", ".")) > 0 ? parseFloat(this.value.replaceAll(".", "").replace(",", ".")) : 0;
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
    function limparModalAlterar() {
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

    //Adicioanr produto
    botaoModal.addEventListener("click", function () {
        limparModalAddProduto();
        modalProduto.show();
    });

    function limparModalAddProduto() {
        nomeProdutoNovo.value = "";
        idMateriaPrimaNovo.value = 0;
        quantidadeNovo.value = "";
        valorUnitarioNovo.value = "";
        valorTotalNovo.value = "";
        materiaPrimaNome = "";
    }
    idMateriaPrimaNovo.addEventListener("change", function () {
        var index = this.selectedIndex;
        if (index != -1)
            materiaPrimaNome = this.options[index].innerText;
        else
            materiaPrimaNome = "";
    });
    valorUnitarioNovo.addEventListener("change", function () {
        valorUnitariolUniversal = parseFloat(this.value.replaceAll(".", "").replace(",", ".")) > 0 ? parseFloat(this.value.replaceAll(".", "").replace(",", ".")) : 0;
        var quantidadeTotal = parseInt(quantidadeNovo.value) > 0 ? parseInt(quantidadeNovo.value) : 0;


        if (isNaN(quantidadeTotal))
            quantidadeTotal = 0;
        if (isNaN(valorUnitariolUniversal))
            valorUnitariolUniversal = 0;

        valorTotalUniversal = valorUnitariolUniversal * quantidadeTotal;
        valorTotalUniversal = formatMoney(valorTotalUniversal);
        valorTotalNovo.value = formatMoney(valorTotalUniversal);
        valorUnitariolUniversal = formatMoney(valorUnitariolUniversal);

    });

    botaoAddProduto.addEventListener("click", function () {
        var nome = nomeProdutoNovo.value;
        var idMateriaPrima = idMateriaPrimaNovo.value;
        var quantidade = quantidadeNovo.value;
        var valorUnitario = valorUnitarioNovo;
        var valorTotal = valorTotalNovo.value;
        var list = "";
        var linha = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "'] > td > .bi-trash-fill");
        var linhaNova = document.querySelector("#tabela-produto > tbody > tr[data-index='" + linhaGlobal + "']");
        //var json = new Object.JSON;
        //json = JSON.parse(json);
        var indexMateriaPrima = 0;
        indexMateriaPrima = idMateriaPrimaNovo.selectedIndex;
        materiaPrimaNome = idMateriaPrimaNovo.options[indexMateriaPrima].innerText == "Matéria-prima" ? "" : idMateriaPrimaNovo.options[indexMateriaPrima].innerText;

        idMateriaPrima = isNaN(parseInt(idMateriaPrima)) ? 0 : parseInt(idMateriaPrima);
        quantidade = isNaN(parseInt(quantidade)) ? 0 : parseInt(quantidade);

        var obj = {};
        obj = {
            "IdOrcamentoProduto": 0,
            "IdOrcamento": 0,
            "Nome": nome,
            "IdMateriaPrima": idMateriaPrima,
            "Quantidade": quantidade,
            "ValorUnitario": valorUnitario,
            "ValorTotal": valorTotal
        }
        //json.Nome = nome;
        //if (json.IdMateriaPrima != undefined)
        //    json.IdMateriaPrima = idMateriaPrima;
        //json.Quantidade = quantidade;
        //json.ValorUnitario = valorUnitario;
        //json.ValorTotal = valorTotal;

        var list = tabela.innerHTML;
        if (nomeProdutoNovo.value != "" && quantidadeNovo.value != "" && valorUnitarioNovo.value != "" && valorTotalNovo.value != "") {
            var valorFormatadoUnitario = parseFloat(valorUnitariolUniversal.replaceAll('.', '').replace(',', '.'));
            var valorFormatadoTotal = parseFloat(valorTotalUniversal.replaceAll('.', '').replace(',', '.'));
            list += "<tr>" +
                "<td>" + nomeProdutoNovo.value + "</td>" +
                "<td>" + materiaPrimaNome +
                "<input type='hidden' name='OrcamentoProduto[].IdMateriaPrima' value='" + idMateriaPrimaNovo.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].IdOrcamentoProduto' value=''/>" +
                "<input type='hidden' name='OrcamentoProduto[].Nome' value='" + nomeProdutoNovo.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].Quantidade' value='" + quantidadeNovo.value + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorUnitario' value='" + valorFormatadoUnitario + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].ValorTotal' value='" + valorFormatadoTotal + "'/>" +
                "<input type='hidden' name='OrcamentoProduto[].IdOrcamento' value=''/>" +
                "</td>" +
                "<td>" + quantidadeNovo.value + "</td>" +
                "<td>" + valorUnitariolUniversal + "</td>" +
                "<td>" + formatMoney(valorTotalNovo.value) + "</td>" +
                "<td>" +
                "<i class='bi bi-arrow-counterclockwise' style='font-size: 25px; color:black'></i>" +
                "<i class='bi bi-trash-fill' data-json='" + JSON.stringify(obj) + "' style='font-size:25px; color:black'></i>" +
                "</td>" +
                "</tr>"

            tabela.innerHTML = list;
            Notiflix.Notify.success('Produto adicionado com sucesso!!!');
            limparModalAddProduto();
            indexarLinhasProduto()
            modalProduto.hide();
        }
        else {
            Notiflix.Notify.failure('Preencha os campos corretamente!');
        }
    });
    botaoAlterarOrcamento.addEventListener("click",function () {
        $("#Cep").unmask();
    });
})();
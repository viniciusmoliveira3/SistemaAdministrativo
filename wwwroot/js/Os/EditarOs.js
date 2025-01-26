
(function () {
    var relItensGlobal = new Object();
    var extintorGlobal = new Object();

    var proximaRecargaAno = document.getElementById("DataProximaRecargaHidden").value;

    //Mask
    $("#Lote").mask("00/00");
    $("#CadastrarLote").mask("00/00");
    $("#AlterarLote").mask("00/00");
    $("#CapacExtintora").mask("00:AA AA");
    $("#CadastrarCapacExtintora").mask("00:AA AA");
    $("#AlterarCapacExtintora").mask("00:AA AA");


    //if (notify != undefined) {
    //    if (notify.getAttribute("data-type") === "bc")
    //        Notiflix.Notify.warning(notify.innerText);
    //    if (notify.getAttribute("data-type") === "abc")
    //        Notiflix.Notify.warning(notify.innerText);
    //    if (notify.getAttribute("data-type") === "co2")
    //        Notiflix.Notify.warning(notify.innerText);
    //    if (notify.getAttribute("data-type") === "espmec")
    //        Notiflix.Notify.warning(notify.innerText);
    //    if (notify.getAttribute("data-type") === "success")
    //        Notiflix.Notify.success(notify.innerText);
    //    if (notify.getAttribute("data-type") === "error")
    //        Notiflix.Notify.failure(notify.innerText);
    //}

    var linhaGeral = null;
    var tabela = document.getElementById("tabela-relatorio");

    var varModalItens = document.getElementById("modaAlterarRelItens");
    var modalAlterarItens = new bootstrap.Modal(varModalItens);

    var varModalApagarExtintor = document.getElementById("ModalExclusaoExtintor");
    var modalApagarExtintor = new bootstrap.Modal(varModalApagarExtintor);
    var botaoApagarExtintor = document.getElementById("btnApagarExtintor");
    var idRelItemApagado = 0;
    var inputExclusao = document.getElementById("itensExclusao");


    var varModalAlterarExtintor = document.getElementById("modalAlterarExtintor");
    var modalAlterarExtintor = new bootstrap.Modal(varModalAlterarExtintor);

    var botaoAlterarRelItens = document.getElementById("btn-alterar-relatorioitens");
    var botaoCadastrarExtintor = document.getElementById("btnCadastrarExtintor");
    var botaoSalvarCadastro = document.getElementById("btnSalvarExtintor");

    var iconeAlterarExtintor = document.getElementById("botaoAlterarExtintor");
    var botaoConfirmarAlteracao = document.getElementById("btnAlterarExtintor");
    var modalAlterarExtintor = document.getElementById("modalAlterarExtintor");
    var modalAlterar = new bootstrap.Modal(modalAlterarExtintor);

    var modalNovoExtintor = document.getElementById("modalCadastrarExtintor");
    var modalCadastrarExtintor = new bootstrap.Modal(modalNovoExtintor);

    var clienteNomeFantasia = document.getElementById("Cliente_NomeFantasia");
    var clienteEnderecoSocial = document.getElementById("Cliente_EnderecoSocial");
    var clienteBairro = document.getElementById("Cliente_Bairro");
    var clienteCidade = document.getElementById("Cliente_Cidade");
    var clienteId = document.getElementById("Cliente_Id");

    var numeroCilindro = document.getElementById("NumeroCilindro");
    var anoFabricacao = document.getElementById("AnoFabricacao");
    var ensaioHidrostatico = document.getElementById("EnsaioHidrostatico");
    var idMarcaExtintor = document.getElementById("IdMarcaExtintor");
    var idMateriaPrima = document.getElementById("IdMateriaPrima");
    var idCapacidade = document.getElementById("IdCapacidade");
    var nbr = document.getElementById("NBR");
    var proximoEnsaio = document.getElementById("ProximoEnsaioHisdrostatico");
    var dataProximaRecarga = document.getElementById("DataProximaRecarga");
    var lote = document.getElementById("Lote");
    var seloAnterior = document.getElementById("SeloAnterior");
    var nivelManutencao = document.getElementById("NivelManutencao");
    var ensaioIndPre = document.getElementById("EnsaioIndPre");
    var ensaioVazVal = document.getElementById("EnsaioVazVal");
    var visualIntacto = document.getElementById("VisualIntacto");
    var inspRosca = document.getElementById("InspRosca");
    var pintura = document.getElementById("Pintura");
    var idComponente1 = document.getElementById("IdComponente1");
    var idComponente2 = document.getElementById("IdComponente2");
    var idComponente3 = document.getElementById("IdComponente3");
    var idComponente4 = document.getElementById("IdComponente4");
    var pesoCilVazio = document.getElementById("PesoCilindroVazio");
    var pesoAgua = document.getElementById("PesoComAgua");
    var volumeLitros = document.getElementById("VolumeLitros");
    var capacidadeMaxima = document.getElementById("CapacidadeMaxima");
    var pressaoTrabalho = document.getElementById("PressaoTrabalho");
    var pressaoTesteCil = document.getElementById("PressaoTesteCilindro");
    var pressaoTesteMang = document.getElementById("PressaoTesteMangueira");
    var pressaoTesteMano = document.getElementById("PressaoTesteManometro");
    var pressaoTesteVal = document.getElementById("PressaoTesteValvula");
    var defInst = document.getElementById("DefInstantanea");
    var defPerma = document.getElementById("DefPermanente");
    var porcEpEt = document.getElementById("PorcEpEt");
    var taraGravada = document.getElementById("TaraGravada");
    var taraReal = document.getElementById("TaraReal");
    var perdaMassa = document.getElementById("PerdaMassa");
    var motivoRepro = document.getElementById("MotivoRepro");
    var laudoAR = document.getElementById("LaudoAR");
    var seloInmetro = document.getElementById("NumSelo");
    var btnAddExtintor = document.getElementById("btn-add-extintor");
    var idCliente = document.getElementById("IdCliente");
    var idExtintor = document.getElementById("IdExtintor");
    var capacidadeExtintora = document.getElementById("CapacExtintora");
    var projeto = document.getElementById("Projeto");
    var nomeCliente = document.getElementById("Cliente_NomeFantasia");
    var numeroOs = document.getElementById("NumeroOrdemServico");
    var reaproveitado = document.getElementById("Reaproveitado");

    ensaioIndPre.checked = true;
    ensaioVazVal.checked = true;
    visualIntacto.checked = true;
    inspRosca.checked = true;
    pintura.checked = false;
    reaproveitado.checked = true;

    var alterarNivelManutencao = document.getElementById("AlterarNivelManutencao");
    var alterarEnsaioInd = document.getElementById("AlterarEnsaioIndPre");
    var alterarEnsaioVaz = document.getElementById("AlterarEnsaioVazVal");
    var alterarVisualInt = document.getElementById("AlterarVisualIntacto");
    var alterarInspRosca = document.getElementById("AlterarInspRosca");
    var alterarPintura = document.getElementById("AlterarPintura");
    var alterarCompo1 = document.getElementById("AlterarIdComponente1");
    var alterarCompo2 = document.getElementById("AlterarIdComponente2");
    var alterarCompo3 = document.getElementById("AlterarIdComponente3");
    var alterarCompo4 = document.getElementById("AlterarIdComponente4");
    var alterarPesoCil = document.getElementById("AlterarPesoCilindroVazio");
    var alterarPesoComAgua = document.getElementById("AlterarPesoComAgua");
    var alterarVolLitros = document.getElementById("AlterarVolumeLitros");
    var alterarCapMaxima = document.getElementById("AlterarCapacidadeMaxima");
    var alterarPressaoTrab = document.getElementById("AlterarPressaoTrabalho");
    var alterarPressaoTesteCil = document.getElementById("AlterarPressaoTesteCilindro");
    var alterarPressaoTesteMang = document.getElementById("AlterarPressaoTesteMangueira");
    var alterarPressaoTesteMano = document.getElementById("AlterarPressaoTesteManometro");
    var alterarPressaoTesteVal = document.getElementById("AlterarPressaoTesteValvula");
    var alterarDefInst = document.getElementById("AlterarDefInstantanea");
    var alterarDefPerma = document.getElementById("AlterarDefPermanente");
    var alterarPorcEp = document.getElementById("AlterarPorcEpEt");
    var alterarTaraGravada = document.getElementById("AlterarTaraGravada");
    var alterarTaraReal = document.getElementById("AlterarTaraReal");
    var alterarPerdaMassa = document.getElementById("AlterarPerdaMassa");
    var alterarMotivoRepro = document.getElementById("AlterarMotivoRepro");
    var alterarLaudoAR = document.getElementById("AlterarLaudoAR");
    var alterarNumSelo = document.getElementById("AlterarNumSelo");
    var alterarIdRelatorioItem = document.getElementById("IdRelatorioItem");
    var alterarIdExtintor = document.getElementById("IdExtintor");
    var alterarIdOrdemServico = document.getElementById("IdOrdemServico");
    var alterarReaproveitado = document.getElementById("AlterarReaproveitado");
    var alterarDataProximaRecarga = document.getElementById("AlterarDataProximaRecarga");

    var alterarNumeroCilindro = document.getElementById("AlterarNumeroCilindro");
    var alterarAnoFabricacao = document.getElementById("AlterarAnoFabricacao");
    var alterarEnsaioHidrostatico = document.getElementById("AlterarEnsaioHidrostatico");
    var alterarIdMarcaExtintor = document.getElementById("AlterarIdMarcaExtintor");
    var alterarIdMateriaPrima = document.getElementById("AlterarIdMateriaPrima");
    var alterarIdCapacidade = document.getElementById("AlterarIdCapacidade");
    var alterarNBR = document.getElementById("AlterarNBR");
    var alterarProximoEnsaioHisdrostatico = document.getElementById("AlterarProximoEnsaioHisdrostatico");
    var alterarCapacExtintora = document.getElementById("AlterarCapacExtintora");
    var alterarProjeto = document.getElementById("AlterarProjeto");
    var alterarLote = document.getElementById("AlterarLote");
    var alterarSeloAnterior = document.getElementById("AlterarSeloAnterior");

    var cadastrarNumeroCilindro = document.getElementById("CadastrarNumeroCilindro");
    var cadastrarAnoFabricacao = document.getElementById("CadastrarAnoFabricacao");
    var cadastrarEnsaioHidrostatico = document.getElementById("CadastrarEnsaioHidrostatico");
    var cadastrarIdMarcaExtintor = document.getElementById("CadastrarIdMarcaExtintor");
    var cadastrarIdMateriaPrima = document.getElementById("CadastrarIdMateriaPrima");
    var cadastrarIdCapacidade = document.getElementById("CadastrarIdCapacidade");
    var cadastrarNBR = document.getElementById("CadastrarNBR");
    var cadastrarProximoEnsaioHisdrostatico = document.getElementById("CadastrarProximoEnsaioHisdrostatico");
    var cadastrarCapacExtintora = document.getElementById("CadastrarCapacExtintora");
    var cadastrarProjeto = document.getElementById("CadastrarProjeto");
    var cadastrarLote = document.getElementById("CadastrarLote");
    var cadastrarSeloAnterior = document.getElementById("CadastrarSeloAnterior");


    tabela.addEventListener("click", function (evt) {

        if (evt.target.tagName == "I") {
            var linha = evt.target.closest("tr");
            var indexLinha = linha.getAttribute("data-index");
            if (isNaN(indexLinha))
                indexLinha = null;
            
            linhaGeral = parseInt(indexLinha);
        }
    });

    tabela.addEventListener("click", function (evt) {
        var json = null;
        var target = evt.target;
        if (target.classList.contains("bi-gear-fill")) {
            var linha = document.querySelector("#tabela-relatorio > tbody > tr[data-index='" + linhaGeral + "'] > td .bi-gear-fill");

            json = linha.getAttribute("data-json");
            json = JSON.parse(json);
            recuperarRelItens(json.IdRelatorioItem);
            criarObjetoRelatorioItem();
            modalAlterarItens.show()
        }
    });

    tabela.addEventListener("click", function (evt) {
        var json = null;
        var target = evt.target;
        if (target.classList.contains("bi-trash3-fill")) {
            var linha = document.querySelector("#tabela-relatorio > tbody > tr[data-index='" + linhaGeral + "'] > td .bi-gear-fill");

            json = linha.getAttribute("data-json");
            json = JSON.parse(json);
            idRelItemApagado = json.IdRelatorioItem;
            modalApagarExtintor.show();
        }
    });
    botaoApagarExtintor.addEventListener("click", function () {
        if (linhaGeral != null && linhaGeral >= 0) {
            if (idRelItemApagado != null || idRelItemApagado > 0) {
                var list = inputExclusao.value;
                list = list + idRelItemApagado.toString() + ',';
                inputExclusao.value = list;

            }
           
            var tabelaExtintor = document.getElementById("tabela-relatorio");
            var linhaRemovida = tabelaExtintor.querySelector("tbody > tr[data-index='" + linhaGeral + "']");
            if (linhaRemovida) {
                linhaRemovida.parentNode.removeChild(linhaRemovida);
                indexarLinhasRelatorio();
                modalApagarExtintor.hide();

            }
        }
        


    });
    botaoAlterarRelItens.addEventListener("click", function () {
        alterarRelatorioItens()
        criarObjetoRelatorioItem()
        modalAlterarItens.hide();
        

    });
    function alterarRelatorioItens() {
        definindoDadosAlterarItens();
        validarCamposModalAlterarRelatorioItens();
        if (relItensGlobal.NivelManutencao != 0) {

            $.ajax({
                type: "POST",
                url: "/os/AlterarRelatorioItens",
                dataType: "json",
                data: { viewModel: relItensGlobal },
                success: function (json) {
                    if (json.mensagem == "success") {
                        Notiflix.Notify.success("Dados alterados com sucesso!");
                    }
                    else {
                        Notiflix.Notify.failure("Não foi possível altear os dados!")
                    }
                }

            });
        }
        indexarLinhasRelatorio();

    }
    function recuperarRelItens(idRelItem) {
        var idRelatorioItem = parseInt(idRelItem);
        $.ajax({
            type: "GET",
            url: "/os/RecuperarRelatorioItens",
            data: { idRelatorioItem: idRelatorioItem },
            dataType: "json",

            success: function (json) {
                if (json != null) {
                    alterarNivelManutencao.value = json.nivelManutencao;
                    alterarEnsaioInd.checked = json.ensaioIndPre;
                    alterarEnsaioVaz.checked = json.ensaioVazVal;
                    alterarVisualInt.checked = json.visualIntacto;
                    alterarInspRosca.checked = json.inspRosca;
                    alterarPintura.checked = json.pintura;
                    alterarCompo1.value = json.idComponentes1;
                    alterarCompo2.value =json.idComponentes2;
                    alterarCompo3.value = json.idComponentes3;
                    alterarCompo4.value = json.idComponentes4;
                    alterarPesoCil.value = json.pesoCilindroVazio;
                    alterarPesoComAgua.value = json.pesoComAgua;
                    alterarVolLitros.value = json.volumeLitros;
                    alterarCapMaxima.value = json.capacidadeMaxima;
                    alterarPressaoTrab.value = json.pressaoTrabalho;
                    alterarPressaoTesteCil.value = json.pressaoTesteCilindro
                    alterarPressaoTesteMang.value = json.pressaoTesteMangueira;
                    alterarPressaoTesteMano.value = json.pressaoTesteManometro;
                    alterarPressaoTesteVal.value = json.pressaoTesteValvula;
                    alterarDefInst.value = json.defInstantanea;
                    alterarDefPerma.value = json.defPermanente;
                    alterarPorcEp.value = json.porcEpEt;
                    alterarTaraGravada.value = json.taraGravada;
                    alterarTaraReal.value = json.taraReal;
                    alterarPerdaMassa.value = json.perdaMassa;
                    alterarMotivoRepro.value = json.motivoRepro;
                    alterarLaudoAR.value = json.laudoAR;
                    alterarNumSelo.value = json.numSelo;
                    alterarIdRelatorioItem.value = json.idRelatorioItens;
                    alterarIdExtintor.value = json.idExtintor;
                    alterarIdOrdemServico.value = json.idOServico;
                    alterarReaproveitado.checked = json.reaproveitado;
                    alterarDataProximaRecarga.value = json.dataProximaRecarga
                }
            },
            error: function (msg) {
                Notiflix.Notify.failure('Erro ao buscar o extintor, procure o administrador do sistema');
            }
        });
        

    }
    function definindoDadosAlterarItens() {
        relItensGlobal.IdRelatorioItens = isNaN(parseInt(alterarIdRelatorioItem.value))? 0 : parseInt(alterarIdRelatorioItem.value);
        relItensGlobal.NivelManutencao = isNaN(parseInt(alterarNivelManutencao.value)) ? 0 : parseInt(alterarNivelManutencao.value);
        relItensGlobal.EnsaioIndPre = alterarEnsaioInd.checked;
        relItensGlobal.EnsaioVazVal = alterarEnsaioVaz.checked;
        relItensGlobal.VisualIntacto = alterarVisualInt.checked;
        relItensGlobal.InspRosca = alterarInspRosca.checked;
        relItensGlobal.Pintura = alterarPintura.checked;
        relItensGlobal.IdComponentes1 = isNaN(parseInt(alterarCompo1.value)) ? 0 : parseInt(alterarCompo1.value);
        relItensGlobal.IdComponentes2 = isNaN(parseInt(alterarCompo2.value)) ? 0 : parseInt(alterarCompo2.value);
        relItensGlobal.IdComponentes3 = isNaN(parseInt(alterarCompo3.value)) ? 0 : parseInt(alterarCompo3.value);
        relItensGlobal.IdComponentes4 = isNaN(parseInt(alterarCompo4.value)) ? 0 : parseInt(alterarCompo4.value);
        relItensGlobal.PesoCilindroVazio = isNaN(parseFloat(alterarPesoCil.value)) ? 0 : parseFloat(alterarPesoCil.value); 
        relItensGlobal.PesoComAgua = isNaN(parseFloat(alterarPesoComAgua.value)) ? 0 : parseFloat(alterarPesoComAgua.value); 
        relItensGlobal.VolumeLitros = isNaN(parseFloat(alterarVolLitros.value)) ? 0 : parseFloat(alterarVolLitros.value);
        relItensGlobal.CapacidadeMaxima = isNaN(parseFloat(alterarCapMaxima.value)) ? 0 : parseFloat(alterarCapMaxima.value);
        relItensGlobal.PressaoTrabalho = isNaN(parseFloat(alterarPressaoTrab.value)) ? 0 : parseFloat(alterarPressaoTrab.value);
        relItensGlobal.PressaoTesteCilindro = isNaN(parseFloat(alterarPressaoTesteCil.value)) ? 0 : parseFloat(alterarPressaoTesteCil.value);
        relItensGlobal.PressaoTesteMangueira = isNaN(parseFloat(alterarPressaoTesteMang.value)) ? 0 : parseFloat(alterarPressaoTesteMang.value);
        relItensGlobal.PressaoTesteManometro = isNaN(parseFloat(alterarPressaoTesteMano.value)) ? 0 : parseFloat(alterarPressaoTesteMano.value);
        relItensGlobal.PressaoTesteValvula = isNaN(parseFloat(alterarPressaoTesteVal.value)) ? 0 : parseFloat(alterarPressaoTesteVal.value);
        relItensGlobal.DefInstantanea = isNaN(parseFloat(alterarDefInst.value)) ? 0 : parseFloat(alterarDefInst.value);
        relItensGlobal.DefPermanente = isNaN(parseFloat(alterarDefPerma.value)) ? 0 : parseFloat(alterarDefPerma.value);
        relItensGlobal.PorcEpEt = isNaN(parseFloat(alterarPorcEp.value)) ? 0 : parseFloat(alterarPorcEp.value);
        relItensGlobal.TaraGravada = isNaN(parseFloat(alterarTaraGravada.value)) ? 0 : parseFloat(alterarTaraGravada.value);
        relItensGlobal.TaraReal = isNaN(parseFloat(alterarTaraReal.value)) ? 0 : parseFloat(alterarTaraReal.value);
        relItensGlobal.PerdaMassa = isNaN(parseFloat(alterarPerdaMassa.value)) ? 0 : parseFloat(alterarPerdaMassa.value);
        relItensGlobal.MotivoRepro = isNaN(parseFloat(alterarMotivoRepro.value)) ? 0 : parseFloat(alterarMotivoRepro.value);
        relItensGlobal.LaudoAR = alterarLaudoAR.value;
        relItensGlobal.NumSelo = isNaN(parseFloat(alterarNumSelo.value)) ? 0 : parseFloat(alterarNumSelo.value);
        relItensGlobal.Reaproveitado = isNaN(parseFloat(alterarNumSelo.value)) ? 0 : parseFloat(alterarNumSelo.value);
        relItensGlobal.IdExtintor = isNaN(parseFloat(alterarIdExtintor.value)) ? 0 : parseFloat(alterarIdExtintor.value);
        relItensGlobal.IdOServico = isNaN(parseFloat(alterarIdOrdemServico.value)) ? 0 : parseFloat(alterarIdOrdemServico.value);
        relItensGlobal.Reaproveitado = alterarReaproveitado.checked;
        relItensGlobal.DataProximarecarga = alterarDataProximaRecarga.value;
   

    }
    function criarObjetoRelatorioItem() {
        relItensGlobal.IdRelatorioItens = 0
        relItensGlobal.NivelManutencao = 0
        relItensGlobal.EnsaioIndPre = false;
        relItensGlobal.EnsaioVazVal = false
        relItensGlobal.VisualIntacto = false;
        relItensGlobal.InspRosca = false;
        relItensGlobal.Pintura = false;
        relItensGlobal.Componente1 = 0;
        relItensGlobal.Componente2 = 0;
        relItensGlobal.Componente3 = 0;
        relItensGlobal.Componente4 = 0;
        relItensGlobal.PesoClindroVazio = 0;
        relItensGlobal.PesoCilindroComAgua = 0;
        relItensGlobal.VolumeLitros = 0;
        relItensGlobal.CapacidadeMaxima = 0;
        relItensGlobal.PressaoTrabalho = 0;
        relItensGlobal.PressaoTesteCilindro = 0;
        relItensGlobal.PressaoTesteMangueira = 0;
        relItensGlobal.PressaoTesteManometro = 0;
        relItensGlobal.PressaoTesteValvula = 0;
        relItensGlobal.DefInst = 0;
        relItensGlobal.DefPermanente = 0;
        relItensGlobal.PorEpEt = 0;
        relItensGlobal.TaraGravada = 0;
        relItensGlobal.TaraReal = 0;
        relItensGlobal.PerdaMassa = 0;
        relItensGlobal.MotivoReprova = null;
        relItensGlobal.LaudoAR = null;
        relItensGlobal.NumeroSelo = null;
        relItensGlobal.IdExtintor = 0;
        relItensGlobal.IdOServico = 0;
        relItensGlobal.Reaproveitado = false;
        relItensGlobal.DataProximarecarga = "";
    }
    function validarCamposModalAlterarRelatorioItens() {

        if (alterarNivelManutencao.value == "") {
            Notiflix.Notify.failure('Digite o número da manutenção');
            evt.preventDefault()
        }
    }
    //if (notify != undefined) {
    //    if (notify.getAttribute("data-type") === "error")
    //        Notiflix.Notify.failure(notify.innerText);
    //}
    btnAddExtintor.addEventListener("click", function (evt) {
        if (nomeCliente.value == "") {
            Notiflix.Notify.failure('Digite o nome do cliente');
            evt.preventDefault()
        }
        if (numeroOs.value == "") {
            Notiflix.Notify.failure('Digite o número da ordem de serviço');
            evt.preventDefault()
        }
        if (numeroOs.value == "") {
            Notiflix.Notify.failure('Digite o número da ordem de serviço');
            evt.preventDefault()
        }

    });
    $("#NumeroCilindro").on("focusout", function () {
        var numero = $(this).val();


        $.ajax({
            type: "GET",
            url: "/os/PesquisarExtintorCilindro",
            data: { numeroCilindro: numero },
            dataType: "json",
            success: function (json) {
                console.log(json);
                if (json != null) {
                    var extintor = json;

                    console.log(extintor);

                    $("#NumeroCilindro").val(extintor.NumeroCilindro);
                    $("#AnoFabricacao").val(extintor.AnoFabricacao);
                    $("#EnsaioHidrostatico").val(extintor.UltimoEnsaio);
                    $("#IdMarcaExtintor").val(extintor.Fabricante);
                    $("#IdMateriaPrima").val(extintor.MateriaPrima);
                    $("#IdCapacidade").val(extintor.Capacidade);
                    $("#NBR").val(extintor.NBR);
                    $("#ProximoEnsaioHisdrostatico").val(extintor.ProximoEnsaio);
                    $("#CapacExtintora").val(extintor.CapacidadeExtintora);
                    $("#Projeto").val(extintor.Projeto);
                    $("#IdExtintor").val(extintor.IdExtintor);
                    $("#Lote").val(extintor.Lote);
                    $("#SeloAnterior").val(extintor.SeloAnterior);
                    desabilitarCamposExtintor();
                }
            },
            error: function (msg) {
                Notiflix.Notify.failure('Extintor não encontrado, cadastre um novo!!! ');
                limparModalCadastrarExtintor();
                modalCadastrarExtintor.show();
                cadastrarNumeroCilindro.value =numeroCilindro.value
            }
        });

    });
    function limparRelatorio() {

        numeroCilindro.value = "";
        anoFabricacao.value = "";
        ensaioHidrostatico.value = "";
        idMarcaExtintor.value = 0;
        idMateriaPrima.value = 0;
        idCapacidade.value = 0;
        nbr.value = "";
        proximoEnsaio.value = "";
        dataProximaRecarga.value = "";
        lote.value = "";
        nivelManutencao.value = "";
        ensaioIndPre.checked = false;
        ensaioVazVal.checked = false;
        visualIntacto.checked = false;
        inspRosca.checked = false;
        pintura.checked = false;
        reaproveitado.checked = true;
        idComponente1.value = 0;
        idComponente2.value = 0;
        idComponente3.value = 0;
        idComponente4.value = 0;
        pesoCilVazio.value = "";
        pesoAgua.value = "";
        volumeLitros.value = "";
        capacidadeMaxima.value = "";
        pressaoTrabalho.value = "";
        pressaoTesteCil.value = "";
        pressaoTesteMang.value = "";
        pressaoTesteMano.value = "";
        pressaoTesteVal.value = "";
        defInst.value = "";
        defPerma.value = "";
        porcEpEt.value = "";
        taraGravada.value = "";
        taraReal.value = "";
        perdaMassa.value = "";
        motivoRepro.value = "";
        laudoAR.value = "";
        seloInmetro.value = "";
        idExtintor.value = "";
        capacidadeExtintora.value = "";
        projeto.value = "";
        seloAnterior.value = "";
    };
    async function addExtintor() {
        var btnAddExtintor = document.getElementById("btn-add-extintor");
        var fabricante = document.querySelector("#IdMarcaExtintor option[value='" + idMarcaExtintor.value + "']").innerHTML;
        var numSelo = seloInmetro.value != "" ? seloInmetro.value : "";

        var pressaoTrabalhoValor = 0;
        var pressaoTesteCilValor = 0;
        var pressaoTesteMangValor = 0;
        var pressaoTesteManoValor = 0;
        var pressaoTesteValValor = 0;

        //pressaoTrabalhoValor = pressaoTrabalho.value != "" ? parseFloat(pressaoTrabalho.value.replaceAll('.', '').replace(',', '.')) : 0;
        //pressaoTesteCilValor = (isNaN(pressaoTesteCil.value) || pressaoTesteCil.value == "") ? 0 : parseFloat(pressaoTesteCil.value.replaceAll('.', '').replace(',', '.'));
        //pressaoTesteMangValor = pressaoTesteMang.value != "" ? parseFloat(pressaoTesteMang.value.replaceAll('.', '').replace(',', '.')) : 0;
        //pressaoTesteManoValor = (isNaN(pressaoTesteMano.value) || pressaoTesteMano.value == "") ? 0 : parseFloat(pressaoTesteMano.value.replaceAll('.', '').replace(',', '.'));
        //pressaoTesteValValor = (isNaN(pressaoTesteVal.value) || pressaoTesteVal.value == "") ? 0 : parseFloat(pressaoTesteVal.value.replaceAll('.', '').replace(',', '.'));

        pressaoTrabalhoValor = pressaoTrabalho.value != "" ? pressaoTrabalho.value : 0;
        pressaoTesteCilValor = (isNaN(pressaoTesteCil.value) || pressaoTesteCil.value == "") ? 0 : pressaoTesteCil.value;
        pressaoTesteMangValor = pressaoTesteMang.value != "" ? pressaoTesteMang.value : 0;
        pressaoTesteManoValor = (isNaN(pressaoTesteMano.value) || pressaoTesteMano.value == "") ? 0 : pressaoTesteMano.value;
        pressaoTesteValValor = (isNaN(pressaoTesteVal.value) || pressaoTesteVal.value == "") ? 0 : pressaoTesteVal.value;

        var tbody = document.querySelector("tbody");
        var list = tbody.innerHTML;
        if ( nivelManutencao.value != "" &&  numeroOs.value != "" && numeroCilindro.value != "") {
            list = "<tr>" +
                "<td></td>" +
                "<td>" + numeroCilindro.value + "</td>" +
                "<td>" + anoFabricacao.value +
                "<input type='hidden' name='RelatorioItens[].Extintor.NumeroCilindro' value='" + numeroCilindro.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.AnoFabricacao' value='" + anoFabricacao.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.EnsaioHidrostatico' value='" + ensaioHidrostatico.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.IdMarcaExtintor' value='" + idMarcaExtintor.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.IdCapacidade' value='" + idCapacidade.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.IdMateriaPrima' value='" + idMateriaPrima.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.NBR' value='" + nbr.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.ProximoEnsaioHisdrostatico' value='" + proximoEnsaio.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].DataProximaRecarga' value='" + dataProximaRecarga.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.Id' value='" + idExtintor.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.Lote' value='" + lote.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].NivelManutencao' value='" + nivelManutencao.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].EnsaioIndPre' value='" + ensaioIndPre.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].EnsaioVazVal' value='" + ensaioVazVal.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].VisualIntacto' value='" + visualIntacto.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].InspRosca' value='" + inspRosca.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].Pintura' value='" + pintura.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].IdComponentes1' value='" + idComponente1.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].IdComponentes2' value='" + idComponente2.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].IdComponentes3' value='" + idComponente3.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].IdComponentes4' value='" + idComponente4.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PesoCilindroVazio' value='" + pesoCilVazio.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PesoComAgua' value='" + pesoAgua.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].VolumeLitros' value='" + volumeLitros.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].CapacidadeMaxima' value='" + capacidadeMaxima.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PressaoTrabalho' value='" + pressaoTrabalhoValor + "' />" +
                "<input type='hidden' name='RelatorioItens[].PressaoTesteCilindro' value='" + pressaoTesteCilValor + "' />" +
                "<input type='hidden' name='RelatorioItens[].PressaoTesteMangueira' value='" + pressaoTesteMangValor + "' />" +
                "<input type='hidden' name='RelatorioItens[].PressaoTesteManometro' value='" + pressaoTesteManoValor + "' />" +
                "<input type='hidden' name='RelatorioItens[].DefInstantanea' value='" + defInst.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].DefPermanente' value='" + defPerma.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PorcEpEt' value='" + porcEpEt.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].TaraGravada' value='" + taraGravada.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].TaraReal' value='" + taraReal.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PerdaMassa' value='" + perdaMassa.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].MotivoRepro' value='" + motivoRepro.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].LaudoAR' value='" + laudoAR.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].NumSelo' value='" + seloInmetro.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.CapacExtintora' value='" + capacidadeExtintora.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.Projeto' value='" + projeto.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].Reaproveitado' value='" + reaproveitado.checked + "' />" +
                "<input type='hidden' name='RelatorioItens[].Extintor.SeloAnterior' value='" + seloAnterior.value + "' />" +
                "<input type='hidden' name='RelatorioItens[].PressaoTesteValvula' value='" + pressaoTesteValValor + "' />" +
                "<input type='hidden' name='RelatorioItens[].IdRelatorioItens' value='" + 0 + "' />" +
                "</td>" +
                "<td>" + numSelo + "</td>" +
                "<td>" + fabricante + "</td>" +
                "<td>" +
                "<i class='bi-trash3-fill' style='font-size:24px';data-toggle='tooltip' data-placement='top' title='Excluir extintor'></i>" +
                "</td>" +
                "</tr>" + list;

            Notiflix.Notify.success('Extintor adicionado com sucesso!!!');
            tbody.innerHTML = list;
            indexarLinhasRelatorio();
            limparRelatorio();

            ensaioIndPre.checked = true;
            ensaioVazVal.checked = true;
            visualIntacto.checked = true;
            inspRosca.checked = true;
            pintura.checked = false;
            reaproveitado.checked = true;
        }
        else {
            Notiflix.Notify.error('Campos obrigatorios em branco!!!');

        }

    };
    function indexarLinhasRelatorio() {
       

        var indexarLinhas = document.querySelectorAll("#tabela-relatorio > tbody > tr")
        var indexarLinhaInput1 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(1)");
        var indexarLinhaInput2 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(2)");
        var indexarLinhaInput3 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(3)");
        var indexarLinhaInput4 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(4)");
        var indexarLinhaInput5 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(5)");
        var indexarLinhaInput6 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(6)");
        var indexarLinhaInput7 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(7)");
        var indexarLinhaInput8 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(8)");
        var indexarLinhaInput9 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(9)");
        var indexarLinhaInput10 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(10)");
        var indexarLinhaInput11 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(11)");
        var indexarLinhaInput12 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(12)");
        var indexarLinhaInput13 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(13)");
        var indexarLinhaInput14 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(14)");
        var indexarLinhaInput15 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(15)");
        var indexarLinhaInput16 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(16)");
        var indexarLinhaInput17 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(17)");
        var indexarLinhaInput18 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(18)");
        var indexarLinhaInput19 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(19)");
        var indexarLinhaInput20 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(20)");
        var indexarLinhaInput21 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(21)");
        var indexarLinhaInput22 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(22)");
        var indexarLinhaInput23 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(23)");
        var indexarLinhaInput24 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(24)");
        var indexarLinhaInput25 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(25)");
        var indexarLinhaInput26 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(26)");
        var indexarLinhaInput27 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(27)");
        var indexarLinhaInput28 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(28)");
        var indexarLinhaInput29 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(29)");
        var indexarLinhaInput30 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(30)");
        var indexarLinhaInput31 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(31)");
        var indexarLinhaInput32 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(32)");
        var indexarLinhaInput33 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(33)");
        var indexarLinhaInput34 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(34)");
        var indexarLinhaInput35 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(35)");
        var indexarLinhaInput36 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(36)");
        var indexarLinhaInput37 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(37)");
        var indexarLinhaInput38 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(38)");
        var indexarLinhaInput39 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(39)");
        var indexarLinhaInput40 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(40)");
        var indexarLinhaInput41 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(41)");
        var indexarLinhaInput42 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(42)");
        var indexarLinhaInput43 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(43)");
        var indexarLinhaInput44 = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(3) > input:nth-child(44)");
        var contarLinha = document.querySelectorAll("#tabela-relatorio  > tbody > tr > td:nth-child(1)");


        var count = 0;
        var countLinha = 1;
            indexarLinhas.forEach(function (element) {

                element.setAttribute("data-index", count);

                count++;
            });
            count = 0;
            indexarLinhaInput1.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.NumeroCilindro");
                count++;
            });
            count = 0;
            indexarLinhaInput2.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.AnoFabricacao");
                count++;
            });
            count = 0;
            indexarLinhaInput3.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.EnsaioHidrostatico");
                count++;
            });
            count = 0;
            indexarLinhaInput4.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.IdMarcaExtintor");
                count++;
            });
            count = 0;
            indexarLinhaInput5.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.IdCapacidade");
                count++;
            });
            count = 0;
            indexarLinhaInput6.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.IdMateriaPrima");
                count++;
            });
            count = 0;
            indexarLinhaInput7.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.NBR");
                count++;
            });
            count = 0;
            indexarLinhaInput8.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.ProximoEnsaioHisdrostatico");
                count++;
            });
            count = 0;
            indexarLinhaInput9.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].DataProximaRecarga");
                count++;
            });
            count = 0;
            indexarLinhaInput10.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.Id");
                count++;
            });
            count = 0;
            indexarLinhaInput11.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.Lote");
                count++;
            });
            count = 0;
            indexarLinhaInput12.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].NivelManutencao");
                count++;
            });
            count = 0;
            indexarLinhaInput13.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].EnsaioIndPre");
                count++;
            });
            count = 0;
            indexarLinhaInput14.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].EnsaioVazVal");
                count++;
            });
            count = 0;
            indexarLinhaInput15.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].VisualIntacto");
                count++;
            });
            count = 0;
            indexarLinhaInput16.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].InspRosca");
                count++;
            });
            count = 0;
            indexarLinhaInput17.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Pintura");
                count++;
            });
            count = 0;
            indexarLinhaInput18.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].IdComponentes1");
                count++;
            });
            count = 0;
            indexarLinhaInput19.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].IdComponentes2");
                count++;
            });
            count = 0;
            indexarLinhaInput20.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].IdComponentes3");
                count++;
            });
            count = 0;
            indexarLinhaInput21.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].IdComponentes4");
                count++;
            });
            count = 0;
            indexarLinhaInput22.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PesoCilindroVazio");
                count++;
            });
            count = 0;
            indexarLinhaInput23.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PesoComAgua");
                count++;
            });
            count = 0;
            indexarLinhaInput24.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].VolumeLitros");
                count++;
            });
            count = 0;
            indexarLinhaInput25.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].CapacidadeMaxima");
                count++;
            });
            count = 0;
            indexarLinhaInput26.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PressaoTrabalho");
                count++;
            });
            count = 0;
            indexarLinhaInput27.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PressaoTesteCilindro");
                count++;
            });
            count = 0;
            indexarLinhaInput28.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PressaoTesteMangueira");
                count++;
            });
            count = 0;
            indexarLinhaInput29.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PressaoTesteManometro");
                count++;
            });
            count = 0;
            indexarLinhaInput30.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].DefInstantanea");
                count++;
            });
            count = 0;
            count = 0;
            indexarLinhaInput31.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].DefPermanente");
                count++;
            });
            count = 0;
            indexarLinhaInput32.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PorcEpEt");
                count++;
            });
            count = 0;
            indexarLinhaInput33.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].TaraGravada");
                count++;
            });
            count = 0;
            indexarLinhaInput34.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].TaraReal");
                count++;
            });
            count = 0;
            indexarLinhaInput35.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PerdaMassa");
                count++;
            });
            count = 0;
            indexarLinhaInput36.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].MotivoRepro");
                count++;
            });
            count = 0;
            indexarLinhaInput37.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].LaudoAR");
                count++;
            });
            count = 0;
            indexarLinhaInput38.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].NumSelo");
                count++;
            });
            count = 0;
            indexarLinhaInput39.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.CapacExtintora");
                count++;
            });
            count = 0;
            indexarLinhaInput40.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.Projeto");
                count++;
            });
            count = 0;
            indexarLinhaInput41.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Reaproveitado");
                count++;
            });
            count = 0;
            indexarLinhaInput42.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].Extintor.SeloAnterior");
                count++;
            });
            count = 0;
            indexarLinhaInput43.forEach(function (element) {

                element.setAttribute("name", "RelatorioItens[" + count + "].PressaoTesteValvula");
                count++;
            });
            count = 0;
        indexarLinhaInput44.forEach(function (element) {

            element.setAttribute("name", "RelatorioItens[" + count + "].IdRelatorioItens");
            count++;
        });
        count = 0;
        contarLinha.forEach(function (element) {

            element.textContent = countLinha;
            countLinha++;
        });
        countLinha = 0;
    };

    
   
    btnAddExtintor.addEventListener("click", function () {
        addExtintor();
        $("#Lote").mask("00/00");
        $("#CapacExtintora").mask("00:Bc");
        habilitarCamposExtintor();
        dataProximaRecarga.value = proximaRecargaAno;

    });
    function criarObjetoExtintor() {
        extintorGlobal.Id = 0;
        extintorGlobal.NumeroCilindro = null;
        extintorGlobal.AnoFabricacao = 0;
        extintorGlobal.EnsaioHidrostatico = 0;
        extintorGlobal.IdMarcaExtintor = 0;
        extintorGlobal.IdMateriaPrima = 0;
        extintorGlobal.IdCapacidade = 0;
        extintorGlobal.NBR = null;
        extintorGlobal.ProximoEnsaioHisdrostatico = 0;
        extintorGlobal.Projeto = null;
        extintorGlobal.CapacExtintora = null;
        extintorGlobal.Lote = null;
        extintorGlobal.SeloAnterior = null;

    };
    function validarCamposModalCadastrarExtintor(evt) {

        if (cadastrarNumeroCilindro.value == "") {
            Notiflix.Notify.failure('Digite o número do cilíndro');
            evt.preventDefault()
        }
        if (cadastrarAnoFabricacao.value == "") {
            Notiflix.Notify.failure('Digite o ano de fabricação');
            evt.preventDefault()
        }
        if (cadastrarEnsaioHidrostatico.value == "") {
            Notiflix.Notify.failure('Digite o ensaio hidrostático');
            evt.preventDefault()
        }
        if (cadastrarIdMarcaExtintor.value == 0) {
            Notiflix.Notify.failure('Selecione um fabricante');
            evt.preventDefault()
        }
        if (cadastrarIdMateriaPrima.value == 0) {
            Notiflix.Notify.failure('Selecione uma matéria-prima');
            evt.preventDefault()
        }
        if (cadastrarIdCapacidade.value == 0) {
            Notiflix.Notify.failure('Selecione uma capacidade');
            evt.preventDefault()
        }
        if (cadastrarNBR.value == "") {
            Notiflix.Notify.failure('Digite a NBR');
            evt.preventDefault()
        }
        if (cadastrarProximoEnsaioHisdrostatico.value == "") {
            Notiflix.Notify.failure('Digite o próximo ensaio hidrostático');
            evt.preventDefault()
        }

    };
    function limparModalCadastrarExtintor() {
        cadastrarNumeroCilindro.value = "";
        cadastrarAnoFabricacao.value = "";
        cadastrarEnsaioHidrostatico.value = "";
        cadastrarIdMarcaExtintor.value = 0;
        cadastrarIdMateriaPrima.value = 0;
        cadastrarIdCapacidade.value = 0;
        cadastrarNBR.value = "";
        cadastrarProximoEnsaioHisdrostatico.value = "";
        cadastrarCapacExtintora.value = "";
        cadastrarProjeto.value = "";
        cadastrarLote.value = "";
        cadastrarSeloAnterior.value = "";
    };
    function definindoDadosExtintorCadastrar() {
        var numeroCilindro = cadastrarNumeroCilindro.value;
        var anoFabricacao = cadastrarAnoFabricacao.value;
        var ensaioHisdrotatico = cadastrarEnsaioHidrostatico.value;
        var idMarcaExtintor = cadastrarIdMarcaExtintor.value;
        var idMateriaPrima = cadastrarIdMateriaPrima.value;
        var idCapacidade = cadastrarIdCapacidade.value;
        var nbr = cadastrarNBR.value;
        var proximoEnsaio = cadastrarProximoEnsaioHisdrostatico.value;
        var capacidadeExtintora = cadastrarCapacExtintora.value;
        var projeto = cadastrarProjeto.value;
        var lote = cadastrarLote.value;
        var seloAnterior = cadastrarSeloAnterior.value;

        idMarcaExtintor = isNaN(parseInt(idMarcaExtintor)) ? 0 : parseInt(idMarcaExtintor);
        idMateriaPrima = isNaN(parseInt(idMateriaPrima)) ? 0 : parseInt(idMateriaPrima);
        idCapacidade = isNaN(parseInt(idCapacidade)) ? 0 : parseInt(idCapacidade);

        extintorGlobal.Id = 0;
        extintorGlobal.NumeroCilindro = numeroCilindro;
        extintorGlobal.AnoFabricacao = anoFabricacao;
        extintorGlobal.EnsaioHidrostatico = ensaioHisdrotatico;
        extintorGlobal.IdMarcaExtintor = idMarcaExtintor;
        extintorGlobal.IdMateriaPrima = idMateriaPrima;
        extintorGlobal.IdCapacidade = idCapacidade;
        extintorGlobal.NBR = nbr;
        extintorGlobal.ProximoEnsaioHisdrostatico = proximoEnsaio;
        extintorGlobal.Projeto = projeto;
        extintorGlobal.CapacExtintora = capacidadeExtintora;
        extintorGlobal.Lote = lote;
        extintorGlobal.SeloAnterior = seloAnterior;

    };
    function definindoDadosExtintorAlterar() {
        var ExtintorId = idExtintor.value;
        var numeroCilindro = alterarNumeroCilindro.value;
        var anoFabricacao = alterarAnoFabricacao.value;
        var ensaioHisdrotatico = alterarEnsaioHidrostatico.value;
        var idMarcaExtintor = alterarIdMarcaExtintor.value;
        var idMateriaPrima = alterarIdMateriaPrima.value;
        var idCapacidade = alterarIdCapacidade.value;
        var nbr = alterarNBR.value;
        var proximoEnsaio = alterarProximoEnsaioHisdrostatico.value;
        var capacidadeExtintora = alterarCapacExtintora.value;
        var projeto = alterarProjeto.value;
        var lote = alterarLote.value;
        var seloAnterior = alterarSeloAnterior.value;

        IdExtintor = isNaN(parseInt(IdExtintor)) ? 0 : parseInt(IdExtintor);
        idMarcaExtintor = isNaN(parseInt(idMarcaExtintor)) ? 0 : parseInt(idMarcaExtintor);
        idMateriaPrima = isNaN(parseInt(idMateriaPrima)) ? 0 : parseInt(idMateriaPrima);
        IdCapacidade = isNaN(parseInt(IdCapacidade)) ? 0 : parseInt(IdCapacidade);
        ExtintorId = isNaN(parseInt(ExtintorId)) ? 0 : parseInt(ExtintorId);

        extintorGlobal.Id = ExtintorId;
        extintorGlobal.NumeroCilindro = numeroCilindro;
        extintorGlobal.AnoFabricacao = anoFabricacao;
        extintorGlobal.EnsaioHidrostatico = ensaioHisdrotatico;
        extintorGlobal.IdMarcaExtintor = idMarcaExtintor;
        extintorGlobal.IdMateriaPrima = idMateriaPrima;
        extintorGlobal.IdCapacidade = idCapacidade;
        extintorGlobal.NBR = nbr;
        extintorGlobal.ProximoEnsaioHisdrostatico = proximoEnsaio;
        extintorGlobal.Projeto = projeto;
        extintorGlobal.CapacExtintora = capacidadeExtintora;
        extintorGlobal.Lote = lote;
        extintorGlobal.SeloAnterior = seloAnterior;

    };
    function validarCamposModalAlterarExtintor() {

        if (alterarNumeroCilindro.value == "") {
            Notiflix.Notify.failure('Digite o número do cilíndro');
            evt.preventDefault()
        }
        if (alterarAnoFabricacao.value == "") {
            Notiflix.Notify.failure('Digite o ano de fabricação');
            evt.preventDefault()
        }
        if (alterarEnsaioHidrostatico.value == "") {
            Notiflix.Notify.failure('Digite o ensaio hidrostático');
            evt.preventDefault()
        }
        if (alterarIdMarcaExtintor.value == 0) {
            Notiflix.Notify.failure('Selecione um fabricante');
            evt.preventDefault()
        }
        if (alterarIdMateriaPrima.value == 0) {
            Notiflix.Notify.failure('Selecione uma matéria-prima');
            evt.preventDefault()
        }
        if (alterarIdCapacidade.value == 0) {
            Notiflix.Notify.failure('Selecione uma capacidade');
            evt.preventDefault()
        }
        if (alterarNBR.value == "") {
            Notiflix.Notify.failure('Digite a NBR');
            evt.preventDefault()
        }
        if (alterarProximoEnsaioHisdrostatico.value == "") {
            Notiflix.Notify.failure('Digite o próximo ensaio hidrostático');
            evt.preventDefault()
        }

    };

    function limparModalAlterarExtintor() {
        alterarNumeroCilindro.value = "";
        alterarAnoFabricacao.value = "";
        alterarEnsaioHidrostatico.value = "";
        alterarIdMarcaExtintor.value = 0;
        alterarIdMateriaPrima.value = 0;
        alterarIdCapacidade.value = 0;
        alterarNBR.value = "";
        alterarProximoEnsaioHisdrostatico.value = "";
        alterarCapacExtintora.value = "";
        alterarProjeto.value = "";
        alterarLote.value = "";
        alterarSeloAnterior.value = "";
    };
    function salvarCadastroExtintor() {
        definindoDadosExtintorCadastrar();
        validarCamposModalCadastrarExtintor()

        if (extintorGlobal.NumeroCilindro != "" || extintorGlobal.AnoFabricacao != "" || extintorGlobal.EnsaioHidrostatico != ""
            || extintorGlobal.IdMarcaExtintor != 0 || extintorGlobal.IdMateriaPrima != 0 || extintorGlobal.IdCapacidade != 0 || extintorGlobal.ProximoEnsaioHisdrostatico != "" && extintorGlobal.Lote != "") {

            $.ajax({
                type: "POST",
                url: "/os/CadastrarExtintor",
                dataType: "json",
                data: { viewModel: extintorGlobal },
                success: function (json) {
                    var extintor = json;
                    if (extintor != null) {
                        idExtintor.value = extintor.id;
                        numeroCilindro.value = extintor.numeroCilindro;
                        ensaioHidrostatico.value = extintor.ensaioHidrostatico;
                        anoFabricacao.value = extintor.anoFabricacao;
                        idMarcaExtintor.value = extintor.idMarcaExtintor;
                        idMateriaPrima.value = extintor.idMateriaPrima;
                        idCapacidade.value = extintor.idCapacidade;
                        nbr.value = extintor.nbr;
                        proximoEnsaio.value = extintor.proximoEnsaioHisdrostatico;
                        capacidadeExtintora.value = extintor.capacExtintora;
                        projeto.value = extintor.projeto;
                        lote.value = extintor.lote;
                        seloAnterior.value = extintor.seloAnterior;

                        modalCadastrarExtintor.hide()
                        Notiflix.Notify.success("Extintor cadastrado com sucesso!!!");
                    }
                },
                error: function (msg) {
                    Notiflix.Notify.failure('Erro ao cadastrar, procure o administrador do sistema');
                }
            })

        }
        else (evt) => {

            Notiflix.Notify.warning('Campos obrigatorios em branco!!!');
            evt.preventDefault();
        }

    };

    function alterarExtintor() {
        definindoDadosExtintorAlterar();
        validarCamposModalAlterarExtintor();

        if (extintorGlobal.NumeroCilindro != "" || extintorGlobal.AnoFabricacao != "" || extintorGlobal.EnsaioHidrostatico != ""
            || extintorGlobal.IdMarcaExtintor != 0 || extintorGlobal.IdMateriaPrima != 0 || extintorGlobal.IdCapacidade != 0 || extintorGlobal.ProximoEnsaioHisdrostatico != "" || extintorGlobal.Lote != "") {
            $.ajax({
                type: "POST",
                url: "/os/alterarextintor",
                dataType: "json",
                data: { viewModel: extintorGlobal },
                success: function (json) {
                    var extintor = json;

                    if (extintor != null) {

                        $("#NumeroCilindro").val(extintor.numeroCilindro);
                        $("#AnoFabricacao").val(extintor.anoFabricacao);
                        $("#EnsaioHidrostatico").val(extintor.ensaioHidrostatico);
                        $("#IdMarcaExtintor").val(extintor.idMarcaExtintor);
                        $("#IdMateriaPrima").val(extintor.idMateriaPrima);
                        $("#IdCapacidade").val(extintor.idCapacidade);
                        $("#NBR").val(extintor.nbr);
                        $("#ProximoEnsaioHisdrostatico").val(extintor.proximoEnsaioHisdrostatico);
                        $("#CapacExtintora").val(extintor.capacExtintora);
                        $("#Projeto").val(extintor.projeto);
                        $("#Lote").val(extintor.lote);
                        $("#SeloAnterior").val(extintor.seloAnterior);

                        modalAlterar.hide();

                        limparModalAlterarExtintor();
                        criarObjetoExtintor();
                        indexarLinhasRelatorio() 

                        Notiflix.Notify.success("Extintor alterado com sucesso!")
                    }
                },
                error: function (msg) {
                    Notiflix.Notify.failure('Erro ao alterar, procure o administrador do sistema');
                }
            });
        }

    }

    function RecuperarExtintorAlterar() {
        var ExtintorId = idExtintor.value;

        if (!isNaN(ExtintorId)) {

            $.ajax({

                type: "GET",
                url: "/os/RecuperarExtintorAlterar",
                data: { idExtintor: ExtintorId },
                dataType: "json",
                success: function (json) {
                    console.log(json);
                    if (json != null) {
                        var extintor = json;


                        $("#AlterarNumeroCilindro").val(extintor.NumeroCilindro);
                        $("#AlterarAnoFabricacao").val(extintor.AnoFabricacao);
                        $("#AlterarEnsaioHidrostatico").val(extintor.UltimoEnsaio);
                        $("#AlterarIdMarcaExtintor").val(extintor.Fabricante);
                        $("#AlterarIdMateriaPrima").val(extintor.MateriaPrima);
                        $("#AlterarIdCapacidade").val(extintor.Capacidade);
                        $("#AlterarNBR").val(extintor.NBR);
                        $("#AlterarProximoEnsaioHisdrostatico").val(extintor.ProximoEnsaio);
                        $("#AlterarCapacExtintora").val(extintor.CapacidadeExtintora);
                        $("#AlterarProjeto").val(extintor.Projeto);
                        $("#AlterarLote").val(extintor.Lote);
                        $("#IdExtintor").val(extintor.IdExtintor);
                        $("#AlterarSeloAnterior").val(extintor.SeloAnterior);
                    }
                }
            });
        }

    }

    iconeAlterarExtintor.addEventListener("click", function () {
        modalAlterar.show();
        RecuperarExtintorAlterar();

    });

    botaoConfirmarAlteracao.addEventListener("click", function () {
        alterarExtintor();
        desabilitarCamposExtintor();
    });

    botaoCadastrarExtintor.addEventListener("click", function () {
        criarObjetoExtintor();
        limparModalCadastrarExtintor();
        modalCadastrarExtintor.show();

        setTimeout(function () {
            cadastrarNumeroCilindro.focus();
        }, 500);
    });

    botaoSalvarCadastro.addEventListener("click", function () {
        salvarCadastroExtintor();
        desabilitarCamposExtintor()
        limparModalCadastrarExtintor();

    });
    alterarEnsaioHidrostatico.addEventListener("blur", function () {
        var anoAdiciionado5anos = parseInt(alterarEnsaioHidrostatico.value);
        anoAdiciionado5anos += 5;
        alterarProximoEnsaioHisdrostatico.value = anoAdiciionado5anos;
    });
    cadastrarEnsaioHidrostatico.addEventListener("blur", function () {
        var anoAdiciionado5anos = parseInt(cadastrarEnsaioHidrostatico.value);
        anoAdiciionado5anos += 5;
        cadastrarProximoEnsaioHisdrostatico.value = anoAdiciionado5anos;
    });

    function desabilitarCamposExtintor() {
        anoFabricacao.disabled = true;
        ensaioHidrostatico.disabled = true;
        idMarcaExtintor.disabled = true;
        idMateriaPrima.disabled = true;
        idCapacidade.disabled = true;
        nbr.disabled = true;
        proximoEnsaio.disabled = true;
        capacidadeExtintora.disabled = true;
        projeto.disabled = true;
        lote.disabled = true;
        seloAnterior.disabled = true;
    };
    function habilitarCamposExtintor() {
        anoFabricacao.disabled = false;
        ensaioHidrostatico.disabled = false;
        idMarcaExtintor.disabled = false;
        idMateriaPrima.disabled = false;
        idCapacidade.disabled = false;
        nbr.disabled = false;
        proximoEnsaio.disabled = false;
        capacidadeExtintora.disabled = false;
        projeto.disabled = false;
        lote.disabled = false;
        seloAnterior.disabled = false;

    };
    apagarCampos();
    function apagarCampos() {
        numeroCilindro.addEventListener("keydown", function (evt) {
            if (evt.key === "Backspace") {
                anoFabricacao.value = "";
                ensaioHidrostatico.value = "";
                idMarcaExtintor.value = 0;
                idMateriaPrima.value = 0;
                idCapacidade.value = 0;
                nbr.value = "";
                proximoEnsaio.value = "";
                capacidadeExtintora.value = "";
                projeto.value = "";
                lote.value = "";
                idExtintor.value = "";
                seloAnterior.value = "";


                habilitarCamposExtintor();
            }
        });
    };
    addDataCampo();
    function addDataCampo() {
        dataProximaRecarga.value = proximaRecargaAno;
    };

 
    nivelManutencao.addEventListener("change", function () {

        if (this.value == 2) {
            pressaoTrabalho.value = "10,5";
        }
        if (this.value == 3) {
            pressaoTrabalho.value = "10,5";
            pressaoTesteCil.value = 26;
            pressaoTesteMang.value = 16;
            pressaoTesteMano.value = "10,5";
            pressaoTesteVal.value = 26;
        }
    });
    nivelManutencao.addEventListener("keydown", function (evt) {
        if (evt.key === "Backspace") {
            pressaoTrabalho.value = "";
            pressaoTrabalho.value = "";
            pressaoTesteCil.value = "";
            pressaoTesteMang.value = "";
            pressaoTesteMano.value = "";
            pressaoTesteVal.value = "";
        }

    });
})();
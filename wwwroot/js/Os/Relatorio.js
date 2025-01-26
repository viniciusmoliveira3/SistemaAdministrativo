(function () {
    Notiflix.Notify.init({});

    const { jsPDF } = window.jspdf;


    const notify = document.getElementById("notify");
    var conteudo = document.querySelector(".container");

    var valorensaioInd = document.querySelectorAll("#valorEnsaioInd");
    var valorensaioVaz = document.querySelectorAll("#valorEnsaioVaz");
    var valorVizu = document.querySelectorAll("#valorVizu");
    var valorInsoRosca = document.querySelectorAll("#valorInspRosca");
    var valorPintura = document.querySelectorAll("#valorPintura");

    var botaoImprimir = document.getElementById("btnImprimir");
    var botaoSalvar = document.getElementById("botaoSalvar");
    var botoesRelatorio = document.getElementById("botoesRelatorio");

    var loteReaproveitado = document.querySelectorAll("#loteReaproveitado");
    var loteReaproveitadoAbc = document.getElementById("somaReaproveitadoAbc");
    var loteReaproveitadoBc = document.getElementById("somaReaproveitadoBc");
    var loteReaproveitadoCo2 = document.getElementById("somaReaproveitadoCo2");
    var loteAtualBc = document.getElementById("loteAtualBc");
    var loteAtualAbc = document.getElementById("loteAtualAbc");
    var loteAtualCo2 = document.getElementById("loteAtualCo2");
    var loteAtualEm = document.getElementById("loteAtualEm");
    var loteAtualBcTabela= document.getElementById("somaLoteBc");
    var loteAtualAbcTabela = document.getElementById("somaLoteAbc");
    var loteAtualCo2Tabela = document.getElementById("somaLoteCo2");
    var loteAtualEmTabela = document.getElementById("somaLoteEm");

    var nomeCliente = document.getElementById("clienteNome");

   
    nomeCliente = nomeCliente.textContent;
    console.log(nomeCliente);
    
    var somaReaproveitado = document.getElementById("somaReaproveitado");
    
    //var numOs = document.getElementById("numOs").value;
    //var osNumero = numOs.toString();

    

    //loteReaproveitado.forEach(function (evt) {
    //    somaLote += parseInt(evt.textContent);

    //});
    //somaReaproveitado.textContent = somaLote + "Kg";
    somarLoteReaproveitado();
    function somarLoteReaproveitado() {
        var tabela = document.getElementById("tabelaPrincipal");
        console.log(tabela);
        var linhas = tabela.getElementsByTagName("tr");
        console.log(linhas)
        var somaLoteAtualBc = 0;
        var somaLoteAtualAbc = 0;
        var somaLoteAtualCo2 = 0;
        var somaLoteAtualEm = 0;
        var somaLoteReaproveitadoBc = 0;
        var somaLoteReaproveitadoAbc = 0;
        var somaLotereaproveitadoCo2 = 0;

        for (var i = 0; i < linhas.length; i++) {
            var celulasTrue = linhas[i].querySelector(".reaproveitado");
            console.log(celulasTrue);
            var celulasValor = linhas[i].getElementsByTagName("td")[6];
            console.log(celulasValor);
            var celulasMaterial = linhas[i].getElementsByTagName("td")[5];
            console.log(celulasMaterial);
            var celulasLote = linhas[i].getElementsByTagName("td")[11];
            console.log(celulasLote);
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "true" && (celulasMaterial.innerText.trim().toLowerCase() == "pó bc" || celulasMaterial.innerText.trim().toLowerCase() == "po bc")) {
                somaLoteReaproveitadoBc += parseInt(celulasValor.innerText);
                console.log(somaLoteReaproveitadoBc);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "true" && (celulasMaterial.innerText.trim().toLowerCase() == "pó abc" || celulasMaterial.innerText.trim().toLowerCase() == "po abc")) {
                somaLoteReaproveitadoAbc += parseInt(celulasValor.innerText);
                console.log(somaLoteReaproveitadoAbc);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "true" && (celulasMaterial.innerText.trim().toLowerCase() == "co2" || celulasMaterial.innerText.trim().toLowerCase() == "co²")) {
                somaLotereaproveitadoCo2 += parseInt(celulasValor.innerText);
                console.log(somaLotereaproveitadoCo2);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "false" && (celulasMaterial.innerText.trim().toLowerCase() == "pó bc" || celulasMaterial.innerText.trim().toLowerCase() == "po bc")
                && celulasLote.innerText.trim() == loteAtualBc.value) {
                somaLoteAtualBc += parseInt(celulasValor.innerText);
                console.log(somaLoteAtualBc);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água")  && celulasTrue.innerText.trim().toLowerCase() === "false" && (celulasMaterial.innerText.trim().toLowerCase() == "pó abc" || celulasMaterial.innerText.trim().toLowerCase() == "po abc")
                && celulasLote.innerText.trim() == loteAtualAbc.value) {
                somaLoteAtualAbc += parseInt(celulasValor.innerText);
                console.log(somaLoteAtualAbc);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "false" && (celulasMaterial.innerText.trim().toLowerCase() == "co2" || celulasMaterial.innerText.trim().toLowerCase() == "co2")
                && celulasLote.innerText.trim() == loteAtualCo2.value) {
                somaLoteAtualCo2 += parseInt(celulasValor.innerText);
                console.log(somaLoteAtualAbc);
            }
            if ((celulasMaterial.innerText.trim().toLowerCase() != "agua" || celulasMaterial.innerText.trim().toLowerCase() != "água") && celulasTrue.innerText.trim().toLowerCase() === "false" && (celulasMaterial.innerText.trim().toLowerCase() == "em" || celulasMaterial.innerText.trim().toLowerCase() == "espuma mecaninca")
                && celulasLote.innerText.trim() == loteAtualEm.value) {
                somaLoteAtualEm += parseInt(celulasValor.innerText);
            }
        }
        loteReaproveitadoAbc.textContent = somaLoteReaproveitadoAbc + "Kg";
        loteReaproveitadoBc.textContent = somaLoteReaproveitadoBc + "Kg";
        loteReaproveitadoCo2.textContent = somaLotereaproveitadoCo2 + "Kg";

        loteAtualBcTabela.textContent = somaLoteAtualBc + "Kg";
        loteAtualAbcTabela.textContent = somaLoteAtualAbc + "Kg";
        loteAtualCo2Tabela.textContent = somaLoteAtualCo2 + "Kg";
        loteAtualEmTabela.textContent = somaLoteAtualEm + "Kg";
    };
   
    
    valorensaioInd.forEach(function (element) {
        if (element.textContent.trim() === 'True') {
            element.textContent = "X";
        }
        else {
            element.textContent = "-";
        }
    });
    valorensaioVaz.forEach(function (element) {
        if (element.textContent.trim() === 'True') {
            element.textContent = "X";
        }
        else {
            element.textContent = "-";
        }
    });

    valorVizu.forEach(function (element) {
        if (element.textContent.trim() === 'True') {
            element.textContent = "X";
        }
        else {
            element.textContent = "-";
        }
    });
  
    valorInsoRosca.forEach(function (element) {
        if (element.textContent.trim() === 'True') {
            element.textContent = "X";
        }
        else {
            element.textContent = "-";
        }
    });
  
    valorPintura.forEach(function (element) {
        if (element.textContent.trim() === 'True') {
            element.textContent = "X";
        }
        else {
            element.textContent = "-";
        }
    });

    function ImprimirRelatorio() {
        var divImpressa = document.getElementById("body");
        var resultadoImpressão = window.print()

        if (!resultadoImpressão) {
            Notiflix.Notify.warning('Impressão cancelada');
        }

    };
    botaoImprimir.addEventListener("click", function () {
        ImprimirRelatorio();

    });

    botaoSalvar.addEventListener("click", () => {

        var option = {
            width: 2200,
            height:900,
            scale:2,
            ignoreElements: botoesRelatorio
            
        };
   
        html2canvas(conteudo, option).then(function (canvas) {
           
            const filename = `${nomeCliente}.pdf`
            let imagem = canvas.toDataURL('image/png');
            let pdf = new jsPDF({
                orientation: 'l', // landscape
                unit: 'pt', // points, pixels won't work properly
                format: [canvas.width, canvas.height] // set needed dimensions for any element
            });

            pdf.addImage(imagem, 'PNG', 0, 0, canvas.width, canvas.height);
            pdf.save(filename)

        });
       
        //const options = {
        //    margin: [5, 5, 5, 5],
        //    filename: "arquivo.pdf",
        //    html2canvas: { scale:2 },
        //    jsPDF: { unit: "mm", format: "a4", orientation: "landscape", precision: 20 }
        //}

        // html2pdf().set(options).from(conteudo).save();
        //const pdf = new jsPDF();
        //pdf.autoTable({ html: '#tabela-principal' });
        //pdf.save('table.pdf')
    });

})();



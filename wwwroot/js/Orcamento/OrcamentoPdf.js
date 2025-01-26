(function () {

    var nomeCliente = document.getElementById("nomeCliente");
    var botaoImprimir = document.getElementById("btnImprimir");
    var botaoSalvar = document.getElementById("botaoSalvar");
    var conteudo = document.querySelector(".container");

    const { jsPDF } = window.jspdf;


    nomeCliente = nomeCliente.textContent;
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
            width: 675,
            height: 1080,
            scale: 3,
            ignoreElements: botoesOrcamento

        };

        html2canvas(conteudo, option).then(function (canvas) {

            const filename = `${nomeCliente}.pdf`
            let imagem = canvas.toDataURL('image/png');
            let pdf = new jsPDF({
                orientation: 'p', // landscape
                unit: 'pt', // points, pixels won't work properly
                format: [canvas.width, canvas.height] // set needed dimensions for any element
            });

            pdf.addImage(imagem, 'PNG', 0, 0, canvas.width, canvas.height,undefined, 'FAST');
            pdf.save(filename)

        });
        
    });



})();
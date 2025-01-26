(function () {
    Notiflix.Notify.init({});
    const { jsPDF } = window.jspdf;

    var botaoImprimir = document.getElementById("btnImprimir");
    var botaoSalvar = document.getElementById("botaoSalvar");
    var conteudo = document.querySelector(".container");
    var numero = document.getElementById("numeroExtintor");

    var numeroExtintor = "";
    numeroExtintor = numero.textContent;

    function ImprimirRelatorio() {
        var divImpressa = document.getElementById("printable");
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
            width: 760,
            height: 900,
            scale: 2,
        };

        html2canvas(conteudo, option).then(function (canvas) {
            const filename = `${numeroExtintor}.pdf`
            let imagem = canvas.toDataURL('image/png');
            let pdf = new jsPDF({
                orientation: 'p', // landscape
                unit: 'pt', // points, pixels won't work properly
                format: [canvas.width, canvas.height] // set needed dimensions for any element
            });

            pdf.addImage(imagem, 'PNG', 0, 0, canvas.width, canvas.height);
            pdf.save(filename)

        });
    });

})();
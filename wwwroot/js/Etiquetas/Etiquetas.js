(function () {
    //var IdExtintor = document.getElementById("idExtintor").value;
    //new QRCode(document.getElementById("qrcode"), IdExtintor); 
    const notify = document.getElementById("notify");
    var i = 0;
    //Notiflix.Notify.init({});
    //const { jsPDF } = window.jspdf;

    var botaoImprimir = document.getElementById("btnImprimir");
    var conteudo = document.querySelector("printable");
   /* var qrcode = new QRCode("qrcode");*/
    var listQrCode = document.querySelectorAll(".qrcode")
    var listIdExtintior = document.querySelectorAll("#idExtintor");
    console.log(listQrCode);
    listIdExtintior.forEach(function (e) {
        console.log(e.value);
    });

    gerarQrCode();
   function gerarQrCode() {
      listQrCode.forEach(function (element) {
          var qrcode = new QRCode(element);

        console.log(listIdExtintior[i].value)
          qrcode.makeCode(listIdExtintior[i].value)
          i++;
      });
        
        /*qrcode.makeCode(IdExtintor.value);*/
   };
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
})();

var showQr = function () {
    var authenticatorUriCodeElement = document.getElementById('authenticatorUriCode');
    var authenticatorUriCanvasElement = document.getElementById('authenticatorUriCanvas');

    if (authenticatorUriCodeElement && authenticatorUriCanvasElement) {
        var uri = authenticatorUriCodeElement.innerHTML;
        if (uri) {

            //Create qr object
            //Minus the url, these are the defaults
            var qr = new VanillaQR({

                url: uri,
                size: 280,

                colorLight: "#ffffff",
                colorDark: "#000000",

                //output to table or canvas
                toTable: false,

                //Ecc correction level 1-4
                ecclevel: 1,

                //Use a border or not
                noBorder: false,

                //Border size to output at
                borderSize: 4

            });

            authenticatorUriCanvasElement.appendChild(qr.domElement);
        }
    }
};

if(window.attachEvent) {
    window.attachEvent('onload', showQr);
} else {
    if(window.onload) {
        var curronload = window.onload;
        var newonload = function(evt) {
            curronload(evt);
            showQr(evt);
        };
        window.onload = newonload;
    } else {
        window.onload = showQr;
    }
}      
function ValidacijaUplatnice(forma) {
    let validno = true;
    let uplatilacSpan = document.querySelector("#UplatilacSpan");
    let iznosSpan = document.querySelector("#IznosSpan");
    let datumSpan = document.querySelector("#DatumSpan");
    let svrhaSpan = document.querySelector("#SvrhaSpan");
    let uplatilac = forma["Uplatnica.Uplatilac"];
    let iznos = forma["Uplatnica.IznosUplate"];
    let datum = forma["Uplatnica.DatumPrometa"];
    let svrha = forma["Uplatnica.SvrhaUplate"];
    if (!uplatilac.value) {
        uplatilacSpan.innerHTML = "obavezno polje";
        uplatilac.style.backgroundColor = "red";
        validno = false;
    }
    else {
        uplatilacSpan.innerHTML = "";
        uplatilac.style.backgroundColor = "";
    }

    if (!iznos.value) {
        iznosSpan.innerHTML = "obavezno polje";
        iznos.style.backgroundColor = "red";
        validno = false;
    }
    else if (!parseFloat(iznos.value)){
        iznosSpan.innerHTML = "polje mora biti broj";
        iznos.style.backgroundColor = "red";
        validno = false;
    }
    else {
        iznosSpan.innerHTML = "";
        iznos.style.backgroundColor = "";
    }

    if (!datum.value) {
        datumSpan.innerHTML = "obavezno polje";
        datum.style.backgroundColor = "red";
        validno = false;
    }
    else {
        datumSpan.innerHTML = "";
        datum.style.backgroundColor = "";
    }

    if (!svrha.value) {
        svrhaSpan.innerHTML = "obevezno polje";
        svrha.style.backgroundColor = "red";
        validno = false;

    }
    else {
        svrhaSpan.innerHTML = "";
        svrha.style.backgroundColor = "";
    }
    return validno;

}
function ValidacijaDatuma(forma) {
    let validno = true;
    let iznosOd = forma["Racun.IznosOd"];
    let iznosDo = forma["Racun.IznosDo"];
    let iznosOdSpan = document.querySelector("#IznosOdSpan");
    let iznosDoSpan = document.querySelector("#IznosDoSpan");
    if (iznosOd.value > iznosDo.value) {
        iznosDoSpan.innerHTML = "ovaj iznos mora biti vece od iznosa od";
        iznosDo.style.backgroundColor = "red";
        validno = false;
    }
    else {
        iznosDoSpan.innerHTML = "";
        iznosDo.style.backgroundColor = "";

    }
    return validno;
}
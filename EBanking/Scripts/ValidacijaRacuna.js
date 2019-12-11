function Validacija(forma) {
    let validno = true;
    var letters = /^[0-9a-zA-Z]+$/;
    let nosilacSpan = document.querySelector("#NosilacSpan");
    let brojRacunaSpan = document.querySelector("#BrojRacunaSpan");
    let Nosilac = forma["Racun.NosilacRacuna"];
    let Broj = forma["Racun.BrojRacuna"];
    if (!Nosilac.value) {
        nosilacSpan.innerHTML = "obavezno polje za unos";
        Nosilac.style.backgroundColor = "red";
        validno = false;
    }
    else if (!Nosilac.value.match(letters)) {
        nosilacSpan.innerHTML = "Greska. Molimo unesite samo alfanumericke vrednosti!";
        Nosilac.style.backgroundColor = "red";
        validno = false;
    }
    else {
        nosilacSpan.innerHTML = "";
        Nosilac.style.backgroundColor = "";
    }

    if (!Broj.value) {
        brojRacunaSpan.innerHTML = "obavezno polje za unos";
        Broj.style.backgroundColor = "red";
        validno = false;
    }
    else {
        Broj.style.backgroundColor = "";
        brojRacunaSpan.innerHTML = "";
    }
    return validno;

}
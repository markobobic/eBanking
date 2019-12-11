insert into racun(nosilac_racuna,broj_racuna)
values
    ('FTN Informatika','02032321321321'),
    ('RTV Vojvodina','22032321321321'),
    ('Fakultet Tehnicih Nauka','32032321321321');

insert into uplatnica(id_racuna,iznos_uplate,datum_prometa,svrha_uplate,uplatilac)
values
    (1,40000.00,'2019-10-11','Uplata prve rate za kurs','Pera Peric'),
    (1,40000.00,'2019-11-11','Uplata druge rate za kurs','Pera Peric'),
    (2,1000.00,'2018-12-10 16:30:00','RTV pretplata','Milovan Milovanovic'),
    (3,-100000.00,'2019-12-10 11:30:00','Plate za profesore','FTN Informatika');
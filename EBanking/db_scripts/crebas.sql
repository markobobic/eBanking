create table racun
(
    id_racuna int primary key identity,
    nosilac_racuna nvarchar(150) not null,
    broj_racuna nvarchar(100) not null unique,
    aktivan_racun bit not null default 1,
    online_banking bit not null default 1);

create table uplatnica
(
    id_uplatnice int primary key identity,
    id_racuna int foreign key references racun(id_racuna) on delete cascade,
    iznos_uplate decimal not null,
    datum_prometa datetime,
    svrha_uplate nvarchar(200) not null,
    uplatilac nvarchar(200) not null,
    hitno bit not null default 0);


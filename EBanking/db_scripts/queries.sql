select * from racun,uplatnica where racun.id_racuna=uplatnica.id_racuna and racun.id_racuna=1 
select sum(iznos_uplate) from racun,uplatnica where racun.id_racuna=uplatnica.id_racuna and racun.id_racuna=3 group by racun.id_racuna
select * from uplatnica,racun where uplatnica.id_racuna=racun.id_racuna and uplatnica.id_uplatnice=1

drop table uplatnica
drop table racun
select * from racun
select * from uplatnica

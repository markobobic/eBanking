using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBanking.Models;
using EBanking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestRacuna()
        {
            UplatnicaRepo uRepo = new UplatnicaRepo();
            RacunRepo rRepo = new RacunRepo();
            Racun racun = new Racun();

            racun.BrojRacuna = "TEST BROJ RACUNA";
            racun.NosilacRacuna = "TEST NOSILAC RACUNA";
            int idRac = rRepo.Create(racun);
            Assert.IsTrue(idRac > 0,"Kreiranje racuna");
            racun.Id = idRac;

            Assert.IsTrue(rRepo.GetSum(idRac) == 0, "Prvi test ukupnog stanja");

            Uplatnica uplatnica = new Uplatnica();
            uplatnica.Racun = racun;
            uplatnica.IznosUplate = 100;
            uplatnica.SvrhaUplate = "TEST SVRHE";
            uplatnica.Uplatilac = "TEST UPLATILAC";

            int idUpl1 = uRepo.Create(uplatnica);
            Assert.IsTrue(idUpl1 > 0, "Kreiranje uplatnice 1");

            Assert.IsTrue(rRepo.GetSum(idRac) == 100, "Drugi test ukupnog stanja");

            Uplatnica uplatnica2 = new Uplatnica();
            uplatnica2.Racun = racun;
            uplatnica2.IznosUplate = -100;
            uplatnica2.SvrhaUplate = "TEST SVRHE";
            uplatnica2.Uplatilac = "TEST UPLATILAC";

            int idUpl2 = uRepo.Create(uplatnica2);
            Assert.IsTrue(idUpl2 > 0, "Kreiranje uplatnice 2");

            Assert.IsTrue(rRepo.GetSum(idRac) == 0, "Treci test ukupnog stanja");

            bool deleteRacun = rRepo.Delete(idRac);
            Assert.IsTrue(deleteRacun, "Brisanje Racuna");
        }
    }
}

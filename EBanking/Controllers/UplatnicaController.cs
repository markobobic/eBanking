using EBanking.Models;
using EBanking.Repository;
using EBanking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBanking.Controllers
{
    public class UplatnicaController : Controller
    {
        public RacunRepo RacunRepo { get; set; } = new RacunRepo();
        public UplatnicaRepo UplatnicaRepo { get; set; } = new UplatnicaRepo();

        public ActionResult Index(int id)
        {
            RacunUplatniceViewModel ruvm = new RacunUplatniceViewModel();
            ruvm.Racun = RacunRepo.GetById(id);
            Uplatnica uplatnica = new Uplatnica();
            uplatnica.Racun = ruvm.Racun;
            ruvm.Uplatnica = uplatnica;
            ruvm.UplatniceRacuna = UplatnicaRepo.GetByRacunId(id);
            return View(ruvm);
        }

        [HttpPost]
        public ActionResult Index(RacunUplatniceViewModel ruvm)
        {
            if (ModelState.IsValid)
            {
                int idUpl = UplatnicaRepo.Create(ruvm.Uplatnica);
                if (idUpl > 0)
                {
                    return RedirectToAction("Index", new { ruvm.Uplatnica.Racun.Id });
                }

            }
            ruvm.Racun = RacunRepo.GetById(ruvm.Uplatnica.Racun.Id);
            ruvm.Uplatnica.Racun = ruvm.Racun;
            ruvm.UplatniceRacuna = UplatnicaRepo.GetByRacunId(ruvm.Racun.Id);
            return View(ruvm);
        }

        public ActionResult Delete(int id)
        {
            Uplatnica uplatnica = UplatnicaRepo.GetById(id);
            return View(uplatnica);
        }

        [HttpPost]
        public ActionResult Delete(Uplatnica uplatnica)
        {
            int id = uplatnica.Racun.Id;
            UplatnicaRepo.Delete(uplatnica.Id);
            return RedirectToAction("Index", new { id });
        }

        public ActionResult Update(int id)
        {
            Uplatnica uplatnica = UplatnicaRepo.GetById(id);
            return View(uplatnica);
        }

        [HttpPost]
        public ActionResult Update(Uplatnica uplatnica)
        {
            int id = uplatnica.Racun.Id;
            if (ModelState.IsValid)
            {
                bool updated = UplatnicaRepo.Update(uplatnica);
                if (updated)
                {
                    return RedirectToAction("Index", new { id });
                }

            }
            return View(uplatnica);
        }

        public ActionResult SamoUplate(int id)
        {
            return View(UplatnicaRepo.GetByRacunId(id));
        }

        public ActionResult SamoIsplate(int id)
        {
            return View(UplatnicaRepo.GetByRacunId(id));
        }

        public ActionResult Pretraga()
        {
            UplatnicaUplatniceViewModel uuvm = new UplatnicaUplatniceViewModel();
            uuvm.Uplatnica = new Uplatnica();
            uuvm.Uplatnice = UplatnicaRepo.GetAll();
            return View(uuvm);
        }

        [HttpPost]
        public ActionResult Pretraga(UplatnicaUplatniceViewModel uuvm)
        {
            uuvm.Uplatnice = UplatnicaRepo.GetAll();
            return View(uuvm);
        }
    }
}
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
    public class RacunController : Controller
    {
        public RacunRepo RacunRepo { get; set; } = new RacunRepo();
        public UplatnicaRepo UplatnicaRepo { get; set; } = new UplatnicaRepo();

        public ActionResult Index()
        {
            RacunRacuniViewModel rrvm = new RacunRacuniViewModel();
            Racun racun = new Racun();
            rrvm.Racun = racun;
            rrvm.Racuni = RacunRepo.GetAll();
            return View(rrvm);
        }

        [HttpPost]
        public ActionResult Index(RacunRacuniViewModel rrvm)
        {
            if (ModelState.IsValid)
            {
                int idRac = RacunRepo.Create(rrvm.Racun);
                if (idRac > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            rrvm.Racuni = RacunRepo.GetAll();
            return View(rrvm);
        }

        public ActionResult Deaktiviraj(int id)
        {
            bool deaktivirano = RacunRepo.Deactivate(id);
            return RedirectToAction("Index");
        }

        public ActionResult Aktiviraj(int id)
        {
            bool deaktivirano = RacunRepo.Activate(id);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Racun racun = RacunRepo.GetById(id);

            return View(racun);
        }

        [HttpPost]
        public ActionResult Delete(Racun racun)
        {
            RacunRepo.Delete(racun.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            Racun racun = RacunRepo.GetById(id);
            return View(racun);
        }

        [HttpPost]
        public ActionResult Update(Racun racun)
        {
            if (ModelState.IsValid)
            {

                bool updated = RacunRepo.Update(racun);
                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(racun);

        }

        public ActionResult Pretraga()
        {
            RacunRacuniViewModel rrvm = new RacunRacuniViewModel();
            rrvm.Racun = new Racun();
            rrvm.Racuni = RacunRepo.GetAll();
            return View(rrvm);
        }

        [HttpPost]
        public ActionResult Pretraga(RacunRacuniViewModel rrvm)
        {
            rrvm.Racuni = RacunRepo.GetAll();
            return View(rrvm);
        }

    }
}
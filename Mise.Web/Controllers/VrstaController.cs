using Mise.Model;
using Mise.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.Controllers
{
    public class VrstaController : ControllerBase
    {
        // GET: Vrsta
        public ActionResult Index()
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<VrstaVM> lista = new List<VrstaVM>();

            foreach (var vr in model.Vrsta)
            {

                var vm = new VrstaVM()
                {

                    Id = vr.Id,
                    Naziv = vr.Naziv

                };

                lista.Add(vm);
            }



            return View(lista);
        }


        [HttpGet]
        public ActionResult Uredi(int id)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var v = model.Vrsta.First(r => r.Id == id);

            var vm = new VrstaVM()
            {
                Naziv = v.Naziv

            };




            return View(vm);

        }


        [HttpPost]
        public ActionResult Uredi (VrstaVM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vr = model.Vrsta.First(r => r.Id == vm.Id);

            
            vr.Naziv = vm.Naziv;
                       

            model.SaveChanges();

            return View (vm);
        }

        [HttpGet]
        public ActionResult Dodaj()
        {



            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(VrstaVM vm)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vr = new Vrsta()
            {
                Naziv = vm.Naziv


            };


            model.Vrsta.Add(vr);

            model.SaveChanges();

            return View("Dodaj");
        }

        [HttpGet]
        public ActionResult Brisi(int id)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vr = model.Vrsta.FirstOrDefault(r => r.Id == id);


            var vm = new VrstaVM()
            {
                Naziv = vr.Naziv

        };




            return View(vm);
        }



        [HttpPost]
        public ActionResult Brisi(VrstaVM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vr = model.Vrsta.FirstOrDefault(r => r.Id == vm.Id);


            model.Vrsta.Remove(vr);

            model.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
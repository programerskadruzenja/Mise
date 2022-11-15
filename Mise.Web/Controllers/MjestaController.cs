using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mise.Model;
using Mise.Web.ViewModels.Mjesta;

namespace Mise.Web.Controllers
{
    public class MjestaController : ControllerBase
    {
        public ActionResult Index()
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<MjestaVM> lista = new List<MjestaVM>();



            foreach (var mj in model.Mjesta)
            {

                var vm = new MjestaVM()
                {
                    Id = mj.Id,
                    Mjesto = mj.Mjesto,
                    PreferiraniTermin = mj.Termin.Naziv



                };
                lista.Add(vm);


            }




            return View(lista);
        }


        [HttpGet]
        public ActionResult Uredi(int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vm = new MjestaEM();

            vm.DostupniTermini = new List<SelectListItem>();

            var mj = model.Mjesta.FirstOrDefault(m => m.Id == id);
        

            foreach (var ter in model.Termin)
            {

                var sl = new SelectListItem()
                {
                    Text = ter.Naziv,
                    Value = ter.Id.ToString(),


            };
                vm.DostupniTermini.Add(sl);
                
            }
            
            vm.Mjesto = mj.Mjesto;
            
                       
           

            return View(vm);
        }



        [HttpPost]
        public ActionResult Uredi(MjestaEM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var mj = model.Mjesta.FirstOrDefault(m => m.Id == vm.Id);

            mj.Mjesto = vm.Mjesto;
            mj.PreferiraniTerminId = int.Parse(vm.PreferiraniTerminId);

            model.SaveChanges();
                        
          return  RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Dodaj()
        {
            var vm = new MjestaEM();

            vm.DostupniTermini = new List<SelectListItem>();

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            foreach(var termin in model.Termin.OrderBy(o=>o.Vrijeme))
            {
                var sli = new SelectListItem()
                {
                    Text = termin.Naziv,
                    Value = termin.Id.ToString(),
                    
                };

                vm.DostupniTermini.Add(sli);
            }

            
            return View(vm);
            
        }


        [HttpPost]
        public ActionResult Dodaj(MjestaEM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            if (vm.Mjesto == null)
                ModelState.AddModelError("Mjesto", "Naziv je obavezan");

            if (!ModelState.IsValid)
            {
                vm.DostupniTermini = new List<SelectListItem>();

                foreach (var termin in model.Termin.OrderBy(o => o.Vrijeme))
                {
                    var sli = new SelectListItem()
                    {
                        Text = termin.Naziv,
                        Value = termin.Id.ToString(),

                    };

                    vm.DostupniTermini.Add(sli);
                }

                return View(vm);
            }

            var mj = new Mjesta()
            {
                Mjesto = vm.Mjesto,
                PreferiraniTerminId = int.Parse(vm.PreferiraniTerminId)
            };

            model.Mjesta.Add(mj);

            model.SaveChanges();


            return RedirectToAction("Index");



        }

        [HttpGet]
        public ActionResult Brisi(int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();


            var mj = model.Mjesta.FirstOrDefault(m => m.Id ==id);

            var mje = new MjestaEM()
            {
                Mjesto = mj.Mjesto,
                PreferiraniTerminId = mj.PreferiraniTerminId.ToString(),
                PreferiraniTermin = mj.Termin.Vrijeme.ToString()
                
            };

            return View(mje);
        }


        [HttpPost]
        public ActionResult Brisi(MjestaEM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var mj = model.Mjesta.FirstOrDefault(m => m.Id == vm.Id);

            model.Mjesta.Remove(mj);

            model.SaveChanges();


            return View();
        }

    }
}
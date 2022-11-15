using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mise.Model;
using Mise.Web.ViewModels.Termin;
using System.Globalization;

namespace Mise.Web.Controllers
{
    public class DefaultController : ControllerBase
    {
        // GET: Default
        public ActionResult Index()
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<TerminVM> lista = new List<TerminVM>();

            foreach (var tr in model.Termin)
            {
                var vm = new TerminVM()
                {
                    Id = tr.Id,
                    Naziv = tr.Naziv,
                    Vrijeme = string.Format("{0:00}:{1:00}", tr.Vrijeme.Hours, tr.Vrijeme.Minutes)
                    
                };
                
                lista.Add(vm);
            }

            return View(lista);
        }


        [HttpGet]
        public ActionResult Uredi (int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var tr = model.Termin.FirstOrDefault(t => t.Id == id);

            if (tr == null)
                throw new ApplicationException("Termin ne postoji u bazi");

            var vm = new TerminVM()
            {
                Id = tr.Id,
                Naziv = tr.Naziv,
                Vrijeme = string.Format("{0:00}:{1:00}", tr.Vrijeme.Hours, tr.Vrijeme.Minutes)
            };

            return View(vm);

        }

        [HttpPost]
        public ActionResult Uredi(TerminVM vm)
        {
            if (vm.Naziv == null)
                ModelState.AddModelError("Naziv", "Naziv je obavezan!!!!");
            else if (vm.Naziv.Length < 3)
                ModelState.AddModelError("Naziv", "Naziv mora imati barem 3 znaka");

            //if()
            //ModelState.AddModelError("", "Ovo je greška");

            if (!ModelState.IsValid)
                return View(vm);

            ZUPATISNOEntities model = new ZUPATISNOEntities();

              var tr = model.Termin.First(t => t.Id == vm.Id);
            
            tr.Naziv = vm.Naziv;
            tr.Vrijeme =  TimeSpan.Parse(vm.Vrijeme);
             
            

            model.SaveChanges();
                        
            return View(vm);

        }

        [HttpGet]
        public ActionResult Brisi(int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var tr = model.Termin.First(t => t.Id == id);

            var vm = new TerminVM()
            {
                Id = id,
                Naziv = tr.Naziv,
                Vrijeme = string.Format("{0:00}:{1:00}", tr.Vrijeme.Hours, tr.Vrijeme.Minutes)
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Brisi(TerminVM vm)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var tr = model.Termin.First(t => t.Id == vm.Id);
            model.Termin.Remove(tr);

            model.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
       public ActionResult Dodaj ()
        {

           
            return View("Dodaj");
        }


        [HttpPost]
        public ActionResult Dodaj(TerminVM vm)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var termin = new Termin()
            { 
            Naziv = vm.Naziv,
            Vrijeme = TimeSpan.Parse(vm.Vrijeme)
            
            };

            model.Termin.Add(termin);

            try
            {
                 model.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new ApplicationException ("Greška pri unosu novog zapisa!" + ex.Message, ex) ;
            }
           
            
            return View("Dodaj");

        }
    }
}
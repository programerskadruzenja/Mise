using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mise.Web.ViewModels;
using Mise.Web.ViewModels.GodisnjiUnos;
using Mise.Model;

namespace Mise.Web.Controllers
{
    public class GodisnjiUnosController : ControllerBase
    {
        // GET: GodisnjiUnos
               
        public ActionResult Index()
        {

            UnosGodineVM vm = new UnosGodineVM();

            inicijaliziraj(vm);


            //UnosGodineVM vm = new UnosGodineVM();
            //vm.Godine = new List<SelectListItem>();


            //for (int i = 0; i <= 2; i++)
            //{

            //    var lst = new SelectListItem()
            //    {
            //        Text = DateTime.Now.AddYears(i).Year.ToString(),
            //        Value = DateTime.Now.AddYears(i).Year.ToString()


            //    };
            //    vm.Godine.Add(lst);


            //}




            //    int g = DateTime.Now.Year;
            //for (int i = 0; i <= 2; i++)
            //{

            //    var li = new SelectListItem()

            //    {
            //        Text = (g+i).ToString(),
            //        Value = (g+ i).ToString()
            //    };


            //    vm.Godine.Add(li);



            //}



            return View(vm);


        }

        private void inicijaliziraj (UnosGodineVM vm)
        {
            vm.Godine = new List<SelectListItem>();


            for (int i = 0; i <= 3; i++)
            {


                SelectListItem lst = new SelectListItem()
                {
                    Text = DateTime.Now.AddYears(i).Year.ToString(),
                    Value = DateTime.Now.AddYears(i).Year.ToString()

                };

                vm.Godine.Add(lst);


            }
        }

        [HttpPost]
        public ActionResult Index (UnosGodineVM vm)
        {

            if (vm == null)
                return View (vm);

            int god;

            if (!int.TryParse(vm.Godina, out god))
                ModelState.AddModelError("Godina","Neispravan format");
            else
            {
                if (god<2000 || god>3000)
                {
                    ModelState.AddModelError("Godina", "Nedozvoljen unos");
                }


            }


            inicijaliziraj(vm);


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

                        
                       
          return  RedirectToAction("Unos", vm);
        }



        [HttpGet]
        public ActionResult Unos(int godina)
        {

            var rezultat = new GodisnjiUnosEM
            {
                Godina = godina.ToString(),
                Mjesta = new List<string>(),
                Retci = new List<RedakEM>()
            };

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var mjesta = model.Mjesta
                .Where(o => !o.Obrisano)
                .OrderBy(o => o.Br)
                .ToList();

            foreach (var mjesto in mjesta)
            {
                rezultat.Mjesta.Add(mjesto.Mjesto);
                
            }

            var rezerviraneMise = model.Rezervirane
                .Where(o => !o.Obrisano.Value && o.Datum.HasValue)
                .OrderBy(o => o.Datum)
                .ToList();
            rezerviraneMise.AddRange(model.Rezervirane
                .Where(o => !o.Obrisano.Value && !o.Datum.HasValue)
                .OrderBy(o => o.Datum));


            //------------------------------------------------
            foreach (var rezerviranaMisa in rezerviraneMise)
            {
                string noviDatum = null;

                if (rezerviranaMisa.Datum.HasValue)
                {
                    noviDatum = string.Format("{0:00}.{1:00}.{2:0000}.", rezerviranaMisa.Datum.Value.Day, rezerviranaMisa.Datum.Value.Month, godina);
                }


                var redakEM = new RedakEM
                {
                    Naziv = rezerviranaMisa.Naziv,
                    Datum = noviDatum,
                    Opis = rezerviranaMisa.Opis,
                    RezerviranaMisaID = rezerviranaMisa.Id,
                    Mise = new List<MisaEM>() // ako postoje 2 mjesta, kolekcija ce imati 2 elementa
                   
                };

                rezultat.Retci.Add(redakEM);

                foreach (var mjesto in mjesta.OrderBy(o => o.Br))
                {
                    var misaEM = new MisaEM();

                         {    
                            misaEM.MjestoID = mjesto.Id;
                            misaEM.Mjesto = mjesto.Mjesto;
                            
                                if(misaEM.MjestoID==4)
                                misaEM.PotvrdiMisu = "on";

                        }
                      

                        redakEM.Mise.Add(misaEM); 
                    }
                                   
                
            }
                                    


            return View(rezultat);
        }


        [HttpPost]
        public ActionResult Unos(GodisnjiUnosEM em)
        {

            //DateTime god = DateTime.Parse(em.Retci[0].Datum);
            //string g= string.Format("{0:00}.{1:00}.{2:0000}.", god.Day, god.Month, god.Year);

            


            ZUPATISNOEntities model = new ZUPATISNOEntities();


                      

                foreach (var redak in em.Retci)
                {
                    if (redak.Datum == null)
                    {
                        ModelState.AddModelError("", "Unesite datum za " + redak.Naziv);
                        continue;
                    }

                    DateTime d = DateTime.Parse(redak.Datum);

                Rezervirane rezervirane = model.Rezervirane.First(r => r.Id == redak.RezerviranaMisaID);

                         redak.Naziv = rezervirane.Naziv;
                         redak.Opis = rezervirane.Opis;
                         em.Mjesta = new List<string>();
                


                foreach (var misaUMjestu in redak.Mise)
                      {
                    em.Mjesta.Add(misaUMjestu.Mjesto);

                 if (misaUMjestu.MjestoID == 4)
                        misaUMjestu.PotvrdiMisu= "on";
                   
                    else
                        misaUMjestu.PotvrdiMisu = "false";
                   

                   
                    if (misaUMjestu.PotvrdiMisu == null || misaUMjestu.PotvrdiMisu.ToLower() != "on")
                            continue;

                  

                    // provjera da li misa već postoji
                    var postojeceMiseUBazi = model.Detalji.Where(o => o.MjestoID == misaUMjestu.MjestoID
                    && o.VrijemePocetka.Year == d.Year
                    && o.VrijemePocetka.Month == d.Month
                    && o.VrijemePocetka.Day == d.Day);

                    if (postojeceMiseUBazi.Any())
                    
                        ModelState.AddModelError("", "Misa " + redak.Opis + " već postoji u mjestu " + misaUMjestu.Mjesto + " " + redak.Datum);

                    
                   
                        Detalji detalj = new Detalji();

                        detalj.MjestoID = misaUMjestu.MjestoID;
                        detalj.Opis = rezervirane.Opis;
                        detalj.RezerviranaMisaID = rezervirane.Id;
                        detalj.VrijemePocetka = d;

                        detalj.VrijemeZavrsetka = d;
                        detalj.NaruciteljID = rezervirane.NaruciteljID;
                        detalj.VrijemeUpisa = DateTime.Now;
                        detalj.VrstaId = rezervirane.VrstaId;

                        model.Detalji.Add(detalj);

                   
                    
                    }

                }


            if (ModelState.IsValid)
            {
                model.SaveChanges();
            }



            //em.Mjesta = new List<string>();
            
            //foreach (var mj in model.Mjesta.OrderBy(o => o.Br))
            //{

            //    em.Mjesta.Add(mj.Mjesto);

            //}

          




            return View(em);
        }


        }
}
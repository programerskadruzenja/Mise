using Mise.Model;
using Mise.Web.ViewModels.Rezervirane;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mise.Web.Controllers
{
    public class RezerviranaMisaController : ControllerBase
    {
        // GET: RezerviranaMisa
        public ActionResult Index()
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<RezerviranaMisaVM> lista = new List<RezerviranaMisaVM>();
            
           
            int i = 0; ;

            if (model.Rezervirane.Any())

            {
                foreach (var r in model.Rezervirane)
                {

                    RezerviranaMisaVM vm = new RezerviranaMisaVM()
                    {
                        Opis = r.Opis,
                        Naziv = r.Naziv,
                        ID = r.Id,
                        br = ++i + ".",
                        //Vrsta = r.Vrsta.Naziv
                       
                    };

                    var n = model.Osobe.FirstOrDefault(o => o.Id == r.NaruciteljID);

                    vm.Narucitelj = n.Ime + " " + n.Prezime;
                    lista.Add(vm);
                }

            }


            else

            {
                RezerviranaMisaVM vm = new RezerviranaMisaVM();
                vm.br =  "Nema zapisa u bazi";
                lista.Add(vm);
            }


                           

            return View(lista);
        }
                   

    

            

           


        [HttpGet]
        public ActionResult Uredi(int id)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

           
            var rez = model.Rezervirane.FirstOrDefault(r => r.Id == id);

            var nar = model.Osobe.FirstOrDefault(o => o.Id == rez.NaruciteljID);
           //var rez = model.Rezervirane.Where(r => r.Id == id).FirstOrDefault();

            if (rez== null)
                throw new ApplicationException("Nema podataka u bazi");




            var vm = new RezerviranaMisaEM()
            {
                Naziv = rez.Naziv,
                Opis = rez.Opis,
                Datum = string.Format("{0:dd/MM/yyyy.}", rez.Datum),
                NaruciteljId = rez.NaruciteljID,
               // Narucitelji = rez.NaruciteljID.HasValue ? nar.Ime + ", "  + nar.Prezime : "",
                
               
            };

            if (rez.NaruciteljID!=0)
                vm.Narucitelji = nar.Ime + ", " + nar.Prezime;
            else
                vm.Narucitelji = "";

            vm.Vrste = new List<SelectListItem>();

            foreach (var item in model.Vrsta)
                vm.Vrste.Add(new SelectListItem() {Text = item.Naziv, Value = item.Id.ToString() });

            


            

            vm.VrstaID = rez.VrstaId.ToString();

           


            return View (vm);

        }



        [HttpPost]
        public ActionResult Uredi (RezerviranaMisaEM vm)

        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var rez = model.Rezervirane.FirstOrDefault(m => m.Id == vm.ID);

            rez.Naziv = vm.Naziv;
            rez.Opis = vm.Opis;
            rez.VrstaId = int.Parse(vm.VrstaID);
            rez.NaruciteljID = int.Parse(vm.NaruciteljId.ToString());
            

            if (vm.Datum != null)
            {
                rez.Datum = DateTime.Parse(vm.Datum);
                
            }
            else
                rez.Datum = null;


            model.SaveChanges();
            
                     


            return RedirectToAction("Index");


        }


        [HttpGet]
        public ActionResult Dodaj ()
        {
            
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var vm = new RezerviranaMisaEM();

            //vm.Vrste = new List<SelectListItem>();
           vm.Vrste  = new List<SelectListItem>();

            foreach (var vr in model.Vrsta)
               vm.Vrste.Add(new SelectListItem { Text = vr.Naziv, Value = vr.Id.ToString ()});
                        

            //foreach (var item in model.Vrsta)
            //{

            //    var list = new SelectListItem()
            //    {
            //        Value = item.Id.ToString(),
            //        Text = item.Naziv,

            //    };
            //    vm.Vrste.Add(list);
            //};
           


            return View(vm);

        }


        [HttpPost]
        public ActionResult Dodaj(RezerviranaMisaEM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

           

            vm.Vrste = new List<SelectListItem>();

            foreach (var vr in model.Vrsta)
                vm.Vrste.Add(new SelectListItem { Text = vr.Naziv, Value = vr.Id.ToString() });


            var rez = new Rezervirane()
            {
                Opis = vm.Opis,
                Naziv = vm.Naziv,
                //Vrsta = vm.Vrste.Where(t=>t.Value== vm.VrstaID).ElementAt(0).Text,
                VrstaId = int.Parse(vm.VrstaID),
                Obrisano = false,
                NaruciteljID = (int)vm.NaruciteljId
            };

            if (vm.Datum != null)
                rez.Datum = DateTime.Parse(vm.Datum);

            try
            {

                model.Rezervirane.Add(rez);
                model.SaveChanges();
            }
            catch (Exception ex )
            {


                throw new ApplicationException("greška " + ex.Message, ex);
            }
                 

            return View(vm);


        }

        [HttpGet]
        public ActionResult Brisi(int id)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            
            var rez = model.Rezervirane.FirstOrDefault(m => m.Id == id);

            var nar = model.Osobe.FirstOrDefault(n => n.Id == rez.NaruciteljID);


            var vm = new RezerviranaMisaEM()
            {
                ID = rez.Id,
                Naziv = rez.Naziv,
                NaruciteljId = rez.NaruciteljID,
                Narucitelji = nar.Ime + " " + nar.Prezime,
                Datum = rez.Datum.HasValue ? rez.Datum.Value.ToString("dd-MMMM-yyyy") : "",
                Opis = rez.Opis,
                //Vrsta = rez.Vrsta.Naziv,
                VrstaID = rez.VrstaId.ToString(),
                


            };

            vm.Vrste = new List<SelectListItem>();

            foreach (var item in model.Vrsta)
                vm.Vrste.Add(new SelectListItem() { Text = item.Naziv, Value = item.Id.ToString() });

            

            return View(vm);
        }

        [HttpPost]
        public ActionResult Brisi (RezerviranaMisaEM vm)
        {



            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var rez = model.Rezervirane.FirstOrDefault(r => r.Id == vm.ID);


            //var det = model.Detalji.Where(d => d.NaruciteljID == vm.NaruciteljId);

            foreach (var d in rez.Detalji.ToList())
                model.Detalji.Remove(d);
                

            try
            {

            model.Rezervirane.Remove(rez);

            model.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new ApplicationException (ex.Message);
            }
            
            


          return  RedirectToAction("Index");
        }


        }
    }

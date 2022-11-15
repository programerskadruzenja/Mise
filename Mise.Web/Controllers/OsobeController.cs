using Mise.Model;
using Mise.Web.ViewModels.Osobe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Mise.Web.Controllers
{
    public class OsobeController : ControllerBase
    {
        private const string direktorijSaSlikama = "Slike";
        private const string nemaSlike = "nemaslike.png";

        // GET: Osobe
        public ActionResult Index(string pretraga)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<OsobaVM> lista = new List<OsobaVM>();

            List<Osobe> osobe = null;

          

            if (pretraga == null)
                osobe = model.Osobe.OrderBy(o => o.Ime).ThenBy(o => o.Prezime).ToList();
            else
                osobe = model.Osobe
                    .Where(o=>o.Prezime.Contains(pretraga) || o.Ime.Contains(pretraga))
                    .OrderBy(o => o.Prezime).ThenBy(o => o.Ime).ToList();

           
                
                int i = 0;

            foreach (var os in osobe )
            {

                //i++;
                //i += 1;
                var vm = new OsobaVM()
                {
                    Id = os.Id,
                    Ime = os.Ime,

                    Prezime = os.Prezime,
                    Telefon = os.Telefon,
                    Mobitel = os.Mobitel,
                    Slika = vratiPutanjuSlike(os),
                    red = ++i + "."
                };

                

                lista.Add(vm);

            }
            
            return View(lista);

        }

        private string vratiPutanjuSlike(Osobe osoba)
        {
           
            return osoba.Slika == null
                ? "~\\" + Path.Combine(direktorijSaSlikama, nemaSlike)
                : "~\\" + Path.Combine(direktorijSaSlikama, osoba.Slika);
        }


        [HttpGet]
        public ActionResult Uredi(int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

             var os = model.Osobe.FirstOrDefault(o => o.Id == id);

            if (os == null)
                throw new ApplicationException("Ne postoji u bazi");


            var vm = new OsobaVM()
            {
                Id = os.Id,
                Ime = os.Ime,
                Prezime = os.Prezime,
                Opaska = os.Opaska,
                OIB = os.OIB,
                Adresa = os.Adresa,
                Telefon = os.Telefon,
                Mobitel = os.Mobitel,
                Email = os.Email,
                DaNe = os.DaNe,
                Slika = vratiPutanjuSlike(os),
                //Slika

                Iznos = string.Format ("{0:0.00}",os.Iznos)

            };

            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Uredi (OsobaVM vm)
        {
            
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var os = model.Osobe.FirstOrDefault(o => o.Id == vm.Id);

            os.Ime = vm.Ime;
            os.Prezime = vm.Prezime;
            os.Opaska = vm.Opaska;

            os.OIB = vm.OIB;

            os.Adresa = vm.Adresa;
            os.Telefon = vm.Telefon;
            os.Mobitel = vm.Mobitel;
            os.Email = vm.Email;
            os.DaNe = vm.DaNe;


            if (vm.Iznos != null)
            {
                os.Iznos = double.Parse(vm.Iznos);
                vm.Iznos = string.Format("{0:0.00}", os.Iznos);
            }

                                    //Slika


            try
            {
                model.SaveChanges();
            }
            catch (Exception ex )
            {

                throw new ApplicationException("greška " + ex.Message, ex);
            }

            vm.Slika = vratiPutanjuSlike(os);

            if (vm.NovaSlika != null)
            {
                try
                {
                    string fizickaPutanja = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, direktorijSaSlikama);
                    // Brisanje postojeće slike
                    if (os.Slika != null)
                        System.IO.File.Delete(Path.Combine(fizickaPutanja, os.Slika));

                    // Spremanje slike
                    string ektenzija = Path.GetExtension(vm.NovaSlika.FileName);
                    // .jpg
                    if (!Directory.Exists(fizickaPutanja))
                        Directory.CreateDirectory(fizickaPutanja);

                    fizickaPutanja = Path.Combine(fizickaPutanja, vm.Id + ektenzija); //5.jpg


                    vm.NovaSlika.SaveAs(fizickaPutanja);
                    os.Slika = vm.Id + ektenzija;//5.jpg
                    model.SaveChanges();
                    vm.Slika = vratiPutanjuSlike(os);
                }

                
                
                catch (Exception e)
                {
                    throw new ApplicationException("Pogreška prilikom spremanja slike\r\n" + e.Message, e);
                }
            }

            
            else

             if (vm.DaNe)
            {
                string Putanja = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, direktorijSaSlikama);
                System.IO.File.Delete(Path.Combine(Putanja, os.Slika));
                os.Slika = null;
                vm.Slika=vratiPutanjuSlike(os);
            }

            model.SaveChanges();




            return RedirectToAction("Uredi", new {id=vm.Id });

          //  return View(vm);

        }

        [HttpGet]
        public ActionResult Dodaj ()
        {
            
            

            return View ("Dodaj");
        }

        [HttpPost]
        public ActionResult Dodaj(OsobaVM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            if (!vm.Unesi && model.Osobe.Any(o => o.Ime == vm.Ime && o.Prezime == vm.Prezime))
                ModelState.AddModelError("", "Osoba s tim imenom i prezimenom već postoji");

            if (!ModelState.IsValid)
                return View(vm);



            var os = new Osobe()
            {
                Ime = vm.Ime,
                Prezime = vm.Prezime,
                Opaska = vm.Opaska,
                OIB = vm.OIB,
                Adresa = vm.Adresa,
                Telefon = vm.Telefon,
                Mobitel = vm.Mobitel,
                Email = vm.Email,
                DaNe = vm.DaNe


                //Slika
            };

                    

        if (vm.Iznos != null)
            os.Iznos = double.Parse(vm.Iznos);




            if (vm.NovaSlika != null)
            {
                try
                {
                    
                    string fizickaPutanja = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, direktorijSaSlikama);
                    // Brisanje postojeće slike
                    //if (os.Slika != null)
                    //    System.IO.File.Delete(Path.Combine(fizickaPutanja, os.Slika));

                    // Spremanje slike
                    string ektenzija = Path.GetExtension(vm.NovaSlika.FileName);
                    // .jpg
                    if (!Directory.Exists(fizickaPutanja))
                        Directory.CreateDirectory(fizickaPutanja);

                    model.Osobe.Add(os);
                    model.SaveChanges();

                    fizickaPutanja = Path.Combine(fizickaPutanja, os.Id + ektenzija); //5.jpg


                    vm.NovaSlika.SaveAs(fizickaPutanja);

                    //os.Slika = vm.Id + ektenzija;//5.jpg

                    os.Slika = os.Id + ektenzija;//5.jpg


                    //model.SaveChanges();

                    //vm.Slika = vratiPutanjuSlike(os);


                }

                catch (Exception e)
                {
                    throw new ApplicationException("Pogreška prilikom spremanja slike\r\n" + e.Message, e);
                }
            }

            else
            {
                os.Slika = null;

                model.Osobe.Add(os);
            }




            try
            {
                    model.SaveChanges();
            }

            catch (Exception ex)
            {

                throw new ApplicationException("Greška pri unosu" + ex.Message, ex);
            }

                      
            
            return RedirectToAction("Dodaj");

        }



        [HttpGet]
        public ActionResult Brisi (int id)
        {
             ZUPATISNOEntities model = new ZUPATISNOEntities();

            var os = model.Osobe.FirstOrDefault(o => o.Id == id);


            var vm = new OsobaVM()
            {

                Ime = os.Ime,
                Prezime = os.Prezime,                                            
                Opaska = os.Opaska,
                OIB = os.OIB,
                Adresa = os.Adresa,
                Telefon = os.Telefon,
                Mobitel = os.Mobitel,
                Email = os.Email,
                DaNe = os.DaNe,

            };


            return View(vm);


        }


        [HttpPost]
        public ActionResult Brisi (OsobaVM vm)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var os = model.Osobe.FirstOrDefault(o => o.Id == vm.Id);

           var nar = model.Detalji.Where(o => o.NaruciteljID == vm.Id);

            foreach (var item in nar)
            {
                model.Detalji.Remove(item);
            }

           


            model.Osobe.Remove(os);
            

            try
            {
             model.SaveChanges();
            }
            catch (Exception e)
            {

                throw new ApplicationException("Nastupila je greška " + e.Message, e);
            }


            


         return  RedirectToAction("Index");

        }

        public void BrisiSliku(int id)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var os = model.Osobe.FirstOrDefault(o => o.Id == id);
            if (os.Slika == null)
                return;

            string Putanja = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, direktorijSaSlikama);
            System.IO.File.Delete(Path.Combine(Putanja, os.Slika));
            os.Slika = null;

            model.SaveChanges();
        }


        public JsonResult Search(string term)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            IOrderedQueryable<Osobe> lista = null;

            if (term != null)
            {
                lista = model.Osobe
                    .Where(o => o.Ime.Contains(term) || o.Prezime.Contains(term))
                    .OrderBy(o => o.Prezime)
                    .ThenBy(o => o.Ime);
            }

            List<OsobaVM> rezultat = new List<OsobaVM>();

            foreach (var osoba in lista)
            {
                var vm = new OsobaVM();

                vm.Id = osoba.Id;
                vm.PunoIme = string.Format("{0}, {1}", osoba.Prezime, osoba.Ime);
                if (osoba.Adresa != null)
                    vm.PunoIme += " (" + osoba.Adresa + ") ";

                var kontakt = osoba.Mobitel;
                if (vm.Mobitel != null && osoba.Telefon != null)
                    vm.Mobitel += ", " + osoba.Telefon;
                else if (osoba.Telefon != null)
                    vm.Mobitel = osoba.Telefon;

                if (kontakt != null)
                    vm.PunoIme += " (" + kontakt + ") ";
                rezultat.Add(vm);
            }
            return Json(rezultat);
        }

        [HttpGet]
        public ActionResult BrojMisa ()
        {
            return View(new BrojMisaZahtjev()
            {
                Godina=2018
            });
        }

        [HttpPost]
        public ActionResult BrojMisa(BrojMisaZahtjev zahtjev)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var podaci = model.VratiBrojMisaZaNarucitelje(zahtjev.MjestoID, zahtjev.Godina);
           
           
            var rezultat = new List<BrojMisaRedak>();

            foreach(var podatak in podaci)
            {
                rezultat.Add(new BrojMisaRedak()
                {
                    NaruciteljID=podatak.NaruciteljID,
                    Ime=podatak.Ime,
                    Prezime=podatak.Prezime,
                    Broj=podatak.Broj.Value
                });
            }

            return View("BrojMisaRezultat", rezultat);
        }


    }
}
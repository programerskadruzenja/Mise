using Mise.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mise.Model;
using Mise.Web.ViewModels.Detalji;
using Mise.Web.ViewModels.Mjesta;
using Mise.Web.ViewModels.Ispis;
using System.Globalization;

namespace Mise.Web.Controllers
{
    public class KalendarController : ControllerBase
    {
        // GET: Kalendar
        public ActionResult Index(DateTime? datum)
        {


            KalendarVM vm = new KalendarVM()
            {
                TrenutniDatum = datum == null ? DateTime.Now.ToString("yyyy-MM-dd") : datum.Value.ToString("yyyy-MM-dd")

            };

            return View(vm);
        }

        public string getString (DateTime ? dateTime)
        {

            if (!dateTime.HasValue)
                return null;
           return  dateTime.Value.ToString("yyyy-MM-dd") + "T" + dateTime.Value.ToString("HH:mm:ss");
            
        }


        public ActionResult Get(DateTime odDatuma, DateTime doDatuma, int? mjestoID)
        {



            try
            {
                ZUPATISNOEntities model = new ZUPATISNOEntities();
                List<Detalji> det = null;


                if (mjestoID.HasValue)
                {

                    det = model.Detalji
                        .Where(d => d.VrijemePocetka >= odDatuma && d.VrijemeZavrsetka < doDatuma && d.MjestoID == mjestoID)
                        .OrderBy(d => d.MjestoID)
                        .ToList();


                }

                else
                {

                    det = model.Detalji
                        .Where(d => d.VrijemePocetka >= odDatuma && d.VrijemeZavrsetka <= doDatuma)
                        .OrderBy(d => d.MjestoID)
                        .ToList();

                }


                var boje = model.Boja.ToList();

                Dictionary<int, Boja> mjestoBoje = new Dictionary<int, Boja>();

                int trenutnaBojaID = 1;

                int brojBoja = boje.Count(b => b.Rezervirana.HasValue && !b.Rezervirana.Value);

                foreach (var d in det)
                {

                    if (!mjestoBoje.ContainsKey(d.MjestoID))
                    {

                        mjestoBoje[d.MjestoID] = boje.First(b => b.Id == trenutnaBojaID);
                        trenutnaBojaID = trenutnaBojaID % brojBoja + 1;


                    }

                }

                var rezerviranaBoja = boje.First(b => b.Rezervirana.HasValue && !b.Rezervirana.Value);

                List<DetaljiVM> rezultat = new List<DetaljiVM>();
                Dictionary<string, int> dict = new Dictionary<string, int>();
               
                foreach (var d in det)
                {
                    string boja = d.RezerviranaMisaID.HasValue ? rezerviranaBoja.Pozadina : mjestoBoje[d.MjestoID].Pozadina;

                    var vm = new DetaljiVM()
                    {

                        MjestoNaziv = d.Mjesta.Mjesto,
                        MjestoID = d.MjestoID,
                        EventColorValue = boja,
                        EventColorIsDark = false,
                        EventStartTimeStamp = getString(d.VrijemePocetka),
                        EventEndTimeStamp = getString(d.VrijemeZavrsetka),
                        EventID = d.Id,
                        EventIsAllDay = false,
                        EventTitle = d.Opis,
                        OsobaPunoIme = d.NaruciteljID.HasValue ? d.Osobe.Ime + " " + d.Osobe.Prezime : "",
                        NaruciteljID = d.NaruciteljID,
                        ZaPokojne = d.VrstaId == Konstante.MisaZaPokojneID
                    };

                    rezultat.Add(vm);


                }

                return Json(rezultat);

            }



            catch (Exception ex)
            {


                throw new ApplicationException (ex.Message);

            }

                
        }

        [HttpGet]
        public ActionResult CreateTemp (string odDatumaTekst, string doDatumaTekst)
        {

            TempData["OdDatumaTekst"] = odDatumaTekst;
            TempData["doDatumaTekst"] = doDatumaTekst;

            return RedirectToAction("Dogadaj");
        }

        [HttpGet]
        public ActionResult UpdateTemp (int id)
        {
            
            TempData["id"] = id;

            return RedirectToAction("Dogadaj");

        }


        [HttpGet]
        public ActionResult Dogadaj()
        {
            DateTime odDatuma, doDatuma;

            int? id = (int?)TempData["id"];

            Detalji det = null;

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            if (id.HasValue)
            {
                det = model.Detalji.FirstOrDefault(o => o.Id == id);

                odDatuma = det.VrijemePocetka;
                doDatuma = det.VrijemeZavrsetka;


            }

            else

            {
                //string temp = null;
                //if (TempData["OdDatumatekst"] != null)
                //    temp = TempData["OdDatumatekst"].ToString();

                string odDatumaTekst = TempData["OdDatumatekst"]?.ToString();
                string doDatumaTekst = TempData["dodatumaTekst"]?.ToString();

                if (!DateTime.TryParse(odDatumaTekst, out odDatuma))
                    odDatuma = DateTime.Now;

                if (!DateTime.TryParse(doDatumaTekst, out doDatuma))
                    doDatuma = DateTime.Now.AddHours(1);

            }


            DetaljiEM vm = new DetaljiEM()
            {
                ID = id,
                Datum = odDatuma.ToString("dd.MM.yyyy"),
                VrijemePocetka = odDatuma.ToString("HH:mm:ss"),
                VrijemeZavrsetka = doDatuma.ToString("HH:mm:ss"),

                VrijemeUpisa = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),

                Placeno ="0,00"

            };



            inicijalizirajKolekcije(vm, true);

           

            if (id.HasValue)
            {

                //det = model.Detalji.FirstOrDefault(o => o.Id == id);
                det = model.Detalji.FirstOrDefault(o => o.Id == id);

                vm.Opis = det.Opis;
                vm.MjestoID = det.MjestoID.ToString();
                vm.NaruciteljID = det.NaruciteljID;
                vm.OsobaImeIPrezime = det.NaruciteljID.HasValue ? det.Osobe.Prezime + ", " + det.Osobe.Ime : "";
                vm.VrstaId = det.VrstaId.ToString();

                vm.VrijemeUpisa = det.VrijemeUpisa.ToString();
                
            }


          
           

            //foreach (var m in model.Mjesta)
            //{
            //    var sl = new SelectListItem()
            //    {
            //        Value = m.Id.ToString(),
            //        Text = m.Mjesto

            //    };

            //    vm.Mjesta.Add(sl);
            //}


            

                return View(vm);
        }


        [HttpPost]
        public ActionResult Dogadaj(DetaljiEM vm)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            Mjesta mjesto = null;


            if (vm.MjestoID == null)
                ModelState.AddModelError("MjestoID", "Obavezno unesite mjesto");

            else
            {
                int mjestoId;

                if (!int.TryParse(vm.MjestoID, out mjestoId))
                    ModelState.AddModelError("MjestoID", "neispravan format");
                else
                    mjesto = model.Mjesta.FirstOrDefault(m => m.Id == mjestoId);
            }


            Vrsta vr = null;

            if (vm.VrstaId == null)
                ModelState.AddModelError("VrstaId", "Unesite vrstu");

            else

            {
                int vrId;

                if (!int.TryParse(vm.VrstaId, out vrId))
                    ModelState.AddModelError("VrstaId", "Neispravan format");
                else
                    vr = model.Vrsta.FirstOrDefault(v => v.Id == vrId);

            }

            Osobe os = null;


            if (!vm.NaruciteljID.HasValue)
                ModelState.AddModelError("NaruciteljID", "Obavezno unesite osobu");
            else
            {
              os = model.Osobe.FirstOrDefault(m => m.Id == vm.NaruciteljID.Value);
            }


            DateTime pocetak, kraj;

            if (!DateTime.TryParse(vm.Datum + " " + vm.VrijemePocetka, out pocetak))
                ModelState.AddModelError("VrijemePocetka", "Neispravan format datuma");
            if (!DateTime.TryParse(vm.Datum + " " + vm.VrijemeZavrsetka, out kraj))
                ModelState.AddModelError("VrijemeZavrsetka", "Neisprvan format datuma");

            if (pocetak > kraj)
                ModelState.AddModelError("VrijemeZavrsetka", "Početni datum treba biti manji od završnog");

            float placeno;
            if (!float.TryParse(vm.Placeno, out placeno))
            {
                ModelState.AddModelError("Placeno", "Neispravan format broja");
            }

            if (!ModelState.IsValid)
            {
                inicijalizirajKolekcije(vm, true);
                return View(vm);
            }


            if (!vm.KolizijaPotvrdena)
            {
                //IEnumerable<Detalji> kolizije = vm.ID.HasValue
                //    ? model.Detalji.Where(o =>
                //        (pocetak >= o.VrijemePocetka && pocetak <= o.VrijemeZavrsetka ||
                //        kraj >= o.VrijemePocetka && kraj <= o.VrijemeZavrsetka ||
                //        pocetak <= o.VrijemePocetka && kraj >= o.VrijemeZavrsetka) && o.Id != vm.ID.Value)
                //    : model.Detalji.Where(o =>
                //        (pocetak >= o.VrijemePocetka && pocetak <= o.VrijemeZavrsetka ||
                //        kraj >= o.VrijemePocetka && kraj <= o.VrijemeZavrsetka ||
                //        pocetak <= o.VrijemePocetka && kraj >= o.VrijemeZavrsetka));

                IEnumerable<Detalji> kolizije = vm.ID.HasValue
                    ? model.Detalji.Where(o =>
                        (pocetak.Year == o.VrijemePocetka.Year && pocetak.Month == o.VrijemePocetka.Month && pocetak.Day == o.VrijemePocetka.Day
                        && o.Id != vm.ID.Value))
                        : model.Detalji.Where(o =>
                        (pocetak.Year == o.VrijemePocetka.Year && pocetak.Month == o.VrijemePocetka.Month 
                        && pocetak.Day == o.VrijemePocetka.Day));

                if (kolizije.Any ())
                {
                    vm.Kolizije = new List<KolizijaVM>();

                    foreach (var k in kolizije)
                    {
                        var kolizijaVM = new KolizijaVM()
                        
                        {
                            Vrijeme = k.VrijemePocetka.ToString("HH:mm") + " - " + k.VrijemeZavrsetka.ToString("HH:mm"),
                            Opis = k.Opis,
                            Mjesto=k.Mjesta.Mjesto,
                            Osoba= k.NaruciteljID.HasValue ? k.Osobe.Prezime + ", " + k.Osobe.Ime : ""
                            

                        };

                        vm.Kolizije.Add(kolizijaVM);

                    }
                    inicijalizirajKolekcije(vm, false);
                    return View(vm);

                }
            }

            // Sve provjere su prošle
            try
            {
                Detalji misa = null;
                if (vm.ID.HasValue)
                    // radi se o updateu
                    misa = model.Detalji.First(o => o.Id == vm.ID.Value);
                else
                {
                    // radi se o insertu
                    misa = new Detalji();
                    model.Detalji.Add(misa);
                }
                misa.VrstaId = vr.Id;
                misa.VrijemePocetka = pocetak;
                misa.VrijemeZavrsetka = kraj;
                misa.Opis = vm.Opis ?? "";
                misa.NaruciteljID = os?.Id;
                misa.MjestoID = mjesto.Id;
                misa.VrijemeUpisa = DateTime.Now;
                model.SaveChanges();

                return RedirectToAction("Index", new {datum = misa.VrijemePocetka });
            }
            catch (Exception e)
            {
                throw new ApplicationException("Pogreška prilikom spremanja događaja. " + e.Message);
            }
        }


        

        public void inicijalizirajKolekcije (DetaljiEM vm, bool pocetnevrijednosti)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            vm.Mjesta = new List<SelectListItem>();

            foreach (var mjesto in model.Mjesta.OrderByDescending(m=>m.Mjesto))
                vm.Mjesta.Add(new SelectListItem() { Text = mjesto.Mjesto, Value = mjesto.Id.ToString()});

            vm.Vrste = new List<SelectListItem>();

            model.Vrsta.ToList().ForEach(o => vm.Vrste.Add(new SelectListItem() { Text = o.Naziv, Value = o.Id.ToString() }));


            if (pocetnevrijednosti)
            {
                vm.MjestoID = vm.Mjesta.First().Value;
                vm.VrstaId = vm.Vrste.First().Value;
            }
        }


        

        [HttpGet]
        public ActionResult Ispis()
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            
            IspisVM vm = new IspisVM();

            vm.Mjesto = new List<SelectListItem>();

            foreach (var mj in model.Mjesta)
            {
                vm.Mjesto.Add(new SelectListItem() {Value = mj.Id.ToString(), Text = mj.Mjesto});
            }

            
                vm.MjId = vm.Mjesto.FirstOrDefault(o=>o.Text == "Tisno")?.Value;


            vm.odDatuma = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd.MM.yyyy.");
            int brojDanaUTrenutnomMjesecu = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            vm.doDatuma = new DateTime(DateTime.Now.Year, DateTime.Now.Month, brojDanaUTrenutnomMjesecu).ToString("dd.MM.yyyy.");

            //vm.odDatuma = "1.1.2017.";
            //vm.doDatuma = "31.12.2018.";
           

            return View (vm);

        }


        [HttpPost]
        public ActionResult Ispis (IspisVM vm)
        {

            DateTime od, doDat;


            ZUPATISNOEntities model = new ZUPATISNOEntities();

            if (!DateTime.TryParse(vm.odDatuma, out od))
                ModelState.AddModelError("odDatuma", "Neisparavan datum");

            if (!DateTime.TryParse(vm.doDatuma, out doDat))
                ModelState.AddModelError("doDatuma", "Neisparavan datum");

            if (!ModelState.IsValid)
                return View(vm);

            int id = int.Parse(vm.MjId);

            var det = model.Detalji.Where(d => d.VrijemePocetka >= od && d.VrijemeZavrsetka <= doDat && d.MjestoID == id);

             
          



            var rezulatVM = new IspisRezultatVM();
            rezulatVM.Detalji = new List<DetaljiVM>();

            var cultureInfo = new CultureInfo("hr-HR");

            

            // --------------------------
            if (vm.NaruciteljID.HasValue)
            {
                var detnar = model.Detalji.Where(d => d.VrijemePocetka >= od && d.VrijemeZavrsetka <= doDat && d.MjestoID == id && d.NaruciteljID ==vm.NaruciteljID);

                // s naruciteljem
                var nar = model.Osobe.First(o => o.Id == vm.NaruciteljID.Value);
                rezulatVM.Naslov = nar.Ime.ToUpper() + " " + nar.Prezime.ToUpper();

                foreach (var d in detnar.OrderBy(o => o.VrijemePocetka))
                {
                    var pod = new DetaljiVM()
                    {
                        OsobaPunoIme = d.NaruciteljID.HasValue ? "(" + d.Osobe.Ime + " " + d.Osobe.Prezime +")" : "",
                        MjestoNaziv = d.Mjesta.Mjesto,
                        EventStartTimeStamp = d.VrijemePocetka.ToString ("ddd, dd.MM.yyyy.").ToUpper(),
                        ZaPokojne = d.VrstaId == 2,
                        Opis = d.Opis.ToUpper()
                    };
                    
                    rezulatVM.Detalji.Add(pod);
                }

                return View("IspisNarucitelj", rezulatVM);
            }


            else
            {
                // Bez narucitelja
                if (od.Year == doDat.Year && od.Month == doDat.Month)
                    //Ako je isti mjesec
                    //   rezulatVM.Naslov = string.Format("{0} {1}.", od.ToString("MMMM"), doDat.Year).ToUpper();
                    rezulatVM.Naslov = string.Format("{0} {1}.", " Misne nakane za " + od.ToString("MMMM"), doDat.Year).ToUpper();

                else
                    // Od datuma do datuma
                   // rezulatVM.Naslov = string.Format("{0} - {1}", od.ToString("dd.MM.yyyy."), doDat.ToString("dd.MM.yyyy."));
                rezulatVM.Naslov = string.Format("{0}", "Misne Nakane").ToUpper();


                foreach (var d in det.OrderBy(o => o.VrijemePocetka))
                {
                    var pod = new DetaljiVM()
                    {
                        OsobaPunoIme = d.NaruciteljID.HasValue ? "(" + d.Osobe.Ime + " " + d.Osobe.Prezime + ")" : "",
                        MjestoNaziv = d.Mjesta.Mjesto,

                        //EventStartTimeStamp = d.VrijemePocetka.Day + ". " + cultureInfo.DateTimeFormat.GetDayName(d.VrijemePocetka.DayOfWeek).ToUpper(),
                        //EventStartTimeStamp = d.VrijemePocetka.Day +  ". " + cultureInfo.DateTimeFormat.GetDayName(d.VrijemePocetka.DayOfWeek).ToUpper(),
                        //EventStartTimeStamp = d.VrijemePocetka.ToString("dd, dd.MM.yyyy."),
                        EventStartTimeStamp =  cultureInfo.DateTimeFormat.GetDayName(d.VrijemePocetka.DayOfWeek).ToUpperInvariant() + "," + d.VrijemePocetka.ToShortDateString(),
                        ZaPokojne = d.VrstaId == 2,
                        Opis = d.Opis.ToUpper()
                    };
                   
                    rezulatVM.Detalji.Add(pod);
                }


          

                return View("IspisRezultat", rezulatVM);
            }


           



        }

                   
        [HttpGet]

        public ActionResult Brisi(int id)
        {

            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var det = model.Detalji.FirstOrDefault(m => m.Id == id);

            if (det == null)
                throw new ApplicationException("Misa ne postoji u bazi");

            model.Detalji.Remove(det);


            try
            {
                model.SaveChanges();
            }
            catch (Exception e)
            {

                throw new ApplicationException("Greška pri brisanju mise", e);
            }





            return RedirectToAction("Index");



           

        }


        [HttpPost]
        public ActionResult Brisi(DetaljiEM vm)
        {




            return RedirectToAction("Index");
        }

    }
}
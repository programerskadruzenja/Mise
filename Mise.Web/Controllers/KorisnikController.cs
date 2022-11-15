using Mise.Model;
using Mise.Web.ViewModels.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.Controllers
{
    public class KorisnikController : ControllerBase
    {
        public int KorisnikVM { get; private set; }

        public ActionResult Index()
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            List<KorisnikVM> Kor = new List<KorisnikVM>();

            foreach (var k in model.Korisnik)
            {
                KorisnikVM vm = new KorisnikVM()
                {
                    ID = k.Id,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    KorIme = k.KorisnickoIme

                };

                Kor.Add(vm);

            }

            

            return View (Kor);
        }

      


        // GET: Korisnik
        [AllowAnonymous]
        public ActionResult Prijava()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Prijava(PrijavaVM vm)
        {
            ZUPATISNOEntities model = new ZUPATISNOEntities();

            var lozinkaZaBazu = "";
            if (vm.Lozinka != null)
            {
                lozinkaZaBazu = kriptiraj(vm.Lozinka);
            }
            else
            {
                ModelState.AddModelError("", "Unesite podatke za logiranje");
                return View(vm);
            }

            var korisnik = model.Korisnik.FirstOrDefault(k => k.KorisnickoIme == vm.KorisnickoIme && k.Lozinka == lozinkaZaBazu);

            if (korisnik == null)
            {
                ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka");

                return View(vm);
            }

            var principal = new GenericPrincipal(new GenericIdentity(vm.KorisnickoIme), new[] { "Administrator" });
            HttpContext.User = principal;
            Thread.CurrentPrincipal = principal;

            Session["Korisnik"] = principal;

            return RedirectToAction("Index", "Kalendar");
        }


        private string kriptiraj (string lozinka)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider provider = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] input = System.Text.ASCIIEncoding.ASCII.GetBytes(lozinka);

            byte[] output = provider.ComputeHash(input);

            return Convert.ToBase64String(output);
        }


        public ActionResult Odjava()
        {
            Session.Clear();

            return RedirectToAction("Prijava");
        }


       
    }





}
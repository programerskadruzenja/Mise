using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Korisnik
{
    public class KorisnikVM
    {
        public int ID {get; set; }
        [DisplayName("Korisničko ime")]
        public string KorIme{ get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

    }
}
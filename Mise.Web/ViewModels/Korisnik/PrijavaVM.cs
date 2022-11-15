using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Korisnik
{
    public class PrijavaVM
    {

        public int Id { get; set; }
        [Display (Name ="Korisničko ime")]

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }



    }
}
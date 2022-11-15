using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Osobe
{
    public class OsobaVM
    {

        public int Id { get; set; }
        
        [Display(Name ="Redni broj")]
        public string red { get; set; }
        [Required(ErrorMessage = "Unesite Ime")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite Prezime")]
        public string Prezime { get; set; }
        public string Opaska { get; set; }

        //[Required(ErrorMessage = "Unesite OIB")]
        public string OIB { get; set; }

        public string Adresa { get; set; }
        [Required(ErrorMessage = "Unesite broj telefona")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite broj mobitela")]
        public string Mobitel { get; set; }
        public string Email { get; set; }
        public string Slika { get; set; }
        public HttpPostedFileBase NovaSlika { get; set; }
        public bool DaNe { get; set; }
        public string Iznos { get; set; }

        public bool Unesi { get; set; }
        public string PunoIme { get; set; }
    }
}
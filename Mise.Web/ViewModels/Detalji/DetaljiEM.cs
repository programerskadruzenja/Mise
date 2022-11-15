using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.ViewModels.Detalji
{
    public class DetaljiEM
    {
        public int? ID { get; set; }
        [Display(Name = "Nakana")]
        public string Opis { get; set; }
        [Display(Name = "Vrijeme početka")]
        public string VrijemePocetka { get; set; }

        [Display(Name = "Vrijeme završetka")]
        public string VrijemeZavrsetka { get; set; }
        public string Datum { get; set; }
        public bool DaNe { get; set; }
        public string Placeno { get; set; }
        [Display (Name ="Vrijeme upisa")]
        public string VrijemeUpisa { get; set; }
        public int? NaruciteljID { get; set; }
        [Display (Name ="Osoba")]
        public string OsobaImeIPrezime {get;set;}
        public bool KolizijaPotvrdena { get; set; }
        [Display (Name ="Mjesto")]
        public string MjestoID { get; set; }
        [Display (Name ="Vrsta")]
        public string VrstaId { get; set; }

        public List<SelectListItem> Mjesta { get; set; }
        public List<SelectListItem> Vrste { get; set; }
        public List<KolizijaVM> Kolizije { get; set; }

    }
}
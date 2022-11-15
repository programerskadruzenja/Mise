using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.ViewModels.Rezervirane
{
    public class RezerviranaMisaEM
    {
        public int ID { get; set; }
        public string Datum { get; set; }
        [Required(ErrorMessage = "Obavezan unos")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Obavezan unos")]
        [Display(Name = "Nakana")]
        public string Naziv { get; set; }
        [Required(AllowEmptyStrings =false)]
        public int? NaruciteljId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Narucitelji { get; set; }
        [Display (Name ="Vrsta")]
        public string VrstaID { get; set; }
        public string Vrsta { get; set; }
        public List<SelectListItem> Vrste { get; set; }





    }
}
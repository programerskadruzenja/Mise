using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Rezervirane
{
    public class RezerviranaMisaVM
    {
        public int? ID { get; set; }
        [Display(Name = "Nakana")]
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Vrsta { get; set; }
        [Display (Name ="Br.")]
        public string br { get; set; }
        public string Narucitelj { get; set; }


    }
}
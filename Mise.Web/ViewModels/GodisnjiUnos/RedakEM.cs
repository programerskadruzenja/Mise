using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.GodisnjiUnos
{
    public class RedakEM
    {

        public int RedniBroj { get; set; }
        public int RezerviranaMisaID { get; set; }
        [Required (ErrorMessage ="Unesite datum")]
        public string Datum { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public List<MisaEM> Mise { get; set; }
        //public bool Pogreska { get; set; }




    }
}
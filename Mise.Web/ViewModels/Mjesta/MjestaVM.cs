using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Mjesta
{
    public class MjestaVM
    {
        public int Id { get; set; }
        [Display(Name = "Naziv")]
        public string Mjesto { get; set; }

        [Display(Name = "Preferirani termin")]
        public string PreferiraniTermin { get; set; }

    }
}
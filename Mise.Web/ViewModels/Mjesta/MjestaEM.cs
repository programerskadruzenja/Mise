using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.ViewModels.Mjesta
{
    public class MjestaEM
    {

        public int Id { get; set; }
        [Display(Name = "Naziv mjesta")]
        public string Mjesto { get; set; }

        [Display(Name = "Preferirani termin")]
        public string PreferiraniTermin { get; set; }


        public string PreferiraniTerminId { get; set; }
        public List<SelectListItem> DostupniTermini { get; set; }
        
        //public virtual ICollection<Detalji> Detalji {get; set; }
        //public virtual Termin Termin { get; set; }



    }
}
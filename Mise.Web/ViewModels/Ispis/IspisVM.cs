using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mise.Web.ViewModels.Mjesta;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mise.Web.ViewModels.Ispis
{
    public class IspisVM
    {
        public string odDatuma { get; set; }
        public string doDatuma { get; set; }

        public string Narucitelj { get; set; }
        public int? NaruciteljID { get; set; }

        [Display(Name = "Mjesto")]
        public string MjId { get; set; }

        public List <SelectListItem> Mjesto { get; set; }


    }
}
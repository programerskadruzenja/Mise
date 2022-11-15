using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mise.Web.ViewModels
{
    public class UnosGodineVM
    {
        [Required(ErrorMessage ="Obavezno unesite godinu" )]
        public string Godina { get; set; }
            
        public List<SelectListItem> Godine { get; set; }


    }
}
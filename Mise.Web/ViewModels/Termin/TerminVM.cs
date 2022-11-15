using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Termin
{
    public class TerminVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Naziv je obavezan")]
        public string Naziv { get; set; }
        [Required (ErrorMessage ="Upišite vrijeme")]
        public string Vrijeme { get; set; }

    }
}
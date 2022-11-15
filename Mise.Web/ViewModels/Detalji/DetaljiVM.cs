using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Detalji
{
    public class DetaljiVM
    {
        public long EventID { get; set; }
        public string EventTitle { get; set; } 
        public bool EventIsAllDay { get; set; }
        public string EventStartTimeStamp { get;set; }
        public string EventEndTimeStamp { get; set; }
        public string EventColorValue { get; set; }
        public bool EventColorIsDark { get; set; }
        [Display (Name = "Mjesto")]
        public int? MjestoID { get; set; }
        public int? NaruciteljID { get;  set; }
        public string MjestoNaziv { get; set; }
        [Display(Name = "osoba")]
        public string OsobaPunoIme { get; set; }
        public bool ZaPokojne { get; set; }
        [Display(Name = "Nakana")]
        public string Opis { get; set; }


    }
}
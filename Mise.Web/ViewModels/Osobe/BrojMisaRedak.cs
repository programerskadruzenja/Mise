using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.Osobe
{
    public class BrojMisaRedak
    {
        public Nullable<int> NaruciteljID { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public int Broj { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mise.Web.ViewModels.GodisnjiUnos
{
    public class GodisnjiUnosEM
    {
        public string Godina { get; set; }
        // Lista naziva mjesta koji će se prikazivati u headeru tablice
        public List<string> Mjesta { get; set; }
        // Lista rezerviranih misa. Svaki redak ima element mi
        public List<RedakEM> Retci { get; set; }
        public bool SadrziPogresku { get; set; }


    }
}
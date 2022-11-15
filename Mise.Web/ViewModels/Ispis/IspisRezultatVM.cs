using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mise.Web.ViewModels.Detalji;
namespace Mise.Web.ViewModels.Ispis
{
    public class IspisRezultatVM
    {
        public string Naslov { get; set; }

        public List<DetaljiVM> Detalji { get; set; }



    }
}
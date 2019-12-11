using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking.ViewModels
{
    public class RacunUplatniceViewModel
    {
        public Racun Racun { get; set; }
        public IEnumerable<Uplatnica> UplatniceRacuna { get; set; }
        public Uplatnica Uplatnica { get; set; }
    }
}
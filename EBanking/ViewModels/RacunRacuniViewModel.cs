using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking.ViewModels
{
    public class RacunRacuniViewModel
    {
        public Racun Racun { get; set; }
        public IEnumerable<Racun> Racuni { get; set; }
    }
}
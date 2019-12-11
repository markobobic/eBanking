using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking.ViewModels
{
    public class UplatnicaUplatniceViewModel
    {
        public Uplatnica Uplatnica { get; set; }
        public IEnumerable<Uplatnica> Uplatnice { get; set; }
    }
}
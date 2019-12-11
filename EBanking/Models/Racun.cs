using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBanking.Models
{
    public class Racun
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:150,MinimumLength =3,ErrorMessage ="Nosilac racuna mora imati najmanje 3 karaktera a najvise 150")]
        public string NosilacRacuna { get; set; }

        [Required]
        [StringLength(maximumLength:100,MinimumLength =10,ErrorMessage ="Broja racuna mora imati najmanje 10 karaktera a najvise 100")]
        public string BrojRacuna { get; set; }

        public bool Aktivan { get; set; }
        public bool OnlineBanking { get; set; }

        public decimal UkupnoStanje { get; set; }

        public decimal? IznosOd { get; set; }

        public decimal? IznosDo { get; set; }

        public Racun()
        {
            Aktivan = false;
            OnlineBanking = false;
        }
    }
}
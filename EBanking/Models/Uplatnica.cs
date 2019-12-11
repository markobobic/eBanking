using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBanking.Models
{
    public class Uplatnica
    {
        public int Id { get; set; }

        public Racun Racun { get; set; }

        [Required]
        [Range(minimum:double.MinValue,maximum:double.MaxValue)]
        public decimal? IznosUplate { get; set; }

        public DateTime DatumPrometa { get; set; }

        [Required]
        [StringLength(maximumLength:200,MinimumLength =3,ErrorMessage ="Svrha uplate mora da ima najmanje 3 karaktera a najvise 200")]
        public string SvrhaUplate { get; set; }

        [Required]
        [StringLength(maximumLength:200,MinimumLength =3,ErrorMessage ="Uplatilac mora da ima najmanje 3 karaktera a najvise 200")]
        public string Uplatilac { get; set; }

        public bool Hitno { get; set; }

        public Uplatnica()
        {
            DatumPrometa = DateTime.Now;
            Hitno = false;
        }
    }
}
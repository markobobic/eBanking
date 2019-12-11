using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking.Repository
{
    public interface IRacunRepo
    {
        IEnumerable<Racun> GetAll();
        Racun GetById(int id);
        int Create(Racun racun);
        bool Delete(int id);
        bool Update(Racun racun);
        bool Deactivate(int id);
        bool Activate(int id);
        decimal GetSum(int id);
    }
}
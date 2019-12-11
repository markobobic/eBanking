using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking.Repository
{
    public interface IUplatnicaRepo
    {
        IEnumerable<Uplatnica> GetByRacunId(int id);
        IEnumerable<Uplatnica> GetAll();
        Uplatnica GetById(int id);
        int Create(Uplatnica uplatnica);
        bool Delete(int id);
        bool Update(Uplatnica uplatnica);
    }
}
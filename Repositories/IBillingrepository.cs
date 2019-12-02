using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public interface IBillingRepository
    {
        IQueryable<Billing> GetBillings { get; }

        void Add(Billing billing);
        void Remove(Billing billing);
        Billing GetBillingById(int id);
    }
}
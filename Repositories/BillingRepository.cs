using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        ApplicationDbContext _context;

        public BillingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Billing> GetBillings { get { return _context.Billings; } }

        public void Remove(Billing billing) { _context.Billings.Remove(billing); }

        public void Add(Billing billing) { _context.Billings.Add(billing); }

        public Billing GetBillingById(int id) { return _context.Billings.Where(b => b.Id == id).SingleOrDefault(); }
    }
}
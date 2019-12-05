using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        ApplicationDbContext _context;

        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetOrders { get { return _context.Orders; } }

        public void Remove(Order billing) { _context.Orders.Remove(billing); }

        public void Add(Order billing) { _context.Orders.Add(billing); }

        public Order GetOrderById(int id) { return _context.Orders.Where(b => b.Id == id).SingleOrDefault(); }
        public IQueryable<Order> GetOrdersByProductId(int id) { return _context.Orders.Where(b => b.ProductId == id); }
        public IQueryable<Order> GetOrdersByCustomerid(int id) { return _context.Orders.Where(b => b.CustomerId == id); }
    }
}
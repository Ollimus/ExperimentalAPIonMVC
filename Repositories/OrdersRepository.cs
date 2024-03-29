﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;
using System.Data.Entity;

namespace TestAPI.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        ApplicationDbContext _context;

        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetOrders { get { return _context.Orders.Include(g => g.Customer).Include(g => g.Product); } }

        public void Remove(Order order) { _context.Orders.Remove(order); }

        public void Add(Order order) { _context.Orders.Add(order); }

        public Order GetOrderById(int id) { return _context.Orders.Include(c => c.Customer).Include(p => p.Product).Where(b => b.Id == id).SingleOrDefault(); }
        public IQueryable<Order> GetOrdersByProductId(int id) { return _context.Orders.Where(b => b.ProductId == id); }
        public IQueryable<Order> GetOrdersByCustomerid(int id) { return _context.Orders.Where(b => b.CustomerId == id); }
        public Decimal CalculateTotalOrderValue(Product product, int amount) { return product.Price * amount; }
    }
}
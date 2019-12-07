using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public interface IOrdersRepository
    {
        IQueryable<Order> GetOrders { get; }

        void Add(Order order);
        void Remove(Order order);
        Order GetOrderById(int id);
        IQueryable<Order> GetOrdersByProductId(int id);
        IQueryable<Order> GetOrdersByCustomerid(int id);

        Decimal CalculateTotalOrderValue(Product product, int amount);
    }
}
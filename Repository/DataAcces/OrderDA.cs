using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class OrderDA(AppDbContext _db) : IOrderDA
    {
        public IQueryable<Order> GetOrders()
        {
            return _db.Orders.AsQueryable();
        }
    }
}
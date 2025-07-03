using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Repository.DataAcces
{
    public class UserDA(AppDbContext _db): IUserDa
    {
        public IQueryable<DeliveryAddress> GetDeliveryAdresses()
        {
            return _db.DeliveryAddresses.AsQueryable();
        }

        public IQueryable<User> GetUsers()
        {
            return _db.Users.AsQueryable();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
    public interface IUserService
    {
        public IQueryable<User> GetUsers();
        public IQueryable<DeliveryAddress> GetDeliveryAdresses();
        public Task<User> ToggleUser(Guid id, bool active);
        public Task<User> ChangeUserRole(Guid id, UserRole role);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
    public class UserQuery(IUserService _service)
    {
        public IQueryable<User> GetUsers()=>_service.GetUsers();
        public IQueryable<DeliveryAddress> GetDeliveryAdresses()=>_service.GetDeliveryAdresses();
        
    }
}
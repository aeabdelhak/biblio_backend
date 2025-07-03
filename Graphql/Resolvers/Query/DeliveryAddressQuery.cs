using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Graphql.Resolvers.Query
{
    public class DeliveryAddressQuery(IDeliveryAddressService _service)
    {
        
        public IQueryable<DeliveryAddress> GetAddresses()=> _service.GetAddresses();
    }
}
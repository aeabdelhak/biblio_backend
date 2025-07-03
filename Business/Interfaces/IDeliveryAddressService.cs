using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
    public interface IDeliveryAddressService
    {
        public IQueryable<DeliveryAddress> GetAddresses();
        public Task<DeliveryAddress> NewAddress(AddressInput input);
        public Task<DeliveryAddress> UpdateAddress(Guid id,AddressInput input);
        public Task<BaseReturnEnum> RemoveAddress(Guid id);
        
    }
}
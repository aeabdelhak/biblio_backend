using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
    public class DeliveryAddressMutation(IDeliveryAddressService _service)
    {
        public Task<DeliveryAddress> NewAddress(AddressInput input)=>_service.NewAddress(input);
        public Task<DeliveryAddress> UpdateAddress(Guid id, AddressInput input)=>_service.UpdateAddress(id,input);
        public Task<BaseReturnEnum> RemoveAddress(Guid id)=>_service.RemoveAddress(id);
    }
}

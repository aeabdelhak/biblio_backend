using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.helpers;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
	public class DeliveryAddressService(
		ICommonDA _commonDA,
		IDeliveryAddressDA _DA,AuthHelper _auth) : IDeliveryAddressService
	{
		public IQueryable<DeliveryAddress> GetAddresses()
		{
			var auth = _auth.GetAuthClaims();
			return _DA.GetAddresses().Where(e => e.UserId == auth.UserId);
		}

		public async Task<DeliveryAddress> NewAddress(AddressInput input)
		{
			var auth = _auth.GetAuthClaims();
			
			var address = new DeliveryAddress
			{
				Address=input.Address,
				City = input.City,
				UserId = auth.UserId,
				ZipCode = input.ZipCode,
			};
			await  _commonDA.DbInsert(address);
			return address;
		}

		public async Task<BaseReturnEnum> RemoveAddress(Guid id)
		{
			var auth = _auth.GetAuthClaims();
			var address =await _DA.GetAddresses().FirstOrDefaultAsync(e => e.UserId == auth.UserId && e.Id == id);
			if (address is null)
				return BaseReturnEnum.NotFound;
			await _commonDA.DbDelete(address);
				return BaseReturnEnum.Success;
			

		}

		public async Task<DeliveryAddress> UpdateAddress(Guid id, AddressInput input)
		{
			var auth = _auth.GetAuthClaims();
			var address =await _DA.GetAddresses().SingleAsync(e => e.UserId == auth.UserId && e.Id == id);
			address.Address = input.Address;
			address.City = input.City;
			address.ZipCode = input.ZipCode;
			await _commonDA.DbUpdate(address);
			return address;
			 
		}
	}
}
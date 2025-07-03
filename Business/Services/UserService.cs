using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.helpers;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
	public class UserService(IUserDa _userDa, ICommonDA _commonDA, AuthHelper authHelper)
		: IUserService
	{
		public async Task<User> ChangeUserRole(Guid id, UserRole role)
		{
			var user = await _userDa.GetUsers().SingleAsync(e => e.Id == id);
			user.Role = role;
			await _commonDA.DbUpdate(user);
			return user;
		}

        public IQueryable<DeliveryAddress> GetDeliveryAdresses()
        {
		return	_userDa.GetDeliveryAdresses().Where(e => e.UserId == authHelper.GetAuthClaims().UserId); 
        }

        public IQueryable<User> GetUsers()
		{
			return _userDa.GetUsers().Where(e=>e.Role != UserRole.SuperAdmin)
			.OrderBy(e => e.Role).ThenByDescending(e => e.CreatedAt);
		}

		public async Task<User> ToggleUser(Guid id, bool active)
		{
			var role = authHelper.GetAuthClaims().Role;
			var user = await _userDa.GetUsers().SingleAsync(e => e.Id == id);
			if (role != UserRole.SuperAdmin && user.Role != UserRole.User)
				throw new Exception();
			user.IsActive = active;
			await _commonDA.DbUpdate(user);
			return user;
		}
	}
}

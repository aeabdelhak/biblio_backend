using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
	public class UserMutation(IUserService _service)
	{   
		public Task<User> ToggleUser(Guid id, bool active)=>_service.ToggleUser(id,active);
		public Task<User> ChangeUserRole(Guid id, UserRole role)=>_service.ChangeUserRole(id,role);
		
	}
}
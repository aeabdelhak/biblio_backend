using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
	public class AuthQuery(IAuthService authService)
	{
		public Task<User> CurrentUser() => authService.CurrentUser();
		
	}
}
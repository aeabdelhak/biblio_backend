using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
	public class AuthMutation(IAuthService authService)
	{
		public Task<GenericResponse<BaseReturnEnum, AuthOutput>> Authenticate(string email, string password)
		{
			return authService.Authenticate(email,password);
		}

		public Task<GenericResponse<BaseReturnEnum, AuthOutput>> Register(NewUserInput input)
		{
			return authService.Register(input);
		}
		public Task< AuthOutput> RenewAccessToken(string refToken)
		{
			return authService.RenewAccessToken(refToken);
		}
		
	}
}
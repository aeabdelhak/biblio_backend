using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface IAuthService
	{
		public Task<GenericResponse<BaseReturnEnum,AuthOutput>> Register(NewUserInput input);
		public Task<GenericResponse<BaseReturnEnum,AuthOutput>> Authenticate(string email, string password);
		public Task<User> CurrentUser();
		public Task<AuthOutput> RenewAccessToken(string RefreshToken);

	}
}
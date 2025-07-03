using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
	public class AuthService(
		ICommonDA _commomDA,
		IUserDa _userDa,
		 AuthHelper _authHelper
		 ) : IAuthService
	{

		public async Task<AuthOutput> RenewAccessToken(string RefreshToken)
		{
			var response = new AuthOutput();
			var refresh = _authHelper.ValidateToken(RefreshToken);
			if (refresh is null || refresh.FindFirst(JwtRegisteredClaimNames.Typ)?.Value != "REFRESH")
				return response;
			var userId = Guid.Parse(refresh.FindFirst("USER_ID")?.Value!);
			var user = await _userDa.GetUsers().FirstOrDefaultAsync(e => e.Id == userId);
			var reftokenId = Guid.Parse(refresh.FindFirst(JwtRegisteredClaimNames.Jti)!.Value.ToString());
			if (user is null) return response;
			string exp = refresh.FindFirst(JwtRegisteredClaimNames.Exp)!.Value;
			var TimeStampExpirationDate = int.Parse(exp);
			DateTime DateTimeExpirationDate = Parsers.TimeStampToDateTime(TimeStampExpirationDate);
			if (DateTimeExpirationDate < DateTime.Now.AddDays(1))
				response.RefreshToken = _authHelper.GenerateRefreshToken(user, reftokenId);
			else response.RefreshToken = RefreshToken;
			response.AccessToken = _authHelper.GenerateAccessToken(user, reftokenId);
			return response;
		}
		public async Task<GenericResponse<BaseReturnEnum, AuthOutput>> Authenticate(string email, string password)
		{
			var response = new GenericResponse<BaseReturnEnum, AuthOutput>();
			var user = await _userDa.GetUsers().SingleOrDefaultAsync(e => e.Email.Equals(email.Trim(), StringComparison.CurrentCultureIgnoreCase));
			if (user is null)
			{
				response.Status = BaseReturnEnum.WrongData;
				return response;
			}

			var isPassword = _authHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

			if (!isPassword)
			{
				response.Status = BaseReturnEnum.WrongData;
				return response;
			}
			var refId = Guid.NewGuid();
			response.Data = new AuthOutput
			{
				AccessToken = _authHelper.GenerateAccessToken(user, refId),
				RefreshToken = _authHelper.GenerateRefreshToken(user, refId),

			};
			response.Status = BaseReturnEnum.Success;
			return response;
		}

		public Task<User> CurrentUser()
		{
			var userId = _authHelper.GetAuthClaims().UserId;
			return _userDa.GetUsers().FirstAsync(e => e.Id == userId);
		}
		public async Task<GenericResponse<BaseReturnEnum, AuthOutput>> Register(NewUserInput input)
		{
			var response = new GenericResponse<BaseReturnEnum, AuthOutput>();
			var exist = await _userDa.GetUsers().SingleOrDefaultAsync(e => e.Email.Equals(input.Email.Trim(), StringComparison.CurrentCultureIgnoreCase));
			if (exist is not null)
			{
				response.Status = BaseReturnEnum.Conflict;
				return response;
			}

			var hash = _authHelper.HashPasword(input.Password, out byte[] salt);
			var user = new User
			{
				FirstName = input.FirstName,
				LastName = input.LastName,
				DisplayName = input.LastName + " " + input.FirstName,
				Email = input.Email,
				PasswordHash = hash,
				PasswordSalt = salt
			};
			await _commomDA.DbInsert(user);
			var refId = Guid.NewGuid();
			response.Data = new AuthOutput
			{
				AccessToken = _authHelper.GenerateAccessToken(user, refId),
				RefreshToken = _authHelper.GenerateRefreshToken(user, refId),

			};
			response.Status = BaseReturnEnum.Success;
			return response;

		}
	}
}
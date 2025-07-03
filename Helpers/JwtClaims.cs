
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.helpers
{
	public class JwtClaims
	{
		public Guid UserId { get; set; }
		public Guid RefTokenId { get; set; }
		public required string DisplayName { get; set; }
		public UserRole Role { get; set; }

	}
}

namespace BiblioPfe.Infrastructure.Entities
{
	public class User: BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DisplayName {  get; set; }
		public string Email { get; set; }
		[GraphQLIgnore]
		public string? EmailVerifyToken { get; set; }
		[GraphQLIgnore]
		public string? PasswordResetToken { get; set; }
		[GraphQLIgnore]
		public DateTime? EmailVerifiedAt { get; set; }
		[GraphQLIgnore]
		public string PasswordHash { get; set; }
		[GraphQLIgnore]
		public byte[] PasswordSalt { get; set; }
		public Guid? AvatarId { get; set; }
		public Document? Avatar { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
		public List<DeliveryAddress> Addresses { get; set; }
		public List<Order> Orders { get; set; }
		public bool IsActive { get; set; } = true;
		[GraphQLIgnore]
		public List<OrderStatus> MadeStatusChanges { get; set; } 
		[GraphQLIgnore]
		public List<BookReview> BookReviews { get; set; } 
		
	}

	public enum UserRole
	{
		SuperAdmin,
		Admin,
		User,
	}
}

using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Repository.Interfaces
{
	public interface IUserDa
	{
		public IQueryable<User> GetUsers();
        public IQueryable<DeliveryAddress> GetDeliveryAdresses();
		
		
	}
}

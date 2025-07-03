using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class CustomClient : BaseEntity
	{
	 public string FirstName { get; set; }   
	 public string LastName { get; set; }   
	 public string PhoneNumber { get; set; }   
	 public string? City { get; set; }   
	 public string? Address { get; set; }   
	 public Guid OrderId { get; set; }
	 public Order Order { get; set; }
	}
}
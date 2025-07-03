using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Common.Input
{
	public class NewOrderInput
	{
		public OrderType Type { get; set; } = OrderType.Selling;
		public string FirstName { get; set; }   
	 	public string LastName { get; set; }   
	 	public string PhoneNumber { get; set; }   
	 	public string? Address { get; set; }   
	}
}
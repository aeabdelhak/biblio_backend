using System;
using System.Collections.Generic;
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
	public class OrderService(
		AuthHelper _authHelper,
		IUserService _userService,
		ICommonDA _commonDA,
		IOrderDA _orderDA,
		IBookServices _bookService,
		IRulesService _rules
	) : IOrderService
	{
		public async Task<Order>
		NewSellingOrder()
		{
			var order = new Order
			{
				Statuses = [new OrderStatus { Status = OrderStatusEnum.Draft, IsCurrent = true, }],
				Details = []
			};
			await _commonDA.DbInsert(order);
			return order;
		}

		static int DaysBetween(DateTime secondDate)
		{
			DateTime date1 = DateTime.Now;
			TimeSpan diff = secondDate - date1;
			return (int)Math.Ceiling(diff.TotalDays);
		}

		public async Task<Order?> GetSellingOrderDraftMode(Guid id)
		{
			var order = await _orderDA
				.GetOrders()
				.Include(e => e.Details)
				.FirstOrDefaultAsync(e =>
					e.Id == id
					&& e.Statuses.Any(e => e.IsCurrent && e.Status == OrderStatusEnum.Draft)
				);

			return order;
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> SiteRemoveFromCart(
			Guid orderId,
			Guid detailId
		)
		{
			var order = await GetSellingOrderDraftMode(orderId);
			var response = new GenericResponse<BaseReturnEnum, Order>
			{
				Data = order,
				Status = BaseReturnEnum.NotFound
			};
			if (order is null)
				return response;
			var exist = order.Details.FirstOrDefault(e => e.Id == detailId);
			if (exist is null)
				return response;

			await _commonDA.DbDelete(exist);

			response.Data = await RecalcutateOrderPrices(order.Id);
			response.Status = BaseReturnEnum.Success;
			return response;
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> SiteAddToCart(
			SellingDetailInput input
		)
		{
			var response = new GenericResponse<BaseReturnEnum, Order>
			{
				Status = BaseReturnEnum.NotFound
			};
			var book = await _bookService.GetBookInernly(input.BookId);
			if (book is null)
				return response;

			var isForSelling = book.Options.Any(e =>
				e.IsActive && e.Option == BookOrderOptionEnum.Selling
			);

			if (!isForSelling)
			{
				response.Status = BaseReturnEnum.NotAcceptable;

				return response;
			}

			var order = input.OrderId is null
				? await NewSellingOrder()
				: await GetSellingOrderDraftMode((Guid)input.OrderId) ?? await NewSellingOrder();

			var existOnOrder = order.Details.Any(e => e.BookId == book.Id);
			if (existOnOrder)
			{
				order.Details = order
					.Details.Select(e =>
					{
						var sellingdetails = book.Options.First(e =>
							e.Option == BookOrderOptionEnum.Selling
						);
						e.Price = e.BookId == book.Id ? sellingdetails.Price : e.Price;
						e.Quantity =
							e.BookId == book.Id
								? e.Quantity + input.Quantity > 10
									? 10
									: e.Quantity + input.Quantity
								: e.Quantity;
						return e;
					})
					.ToList();
				await _commonDA.DbUpdate(order.Details);
			}
			else
			{
				var sellingdetails = book.Options.First(e =>
					e.Option == BookOrderOptionEnum.Selling
				);
				var detail = new OrderDetail
				{
					BookId = book.Id,
					OptionId = sellingdetails.Id,
					OrderId = order.Id,
					Price = sellingdetails.Price,
					Quantity = input.Quantity
				};
				await _commonDA.DbInsert(detail);
			}

			await RecalcutateOrderPrices(order.Id);
			response.Data = await SiteGetOrder(order.Id);
			response.Status = BaseReturnEnum.Success;
			return response;
		}

		private async Task<Order> RecalcutateOrderPrices(Guid id)
		{
			var order = await _orderDA
				.GetOrders()
				.Include(e => e.Details)
				.ThenInclude(e => e.Book.Cover)
				.Include(e => e.Details)
				.ThenInclude(e => e.Option)
				.SingleAsync(e => e.Id == id);

			var rules = await _rules.GetPlatformRules();
			var shipping = rules.ShippingFees;
			var toUpdate = await _orderDA.GetOrders().FirstAsync(e => e.Id == id);
			toUpdate.DeliveryFees = order.DeliveringType == OrderDeliveringType.Delivery ? rules.ShippingFees : 0;
			toUpdate.TotalPrice = order.Details.Sum(e => e.Price * e.Quantity);
			toUpdate.TotalToGetPaid = order.TotalPrice + order.DeliveryFees;
			await _commonDA.DbUpdate(toUpdate);
			return order;
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> SiteUpdateCartDetail(
			Guid orderId,
			Guid detailId,
			int Quantity
		)
		{
			var order = await GetSellingOrderDraftMode(orderId);
			var response = new GenericResponse<BaseReturnEnum, Order>
			{
				Data = order,
				Status = BaseReturnEnum.NotFound
			};
			if (order is null)
				return response;
			var exist = order.Details.FirstOrDefault(e => e.Id == detailId);
			if (exist is null)
				return response;

			exist.Quantity = Quantity > 10 ? 10 : Quantity;

			await _commonDA.DbUpdate(exist);

			response.Data = await RecalcutateOrderPrices(order.Id);
			response.Status = BaseReturnEnum.Success;
			return response;
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> SiteSetCartDeliveryType(
			Guid orderId,
			OrderDeliveringType Type
		)
		{
			var order = await SiteGetOrder(orderId);
			var response = new GenericResponse<BaseReturnEnum, Order>
			{
				Data = order,
				Status = BaseReturnEnum.NotFound
			};
			if (order is null)
				return response;
			order.DeliveringType = Type;
			await _commonDA.DbUpdate(order);
			await RecalcutateOrderPrices(order.Id);

			response.Status = BaseReturnEnum.Success;
			response.Data = await SiteGetOrder(orderId);

			return response;
		}

		public Task<GenericResponse<BaseReturnEnum, Order>> PaymentMethod(
			PaymentMethode PaymentMethode
		)
		{
			throw new NotImplementedException();
		}

		public async Task<Order> NewOrder(NewOrderInput input)
		{
			var order = new Order
			{
				Type = input.Type,
				Number = await GetNewOrderNumber(),
				Client = new CustomClient
				{
					FirstName = input.FirstName,
					LastName = input.LastName,
					PhoneNumber=input.PhoneNumber,
					Address= input.Address
				},
				DeliveringType = OrderDeliveringType.PickUp,
				Statuses = [
						new OrderStatus
						{
							Status = OrderStatusEnum.Draft,
							IsCurrent=true,
							MadeById=_authHelper.GetAuthClaims().UserId
						}
					]

			};
			await _commonDA.DbInsert(order);
			return order;
		}

		public async Task<Order?> GetOrder(Guid id)
		{
			return await _orderDA.GetOrders()
			.Include(e => e.Address)
			.Include(e => e.Client)
			.Include(e => e.Statuses).ThenInclude(e => e.MadeBy.Avatar)
			.Include(e => e.Details).ThenInclude(e => e.Book.Cover)
			.Include(e => e.User).ThenInclude(e => e.Avatar)
			.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> AddToCart(DetailInput input)
		{
			try
			{
				var response = new GenericResponse<BaseReturnEnum, Order>
				{
					Status = BaseReturnEnum.NotFound
				};
				var book = await _bookService.GetBookInernly(input.BookId);
				var order = await GetSellingOrderDraftMode((Guid)input.OrderId);

				if (book is null || order is null)
					return response;



				var option = book.Options.First(e => e.Option.ToString() == order.Type.ToString());

				var existOnOrder = order.Details.FirstOrDefault(e => e.BookId == book.Id);
				if (existOnOrder is not null)
				{

					existOnOrder.Price = option.Price;
					existOnOrder.Quantity = existOnOrder.Quantity + input.Quantity > 10
									? 10
									: existOnOrder.Quantity + input.Quantity;
					await _commonDA.DbUpdate(existOnOrder);
				}
				else
				{

					await _commonDA.DbInsert(new OrderDetail
					{
						BookId = book.Id,
						OptionId = option.Id,
						OrderId = order.Id,
						Price = option.Price,
						Quantity = input.Quantity
					});
				}

				response.Data = await RecalcutateOrderPrices(order.Id);
				response.Status = BaseReturnEnum.Success;
				return response;
			}
			catch (Exception ex)
			{
				await _commonDA.DbInsert(new ErrorLog
				{
					Message = ex.ToString()
				});
				throw;
			}
		}
		public async Task<GenericResponse<BaseReturnEnum, Order>> RemoveFromCart(
		Guid orderId,
		Guid detailId
	)
		{
			var order = await GetSellingOrderDraftMode(orderId);
			var response = new GenericResponse<BaseReturnEnum, Order>
			{
				Data = order,
				Status = BaseReturnEnum.NotFound
			};
			if (order is null)
				return response;
			var exist = order.Details.FirstOrDefault(e => e.Id == detailId);
			if (exist is null)
				return response;

			await _commonDA.DbDelete(exist);

			response.Data = await RecalcutateOrderPrices(order.Id);
			response.Status = BaseReturnEnum.Success;
			return response;
		}



		public IQueryable<Order> GetOrders()
		{
			return _orderDA.GetOrders().Where(e => (
				e.Statuses.Any(e => e.Status != OrderStatusEnum.Draft && e.IsCurrent)
			))
			.Include(e => e.Details).ThenInclude(e => e.Book)
			.Include(e => e.Statuses.Where(e => e.IsCurrent))
			.Include(e => e.User.Avatar)
			.Include(e => e.Client)
			.OrderByDescending(e => e.CreatedAt)
			;
		}
		private async Task<int> GetNewOrderNumber()
		{
			var last = await _orderDA.GetOrders().OrderByDescending(e => e.Number).FirstOrDefaultAsync();
			if (last is not null)
				return last.Number is not null ? (int)(last.Number + 1) : 1;
			return 1;
		}

		public async Task<BaseReturnEnum> UpdateOrderStatus(Guid id,
		 OrderStatusEnum status)
		{
			var auth = _authHelper.GetAuthClaims();
			var order = await _orderDA.GetOrders()
			.Include(e => e.Statuses)
			.FirstOrDefaultAsync(e => e.Id == id);
			if (order is null)
				return BaseReturnEnum.NotFound;
			var currentStatus = order.Statuses.First(w => w.IsCurrent);

			if (status == OrderStatusEnum.Canceled && currentStatus.Status == OrderStatusEnum.Draft)
			{
				await _commonDA.DbDelete(order);
				return BaseReturnEnum.Deleted;
			}
			if (status == OrderStatusEnum.Canceled && order.PaymentMethode == PaymentMethode.Online)
			{
				return BaseReturnEnum.NotAcceptable;
			}
			if (status == OrderStatusEnum.Completed && !order.Paid)
			{
				return BaseReturnEnum.Unauthorized;
			}

			var newStatus = new OrderStatus
			{
				IsCurrent = true,
				OrderId = order.Id,
				MadeById = auth.UserId,
				Status = status
			};

			currentStatus.IsCurrent = false;
			await _commonDA.DbUpdate(currentStatus);
			await _commonDA.DbInsert(newStatus);
			return BaseReturnEnum.Success;




		}

		public async Task<BaseReturnEnum> BorrowNow(Guid orderId, DateTime returnDate)
		{
			var auth = _authHelper.GetAuthClaims();

			var order = await _orderDA.GetOrders()
								.Include(e => e.Details)
								.Include(e => e.Statuses.Where(e => e.IsCurrent))
								.FirstOrDefaultAsync(e => e.Id == orderId);
			if (order == null)
			{
				return BaseReturnEnum.NotFound;
			}
			if (DateTime.Now >= returnDate)
			{
				return BaseReturnEnum.WrongData;
			}
			var cstatus = order.Statuses.First();
			if (cstatus.Status != OrderStatusEnum.Confirmed)
			{
				return BaseReturnEnum.NotAcceptable;
			}
			var newStatus = new OrderStatus
			{
				IsCurrent = true,
				OrderId = order.Id,
				MadeById = auth.UserId,
				Status = OrderStatusEnum.Borrowing
			};
			cstatus.IsCurrent = false;
			await _commonDA.DbUpdate(cstatus);
			var norder = await _orderDA.GetOrders().FirstAsync(e => e.Id == orderId);
			norder.DueTime = returnDate;
			norder.TotalPrice = order.Details.Sum(e => e.Price * e.Quantity) * DaysBetween(returnDate);
			norder.TotalToGetPaid = order.Details.Sum(e => e.Price * e.Quantity) * DaysBetween(returnDate);
			await _commonDA.DbUpdate(norder);
			await _commonDA.DbUpdate(cstatus);
			await _commonDA.DbInsert(newStatus);
			return BaseReturnEnum.Success;

		}

		public async Task<Order> UpdatePaiment(Guid id, decimal Price, bool paid)
		{
			var order = await _orderDA.GetOrders().FirstAsync(e => e.Id == id);
			order.Paid = paid;
			order.TotalPaid = Price;
			await _commonDA.DbUpdate(order);
			return order;
		}

		public async Task<BaseReturnEnum> EndBorrow(Guid orderId, DateTime returnDate)
		{
			var auth = _authHelper.GetAuthClaims();

			var order = await _orderDA.GetOrders()
								.Include(e => e.Details)
								.Include(e => e.Statuses.Where(e => e.IsCurrent))
								.FirstOrDefaultAsync(e => e.Id == orderId);
			if (order == null)
			{
				return BaseReturnEnum.NotFound;
			}

			var cstatus = order.Statuses.First();
			if (cstatus.Status != OrderStatusEnum.Borrowing)
			{
				return BaseReturnEnum.NotAcceptable;
			}

			if (cstatus.CreatedAt >= returnDate)
			{
				return BaseReturnEnum.WrongData;
			}

			var newStatus = new OrderStatus
			{
				IsCurrent = true,
				OrderId = order.Id,
				MadeById = auth.UserId,
				Status = OrderStatusEnum.Returned
			};

			var newTotal = order.Details.Sum(e => e.Price * e.Quantity) * DaysBetween(returnDate);
			cstatus.IsCurrent = false;
			await _commonDA.DbUpdate(cstatus);

			var norder = await _orderDA.GetOrders().FirstAsync(e => e.Id == orderId);
			norder.ReturnDate = returnDate;
			norder.TotalPrice = newTotal > norder.TotalPrice ? newTotal : norder.TotalPrice;
			norder.TotalToGetPaid = newTotal > norder.TotalToGetPaid ? newTotal : norder.TotalToGetPaid;
			await _commonDA.DbUpdate(norder);
			await _commonDA.DbUpdate(cstatus);
			await _commonDA.DbInsert(newStatus);

			return BaseReturnEnum.Success;
		}

		public async Task<Order> UpdateDeliveringType(Guid id, OrderDeliveringType type)
		{
			var order = await _orderDA.GetOrders()
											.Include(e => e.Details)
											.Include(e => e.Statuses.Where(e => e.IsCurrent))
											.FirstAsync(e => e.Id == id);
			if (order.DeliveringType == type)
				return order;
			if (type == OrderDeliveringType.Delivery)
			{
				var DeliveryFees = (await _rules.GetPlatformRules()).ShippingFees;
				order.DeliveryFees = DeliveryFees;
				order.TotalToGetPaid += DeliveryFees;
			}
			else
			{
				order.TotalToGetPaid -= order.DeliveryFees;
				order.DeliveryFees = 0;
			}
			order.DeliveringType = type;
			await _commonDA.DbUpdate(order);

			return order;
		}

		public async Task<Order?> SiteGetOrder(Guid id)
		{
			var order = await _orderDA.GetOrders()
			.Include(e => e.Details).ThenInclude(e => e.Book).ThenInclude(e => e.BookAuthors).ThenInclude(e => e.Author)
			.Include(e => e.Details).ThenInclude(e => e.Book).ThenInclude(e => e.Cover).

			FirstOrDefaultAsync(e => e.Id == id)
			;
			if (order is null)
				return null;
			if (order.UserId is not null)
			{
				var auth = _authHelper.GetAuthClaims();
				if (auth.UserId != order.Id)
					return null;
			}
			return order;
		}

		public async Task<GenericResponse<BaseReturnEnum, Order>> SiteSetDeliveryAddress(Guid orderId, Guid? addressId, AddressInput? input)
		{
			var auth = _authHelper.GetAuthClaims();
			var order = await SiteGetOrder(orderId);
			var response = new GenericResponse<BaseReturnEnum, Order>();
			if (order is null)
			{
				response.Status = BaseReturnEnum.NotFound;
				return response;
			}
			DeliveryAddress? address = null;
			if (addressId is not null)
			{
				address = await _userService.GetDeliveryAdresses().Where(e => e.Id == addressId).FirstOrDefaultAsync();
				if (address is null)
					response.Status = BaseReturnEnum.NotAcceptable;

			}
			else if (input is not null)
			{
				address = new DeliveryAddress
				{

					Address = input.Address,
					City = input.City,
					PhoneNumber = input.PhoneNumber,
					ZipCode = input.ZipCode,
					UserId = auth.UserId
				};
				await _commonDA.DbInsert(address);
			}
			if (address is null)
			{
				response.Status = BaseReturnEnum.Unauthorized;
				return response;
			}

			order.AddressId = address.Id;
			order.UserId = auth.UserId;
			await _commonDA.DbUpdate(order);
			order.Address = address;
			response.Data = order;
			response.Status = BaseReturnEnum.Success;
			return response;

		}

		public async Task<BaseReturnEnum> SitePushOrder(Guid orderId)
		{

			var order = await _orderDA.GetOrders()
			.Include(e => e.Statuses.Where(e => e.IsCurrent))
			.FirstOrDefaultAsync(e => e.Id == orderId && e.UserId == _authHelper.GetAuthClaims().UserId);
			if (order is null)
				return BaseReturnEnum.NotFound;
			var cs = order.Statuses.First();
			if (cs.Status != OrderStatusEnum.Draft)
				return BaseReturnEnum.Unauthorized;
			cs.IsCurrent = false;
			await _commonDA.DbUpdate(cs);
			var newStatus = new OrderStatus
			{
				IsCurrent = true,
				OrderId = order.Id,
				MadeById = _authHelper.GetAuthClaims().UserId,
				Status = OrderStatusEnum.New
			};
			
			await _commonDA.DbInsert(newStatus);
			var norder = await _orderDA.GetOrders().SingleOrDefaultAsync(e => e.Id == orderId);
			norder.Number = await GetNewOrderNumber();
			await _commonDA.DbUpdate(norder);


			return BaseReturnEnum.Success;
		}
	}
}

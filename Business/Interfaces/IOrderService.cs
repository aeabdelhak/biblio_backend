using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface IOrderService
	{
		public Task<Order> NewOrder(NewOrderInput input);
		public Task<Order> UpdateDeliveringType(Guid id, OrderDeliveringType type );
		public Task<Order> UpdatePaiment(Guid id, decimal Price,bool paid);
		public Task<BaseReturnEnum> EndBorrow(Guid orderId, DateTime returnDate );
		public Task<BaseReturnEnum> BorrowNow(Guid orderId, DateTime returnDate );
		public IQueryable<Order> GetOrders();
		public Task<Order?> GetOrder(Guid id);
		public Task<Order?> SiteGetOrder(Guid id);
		public Task<BaseReturnEnum> UpdateOrderStatus(Guid id,OrderStatusEnum status);
		public Task<GenericResponse<BaseReturnEnum, Order>> AddToCart(DetailInput input);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteAddToCart(SellingDetailInput input);
		public Task<BaseReturnEnum> SitePushOrder(Guid orderId);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteSetDeliveryAddress(Guid orderId,Guid? addressId, AddressInput? input);
		public Task<GenericResponse<BaseReturnEnum, Order>> RemoveFromCart(
			Guid orderId,
			Guid detailId
		);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteRemoveFromCart(
			Guid orderId,
			Guid detailId
		);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteUpdateCartDetail(
			Guid orderId,
			Guid detailId,
			int Quantity
		);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteSetCartDeliveryType(
			Guid orderId,
			OrderDeliveringType Type
		);
		public Task<GenericResponse<BaseReturnEnum, Order>> PaymentMethod(
			
			PaymentMethode PaymentMethode
		);
		public Task<Order> NewSellingOrder();
	}
}

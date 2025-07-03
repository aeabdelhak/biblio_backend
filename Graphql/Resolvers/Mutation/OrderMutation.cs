using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
	public class OrderMutation(IOrderService _service)
	{
		
		public Task<Order> NewOrder(NewOrderInput input) => _service.NewOrder(input);
		public Task<GenericResponse<BaseReturnEnum, Order>> AddToCart(DetailInput input) => _service.AddToCart(input);
		public Task<GenericResponse<BaseReturnEnum, Order>> RemoveFromCart(Guid orderId,Guid detailId) => _service.RemoveFromCart(orderId,detailId);
		public Task<BaseReturnEnum> UpdateOrderStatus(Guid id, OrderStatusEnum status) => _service.UpdateOrderStatus(id, status);
		public Task<BaseReturnEnum> BorrowNow(Guid orderId, DateTime returnDate) => _service.BorrowNow(orderId, returnDate);
		public Task<Order> UpdatePaiment(Guid id, decimal Price, bool paid)=> _service.UpdatePaiment(id,Price, paid);
		public Task<BaseReturnEnum> EndBorrow(Guid orderId, DateTime returnDate) => _service.EndBorrow(orderId, returnDate);
		public Task<Order> UpdateDeliveringType(Guid id, OrderDeliveringType type) => _service.UpdateDeliveringType(id, type);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteAddToCart(SellingDetailInput input)=> _service.SiteAddToCart(input);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteUpdateCartDetail(Guid orderId, Guid detailId,int Quantity)=> _service.SiteUpdateCartDetail(orderId,detailId,Quantity);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteRemoveFromCart(Guid orderId, Guid detailId)=> _service.SiteRemoveFromCart(orderId,detailId);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteSetCartDeliveryType(Guid id, OrderDeliveringType type) => _service.SiteSetCartDeliveryType(id, type);
		public Task<GenericResponse<BaseReturnEnum, Order>> SiteSetDeliveryAddress(Guid orderId, Guid? addressId, AddressInput? input) => _service.SiteSetDeliveryAddress(orderId, addressId, input);
		public Task<BaseReturnEnum> SitePushOrder(Guid orderId) => _service.SitePushOrder(orderId);
		
	}
}
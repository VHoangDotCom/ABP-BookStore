using Acme.BookStore.Authors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Orders
{
    [Authorize(BookStorePermissions.Orders.Default)]
    public class OrderAppService : BookStoreAppService, IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderManager _orderManager;

        public OrderAppService(
            IOrderRepository orderRepository,
            OrderManager orderManager)
        {
            _orderRepository = orderRepository;
            _orderManager = orderManager;
        }

        //...SERVICE METHODS WILL COME HERE...
        public async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Order.FullName);
            }

            var orders = await _orderRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _orderRepository.CountAsync()
                : await _orderRepository.CountAsync(
                    order => order.FullName.Contains(input.Filter) || 
                             order.SKU.Contains(input.Filter));

            return new PagedResultDto<OrderDto>(
                totalCount,
                ObjectMapper.Map<List<Order>, List<OrderDto>>(orders)
            );
        }

        [Authorize(BookStorePermissions.Orders.Create)]
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = await _orderManager.CreateAsync(
                input.SKU,
                input.FullName,
                input.PhoneNumber,
                input.Email,
                input.Address,
                input.TotalCheckOut,
                input.Status
            );

            await _orderRepository.InsertAsync(order);

            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        [Authorize(BookStorePermissions.Orders.Edit)]
        public async Task UpdateAsync(Guid id, UpdateOrderDto input)
        {
            var order = await _orderRepository.GetAsync(id);

            if (order.FullName != input.FullName)
            {
                 _orderManager.ChangeNameAsync(order, input.FullName);
            }

            order.PhoneNumber = input.PhoneNumber;
            order.Email = input.Email;
            order.Address = input.Address;
            order.TotalCheckOut = input.TotalCheckOut;
            order.Status = input.Status;

            await _orderRepository.UpdateAsync(order);
        }

        [Authorize(BookStorePermissions.Orders.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }


    }
}

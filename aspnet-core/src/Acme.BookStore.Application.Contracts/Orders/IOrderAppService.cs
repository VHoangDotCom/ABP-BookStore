using Acme.BookStore.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> GetAsync(Guid id);

        Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input);

        Task<OrderDto> CreateAsync(CreateOrderDto input);

        Task UpdateAsync(Guid id, UpdateOrderDto input);

        Task DeleteAsync(Guid id);
    }
}

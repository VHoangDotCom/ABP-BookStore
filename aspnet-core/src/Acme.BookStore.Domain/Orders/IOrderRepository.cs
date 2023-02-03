using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<Order> FindByNameAsync(string fullName);
        Task<Order> FindBySKUAsync(string sku);

        Task<List<Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}

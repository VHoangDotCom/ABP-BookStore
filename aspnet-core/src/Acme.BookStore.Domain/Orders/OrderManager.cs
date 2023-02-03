using Acme.BookStore.Authors;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using System.Xml.Linq;

namespace Acme.BookStore.Orders
{
    public class OrderManager : DomainService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private static Random random = new Random();
        internal string SetSKU()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string SKU = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return SKU;
        }

        public async Task<Order> CreateAsync(
            [NotNull] string sku,
            [NotNull] string fullName,
            [CanBeNull] string phoneNumber,
            [NotNull] string email,
            [CanBeNull] string address,
            [CanBeNull] float total,
            OrderStatus orderStatus)
        {
                sku = SetSKU();
                var existingOrder = await _orderRepository.FindBySKUAsync(sku);
                if (existingOrder != null)
                {
                    throw new OrderAlreadyExistsException(sku);
                }

            Check.NotNullOrWhiteSpace(fullName, nameof(fullName));

            return new Order(
                GuidGenerator.Create(),
                 sku,
                fullName,
                phoneNumber,
                email,
                address,
                total,
                orderStatus
            );

        }

        public void ChangeNameAsync(
            [NotNull] Order order,
            [NotNull] string newName)
        {
            Check.NotNull(order, nameof(order));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            order.ChangeName(newName);
        }
    }
}

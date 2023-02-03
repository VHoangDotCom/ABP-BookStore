using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Orders
{
    public class OrderDto : EntityDto<Guid>
    {
        public string SKU { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public float TotalCheckOut { get; set; }
        public OrderStatus Status { get; set; }
    }
}

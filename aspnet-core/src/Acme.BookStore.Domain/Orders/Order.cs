
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Acme.BookStore.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public string SKU { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public float TotalCheckOut { get; set; }
        public OrderStatus Status { get; set; }

        private Order()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Order(
            Guid id,
            [NotNull] string sku,
            [NotNull] string fullName,
            [CanBeNull] string phoneNumber,
            [NotNull] string email,
            [CanBeNull] string address,
            [CanBeNull] float total,
            OrderStatus orderStatus)
            : base(id)
        {
            SetName(fullName);
            SKU = sku;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            TotalCheckOut = total;
            Status = orderStatus;
        }

        internal Order ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            FullName = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: OrderConsts.MaxNameLength
            );
        }

        
    }
}

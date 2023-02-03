using Acme.BookStore.Authors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Acme.BookStore.Orders
{
    public class CreateOrderDto
    {
        public string SKU { get; set; }
        [Required]
        [StringLength(OrderConsts.MaxNameLength)]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public float TotalCheckOut { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;

       
    }
}

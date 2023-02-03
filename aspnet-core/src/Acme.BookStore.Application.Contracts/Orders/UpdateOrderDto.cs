using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Orders
{
    public class UpdateOrderDto
    {
        [Required]
        [StringLength(OrderConsts.MaxNameLength)]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public float TotalCheckOut { get; set; }
        public OrderStatus Status { get; set; }
    }
}

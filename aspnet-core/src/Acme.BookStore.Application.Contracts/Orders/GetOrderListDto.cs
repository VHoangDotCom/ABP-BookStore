using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Orders
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

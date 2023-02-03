using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.BookStore.Orders
{
    public class OrderAlreadyExistsException : BusinessException
    {
        public OrderAlreadyExistsException(string sku)
            : base(BookStoreDomainErrorCodes.OrderAlreadyExists)
        {
            WithData("sku", sku);
        }
    }
}

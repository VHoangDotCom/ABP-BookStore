using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Orders
{
    public enum OrderStatus
    {
        PENDING,
        AVAILABLE,
        PROCESSING,
        COMPLETED,
        CANCELED,
        REFUND,
        COMPLAINT
    }
}

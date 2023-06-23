using System;
using System.Collections.Generic;

namespace Bussiness.DTO
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual OrderDTO Order { get; set; } = null!;
        public virtual ProductDTO Product { get; set; } = null!;
    }
}

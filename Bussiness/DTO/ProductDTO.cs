using System;
using System.Collections.Generic;

namespace Bussiness.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? PromotionPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string? ImgUrl { get; set; }

        public virtual CategoryDTO? Category { get; set; }
        public virtual SupplierDTO? Supplier { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}

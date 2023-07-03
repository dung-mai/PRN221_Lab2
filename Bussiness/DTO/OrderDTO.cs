using System;
using System.Collections.Generic;

namespace Bussiness.DTO
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}

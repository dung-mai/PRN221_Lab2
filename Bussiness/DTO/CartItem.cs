namespace Bussiness.DTO
{
    public class CartItem
    {
        public ProductDTO Product { get; set; } = new();
        public int Quantity { get; set; }
        public int TotalMoney(){
            return (int) (Quantity* Product.PromotionPrice);
        }
    }
}

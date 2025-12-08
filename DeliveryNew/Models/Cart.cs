using DeliveryNew.Models;

namespace DeliveryNew.Models
{
    public class CartItem
    {
        public DeliveryItem DeliveryItem { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalValue => Items.Sum(i => i.DeliveryItem.Price * i.Quantity);

        public void AddItem(DeliveryItem item, int quantity)
        {
            CartItem? line = Items
                .Where(p => p.DeliveryItem.Id == item.Id)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new CartItem
                {
                    DeliveryItem = item,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(DeliveryItem item) =>
            Items.RemoveAll(l => l.DeliveryItem.Id == item.Id);

        public void Clear() => Items.Clear();
    }
}

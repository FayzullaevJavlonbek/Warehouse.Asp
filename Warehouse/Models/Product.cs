namespace Warehouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
        public double Sum { get; set; }
        public Product(int id, string name, double price, int amount, double sum)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
            Sum = sum;
        }
    }

}

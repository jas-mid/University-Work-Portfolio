public class Item : Product

{
    //defining item exclusive features
    public string ItemName { get; set; }
    public string ItemBrand { get; set; }

    public Item(string type, decimal price, string name, string brand) : base(type, price)
    {
        ItemName = name;
        ItemBrand = brand;
    }

    public override void ProductInfo()
    {
        Console.WriteLine($"Name: {ItemName}, Type: {ProductType}, Brand: {ItemBrand}, Price: {ProductPrice}");
    }

}
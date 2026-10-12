public abstract class Product
{
    //getters and setters for variables used by both pets and shop items
    public string ProductType { get; set; }
    public decimal ProductPrice { get; set; }

    //generic function returning all info about a pet/item to be obverwritten 
    public abstract void ProductInfo();

    public Product(string type, decimal price)
    {
        ProductType = type;
        ProductPrice = price;
    }
}
public interface IPurchasing
{
    //generic functions to do with purchasing

    //process of purchasing an item
    void Purchase(bool cashPurchase, Product product, decimal cashGiven);


    //gives purchase information to customer
    void Receipt(Product product, bool cashPurchase, decimal cashGiven);

}
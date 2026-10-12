using System;
using System.Globalization;
//features used for date and time on the receipt

public class Customer : IPurchasing
{
    //email private for customer privacy
    private string EmailAddress { get; set; }

    //Preferred name only so employees know what to address them as
    //nullable so customers don't have to give their name if they don't want to
    public string? PreferredName { get; set; }
    
    //
    public Customer(string email, string? name)
    {
        EmailAddress = email;
        PreferredName = name;
    }

    //function for an employee to run through a purchase for customers
    //C
    public void Purchase(bool cashPurchase, Product product, decimal cashGiven)
    {
        //retrieving product cost and storing it as the 
        decimal moneyRequired = product.ProductPrice;

        //cash purchase without enough money - cancel transaction
        if (cashPurchase == true && moneyRequired > cashGiven)
        {
            Console.WriteLine("Not enough money. cancel transaction");
        }

        //cash purchase with enough money
        else if (cashPurchase == true && moneyRequired <= cashGiven)
        {
            Console.WriteLine($"Purchase successful. Give £{cashGiven - moneyRequired} change.");
        }
        else if (cashPurchase == false)
        {
            //message if they are buying with card
            Console.WriteLine("Card purchase. follow instructions of card machine.");
        }
        else
        {
            //handle all fringe cases just in case
            //should never be triggered but this triggering doesn't bring an error
            Console.WriteLine("Unrecognised transaction paramaters");
        }
    }

    //receipt printing function

    public void Receipt(Product product, bool cashPurchase, decimal cashGiven)
    {
        Console.WriteLine("Customer Receipt:");

        //receipt changes depending on pet or item
        if (product is Pet pet)
        {
            //returns pet breed on the receipt 
            Console.WriteLine(pet.PetBreed);
        }
        else if (product is Item item)
        {
            //returns name of item and brand 
            Console.WriteLine($"{item.ItemName} from {item.ItemBrand}");
        }
        else
        {
            //to handle all other transactions that don't fall under item or pet
            //not a normal case so a wide approach take
            Console.WriteLine("Other");
        }

        Console.WriteLine($"Total: £{product.ProductPrice}");

        //assume this is after transaction so no validation checks for cash given vs price
        if (cashPurchase == true)
        {
            //return cash given and the change if cash purchase
            Console.WriteLine($"Cash given - £{cashGiven}");
            Console.WriteLine($"Change - £{cashGiven - product.ProductPrice}");
        }
        else
        {
            Console.WriteLine("Card purchase");
        }
        Console.WriteLine("Time of transaction:");
        Console.WriteLine(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "GMT Standard Time").ToString("f", new CultureInfo("en-GB")));
        Console.WriteLine("----------------------------");
        Console.WriteLine("Thank you for shopping with us!");
    }
}
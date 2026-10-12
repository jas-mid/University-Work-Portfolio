using System.Data.Common;

public class Pet : Product
{
    //getters and setters for Pet class specifically

    //The species is stored as product type
    //this way if someone searches all products it won't just come up with a name, nobody is going to know what product "max" is
    //avoids confusion of not knowing what species a breed is from
    public string PetBreed { get; set; }
    public string PetName { get; set; }
    public int PetAge { get; set; }

    //inputs required to initialise a pet type object
    public Pet(string species, decimal price, string breed, string name, int age) : base(species, price)
    {
        //connecting inputs to the corresponding class attributes 
        PetBreed = breed;
        PetName = name;
        PetAge = age;
    }

    //overriding the product info to show all the information about pets specifically
    public override void ProductInfo()
    {
        Console.WriteLine($"Name: {PetName}, Species: {ProductType}, Breed: {PetBreed}, Age: {PetAge}, Price: £{ProductPrice}");
    }
}
using System.Collections;

public class Program
{
    public static void Main()
    {
        //initial welcome to the system and shop
        Console.WriteLine("------------------------");
        Console.WriteLine("Welcome to the pet shop!");
        Console.WriteLine("------------------------");
        Console.WriteLine("");

        //setting up values used throughout
        bool loopBreaker = false;
        int sectionSelect = 0;
        bool programKillSwitch = false;

        //lists of all data
        List<Pet> petStock = new List<Pet>
        {
            new Pet("Dog", 299.99m, "Golden Retriever", "Buddy", 2),
            new Pet("Cat", 149.99m, "Siamese", "Luna", 1),
            new Pet("Fish", 9.99m, "Betta", "Bubbles", 1),
            new Pet("Bird", 79.99m, "Parakeet", "Kiwi", 3),
            new Pet("Hamster", 24.99m, "Syrian", "Nibbles", 1),
            new Pet("Rabbit", 59.99m, "Mini Lop", "Coco", 2),
            new Pet("Lizard", 89.99m, "Bearded Dragon", "Spike", 4),
            new Pet("Snake", 129.99m, "Corn Snake", "Slinky", 3)
        };

        //list of items in stock
        List<Item> itemStock = new List<Item>
        {
            new Item("Food", 12.99m, "Dog Food", "Pedigree"),
            new Item("Food", 9.49m, "Cat Food", "Whiskas"),
            new Item("Accessory", 7.99m, "Fish Net", "AquaSafe"),
            new Item("Equipment", 39.99m, "Bird Cage", "FeatherHome"),
            new Item("Accessory", 5.99m, "Hamster Wheel", "SpinZone"),
            new Item("Food", 14.99m, "Rabbit Pellets", "Nature’s Best"),
            new Item("Equipment", 49.99m, "Reptile Lamp", "HeatPro"),
            new Item("Accessory", 19.99m, "Snake Terrarium Décor", "HabitatPlus")
        };

        //list to add customers to
        List<Customer> customerList = new List<Customer>();

        //list of current employees
        List<Employee> employeeList = new List<Employee>
        {
            new Employee(1, "manager123", "Sarah Johnson", "Manager"),
            new Employee(2, "cashier123", "Tom Lee", "Cashier"),
            new Employee(3, "service123", "Emma Davis", "Customer Service Representative")
        };

        while (programKillSwitch == false)
        {
            //do while loop used so the menu displays once before taking inputs
            do
            {
                //menu options
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Check our pets inventory");
                Console.WriteLine("2. Check our items inventory");
                Console.WriteLine("3. Make a purchase");
                Console.WriteLine("4. Register new customer");
                Console.WriteLine("5. Employee section");
                Console.WriteLine("6. Close application");

                //user inputs their menu selection
                string? menuSelect = Console.ReadLine();

                switch (menuSelect)
                {
                    //if 1 is entered
                    case "1":
                        loopBreaker = true;
                        sectionSelect = 1;
                        Console.WriteLine(sectionSelect);
                        break;

                    //if 2 is entered
                    case "2":
                        loopBreaker = true;
                        sectionSelect = 2;
                        Console.WriteLine(sectionSelect);
                        break;

                    //if 3 is entered
                    case "3":
                        loopBreaker = true;
                        sectionSelect = 3;
                        Console.WriteLine(sectionSelect);
                        break;

                    //if 4 is entered
                    case "4":
                        loopBreaker = true;
                        sectionSelect = 4;
                        Console.WriteLine(sectionSelect);
                        break;
                    
                     //if 5 is entered
                    case "5":
                        loopBreaker = true;
                        sectionSelect = 5;
                        break;
                    
                    //if 6 is entered
                    case "6":
                        loopBreaker = true;
                        sectionSelect = 6;
                        programKillSwitch = true;
                        break;

                    //clause that triggeers if none of the above are met by the user input
                    default:
                        Console.WriteLine("PLEASE WRITE A VALID INPUT");
                        break;
                }
                Console.WriteLine("");
            }
            //clause to exit the loop and end the above
            while (loopBreaker == false);

            //each new 'page' from the above menu selection is a case here
            switch (sectionSelect)
            {
                case 1:
                    {
                        //showing all the pet inventory
                        Console.WriteLine("Here's who is currently in store:");
                        int counter = 1;
                        foreach (Pet pet in petStock)
                        {
                            //numbering the pets so people can choose number or name of pet
                            Console.WriteLine($"Pet {counter}");
                            pet.ProductInfo();
                            counter++;
                        }
                        break;
                    }

                case 2:
                    {
                        //showing all the item inventory
                        Console.WriteLine("Here's what we have in stock:");
                        int counter = 1;
                        foreach (Item item in itemStock)
                        {
                            //numbering items so people choose item number or name
                            Console.WriteLine($"Item number {counter}");
                            item.ProductInfo();
                            counter++;
                        }
                        break;
                    }

                //purchase menu for customers
                case 3:
                    {
                        Console.WriteLine("---Purchase Menu---");
                        Console.WriteLine("");
                        Console.WriteLine("1. Pet");
                        Console.WriteLine("2. Item");
                        Console.WriteLine("Any other input to return");

                        string? petOrItem = Console.ReadLine();

                        if (petOrItem == "1")
                        {
                            int counter = 1;
                            foreach (Pet pet in petStock)
                            {
                                //numbering the pets so people can choose number or name of pet
                                Console.WriteLine($"Pet {counter}");
                                pet.ProductInfo();
                                counter++;
                            }
                            Console.WriteLine("Enter the number of pet you want");
                            Console.WriteLine("Any other input to return to menu");
                            string? productSelectstr = Console.ReadLine();

                            //selecting the pet and aprsing to int
                            int productselectint;
                            bool valid = int.TryParse(productSelectstr, out productselectint);

                            if (!valid || productselectint < 1 || productselectint > petStock.Count)
                            {
                                //cancel transasction stuff
                                Console.WriteLine("Transaction failed, returning to menu");
                            }
                            else
                            {
                                //pulling the pet from the pet stock list
                                Pet selectedPet = petStock[productselectint - 1];
                                Console.WriteLine("1. Cash");
                                Console.WriteLine("2. card");
                                string? paymentMethod = Console.ReadLine();

                                if (paymentMethod == "2")
                                {
                                    //selecting the customer and then buying and giving receipt
                                    Customer buyer = new Customer("temp", "temp@temp.temp");
                                    buyer.Purchase(false, selectedPet, 0);
                                    buyer.Receipt(selectedPet, false, 0);
                                }
                                else if (paymentMethod == "1")
                                {
                                    //if cash purchase
                                }
                                else
                                {
                                    Console.WriteLine("Transaction failed, returning to menu");
                                }
                            
                                
                            }
                            
                        }
                        else if (petOrItem == "2")
                        {

                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                        break;
                    }

                case 4:
                    {
                        //take customer email
                        string? customerEmail = " ";
                        do
                        {
                            //runs once before potentially looping
                            Console.WriteLine("Please enter a valid email:");
                            customerEmail = Console.ReadLine();
                        }
                        //validating the email before continuing. must have 5 characters and contain @ and .
                        while (customerEmail == null || !customerEmail.Contains("@") || !customerEmail.Contains(".") || customerEmail.Length < 4);

                        //take customer name
                        Console.WriteLine("Enter preferred name (optional, leave blank if no)");
                        string? customerName = Console.ReadLine();

                        //create and add new customer to the list of customers
                        Customer newCustomer = new Customer(customerEmail, customerName);
                        customerList.Add(newCustomer);
                        break;
                    }

                //enter employee area
                case 5:
                    {
                        //welcome message
                        Console.WriteLine("---Employee Area---");
                        Console.WriteLine("");

                        //check if anyone is logged in
                        bool? employeeLoggedIn = null;
                        foreach (Employee employee in employeeList)
                        {
                            if (employee.GiveLoginStatus() == true)
                            {
                                employeeLoggedIn = true;
                            }
                            else
                            {
                                employeeLoggedIn = false;
                            }
                        }

                        //if no employees are logged in
                        if (employeeLoggedIn == false)
                        {
                            //nothing can be done unless logged in
                            Console.WriteLine("1. log in");
                            Console.WriteLine("2. return");
                            string? notLoggedInOpts = Console.ReadLine();

                            if (notLoggedInOpts == "1")
                            {
                                string? logInIDString = null;
                                string? logInPassword = null;
                                bool successfulLogin = false;
                                bool backMenu = false;
                                do
                                {
                                    //entering employee details
                                    Console.WriteLine("Enter employee ID");
                                    logInIDString = Console.ReadLine();
                                    Console.WriteLine("Enter password");
                                    logInPassword = Console.ReadLine();
                                    int logInIDInt;
                                    bool valid = int.TryParse(logInIDString, out logInIDInt);


                                    if (logInIDString == null || valid == false)
                                    {
                                        Console.WriteLine("Incorrect details.");
                                        Console.WriteLine("Try again? Y/N");
                                        string? tryAgain = Console.ReadLine();
                                        if (tryAgain == "Y")
                                        {

                                        }
                                        else
                                        {
                                            backMenu = true;
                                        }
                                    }
                                    

                                }
                                while (successfulLogin == false && backMenu == false);

                            }
                            //no statement for 2 as it is there as a formality, any input returns
                        }
                        Console.WriteLine("");
                        break;
                    }                

                default:
                    Console.Write("");
                    break;

            }
        }
    }
}

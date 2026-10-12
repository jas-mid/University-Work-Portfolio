using System.Collections;

public class Program
{
    
    public static void Main()
    {
        
    List<ParkingSpots> allParkingSpots = new List<ParkingSpots>();
    //creates a list for all the different parking spots

    
    /*
    creates different parking spots to put in the parkingSpots list
    */
    DisabledParkingSpot spot1 = new DisabledParkingSpot(true);
    DisabledParkingSpot spot2= new DisabledParkingSpot(false);
    DisabledParkingSpot spot3 = new DisabledParkingSpot(true);
    RegularParkingSpot spot4 = new RegularParkingSpot(true);
    RegularParkingSpot spot5 = new RegularParkingSpot(false);
    RegularParkingSpot spot6 = new RegularParkingSpot(false);
    ElectricVehicleParkingSpot spot7 = new ElectricVehicleParkingSpot(true);
    ElectricVehicleParkingSpot spot8 = new ElectricVehicleParkingSpot(true);
    ElectricVehicleParkingSpot spot9 = new ElectricVehicleParkingSpot(false);
    
        
/*
Creates and initalizes a list of disabledParkingSpots
*/
        List<DisabledParkingSpot> disabledParkingSpots = new List<DisabledParkingSpot>();
        disabledParkingSpots.Add(spot1);
        disabledParkingSpots.Add(spot2);
        disabledParkingSpots.Add(spot3);
        ParkingSystem parkingSystem = ParkingSystem.Instance;
        //create 1 instance of the parkingSystem class (singleton design)

        foreach(DisabledParkingSpot spot in  disabledParkingSpots){
            spot.Subscribe(parkingSystem);
        }
        /*
        creates a list of regular paring spots and initalizes it
        */
        List<RegularParkingSpot> regularParkingSpots = new List<RegularParkingSpot>();
        regularParkingSpots.Add(spot4);
        regularParkingSpots.Add(spot5);
        regularParkingSpots.Add(spot6);
        foreach(RegularParkingSpot spot in  regularParkingSpots){
            spot.Subscribe(parkingSystem);
        }
        /*
        creates a list of electric vehicle parking spots and initalizes it
        */
        List<ElectricVehicleParkingSpot> electricVehicleParkingSpots = new List<ElectricVehicleParkingSpot>();
        electricVehicleParkingSpots.Add(spot7);
        electricVehicleParkingSpots.Add(spot8);
        electricVehicleParkingSpots.Add(spot9);
        foreach(ElectricVehicleParkingSpot spot in  electricVehicleParkingSpots){
            spot.Subscribe(parkingSystem);
        }
        
        /*
        adds all the spots to the overall parking spots list
        */
        allParkingSpots.Add(spot1);
        allParkingSpots.Add(spot2);
        allParkingSpots.Add(spot3);
        allParkingSpots.Add(spot4);
        allParkingSpots.Add(spot5);
        allParkingSpots.Add(spot6);
        allParkingSpots.Add(spot7);
        allParkingSpots.Add(spot8);
        allParkingSpots.Add(spot9);

        Boolean isParking = true;
        //creates a varible which sees if a user is parking
        List<int> allUserParkingSpots = new List<int>();
        //creates a variable for the users parking spots
        while(isParking)
        //runs while a user is parking
        {
            /*
            Menu for user to choose from
            */
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Park");
            Console.WriteLine("2. Leave parking");
            Console.WriteLine("3. Quit");
            String? userParking = Console.ReadLine();
            //user input of what they want to choose
            switch(userParking)
            //switch case to see which option user chooses
            {
                /*
                Case for if a user wants to park
                */
                case "1":
                
                //checks to see if user is already parked

                        
                    Boolean isContinue = true;
                    //ensures the user inputs a valid option
        while(isContinue)
        {
            /*
            Creates a menu for user to choose what type of car they use
            */
            Console.WriteLine("What type of car do you drive?");
            Console.WriteLine("1. Regular car");
            Console.WriteLine("2. Electric car");
            Console.WriteLine("3. Disabled parking access cars");
            Console.WriteLine("Press any other key to quit");
            String? userAnswer = Console.ReadLine();
            //user input of what type of car they use
        
        switch(userAnswer)
        //switch for the menu in which the user chooses what type of car they drive
        {
            /*
            The first case which is if the user drives a regular car
            */
            case "1":
            int count = 0;
            //count of regular parking spots
        
            for (int i = 0; i < allParkingSpots.Count(); i++)
            //itterates through the entire parking spots list
                {
                    if(allParkingSpots[i].TypeOfParking() == "regular parking")
                    //checks to see if the specific parking spot is a regular parking spot
                    {
                        if (allParkingSpots[i].ReturnAvailiability() == true)
                        //checks to see if the specific regular parking spot is available
                        {
                        count++;
                        //increments count if both if statements are true
                        }
                    }
                }
                if (count == 0)
                //checks to see if there are any regular parking spots available
                    {
                        Console.WriteLine("There are currently no parking spots available");
                        //prints out if there are no more regular parking spots available
                        isContinue= false;
                        //ends the user picking the car
                    }
                    else
                    {
                        int regularParkingIndex = -1;
                        //creates a index for the regular parking spot
                        int regularParkingSpotCount= 0;
                        //count to increment the while loop
                        while(regularParkingIndex == -1 && regularParkingSpotCount < allParkingSpots.Count())
                        //itterates through the parking spot until the first available regular parking spot is found
                        {
                            if(allParkingSpots[regularParkingSpotCount].TypeOfParking()== "regular parking")
                            //checks to see if the specific parking spot is a regular parking spot
                            {
                                if(allParkingSpots[regularParkingSpotCount].ReturnAvailiability()== true)
                                //checks to see if the specific regular parking spot is available
                                {
                                    regularParkingIndex=regularParkingSpotCount;
                                    //equals the index for the regular parking spot with the increment of the while loop in order to end the while loop
                                }
                            }
                            regularParkingSpotCount++;
                            //increments the index
                        }
                        allParkingSpots[regularParkingIndex].UpdateAvailability(false);
                        //updates the availability of the parking spot
                        Console.WriteLine("You are currently parked in spot"+" "+ regularParkingIndex);
                        //prints out the parking spot the user is currently parked in
                        allUserParkingSpots.Add(regularParkingIndex);
                        isContinue = false;
                        //ends the while loop to choose the car
                    }
            break;
            //ends case 1
            
            /*
            Lets the user park if they have a electric vehicle
            */
            case "2":
            int electricCount = 0;
            //the count for the amount of electric parking spots available
            for (int i = 0; i < allParkingSpots.Count(); i++)
            //itterates through the parking spots list
                {
                    if(allParkingSpots[i].TypeOfParking() == "electric parking" ) 
                    //checks to see if the specific parking spot is an electric parking spot
                    {
                        if (allParkingSpots[i].ReturnAvailiability() == true)
                        //checks to see if the specific electric parking spot is available
                        {
                        electricCount++;
                        //increments if both of the if statements are true
                        }
                    }
                }
                if (electricCount == 0)
                //checks to see if there are any electric vehicle parking spots available
                {
                    
                        Console.WriteLine("There are currently no electric vehicle parkings available");
                        //prints out if there are not electric vehicle parking spots available
                        for (int i = 0; i <allParkingSpots.Count();i++)
                        //itterates through the parking spots list
                        {
                            if (allParkingSpots[i].TypeOfParking()== "Regular parking")
                            //checks to see if the specific parking spot is a regular parking spots
                            {
                                if(allParkingSpots[i].ReturnAvailiability()==true)
                                //checks to see if the specific regular parking spot is available
                                {
                                    electricCount++;
                                    //increments if both of the if statements are true
                                }
                            }
                        }
                        Console.WriteLine("There are currently"+ " "+ electricCount+" "+ "regular parking spots available");
                        //prints out the amount of regular parking spots currently available
                        int nonElectricVehicleParkingIndex = -1;
                        //creates the index of the non electirc vehicle paring spot
                        int parkingIndex= 0;
                        //creates the index for the while loop
                        while(nonElectricVehicleParkingIndex== -1&& parkingIndex < allParkingSpots.Count())
                        {
                            if(allParkingSpots[parkingIndex].TypeOfParking()== "Regular parking")
                            //checks to see if the the specific parking spot is a regular parking spot
                            {
                                if(allParkingSpots[parkingIndex].ReturnAvailiability()==true)
                                //checks to see if the specific regular parking spot is available
                                {
                                    nonElectricVehicleParkingIndex= parkingIndex;
                                    //equals the index of the non electric vehicle parking to the index of the while loop
                                }
                            }
                            parkingIndex++;
                            //increments the index of the while loop
                        }
                        if(nonElectricVehicleParkingIndex == -1)
                        //checks to see if there is any available parking spot
                        {
                            Console.WriteLine("There are currently no parking spaces available");
                            //prints out if there are no current parking spots available
                            isContinue = false;
                            //stops the while loop to choose the car
                        }
                        else{
        
                        allParkingSpots[nonElectricVehicleParkingIndex].UpdateAvailability(false);
                        //updates the parking spot availability
                        Console.WriteLine("You are currently parked in spot"+" "+nonElectricVehicleParkingIndex);
                        //prints out the parking spot the user is currently in
                        allUserParkingSpots.Add(nonElectricVehicleParkingIndex);
                        }
                }
                        else
                        {
                            {
                                Console.WriteLine("There are currently" + " "+electricCount +" "+ "electric vehicle parking spots available");
                                //outprints the amount of electric vehicle parking that are currently available
                                int electricVehicleParkingIndex = -1;
                                //creates the index for the parking spot of the specific electric vehicle parking spot the user is parked in
                                int electricParkingCount= 0;
                                //index for while loop
                                 while(electricVehicleParkingIndex== -1&& electricParkingCount<allParkingSpots.Count())
                                     {
                            if(allParkingSpots[electricParkingCount].TypeOfParking()== "Electric vehicle parking")
                            //checks to see if the specific parking spot is an electric vehicle parking spots
                            {
                                if(allParkingSpots[electricParkingCount].ReturnAvailiability()==true)
                                //checks to see if the specifc electric vehicle parking is available
                                {
                                    electricVehicleParkingIndex= electricParkingCount;
                                    //equal the index of the users electirc vehicle parking spot to the while loop index
                                }
                            }
                            electricCount++;
                            //increments the while loop index
                        }
                    
        
                        allParkingSpots[electricVehicleParkingIndex].UpdateAvailability(false);
                        //updatesm the availability of the parking spot
                        Console.WriteLine("You are currently parked in spot"+" "+electricVehicleParkingIndex);
                        //prints out the parking spot the user is currently parked in
                        allUserParkingSpots.Add(electricVehicleParkingIndex);
                        //adds the users parking spot to the list
                        isContinue = false;
                        //stops the while loop to choose your car
                                
                            }
                        }

            break;
            //ends case 2

            /*
            Lets the user park if they have disabled access parking cars
            */
            case "3":
            int disabledParkingAccessCarCount = 0;
            //count for the amount of disabled parking spots
            for (int i = 0; i < allParkingSpots.Count(); i++)
            //itterates through the parking spots list
                {
                    if(allParkingSpots[i].TypeOfParking() == "Disabled parking") 
                    //checks to see if the specific parking spot is a disabled parking spot
                    {
                        if (allParkingSpots[i].ReturnAvailiability() == true)
                        //checks to see if the specific disabled parking spot is available
                        {
                        disabledParkingAccessCarCount++;
                        //increments if both of the if statements are true
                        }
                    }
                }
                if(disabledParkingAccessCarCount == 0)
                //checks to see if there are any disabled parking spots available
                    {
                        for (int i = 0; i < allParkingSpots.Count(); i++)
                        //itterates through the entire parking spots list
                        {
                         if(allParkingSpots[i].TypeOfParking() == "regular parking")
                         //checks to see if the specific parking spot is a regular parking spot
                    {
                        if (allParkingSpots[i].ReturnAvailiability() == true)
                        //checks to see if the specific regular parking spot is available
                        {
                        disabledParkingAccessCarCount++;
                        //increments if both of the if statements are true
                        }

                    }
                    }
                    Console.WriteLine("There are currently no disabled parking stops available");
                    //outprints if ther are no disabled parking spots currently available
                    Console.WriteLine("There are"+ " "+ disabledParkingAccessCarCount+" "+"regular parking spots available");
                    //prints out how many regular parking spots are available
                    int regularParkingSpotsIndex = -1;
                    //creates an index for the users regular parking spot
                    int index= 0;
                    //creates an index for the while loop
                    while(regularParkingSpotsIndex == -1&& index < allParkingSpots.Count())
                        {
                            if(allParkingSpots[index].TypeOfParking()== "Regular parking")
                            //checks to see if the specific parking spot is a regular parking spot
                            {
                                if(allParkingSpots[index].ReturnAvailiability()==true)
                                //checks to see if the regular parking spot is available
                                {
                                    regularParkingSpotsIndex= index;
                                    //equals the users index for their parking spot to the index of the while loop
                                }
                            }
                            index++;
                            //increments the index of the while loop
                        }
                        if(regularParkingSpotsIndex == -1)
                        //checks to see if there are any regular parking spots available
                        {
                            Console.WriteLine("There are no parking spots available");
                            //prints out if there are no regualr parking spots avaiable
                        }
                        else{
                        allParkingSpots[regularParkingSpotsIndex].UpdateAvailability(false);
                        //updates the availability of the parking spot
                        Console.WriteLine("You are currently parked in spot"+" "+ regularParkingSpotsIndex);
                        //prints out the spot the user is currently parked in
                        allUserParkingSpots.Add(regularParkingSpotsIndex);
                        //adds the users parking spot to the list of the parking spots
                        isContinue = false;
                        //ends the while loop to choose the car
                        }
                    }
                    else
                    {
                    Console.WriteLine("There are currently" + " "+disabledParkingAccessCarCount +" "+ "disabled parking spots available");
                    //prints out the amount of disabled parking spots currently available
                    int disabledParkingIndex = -1;
                    //creates an index for the disabled parking spot the user is in
                    int disabledParkingCount = 0;
                    //creates an index for the while loop
                    while(disabledParkingIndex == -1&& disabledParkingCount<allParkingSpots.Count())
                        {
                            if(allParkingSpots[disabledParkingCount].TypeOfParking()== "Disabled parking")
                            //checks to see if the specific parking spot is a disabled parking spot
                            {
                                if(allParkingSpots[disabledParkingCount].ReturnAvailiability()== true)
                                //checks to see if the specific disabled parking spot is available
                                {
                                    disabledParkingIndex= disabledParkingCount;
                                    //equals the index for the disapled parking spot the user is in to the while loop index
                                }
                            }
                            disabledParkingCount++;
                            //increments the while loop index
                        }

                        allParkingSpots[disabledParkingIndex].UpdateAvailability(false);
                        //updates the parking spots availability
                        Console.WriteLine("You have parked in spot"+" "+ disabledParkingIndex);
                        //prints out the parking spot the user is currently in
                        allUserParkingSpots.Add(disabledParkingIndex);
                        //adds the parking spot to the list of user parking spots
                        isContinue= false;
                        //ends the while loop to choose the car
                    }
        
                break;
                //ends case 3


                /*
                Ends the while loop
                */
                default:
                isContinue = false;
                Console.WriteLine("Thank you for parking with us");

            break;
        }
       
    
    }
    break;
                    
                   

                    /*
                    Lets the user leave the parking spot they are currently in
                    */
                    case "2":
                    Console.WriteLine("What spot are you parked in?");
                    String? parkingSpot = Console.ReadLine();
                    int personParkingSpot = Int32.Parse(parkingSpot);
                    if (allUserParkingSpots.Contains(personParkingSpot))
                    //checks to see if there is a person parked at that spot
                    {
                        allUserParkingSpots.Remove(personParkingSpot);
                        //removes the spot from the list of the users parking spots
                        allParkingSpots[personParkingSpot].UpdateAvailability(true);
                        //updates the availability of the spot
                        
                    }
                    else
                    {
                        Console.WriteLine("No one is currently parked at that spot");
                        //prints out if there is no one parked in the spot
                    }
                    
                    
                    //lets the user leave the parking
                    break;
                    //ends case 2

                    /*
                    Lets the user leave the parking garage
                    */
                    case "3":
                    isParking = false;
                    //ends the while loop
                    Console.WriteLine("Thank you for using our garage");
                    //prints out after the user wants to leave the garage
                    break;
                    //ends case 3
                    }
                    
                    
}
}
}


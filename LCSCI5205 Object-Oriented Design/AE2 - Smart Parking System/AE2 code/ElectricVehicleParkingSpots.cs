class ElectricVehicleParkingSpot: ParkingSpots, ISubject
/*
creates a class for  electric vehicle parking which inherits its methods from the class ParkingSpots
*/
{
    //variable tracking state of the object
    private Boolean isOccupied;

    //system that triggers a notification if state changes
    public Boolean isElectricVehicleParkingSpotsAvailable
    {
        get => isOccupied;
        set
        {
            this.isOccupied = value;
            Notify(value);
        }
    }

    //creates a variable which stores the availability for the electric vehicle parking
    /*
    initalizes all the variables for the ElectricVehickleParkingSpots class
    */
    public ElectricVehicleParkingSpot(Boolean isElectricVehicleParkingSpotsAvailable)
    {
        this.isElectricVehicleParkingSpotsAvailable= isElectricVehicleParkingSpotsAvailable;
    }

    /*
    returns the availability of the electirc vehicle parking spot
    */
    public override bool ReturnAvailiability()
    {
        return isElectricVehicleParkingSpotsAvailable;
    }

    /*
    returns the type of parking spot it is
    */
    public override string TypeOfParking()
    {
        return "Electric vehicle parking";
    }
    
    /*
    Updates the availability of the parking spot
    */
    public override void UpdateAvailability(bool isAvailable)
    {
        isElectricVehicleParkingSpotsAvailable= isAvailable;
    }



    //observer pattern subject integration

    private List<IObserver> observers = new List<IObserver>();
    //list of all the observers to the parking spot

    //function to add a new subscriber to the observers list
    public void Subscribe(IObserver newObserver)
    {
        bool isAlreadySubscribed = false;
        
        //checks the observer isn't already listed
        foreach (IObserver currentObserver in observers)
        {
            if (newObserver == currentObserver)
            {
                Console.WriteLine("This observer is already linked to the spot");
                isAlreadySubscribed = true;
            }
        }

        //if they aren't already, they are added
        if (isAlreadySubscribed == false)
        {
            observers.Add(newObserver);
            Console.WriteLine("Observer added succesfully");
        }
    }


    //fucntion to remove a subscriber from the observers list
    public void Unsubscribe(IObserver removeObserver)
    {
        //attempts to remove the item from the list
       if (observers.Remove(removeObserver))
        {
            //
            Console.WriteLine("Observer succesfully unsubscribed");
        }
        else
        // if it can't be removed it tells the user it isn't in the list
        {
            Console.WriteLine("Observer is not currently observing subject");
        }
    }

    //updates every observer of the change in state within the object.
    private void Notify(Boolean occupied)
    {
        foreach (IObserver currentObserver in observers)
        {
            currentObserver.Update(occupied);
        }
    }
}
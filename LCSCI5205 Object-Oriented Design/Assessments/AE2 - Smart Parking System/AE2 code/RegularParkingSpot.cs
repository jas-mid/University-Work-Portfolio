class RegularParkingSpot: ParkingSpots, ISubject
/*
    creates a new class for regular parking spots that inherits the methods from the ParkingSpots class
*/
{
    //variable tracking state of the object
    private Boolean isOccupied;

    //the system updating observers if a state change occurs
    public Boolean isRegularSpotAvailable
    {
        get => isOccupied; 
        set
        {
            this.isOccupied = value;
            Notify(value);
        }
    }

   /*
   initalizes all the variables for regular parking spots
   */ 
    public RegularParkingSpot(Boolean isRegularSpotAvailable)
    {
        this.isRegularSpotAvailable= isRegularSpotAvailable;
    }

    /*
    returns the availability of the regular parking spot
    */
    public override Boolean ReturnAvailiability()
    {
        return isRegularSpotAvailable;
    }

    /*
    returns the type of parking spot it is
    */
    public override string TypeOfParking()
    {
        return "regular parking";
    }

    /*
    updates the availability of the parking spot
    */
    public override void UpdateAvailability(bool isAvailable)
    {
        isRegularSpotAvailable = isAvailable;
    }


    //observer pattern subject integration

    private List<IObserver> observers = new List<IObserver>();
    //list of all the observers to the parking spot

    //function to add a new subscriber to the observers list
    public void Subscribe(IObserver newObserver)
    {
        bool isAlreadySubscribed = false;
        
        //checks they observer isn't already listed
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

    //updating every subscribed observer of the object's current state
    private void Notify(Boolean occupied)
    {
        foreach (IObserver currentObserver in observers)
        {
            currentObserver.Update(occupied);
        }
    }
}
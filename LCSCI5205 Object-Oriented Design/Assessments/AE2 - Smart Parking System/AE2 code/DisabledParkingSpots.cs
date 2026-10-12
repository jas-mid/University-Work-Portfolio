using System;
class DisabledParkingSpot: ParkingSpots, ISubject
/*
creates a disabled parking spot class which inherits its methods from the ParkingSpots class
*/
{
    //variable tracking state of the object
    private Boolean isOccupied;

    //system triggered if a change in state occurs
    public Boolean isDisabledParkingSpotsAvailable
    {
        get => isOccupied;
        set
        {
            this.isOccupied = value;
            Notify(value);
        }
    }

    //creates a variable for the availaibility of disabled parking spots
    /*
    initalizes all the variables for the DisabledParkingSpots class
    */
    public DisabledParkingSpot(Boolean isDisabledParkingSpotsAvailable)
    {
        this.isDisabledParkingSpotsAvailable = isDisabledParkingSpotsAvailable;
    }

    /*
    returns the availability of the disabled parking spot
    */
    public override Boolean ReturnAvailiability()
    {
        return isDisabledParkingSpotsAvailable;
    }

    /*
    returns the type of parking it is
    */
    public override string TypeOfParking()
    {
        return "Disabled parking";
    }
    
    /*
    Updates the availability of the parking spot
    */
    public override void UpdateAvailability(Boolean isAvailable)
    {
        isDisabledParkingSpotsAvailable = isAvailable;
    }



    //observer pattern subject integration

    private List<IObserver> observers = new List<IObserver>();
    //list of all the observers to the parking spot

    //function to add a new subscriber to the observers list
    public void Subscribe(IObserver newObserver)
    {
        Boolean isAlreadySubscribed = false;
        
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

    //updating every subscribed observer of the current object status
    private void Notify(Boolean occupied)
    {
        foreach (IObserver currentObserver in observers)
        {
            currentObserver.Update(occupied);
        }
    }
}
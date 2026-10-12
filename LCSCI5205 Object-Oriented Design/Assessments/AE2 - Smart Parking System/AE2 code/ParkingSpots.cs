
abstract class ParkingSpots
/*
creates an abstract class for parking spots
*/
{
    /*
    functions all the parking space types use
    */
    public abstract Boolean ReturnAvailiability ();
    //returns the availability of parking spots
    public abstract string TypeOfParking();
    //returns the type of parking
    
    public abstract void UpdateAvailability(Boolean isAvailable);
    //updates the availability of the parking spot
}
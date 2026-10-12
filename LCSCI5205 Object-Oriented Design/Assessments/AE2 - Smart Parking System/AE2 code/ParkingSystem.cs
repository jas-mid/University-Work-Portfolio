 using System;
 class ParkingSystem: IObserver
{
    private Boolean isTrue;
    //creates a boolean to see if a parking spot is available
    

    static ParkingSystem _Instance;
    //creates an instance of the parking system

    /*
    Ensures that there can only be one instance of the ParkingSystem created
    */
    public static ParkingSystem Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new ParkingSystem(); 
            }
            return _Instance;
        }
    }

    private ParkingSystem()
    {
        
    }


    
    //Defining the observation functions that the ParkingSystem class will use to observe the parking spots

    /*
    
    */
    //implementation of the Update function from IObserver
    public void Update(Boolean isAvailable)
    {
        isTrue = isAvailable;
    }
    
}
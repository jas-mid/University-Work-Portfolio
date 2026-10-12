public interface IObserver
{
    //the interface that observers inherit functions from
    
    
   public void Update(Boolean isAvailable);
   //triggers the observer's response to the change in the subjects
}
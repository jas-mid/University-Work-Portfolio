public interface ISubject 
{
    //interface for subjects 

    //functions to allow a an observer to start observing
    public void Subscribe(IObserver observer);
    //or stop observing the subject
    public void Unsubscribe(IObserver observer);


    //notify is not included as it should be a private function within the subject object itself
}

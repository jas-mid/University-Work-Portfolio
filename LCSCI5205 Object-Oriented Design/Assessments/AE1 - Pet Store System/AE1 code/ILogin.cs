public interface ILogin
{
    //to log in, employees to put in their ID number and password
    bool Login(int employeeId, string password);
    void LogOut();
}
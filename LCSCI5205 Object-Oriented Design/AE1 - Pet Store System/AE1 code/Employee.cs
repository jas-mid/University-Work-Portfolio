//importing parts for hashing passwords
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO.Pipes;
using System.Security.Cryptography.X509Certificates;

public class Employee : ILogin
{
    //all attributes of employees
    public int EmployeeID { get; set; }

    //passwordsto be hashed for security
    private string HashedPassword { get; set; }
    private bool LoginStatus { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeePosition { get; set; }

    public Employee(int id, string password, string name, string role)
    {
        EmployeeID = id;
        //original password not stored at any point
        HashedPassword = Hash(password);
        LoginStatus = false;
        EmployeeName = name;
        EmployeePosition = role;
    }
    
    //function to login to the system for employees
    public bool Login(int id, string password)
    {
        if (id == EmployeeID && HashedPassword == Hash(password))
        {
            // change logged in status to true then inform user
            LoginStatus = true;
            Console.WriteLine("You are logged in");
            return true;
        }
        else
        {
            //output if details incorrect
            Console.WriteLine("Incorrect username or password. Try again");
            return false;
        }
    }

    //to log out of the system only if a user is logged in
    public void LogOut()
    {
        if (LoginStatus == true)
        {
            LoginStatus = false;
            Console.WriteLine("Logged out.");
        }
        //to handle the case someone uses the function when nobody is logged in
        else
        {
            Console.WriteLine("Please log in first");
        }
    }

    //hashing of input for the passwords
    //SHA256 used to reach a higher standard
    public static string Hash(string unhashedPassword)
    {
        //in case an empty string is put in
        if (unhashedPassword.Length == 0)
        {
            //returns an empty string too
            return string.Empty;
        }
        else
        {
            //converts the input password into bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(unhashedPassword);
            //applies the SHA256 hashing to the bytes
            var hashData = SHA256.HashData(inputBytes);
            //converts the bytes back into hex strings meaning capital letters and other symbols are viable
            return Convert.ToHexString(hashData);
        }
    }
        public bool GiveLoginStatus()
    {
        return LoginStatus;
    }
}
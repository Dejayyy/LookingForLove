using LookingForLove;

class Program
{
    static void Main()
    {
        AuthService authService = new AuthService();

        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.Write("Select an option: ");
        string option = Console.ReadLine();

        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        if (option == "1")
        {
            authService.Register(username, password);
        }
        else if (option == "2")
        {
            if (authService.Login(username, password))
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }
}
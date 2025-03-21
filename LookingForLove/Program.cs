using System;

namespace LookingForLove
{
    class Program
    {
        static void Main()
        {
            AuthService authService = new AuthService();

            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("Select an option: ");
            string? option = Console.ReadLine();

            Console.Write("Enter username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            if (option == "1")
            {
                authService.Register(username, password);
                return;
            }
            else if (option == "2")
            {
                if (authService.Login(username, password))
                {
                    Console.WriteLine("Login successful!");
                    ShowMainMenu(authService, username);
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

        static void ShowMainMenu(AuthService authService, string username)
        {
            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Match");
                Console.WriteLine("2. Profile Setup");
                Console.WriteLine("3. Chat");
                Console.WriteLine("4. Logout");
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Matching feature coming soon!");
                        break;
                    case "2":
                        UpdateUserProfile(authService, username);
                        break;
                    case "3":
                        Console.WriteLine("Chat feature coming soon!");
                        break;
                    case "4":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void UpdateUserProfile(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("Enter new bio: ");
            //user.Bio = Console.ReadLine();
            Console.Write("Enter your interests (comma-separated): ");
          //  user.Interests = Console.ReadLine();

            authService.UpdateProfile(user);
        }
    }
}

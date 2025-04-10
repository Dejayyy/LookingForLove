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
                Console.WriteLine("3. View My Profile");
                Console.WriteLine("4. Chat");
                Console.WriteLine("5. Change Contact Info");
                Console.WriteLine("6. Logout");
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
                            PrintUserInfo(authService, username);
                        break;
                    case "4":
                        Console.WriteLine("Chat feature coming soon!");
                        break;
                    case "5":
                        ChangeContactInfo(authService, username);
                        return;
                    case "6":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void PrintUserInfo(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("USER INFORMATION:\n");
            Console.WriteLine("UserName: " + user.Username);
            Console.WriteLine("Full Name: " + user.FirstName + " " + user.LastName);
            Console.WriteLine("Bio: " + user.Bio);
            Console.WriteLine("Interests: \n");
            PrintInterests(authService, username);

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                Console.WriteLine("Email: None provided.");
            }
            else
            {
                Console.WriteLine("Email: " + user.Email);
            }

            if (string.IsNullOrWhiteSpace(user.WhatsApp))
            {
                Console.WriteLine("WhatsApp: None provided.");
            }
            else
            {
                Console.WriteLine("WhatsApp: " + user.WhatsApp);
            }

            Console.WriteLine("Joined: " + user.RegistrationDate);
        }

        static void PrintInterests(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            string[] interests = user.Interests.Split(',');
            foreach (string interest in interests)
            {
                Console.WriteLine(interest);
            }
        }

        static void ChangeContactInfo(AuthService authService, string username)
        {
            Console.Write("Enter new Email (leave blank to skip): ");
            string? newEmail = Console.ReadLine();

            Console.Write("Enter new WhatsApp number (leave blank to skip): ");
            string? newWhatsApp = Console.ReadLine();

            authService.ChangeContactDetails(username, newEmail, newWhatsApp);
        }




        static void UpdateUserProfile(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Do you wish to update your whole profile or just your bio and interests?");
            Console.WriteLine("1. Whole Profile");
            Console.WriteLine("2. Just Bio and Interests");
            string? choice = Console.ReadLine();

            switch(choice)
            {
                //whole profile update
                case "1":
                    Console.Write("Enter your First Name: ");
                    user.FirstName = Console.ReadLine();
                    Console.Write("Enter your Last Name: ");
                    user.LastName = Console.ReadLine();

                    Console.Write("Enter new bio: ");
                    user.Bio = Console.ReadLine();
                    Console.Write("Enter your interests (comma-separated): ");
                    user.Interests = Console.ReadLine();

                    break;


                case "2":
                    Console.Write("Enter new bio: ");
                    user.Bio = Console.ReadLine();
                    Console.Write("Enter your interests (comma-separated): ");
                    user.Interests = Console.ReadLine();

                    break;
            }
            authService.UpdateProfile(user);
        }
    }
}

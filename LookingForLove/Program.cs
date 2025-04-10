using System;

namespace LookingForLove
{
    class Program
    {
        static void Main()
        {
            AuthService authService = new AuthService();

            while (true)
            {
                Console.WriteLine("\n1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string? option = Console.ReadLine();

                if (option == "3")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                if (option == "1")
                {
                    authService.Register(username, password);
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
                Console.WriteLine("5. Set or Change Contact Info");
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
                        break;
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
            while (true)
            {
                Console.WriteLine("\n--- Contact Info Menu ---");
                Console.WriteLine("1. Update Email");
                Console.WriteLine("2. Update WhatsApp");
                Console.WriteLine("3. Return to Main Menu");
                Console.Write("Choose an option: ");
                string? input = Console.ReadLine();

                string? newEmail = null;
                string? newWhatsApp = null;

                switch (input)
                {
                    case "1":
                        Console.Write("Enter new Email (leave blank to cancel): ");
                        newEmail = Console.ReadLine();
                        authService.ChangeContactDetails(username, newEmail, null);
                        break;
                    case "2":
                        Console.Write("Enter new WhatsApp number (leave blank to cancel): ");
                        newWhatsApp = Console.ReadLine();
                        authService.ChangeContactDetails(username, null, newWhatsApp);
                        break;
                    case "3":
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

            Console.WriteLine("Do you wish to update your whole profile or just your bio and interests?");
            Console.WriteLine("1. Whole Profile");
            Console.WriteLine("2. Just Bio and Interests");
            Console.WriteLine("3. Return to Main Menu");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            if (choice == "3") return;


            switch (choice)
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

﻿using System;

namespace LookingForLove
{
    class Program
    {
        static void Main()
        {
            AuthService authService = new AuthService();

            while (true)
            {
                Console.WriteLine("Looking For Love");
                Console.WriteLine("----------------");
                Console.WriteLine("Would you like to:");
                Console.WriteLine("1. Register");
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
                Console.WriteLine("Looking For Love");
                Console.WriteLine("----------------");
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
                        //Console.WriteLine("Matching feature coming soon!");
                        FindAMatch(authService, username);
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

            Console.WriteLine("Looking For Love");
            Console.WriteLine("----------------");
            Console.WriteLine("USER INFORMATION:\n");
            Console.WriteLine("UserName: " + user.Username);
            Console.WriteLine("Full Name: " + user.FirstName + " " + user.LastName);
            Console.WriteLine("Bio: " + user.Bio);
            Console.WriteLine("Interests: ");
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
            Console.WriteLine("Preferred Communication Method: " + user.PreferredContactMethod);
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
                Console.WriteLine("Looking For Love");
                Console.WriteLine("----------------");
                Console.WriteLine("\n--- Contact Info Menu ---");
                Console.WriteLine("1. Update Email");
                Console.WriteLine("2. Update WhatsApp");
                Console.WriteLine("3. Set Preferred Communication Method");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Choose an option: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter new Email (leave blank to cancel): ");
                        string? newEmail = Console.ReadLine();
                        authService.ChangeContactDetails(username, newEmail, null);
                        break;

                    case "2":
                        Console.Write("Enter new WhatsApp number (leave blank to cancel): ");
                        string? newWhatsApp = Console.ReadLine();
                        authService.ChangeContactDetails(username, null, newWhatsApp);
                        break;

                    case "3":
                        Console.WriteLine("Select preferred communication method:");
                        Console.WriteLine("1. Email");
                        Console.WriteLine("2. WhatsApp");
                        Console.Write("Choose: ");
                        string? prefChoice = Console.ReadLine();

                        string? preferredMethod = null;
                        if (prefChoice == "1") preferredMethod = "Email";
                        else if (prefChoice == "2") preferredMethod = "WhatsApp";
                        else
                        {
                            Console.WriteLine("Invalid option.");
                            break;
                        }

                        User user = authService.GetUser(username);
                        user.PreferredContactMethod = preferredMethod;
                        authService.UpdateProfile(user);
                        Console.WriteLine($"Preferred method set to {preferredMethod}");
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }



        static void FindAMatch(AuthService authService, string username)
        {
            User currentUser = authService.GetUser(username);
            string[] userInterests = currentUser.Interests.Split(',');

            List<User> users = authService.LoadUsers();

            foreach (User user in users)
            {
                if (user.Username == username)
                    continue;

                string[] matchInterests = user.Interests.Split(',');

                int matchCount = 0;
                foreach (string interest in userInterests)
                {
                    foreach (string otherI in matchInterests)
                    {
                        if (interest.Trim().ToLower() == otherI.Trim().ToLower())
                        {
                            matchCount++;
                        }
                    }
                }

                if (matchCount >= 3)
                {
                    bool preferred = false;
                    if (user.PreferredContactMethod == "Email")
                    {
                       preferred = true;
                    }


                    Console.WriteLine($"\nYou matched with {user.Username}!");
                    Console.WriteLine($"Shared Interests: {matchCount}");
                    Console.WriteLine($"Bio: {user.Bio}");
                    Console.WriteLine($"Contact: {(preferred ? user.Email : user.WhatsApp)}");
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
            Console.WriteLine("Looking For Love");
            Console.WriteLine("----------------");
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
                    Console.WriteLine("Looking For Love");
                    Console.WriteLine("----------------");
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
                    Console.WriteLine("Looking For Love");
                    Console.WriteLine("----------------");
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

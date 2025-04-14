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
            User user = authService.GetUser(username);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Looking For Love");
                Console.WriteLine("----------------");
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Match");
                Console.WriteLine("2. Profile Setup");
                Console.WriteLine("3. View My Profile");
                Console.WriteLine("4. Set or Change Contact Info");
                if (user.isAdmin)
                {
                    Console.WriteLine("5. Admin Panel");
                }
                Console.WriteLine("6. Logout");
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        FindAMatch(authService, username);
                        break;
                    case "2":
                        UpdateUserProfile(authService, username);
                        break;
                    case "3":
                            PrintUserInfo(authService, username);
                        break;
                    case "4":
                        ChangeContactInfo(authService, username);
                        break;
                    case "5":
                        ShowAdminPage(authService, username);
                        break;
                    case "6":
                        Console.WriteLine("Logging out...");
                        Console.Clear();
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
            Console.Clear();
            Console.WriteLine("Looking For Love");
            Console.WriteLine("----------------");
            Console.WriteLine("USER INFORMATION:\n");
            Console.WriteLine("UserName: " + user.Username);
            Console.WriteLine("Full Name: " + user.FirstName + " " + user.LastName);
            Console.WriteLine("Nickname: " + user.Nickname);
            Console.WriteLine("Bio: " + user.Bio);
            Console.WriteLine("Birthday: " + user.DateOfBirth);
            PrintInterests(authService, username);
            PrintPossessedSkills(authService, username);
            PrintDesiredSkills(authService, username);

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

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        static void PrintInterests(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("Interests: User not found.");
                return;
            }
            string[] interests = user.Interests.Split(',');
            Console.Write("Interests: ");
            Console.WriteLine(string.Join(", ", interests.Select(i => i.Trim())));
        }

        static void PrintPossessedSkills(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("Possessed Skills: User not found.");
                return;
            }
            string[] posSkills = user.PossessedSkills.Split(',');
            Console.Write("Possessed Skills: ");
            Console.WriteLine(string.Join(", ", posSkills.Select(s => s.Trim())));
        }

        static void PrintDesiredSkills(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("Desired Skills: User not found.");
                return;
            }
            string[] desSkills = user.DesiredSkills.Split(',');
            Console.Write("Desired Skills: ");
            Console.WriteLine(string.Join(", ", desSkills.Select(s => s.Trim())));
        }

        static void ShowAdminPage(AuthService authService, string username)
        {
            User user = authService.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            List<User> users = authService.LoadUsers();

            int paidMembers = users.Count(u => u.IsPaidMember);
            int freeMembers = users.Count(u => !u.IsPaidMember);
            int contactExposedMatches = authService.GetExposedMatchesCount();

            Console.Clear();
            Console.WriteLine("=== Admin Dashboard ===");
            Console.WriteLine($"Free Members: {freeMembers}");
            Console.WriteLine($"Paid Members: {paidMembers}");
            Console.WriteLine($"Total Matches with Contact Info Shared: {contactExposedMatches}");
            Console.WriteLine("=========================");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
        static void ChangeContactInfo(AuthService authService, string username)
        {
            while (true)
            {
                Console.Clear();
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
                        Console.Clear();
                        Console.Write("Enter new Email (leave blank to cancel): ");
                        string? newEmail = Console.ReadLine();
                        authService.ChangeContactDetails(username, newEmail, null);
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("Enter new WhatsApp number (leave blank to cancel): ");
                        string? newWhatsApp = Console.ReadLine();
                        authService.ChangeContactDetails(username, null, newWhatsApp);
                        break;

                    case "3":
                        Console.Clear();
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
            bool foundMatch = false;

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
                    foundMatch = true;

                    Console.Clear();
                    Console.WriteLine($"\nYou matched with {user.Username}!");
                    Console.WriteLine($"Shared Interests: {matchCount}");
                    Console.WriteLine($"Bio: {user.Bio}");
                    Console.WriteLine("Interests: ");
                    foreach (string interest in matchInterests)
                    {
                        Console.WriteLine($"- {interest.Trim()}");
                    }

                    Console.WriteLine("\nDo you:");
                    Console.WriteLine("1. Like");
                    Console.WriteLine("2. Not");
                    Console.Write("Choose an option: ");
                    string? input = Console.ReadLine();

                    if (input == "1")
                    {
                            authService.RecordExposedMatch();

                        string contact = currentUser.PreferredContactMethod == "Email" ? user.Email : user.WhatsApp;
                        Console.WriteLine($"\nGreat! Here is {user.Username}'s contact info via {currentUser.PreferredContactMethod}:");
                        Console.WriteLine($"{contact}");

                        Console.WriteLine("\nPlease rate your experience (1-5): ");
                        if (int.TryParse(Console.ReadLine(), out int rating))
                        {
                            if (rating >= 1 && rating <= 5)
                                Console.WriteLine("Thanks for your rating!");
                            else
                                Console.WriteLine("Invalid rating. Must be between 1 and 5.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Rating skipped.");
                        }

                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nOkay, moving to the next match...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }

            if (!foundMatch)
            {
                Console.WriteLine("No matches found at this time. Try updating your profile or interests.");
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
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

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Looking For Love");
                Console.WriteLine("----------------");
                Console.WriteLine("---- Edit Profile ----");
                Console.WriteLine("1. Salutation");
                Console.WriteLine("2. First Name");
                Console.WriteLine("3. Last Name");
                Console.WriteLine("4. Nickname");
                Console.WriteLine("5. Date of Birth");
                Console.WriteLine("6. Gender");
                Console.WriteLine("7. Bio");
                Console.WriteLine("8. Interests");
                Console.WriteLine("9. Member Type (Free or Paid)");
                Console.WriteLine("10. Possessed Skills");
                Console.WriteLine("11. Desired Skills");
                Console.WriteLine("12. Return to Main Menu");
                Console.Write("Choose a field to edit: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Choose your salutation:");
                        Console.WriteLine("1. Mr.");
                        Console.WriteLine("2. Ms.");
                        Console.WriteLine("3. Mrs.");
                        Console.WriteLine("4. Dr.");
                        Console.WriteLine("5. Mx.");
                        Console.Write("Enter number: ");
                        string? salOpt = Console.ReadLine();

                        switch (salOpt)
                        {
                            case "1":
                                user.Salutation = "Mr.";
                                break;
                            case "2":
                                user.Salutation = "Ms.";
                                break;
                            case "3":
                                user.Salutation = "Mrs.";
                                break;
                            case "4":
                                user.Salutation = "Dr.";
                                break;
                            case "5":
                                user.Salutation = "Mx.";
                                break;
                            default:
                                user.Salutation = "Other";
                                break;
                        }

                        break;
                    case "2":
                        Console.Write("Enter your First Name: ");
                        user.FirstName = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Enter your Last Name: ");
                        user.LastName = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Enter your Nickname: ");
                        user.Nickname = Console.ReadLine();
                        break;
                    case "5":
                        Console.Write("Enter your Date of Birth (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                            user.DateOfBirth = dob;
                        else
                            Console.WriteLine("Invalid date format.");
                        break;
                    case "6":
                        Console.Write("Enter your Gender: ");
                        user.Gender = Console.ReadLine();
                        break;
                    case "7":
                        Console.Write("Enter your Bio: ");
                        user.Bio = Console.ReadLine();
                        break;
                    case "8":
                        Console.Write("Enter your Interests (comma-separated): ");
                        user.Interests = Console.ReadLine();
                        break;
                    case "9":
                        Console.Write("Are you a paid member? (yes/no): ");
                        string? paid = Console.ReadLine()?.ToLower();
                        if (paid == "yes")
                        {
                            user.IsPaidMember = true;
                            Console.WriteLine("Congratulations! You are a paid user for 1 day.");
                            Console.WriteLine("An invoice has been sent to your email. Pay it to remain a paid member for a month.");
                            Console.WriteLine("\nPress any key to return to the menu...");
                            Console.ReadKey();
                        }
                        else
                        {
                            user.IsPaidMember = false;
                        }
                        break;
                    case "10":
                        Console.Write("Enter your possessed skills (comma-separated): ");
                        user.PossessedSkills = Console.ReadLine();
                        break;

                    case "11":
                        Console.Write("Enter your desired skills in a match (comma-separated): ");
                        user.DesiredSkills = Console.ReadLine();
                        break;
                    case "12":
                        authService.UpdateProfile(user);
                        Console.WriteLine("Profile updated!");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                authService.UpdateProfile(user);
            }
        }

    }
}

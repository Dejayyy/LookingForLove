using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LookingForLove
{
    public class AuthService
    {
        private const string FilePath = "users.json";

        public void Register(string username, string password)
        {
            List<User> users = LoadUsers();
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("User already exists!");
                return;
            }

            users.Add(new User { Username = username, Password = password });
            SaveUsers(users);
            Console.WriteLine("User registered successfully!");
        }

        public bool Login(string username, string password)
        {
            List<User> users = LoadUsers();
            return users.Exists(u => u.Username == username && u.Password == password);
        }

        public User GetUser(string username)
        {
            List<User> users = LoadUsers();
            return users.Find(u => u.Username == username);
        }

        public void UpdateProfile(User user)
        {
            List<User> users = LoadUsers();
            int index = users.FindIndex(u => u.Username == user.Username);
            if (index != -1)
            {
                users[index] = user;
                SaveUsers(users);
                Console.WriteLine("Profile updated successfully!");
            }
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}

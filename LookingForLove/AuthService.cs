using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
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

        private List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class DataService
    {
        private const string UsersFile = "users.json";
        private const string MatchesFile = "matches.json";
        private const string RatingsFile = "ratings.json";

        private List<T> LoadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        private void SaveData<T>(List<T> data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<User> GetAllUsers() => LoadData<User>(UsersFile);
        public void SaveUsers(List<User> users) => SaveData(users, UsersFile);

        public List<Match> GetAllMatches() => LoadData<Match>(MatchesFile);
        public void SaveMatches(List<Match> matches) => SaveData(matches, MatchesFile);

        public List<MatchRating> GetAllRatings() => LoadData<MatchRating>(RatingsFile);
        public void SaveRatings(List<MatchRating> ratings) => SaveData(ratings, RatingsFile);
    }
}


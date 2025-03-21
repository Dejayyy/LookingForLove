using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class ProfileService
    {
        private readonly DataService _dataService;

        public ProfileService(DataService dataService)
        {
            _dataService = dataService;
        }

        public User GetUser(int userId)
        {
            var users = _dataService.GetAllUsers();
            return users.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateProfile(User updatedUser)
        {
            var users = _dataService.GetAllUsers();
            int index = users.FindIndex(u => u.Id == updatedUser.Id);

            if (index != -1)
            {
                users[index] = updatedUser;
                _dataService.SaveUsers(users);
            }
        }

        public void AddSkillToPossessed(int userId, string skill)
        {
            var user = GetUser(userId);
            if (user != null && !user.PossessedSkills.Contains(skill))
            {
                user.PossessedSkills.Add(skill);
                UpdateProfile(user);
            }
        }

        public void AddSkillToDesired(int userId, string skill)
        {
            var user = GetUser(userId);
            if (user != null && !user.DesiredSkills.Contains(skill))
            {
                user.DesiredSkills.Add(skill);
                UpdateProfile(user);
            }
        }
    }
}

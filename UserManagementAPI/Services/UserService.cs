using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public User Create(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User user)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.Department = user.Department;
            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}
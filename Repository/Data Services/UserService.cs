using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class UserService
{
    private readonly Repository<User> _userRepository;

    public UserService(Repository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllQueryable()
            .Include(u => u.Overviews)
            .Include(u => u.FilmPoints)
            .Include(u => u.ViewStatuses);
    }

    public User GetUserById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var user = _userRepository.GetAllQueryable()
            .Include(u => u.Overviews)
            .Include(u => u.FilmPoints)
            .Include(u => u.ViewStatuses)
            .FirstOrDefault(u => u.UserId == id);
        if (user == null) throw new Exception("User not found");

        return user;
    }

    public User? GetUserByGoogleId(string id)
    {
        if (id is null) return null;

        var user = _userRepository.GetAllQueryable()
            .Include(u => u.Overviews)
            .Include(u => u.FilmPoints)
            .Include(u => u.ViewStatuses)
            .FirstOrDefault(u => u.GoogleId == id);
        return user;
    }
    
    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _userRepository.Add(user);
    }

    public void DeleteUser(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        User user = _userRepository.GetById(id);
        if (user == null) throw new Exception("User not found");
        _userRepository.Delete(user);
    }

    public void AddUserRange(IEnumerable<User> users)
    {
        if (users == null) throw new ArgumentNullException(nameof(users));
        _userRepository.AddRange(users);
    }

    public void UpdateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _userRepository.Update(user);
    }

    public User? GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return null;

        var user = _userRepository.GetAllQueryable()
            .Include(u => u.Overviews)
            .Include(u => u.FilmPoints)
            .Include(u => u.ViewStatuses)
            .FirstOrDefault(u => u.Email == email);
        return user;
    }

    public void UpdateUserRole(int userId, string role)
    {
        if (string.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role));

        var user = _userRepository.GetById(userId);
        if (user == null) throw new Exception("User not found");

        user.Role = role;
        _userRepository.Update(user);
    }
    
}

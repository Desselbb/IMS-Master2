

namespace IMSClassLibrary.Repos
{
    public class UserRepository : IUser<User>
    {
        private readonly SystemDbContext _context;

        public UserRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public List<User> AddRange(List<User> users)
        {
            _context.Users.AddRange(users);
            _context.SaveChanges();
            return users;
        }

        public bool Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}

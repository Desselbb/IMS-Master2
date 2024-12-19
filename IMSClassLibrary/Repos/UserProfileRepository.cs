

namespace IMSClassLibrary.Repos
{
    public class UserProfileRepository : IUserProfile<UserProfile>
    {
        private readonly SystemDbContext _context;

        public UserProfileRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public UserProfile Add(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
            return userProfile;
        }

        public async Task<UserProfile> AddAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }

        public List<UserProfile> AddRange(List<UserProfile> userProfiles)
        {
            _context.UserProfiles.AddRange(userProfiles);
            _context.SaveChanges();
            return userProfiles;
        }

        public bool Delete(UserProfile userProfile)
        {
            _context.UserProfiles.Remove(userProfile);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var userProfile = _context.UserProfiles.Find(id);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public UserProfile Get(int id)
        {
            return _context.UserProfiles.Find(id);
        }

        public List<UserProfile> GetAll()
        {
            return _context.UserProfiles.ToList();
        }

        public UserProfile Update(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            _context.SaveChanges();
            return userProfile;
        }
    }
}

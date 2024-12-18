

namespace IMSClassLibrary.Repos
{

    public class ProfileRepository : IProfile<Profile>
    {
        private readonly SystemDbContext _context;

        public ProfileRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public Profile Add(Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return profile;
        }

        public async Task<Profile> AddAsync(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public List<Profile> AddRange(List<Profile> profiles)
        {
            _context.Profiles.AddRange(profiles);
            _context.SaveChanges();
            return profiles;
        }

        public bool Delete(Profile profile)
        {
            _context.Profiles.Remove(profile);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var profile = _context.Profiles.Find(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Profile Get(int id)
        {
            return _context.Profiles.Find(id);
        }

        public List<Profile> GetAll()
        {
            return _context.Profiles.ToList();
        }

        public Profile Update(Profile profile)
        {
            _context.Profiles.Update(profile);
            _context.SaveChanges();
            return profile;
        }
    }
}



namespace IMSClassLibrary.Repos
{
    public  class Authorisation
    {
       
        SystemDbContext _context;
        public Authorisation(SystemDbContext context)
        {
            _context=context;  
        }

        public bool authorize(int userId, string url)
        {
            try
            {
                if (userId==0 || url==null) { return false; }

                var userProfiles = _context.UserProfiles.Where(p => p.UserId == userId).ToList();
                var mod = _context.Modules.Where(d => d.Url == url).FirstOrDefault();
                if (mod!=null)
                {
                    var found = false;
                    foreach (var up in userProfiles)
                    {
                        if (_context.ProfileModules.Where(m => m.ModuleId == mod.Id && m.ProfileId==up.ProfileId).FirstOrDefault() != null)
                        {
                            found = true;
                            break;
                        }
                    }
                    return found;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}



namespace IMSClassLibrary.Repos
{
    public class UserProfileRepository : IInterface<UserProfile>
    {
        SystemDbContext _context;
        public UserProfileRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<UserProfile> Add(UserProfile userProfile)
        {
            try
            {
                if (_context.UserProfiles.Where(u=>u.ProfileId == userProfile.ProfileId && u.UserId==userProfile.UserId).FirstOrDefault() == null)
                {
                    _context.UserProfiles.Add(userProfile);
                    _context.SaveChanges();
                    return ResultObject<UserProfile>.Success("UserProfiles has been added successfully");
                }
                else
                {
                    return ResultObject<UserProfile>.Failure("There is already another UserProfile with the same details");
                }
            }
            catch (Exception d)
            {
                return ResultObject<UserProfile>.Failure("Could not add user Profile, Something went wrong.");
            }
        }

        public ResultObject<UserProfile> AddRange(List<UserProfile> userProfiles)
        {
            try
            {
                bool addAll = true;
                foreach (var up in userProfiles)
                {
                    if (_context.UserProfiles.Where(u => u.ProfileId == up.ProfileId && u.UserId==up.UserId).FirstOrDefault() == null)
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.UserProfiles.AddRange(userProfiles);
                    _context.SaveChanges();

                    return ResultObject<UserProfile>.Success("UserProfiles have been added successfully");
                }
                else
                {
                    return ResultObject<UserProfile>.Failure("Some UserProfiles in the submited list have Names similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
          
                return ResultObject<UserProfile>.Failure("Could not add the UserProfiles, Something went wrong");
            }



        }

        public ResultObject<UserProfile> Delete(UserProfile item)
        {
            try
            {
                _context.UserProfiles.Remove(item);
                _context.SaveChanges();
                return ResultObject<UserProfile>.Success("The User Profile has been deleted successfuly");
            }
            catch
            {
                return ResultObject<UserProfile>.Failure("Could not delete the User Profile, Something went wrong");
            }
        }

        public ResultObject<UserProfile> DeleteById(int Id)
        {
            try
            {
                _context.UserProfiles.Remove(_context.UserProfiles.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<UserProfile>.Success("The User Profile has been deleted successfuly");
            }
            catch
            {
                return ResultObject<UserProfile>.Failure("Could not delete the User Profile, Something went wrong");
            }
        }

        public ResultObject<UserProfile> Get(int Id)
        {
            try
            {
                return ResultObject<UserProfile>.Success("User Profile Retrived", _context.UserProfiles.Include(b => b.User).Include(b => b.Profile).Single(b => b.Id == Id));
            }
            catch (Exception d)
            {
                return ResultObject<UserProfile>.Failure("Could not retrieve the User Profile, Something went wrong", new UserProfile());
            }
        }

        public ResultObject<UserProfile> Get(int userId, int profileId)
        {
            try
            {
                return ResultObject<UserProfile>.Success("", _context.UserProfiles.Include(b => b.User).Include(b => b.Profile).Single(b => b.UserId == userId && b.ProfileId==profileId));
            }
            catch (Exception d)
            {
                return ResultObject<UserProfile>.Failure("Could not retrive the object", new UserProfile());
            }

        }

        public ResultObject<List<UserProfile>> GetUserProfiles(int userId)
        {
            try
            {
                return ResultObject<List<UserProfile>>.Success("", _context.UserProfiles.Include(b => b.User).Include(b => b.Profile).Where(b => b.UserId == userId).ToList());
            }
            catch (Exception d)
            {
                return ResultObject<List<UserProfile>>.Failure("Could not retrive the object", new List<UserProfile>());
            }

        }
        
        public ResultObject<List<UserProfile>> GetAll()
        {
            try
            {
                return ResultObject<List<UserProfile>>.Success("", _context.UserProfiles.Include(b => b.User).Include(b => b.Profile).ToList());
            }
            catch
            {
                return ResultObject<List<UserProfile>>.Failure("Could not retrive the object", new List<UserProfile>());
            }
        }

        public ResultObject<UserProfile> Update(UserProfile userProfile)
        {
            try
            {
                if (_context.UserProfiles.Where(u=>u.UserId == userProfile.UserId && u.ProfileId==userProfile.ProfileId && u.Id!=userProfile.Id).FirstOrDefault() == null)
                {
                    _context.UserProfiles.Update(userProfile);
                    _context.SaveChanges();
                    return ResultObject<UserProfile>.Success("User profile Updated successfuly");
                }
                else
                {
                    _context.Entry(userProfile).Reload();
                    return ResultObject<UserProfile>.Failure("There is already another User profile with the same details");
				}
                
                
                }
            catch
            {
                _context.Entry(userProfile).Reload();
                return ResultObject<UserProfile>.Failure("Could not update the UserProfile, Something went wrong!");
            }

        }
    }
}


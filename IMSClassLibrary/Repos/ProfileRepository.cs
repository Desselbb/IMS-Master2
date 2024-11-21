using IMSClassLibrary.Interfaces;
using IMSClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using IMSClassLibrary.Context;

namespace IMSClassLibrary.Repos
{
    public class ProfileRepository : IInterface<Profile>
    {
        SystemDbContext _context;

        public ProfileRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<Profile> Add(Profile profile)
        {
            try
            {
                if (this.Get(profile.Name.Trim()).Data == null) // meaning there is no similar recrd
                {
                    _context.Profiles.Add(profile);
                    _context.SaveChanges();
                    return ResultObject<Profile>.Success("Profile has been added successfully");
                }
                else
                {
                    return ResultObject<Profile>.Failure("There is already another Profile with the same name");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Profile>.Failure("There is already another Profile with the same name");
            }
        }

        public ResultObject<Profile> AddRange(List<Profile> profiles)
        {
            try
            {
                bool addAll = true;
                foreach (var md in profiles)
                {
                    if (Get(md.Name.Trim()).Data.Id > 0)
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.Profiles.AddRange(profiles);
                    _context.SaveChanges();

                    return ResultObject<Profile>.Success("Profiles have been added successfully");
                }
                else
                {
                    return ResultObject<Profile>.Failure("Some Profiles in the submited list have Names similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Profile>.Failure("Could not add the Profiles, Something went wrong");
            }

        }

        public ResultObject<Profile> Delete(Profile item)
        {
            try
            {
                _context.Profiles.Remove(item);
                _context.SaveChanges();
                return ResultObject<Profile>.Success("The Profile has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Profile>.Failure("Could not delete the Profile, something went wrong");
            }
        }

        public ResultObject<Profile> DeleteById(int Id)
        {
            try
            {
                _context.Profiles.Remove(_context.Profiles.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<Profile>.Success("The Profile has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Profile>.Failure("Could not delete the Profile, something went wrong");
            }
        }

        public ResultObject<Profile> Get(int Id)
        {
            try
            {
                return ResultObject<Profile>.Success("Profile Retrived", _context.Profiles.Single(b => b.Id == Id));
            }
            catch (Exception d)
            {
                return ResultObject<Profile>.Failure("Could not retrieve the Profile, Something went wrong", new Profile());
            }
        }
        public ResultObject<Profile> Get(string name)
        {
            try
            {
                return ResultObject<Profile>.Success("", _context.Profiles.Where(b => b.Name.Trim() == name.Trim()).FirstOrDefault());
            }
            catch (Exception d)
            {
                return ResultObject<Profile>.Failure("Could not retrive the object, Somehing wentt wrong", new Profile());
            }
        }
        public ResultObject<Profile> Get(string name, int Id)
        {
            try
            {
                return ResultObject<Profile>.Success("", _context.Profiles.Single(b => b.Name.Trim() == name.Trim() && b.Id != Id));
            }
            catch (Exception d)
            {
                return ResultObject<Profile>.Failure("Could not retrive the object", new Profile());
            }
        }

        public ResultObject<List<Profile>> GetAll()
        {
            try
            {
                return ResultObject<List<Profile>>.Success("", _context.Profiles.ToList());
            }
            catch
            {
                return ResultObject<List<Profile>>.Failure("Could not retrive the object", new List<Profile>());
            }
        }

        public ResultObject<Profile> Update(Profile profile)
        {
            try
            {
                if (this.Get(profile.Name, profile.Id).Data.Id < 1)
                {
                    _context.Profiles.Update(profile);
                    _context.SaveChanges();
                    return ResultObject<Profile>.Success("Object Updated successfuly");

                }
                else
                {
                    _context.Entry(profile).Reload();
                    return ResultObject<Profile>.Failure("There is already another Profile with the same name");
                }
            }
            catch
            {
                _context.Entry(profile).Reload();
                return ResultObject<Profile>.Failure("Could not update the Profile, Something went wrong!");
            }
        }
    }
}

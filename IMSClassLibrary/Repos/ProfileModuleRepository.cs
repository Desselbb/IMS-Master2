using IMSClassLibrary.Models;
using IMSClassLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;
using IMSClassLibrary.Context;
using IMSClassLibrary.Interfaces;

namespace IMSClassLibrary.Repos
{
    public class ProfileModuleRepository : IInterface<ProfileModule>
    {
        SystemDbContext _context;
        public ProfileModuleRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<ProfileModule> Add(ProfileModule item)
        {
            try
            {
                if (Get(item.ProfileId, item.ModuleId).Data == null) // if true meaning no similar object was returned so go ahead
                {
                    _context.Add(item);
                    _context.SaveChanges();
                    return ResultObject<ProfileModule>.Success("Profile Module has been added successfully");
                }
                else
                {
                    return ResultObject<ProfileModule>.Failure("This Module has already been attached to this profile");
                }
            }
            catch (Exception ex)
            {
                return ResultObject<ProfileModule>.Failure("Could not add the Module to the profile, something went wrong");
            }

        }

        public ResultObject<ProfileModule> AddRange(List<ProfileModule> items)
        {
            try
            {
                bool addAll = true;
                foreach (var pm in items)
                {
                    if (Get(pm.ProfileId,pm.ModuleId).Data == null) // meaning nothing alike was returned
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.ProfileModules.AddRange(items);
                    _context.SaveChanges();
                    return ResultObject<ProfileModule>.Success("Profile Modules have been added successfully");
                }
                else
                {
                    return ResultObject<ProfileModule>.Failure("Some Module in the submited list are similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<ProfileModule>.Failure("Could not add the Modules, Something went wrong");
            }
        }

        public ResultObject<ProfileModule> DeleteById(int Id)
        {
            try
            {
                _context.ProfileModules.Remove(_context.ProfileModules.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<ProfileModule>.Success("The Profile Module has been deleted successfuly");
            }
            catch
            {
                return ResultObject<ProfileModule>.Failure("Could not delete the Profile Module, something went wrong");
            }
        }

        public ResultObject<ProfileModule> Get(int Id)
        {
            try
            {
                return ResultObject<ProfileModule>.Success("Profile Module Retrived", _context.ProfileModules.Where(b => b.Id == Id).FirstOrDefault());
            }
            catch (Exception d)
            {
                return ResultObject<ProfileModule>.Failure("Could not retrieve the Profile Module Something went wrong", new ProfileModule());
            }
        }
        
        public ResultObject<ProfileModule> Get(int ProfileId,int ModuleId)
        {
            try
            {
                return ResultObject<ProfileModule>.Success("", _context.ProfileModules.Include(w => w.Profile).Include(w => w.Module).Where(s => s.ProfileId == ProfileId && s.ModuleId == ModuleId).FirstOrDefault());
            }
            catch (Exception ex) 
            {
               return  ResultObject<ProfileModule>.Failure("Could Not Retrive the Object, Something went wrong");
            }

        }
        
        public ResultObject<ProfileModule> Get(int Id,int ProfileId,int ModuleId)
        {
            try
            {
                return ResultObject<ProfileModule>.Success("", _context.ProfileModules.Include(w => w.Profile).Include(w => w.Module).Where(s => s.ProfileId == ProfileId && s.ModuleId == ModuleId).FirstOrDefault());
            }
            catch (Exception ex) 
            {
               return  ResultObject<ProfileModule>.Failure("Could Not Retrive the Object, Something went wrong");
            }

        }

		public bool IsMyModule(int ProfileId, int ModuleId)
		{
			try
			{
                if (_context.ProfileModules.Where(s => s.ProfileId == ProfileId && s.ModuleId == ModuleId).FirstOrDefault() == null)
                {
                    return false;
                }
                else 
                {
                    return true;
                }
			}
			catch (Exception ex)
			{
				return false;
			}

		}

		public ResultObject<List<ProfileModule>> GetProfileProfileModules(int ProfileId)
        {
            try
            {
                return ResultObject<List<ProfileModule>>.Success("", _context.ProfileModules.Include(w => w.Profile).Include(w => w.Module).Where(s => s.ProfileId == ProfileId).ToList());
            }
            catch (Exception ex)
            {
                return ResultObject<List<ProfileModule>>.Failure("Could Not Retrive the Object, Something went wrong");
            }

        }

        public ResultObject<List<ProfileModule>> GetAll()
        {
            try
            {
                return ResultObject<List<ProfileModule>>.Success("", _context.ProfileModules.Include(d=>d.Profile).Include(d=>d.Module).ToList());
            }
            catch (Exception d)
            {
                return ResultObject<List<ProfileModule>>.Failure("Could not retrive the object", new List<ProfileModule>());
            }
        }

        public ResultObject<ProfileModule> Update(ProfileModule item)
        {
            try
            {
                if (_context.ProfileModules.Where(d=>d.ProfileId == item.ProfileId && d.ModuleId==item.ModuleId && d.Id!=item.Id).FirstOrDefault() == null) // if true meaning no similar object was returned so go ahead
                {
                    _context.ProfileModules.Update(item);
                    _context.SaveChanges();

                    return ResultObject<ProfileModule>.Success("Profile Module has been updated successfully");
                }
                else
                {
                    _context.Entry(item).Reload();
                    return ResultObject<ProfileModule>.Failure("There is already another Profile Module with similar Data");
                }
            }
            catch
            {
                _context.Entry(item).Reload();
                return ResultObject<ProfileModule>.Failure("The Profile Module Could not be edited, something went wrong");
            }
        }

        public ResultObject<ProfileModule> Delete(ProfileModule item)
        {
            try
            {
                _context.ProfileModules.Remove(item);
                _context.SaveChanges();
                return ResultObject<ProfileModule>.Success("The Module has been deleted successfuly");
            }
            catch
            {
                return ResultObject<ProfileModule>.Failure("Could not delete the Profile Module, something went wrong");
            }
        }
    }
}

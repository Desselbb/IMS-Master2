

namespace IMSClassLibrary.Repos
{
    public class InternProjectRepository : IInterface<InternProject>
    {
        SystemDbContext _context;
        public InternProjectRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<InternProject> Add(InternProject item)
        {
            try
            {
                if (Get(item.ProfileId, item.ProjectId).Data == null) // if true meaning no similar object was returned so go ahead
                {
                    _context.Add(item);
                    _context.SaveChanges();
                    return ResultObject<InternProject>.Success("Profile Module has been added successfully");
                }
                else
                {
                    return ResultObject<InternProject>.Failure("This Module has already been attached to this profile");
                }
            }
            catch (Exception ex)
            {
                return ResultObject<InternProject>.Failure("Could not add the Module to the profile, something went wrong");
            }

        }

        public ResultObject<InternProject> AddRange(List<InternProject> items)
        {
            try
            {
                bool addAll = true;
                foreach (var pm in items)
                {
                    if (Get(pm.ProfileId, pm.ProjectId).Data == null) // meaning nothing alike was returned
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.InternProjects.AddRange(items);
                    _context.SaveChanges();
                    return ResultObject<InternProject>.Success("Profile Modules have been added successfully");
                }
                else
                {
                    return ResultObject<InternProject>.Failure("Some Module in the submited list are similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<InternProject>.Failure("Could not add the Modules, Something went wrong");
            }
        }

        public ResultObject<InternProject> DeleteById(int Id)
        {
            try
            {
                _context.InternProjects.Remove(_context.InternProjects.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<InternProject>.Success("The Project has been deleted successfuly");
            }
            catch
            {
                return ResultObject<InternProject>.Failure("Could not delete the Project Module, something went wrong");
            }
        }

        public ResultObject<InternProject> Get(int Id)
        {
            try
            {
                return ResultObject<InternProject>.Success("Retrived", _context.InternProjects.Where(b => b.Id == Id).FirstOrDefault());
            }
            catch (Exception d)
            {
                return ResultObject<InternProject>.Failure("Could not retrieve the contents Something went wrong", new InternProject());
            }
        }

        public ResultObject<InternProject> Get(int ProfileId, int ProjectId)
        {
            try
            {
                return ResultObject<InternProject>.Success("", _context.InternProjects.Include(w => w.Profile).Include(w => w.ProjectId).Where(s => s.ProfileId == ProfileId && s.ProjectId == ProjectId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ResultObject<InternProject>.Failure("Could Not Retrive the content, Something went wrong");
            }

        }

        public ResultObject<InternProject> Get(int Id, int ProfileId, int ProjectId)
        {
            try
            {
                return ResultObject<InternProject>.Success("", _context.InternProjects.Include(w => w.Profile).Include(w => w.Project).Where(s => s.ProfileId == ProfileId && s.ProjectId == ProjectId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ResultObject<InternProject>.Failure("Could Not Retrive the Object, Something went wrong");
            }

        }

        public bool IsMyModule(int ProfileId, int ProjectId)
        {
            try
            {
                if (_context.ProfileModules.Where(s => s.ProfileId == ProfileId && s.ModuleId == ProjectId).FirstOrDefault() == null)
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

        public ResultObject<List<InternProject>> GetInternProjects(int ProfileId)
        {
            try
            {
                return ResultObject<List<InternProject>>.Success("", _context.InternProjects.Include(w => w.Profile).Include(w => w.Project).Where(s => s.ProfileId == ProfileId).ToList());
            }
            catch (Exception ex)
            {
                return ResultObject<List<InternProject>>.Failure("Could Not Retrive the Object, Something went wrong");
            }

        }

        public ResultObject<List<InternProject>> GetAll()
        {
            try
            {
                return ResultObject<List<InternProject>>.Success("", _context.InternProjects.Include(d => d.Profile).Include(d => d.Project).ToList());
            }
            catch (Exception d)
            {
                return ResultObject<List<InternProject>>.Failure("Could not retrive the object", new List<InternProject>());
            }
        }

        public ResultObject<InternProject> Update(InternProject item)
        {
            try
            {
                if (_context.InternProjects.Where(d => d.ProfileId== item.ProfileId && d.ProjectId == item.ProjectId && d.Id != item.Id).FirstOrDefault() == null) // if true meaning no similar object was returned so go ahead
                {
                    _context.InternProjects.Update(item);
                    _context.SaveChanges();

                    return ResultObject<InternProject>.Success("Profile Module has been updated successfully");
                }
                else
                {
                    _context.Entry(item).Reload();
                    return ResultObject<InternProject>.Failure("There is already another Profile Module with similar Data");
                }
            }
            catch
            {
                _context.Entry(item).Reload();
                return ResultObject<InternProject>.Failure("The Profile Module Could not be edited, something went wrong");
            }
        }

        public ResultObject<InternProject> Delete(InternProject item)
        {
            try
            {
                _context.InternProjects.Remove(item);
                _context.SaveChanges();
                return ResultObject<InternProject>.Success("The Module has been deleted successfuly");
            }
            catch
            {
                return ResultObject<InternProject>.Failure("Could not delete the Profile Module, something went wrong");
            }
        }
    }
}


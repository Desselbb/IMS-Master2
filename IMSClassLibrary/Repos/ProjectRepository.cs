

namespace IMSClassLibrary.Repos
{
    public class ProjectRepository : IInterface<Project>
    {
        SystemDbContext _context;

        public ProjectRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<Project> Add(Project project)
        {
            try
            {
                if (this.Get(project.Name.Trim()).Data == null) // meaning there is no similar recrd
                {
                    _context.Projects.Add(project);
                    _context.SaveChanges();
                    return ResultObject<Project>.Success("Project has been added successfully");
                }
                else
                {
                    return ResultObject<Project>.Failure("There is already another Project with the same name");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Project>.Failure("There is already another Project with the same name");
            }
        }

        public ResultObject<Project> AddRange(List<Project> projects)
        {
            try
            {
                bool addAll = true;
                foreach (var md in projects)
                {
                    if (Get(md.Name.Trim()).Data.Id > 0)
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.Projects.AddRange(projects);
                    _context.SaveChanges();

                    return ResultObject<Project>.Success("Projects have been added successfully");
                }
                else
                {
                    return ResultObject<Project>.Failure("Some Project in the submited list have Names similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Project>.Failure("Could not add the Project, Something went wrong");
            }

        }

        public ResultObject<Project> Delete(Project item)
        {
            try
            {
                _context.Projects.Remove(item);
                _context.SaveChanges();
                return ResultObject<Project>.Success("The Project has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Project>.Failure("Could not delete the Project, something went wrong");
            }
        }

        public ResultObject<Project> DeleteById(int Id)
        {
            try
            {
                _context.Projects.Remove(_context.Projects.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<Project>.Success("The Project has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Project>.Failure("Could not delete the Project, something went wrong");
            }
        }

        public ResultObject<Project> Get(int Id)
        {
            try
            {
                return ResultObject<Project>.Success("Project Retrived", _context.Projects.Single(b => b.Id == Id));
            }
            catch (Exception d)
            {
                return ResultObject<Project>.Failure("Could not retrieve the Project, Something went wrong", new Project());
            }
        }
        public ResultObject<Project> Get(string name)
        {
            try
            {
                return ResultObject<Project>.Success("", _context.Projects.Where(b => b.Name.Trim() == name.Trim()).FirstOrDefault());
            }
            catch (Exception d)
            {
                return ResultObject<Project>.Failure("Could not retrive the Project, Something went wrong", new Project());
            }
        }
        public ResultObject<Project> Get(string name, int Id)
        {
            try
            {
                return ResultObject<Project>.Success("", _context.Projects.Single(b => b.Name.Trim() == name.Trim() && b.Id != Id));
            }
            catch (Exception d)
            {
                return ResultObject<Project>.Failure("Could not retrive the Project", new Project());
            }
        }

        public ResultObject<List<Project>> GetAll()
        {
            try
            {
                return ResultObject<List<Project>>.Success("", _context.Projects.ToList());
            }
            catch
            {
                return ResultObject<List<Project>>.Failure("Could not retrive the Project", new List<Project>());
            }
        }

        public ResultObject<Project> Update(Project project)
        {
            try
            {
                if (this.Get(project.Name, project.Id).Data.Id < 1)
                {
                    _context.Projects.Update(project);
                    _context.SaveChanges();
                    return ResultObject<Project>.Success("Project Updated successfuly");

                }
                else
                {
                    _context.Entry(project).Reload();
                    return ResultObject<Project>.Failure("There is already another Project with the same name");
                }
            }
            catch
            {
                _context.Entry(project).Reload();
                return ResultObject<Project>.Failure("Could not update the Project, Something went wrong!");
            }
        }
    }
}

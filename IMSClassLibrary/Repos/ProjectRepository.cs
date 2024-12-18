

using IMSClassLibrary.Models;

namespace IMSClassLibrary.Repos
{
    public class ProjectRepository : IProject<Project>
    {
        private readonly SystemDbContext _context;

        public ProjectRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public Project Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public async Task<Project> AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public List<Project> AddRange(List<Project> projects)
        {
            _context.Projects.AddRange(projects);
            _context.SaveChanges();
            return projects;
        }

        public bool Delete(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Project Get(int id)
        {
            return _context.Projects.Find(id);
        }

        public List<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public Project Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
            return project;
        }
    }
}

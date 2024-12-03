

namespace IMSClassLibrary.Repos
{
    public class DepartmentRepository : IDepartment<Department>
    {
        private readonly SystemDbContext _context;

        public DepartmentRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public Department Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public async Task<Department> AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department; 
}

        public List<Department> AddRange(List<Department> departments)
        {
            _context.Departments.AddRange(departments);
            _context.SaveChanges();
            return departments;
        }

        public bool Delete(Department department)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Department Get(int id)
        {
            return _context.Departments.Find(id);
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return department;
        }
    }
}

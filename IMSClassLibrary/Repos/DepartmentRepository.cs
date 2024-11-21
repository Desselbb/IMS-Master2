
using IMSClassLibrary.Interfaces;
namespace IMSClassLibrary.Repos
{
    public class DepartmentRepository : IInterface<Department>
    {
        SystemDbContext _context;
        public DepartmentRepository(SystemDbContext context)
        {
            this._context = context;
        }

		public ResultObject<Department> Add(Department Department)
		{
			try
			{
				if (_context.Departments.Where(d => d.Name.Trim() == Department.Name.Trim()).FirstOrDefault() == null)
				{
					_context.Departments.Add(Department);
					_context.SaveChanges();
					return ResultObject<Department>.Success("Department has been added successfully");
				}
				else
				{
					return ResultObject<Department>.Failure("There is already another Department with the same Name");
				}
			}
			catch (Exception d)
			{
				Console.WriteLine(d.Message + "\n" + d.StackTrace);
				return ResultObject<Department>.Failure("Could not add Department, Something went wrong");
			}
		}

		public ResultObject<Department> AddRange(List<Department> Departments)
		{
			try
			{
				bool addAll = true;
				foreach (var dept in Departments)
				{
					if (Get(dept.Id,dept.Name.Trim()).Data.Id>0)
					{
						addAll = false;
						break;
					}
				}
				if (addAll)
				{
					_context.Departments.AddRange(Departments);
					_context.SaveChanges();

					return ResultObject<Department>.Success("Departments have been added successfully");
				}
				else
				{
					return ResultObject<Department>.Failure("Some Departments in the submited list have Name similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
				}
			}
			catch (Exception d)
			{
				Console.WriteLine(d.Message + "\n" + d.StackTrace);
				return ResultObject<Department>.Failure("Could not add the Departments, Something went wrong");
			}
		}

        public ResultObject<Department> Delete(Department item)
        {
            try
            {
                _context.Departments.Remove(item);
                _context.SaveChanges();
                return ResultObject<Department>.Success("The Department has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Department>.Failure("Could not delete the Department, something went wrong");
            }
        }

        public ResultObject<Department> DeleteById(int Id)
		{
			try
			{
				_context.Comments.Remove(_context.Comments.Single(b => b.Id == Id));
				_context.SaveChanges();
				return ResultObject<Department>.Success("The Department has been deleted successfuly");
			}
			catch
			{
				return ResultObject<Department>.Failure("Could not delete the Department, something went wrong");
			}
		}

		public ResultObject<Department> Get(int Id)
		{
			try
			{
				return ResultObject<Department>.Success("Department Retrived", _context.Departments.Single(b => b.Id == Id));
			}
			catch (Exception d)
			{
				return ResultObject<Department>.Failure("Could not retrieve the Department, Something went wrong", new Department());
			}
		}

		public ResultObject<Department> Get(int unitId, string  DepartmentName)
		{
			try
			{
				return ResultObject<Department>.Success("", _context.Departments.Single(b => b.Id == unitId && b.Name.Trim()==DepartmentName.Trim()));
			}
			catch (Exception d)
			{
				return ResultObject<Department>.Failure("Could not retrive the object", new Department());
			}
		}

		public ResultObject<List<Department>> GetAll()
		{
			try
			{
				return ResultObject<List<Department>>.Success("", _context.Departments.ToList());
			}
			catch
			{
				return ResultObject<List<Department>>.Failure("Could not retrive the object", new List<Department>());
			}
		}

		public ResultObject<Department> Update(Department Department)
		{
			try
			{
				if (_context.Departments.FirstOrDefault(c => c.Name.Trim() == Department.Name.Trim()) == null)
				{
					_context.Departments.Update(Department);
					_context.SaveChanges();
					return ResultObject<Department>.Success("Object Updated successfuly");
				}
				else
				{
                    _context.Entry(Department).Reload();
					return ResultObject<Department>.Failure("There is already another department with the same name, Something went wrong!");
                }
			}
			catch
			{
                _context.Entry(Department).Reload();
                return ResultObject<Department>.Failure("Could not update the Department, Something went wrong!");
			}
		}
	}
}

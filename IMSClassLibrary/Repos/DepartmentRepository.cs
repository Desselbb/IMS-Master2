using IMSClassLibrary.Interfaces;
using IMSClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSClassLibrary.Repos
{
    public class DepartmentRepository : IInterface<Department>
    {
        private readonly SystemDbContext _context;
        public DepartmentRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<Department> Add(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return ResultObject<Department>.Success("Department added successfully", department);
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Department> AddRange(List<Department> departments)
        {
            try
            {
                _context.Departments.AddRange(departments);
                _context.SaveChanges();
                return ResultObject<Department>.Success("Departments added successfully");
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Department> Delete(Department department)
        {
            try
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return ResultObject<Department>.Success("Department deleted successfully", department);
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Department> DeleteById(int id)
        {
            try
            {
                var department = _context.Departments.Find(id);
                if (department == null)
                {
                    return ResultObject<Department>.Failure("Department not found", null);
                }
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return ResultObject<Department>.Success("Department deleted successfully", department);
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Department> Get(int id)
        {
            try
            {
                var department = _context.Departments.Find(id);
                if (department == null)
                {
                    return ResultObject<Department>.Failure("Department not found", null);
                }
                return ResultObject<Department>.Success("Department retrieved successfully", department);
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }

        public ResultObject<List<Department>> GetAll()
        {
            try
            {
                var departments = _context.Departments.ToList();
                return ResultObject<List<Department>>.Success("Departments retrieved successfully", departments);
            }
            catch (Exception ex)
            {
                return ResultObject<List<Department>>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Department> Update(Department department)
        {
            try
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return ResultObject<Department>.Success("Department updated successfully", department);
            }
            catch (Exception ex)
            {
                return ResultObject<Department>.Failure(ex.Message, null);
            }
        }
    }
}

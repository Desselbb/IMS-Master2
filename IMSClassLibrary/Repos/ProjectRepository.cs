using IMSClassLibrary.Interfaces;
using IMSClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSClassLibrary.Repos
{
    public class ProjectRepository : IInterface<Project>
    {
        private readonly SystemDbContext _context;
        public ProjectRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<Project> Add(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return ResultObject<Project>.Success("Project added successfully", project);
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Project> AddRange(List<Project> projects)
        {
            try
            {
                _context.Projects.AddRange(projects);
                _context.SaveChanges();
                return ResultObject<Project>.Success("Departments added successfully");
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Project> Delete(Project project)
        {
            try
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return ResultObject<Project>.Success("Department deleted successfully", project);
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Project> DeleteById(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);
                if (project == null)
                {
                    return ResultObject<Project>.Failure("Project not found", null);
                }
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return ResultObject<Project>.Success("Project deleted successfully", project);
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Project> Get(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);
                if (project == null)
                {
                    return ResultObject<Project>.Failure("Project not found", null);
                }
                return ResultObject<Project>.Success("Project retrieved successfully", project);
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }

        public ResultObject<List<Project>> GetAll()
        {
            try
            {
                var projects = _context.Projects.ToList();
                return ResultObject<List<Project>>.Success("Projects retrieved successfully", projects);
            }
            catch (Exception ex)
            {
                return ResultObject<List<Project>>.Failure(ex.Message, null);
            }
        }

        public ResultObject<Project> Update(Project project)
        {
            try
            {
                _context.Projects.Update(project);
                _context.SaveChanges();
                return ResultObject<Project>.Success("Department updated successfully", project);
            }
            catch (Exception ex)
            {
                return ResultObject<Project>.Failure(ex.Message, null);
            }
        }
    }
}

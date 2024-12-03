using IMSClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSClassLibrary.Context
{
    public class SystemDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options): base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }    
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Comment> Comments { get; set; } 
        public virtual DbSet<InternProject> InternProjects { get; set; } 
        public virtual DbSet<Module> Modules{ get; set; } 
        public virtual DbSet<Profile> Profiles  { get; set; } 
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<ProfileModule> ProfileModules { get; set; }

    }
}

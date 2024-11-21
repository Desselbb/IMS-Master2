namespace IMS
{
    using IMSClassLibrary.Context;
    using IMSClassLibrary.Models;
    using Microsoft.EntityFrameworkCore;

    public static class SeedData
    {
        public static void Initialize(SystemDbContext context)
        {
            // Check if the database is empty or if the departments table has no data
            if (!context.Departments.Any())
            {
                // Add some departments if none exist
                var departments = new[]
                {
                new Department
                {
                    Name = "HR",
                    Status = "active",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.Now
                },
                new Department
                {
                    Name = "Finance",
                    Status = "active",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.Now
                },
                new Department
                {
                    Name = "IT",
                    Status = "active",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.Now
                }
            };

                context.Departments.AddRange(departments);
                context.SaveChanges(); // Save changes to the database
            }
        }
    }

}

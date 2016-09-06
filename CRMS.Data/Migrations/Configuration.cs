using System.Data.Entity.Migrations.Model;
using CRMS_Data.Entities;

namespace CRMS_Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRMS_Data.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRMS_Data.DbContext context)
        {
            try
            {
                var employee = new Employee()
                {
                    Name = "Admin",
                    Email = "Admin@crsm.com",
                    Gender = "Male",
                    Phone = "01478523695",
                    Address = "Unknown",
                    Photo = "ccdvv",
                    Birthdate = DateTime.Now,
                    NationalId = "Unknown",
                    PassportNo = "Unknown",
                    Role = "Manager"
                };
                context.Employees.Add(employee);

                var userAccess = new Login()
                {
                    Username = "Admin",
                    Password = "Admin",
                    LastLoginTime = DateTime.Now
                };
                context.Logins.Add(userAccess);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        private static void SetCreatedUtcColumn(PropertyModel column)
        {
            if (column.Name == "LastLoginTime")
            {
                column.DefaultValueSql = "GETUTCDATE()";
            }
        }
    }
}

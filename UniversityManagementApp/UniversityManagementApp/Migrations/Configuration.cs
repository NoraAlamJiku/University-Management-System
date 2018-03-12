using UniversityManagementApp.Models;

namespace UniversityManagementApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityManagementApp.Models.UniversityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniversityManagementApp.Models.UniversityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.RoomNos.AddOrUpdate(
            //  new RoomNo { RoomNumber = "RM-101 " },
            //  new RoomNo { RoomNumber = "RM-102 " },
            //  new RoomNo { RoomNumber = "RM-201 " },
            //  new RoomNo { RoomNumber = "RM-202 " },
            //  new RoomNo { RoomNumber = "RM-301 " }
            // );
            //
        }
    }
}

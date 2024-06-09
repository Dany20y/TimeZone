namespace BussinesLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

   internal sealed class Configuration : DbMigrationsConfiguration<Time_Zone.BussinessLogic.DBModel.ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Time_Zone.BussinessLogic.DBModel.ProductContext";
        }

        protected override void Seed(Time_Zone.BussinessLogic.DBModel.ProductContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
        }



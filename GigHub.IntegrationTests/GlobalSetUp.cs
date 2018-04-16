using GigHub.MVC.Core.Models;
using GigHub.MVC.Persistance;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.MVC.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser { UserName = "teste", Name="Usuario Testador", Email = "teste@teste.com", PasswordHash = "testehash" });
            context.Users.Add(new ApplicationUser { UserName = "user", Name="Usuario", Email = "user@teste.com", PasswordHash = "userhash" });

            context.SaveChanges();
        }
    }
}

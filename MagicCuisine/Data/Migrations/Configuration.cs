namespace Data.Migrations
{
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Data.CuisineDbContext>
    {
        private const string AdministratorUserName = "admin@gmail.com";
        private const string AdministratorPassword = "passW0rd!";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CuisineDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);

            base.Seed(context);
        }

        private void SeedUsers(CuisineDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleName = "Admin";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    AddressId = null,
                    Avatar = "/Avatars/img-default.png"
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private void SeedSampleData(CuisineDbContext context)
        {
            var addressList = new List<List<string>>()
            {
                new List<string>(){"Bulgaria" , "Sofia", "Varna", "Burgas", "Plovdiv"},
                 new List<string>(){"Germany" , "Berlin", "Frankfurt"}
            };

            if (!context.Countries.Any())
            {
                foreach (var item in addressList)
                {
                    var country = new Country() { Name = item[0] };
                    context.Countries.Add(country);
                    for (int i = 1; i < item.Count; i++)
                    {
                        var town = new Town() { Name = item[i], Country = country };
                        context.Towns.Add(town);
                    }
                }
            }
        }

    }
}

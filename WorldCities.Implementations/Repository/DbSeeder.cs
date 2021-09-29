using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WorldCities.Implementations.Contracts;
using WorldCities.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace WorldCities.Implementations.Repository
{
    internal class DbSeeder : IDbSeeder
    {
        private readonly IRepositoryManager repo;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DbSeeder(
            IRepositoryManager repo, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            this.repo = repo;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<string> CreateDefaultUsersAsync()
        {
            // setup the default role names
            string role_RegisteredUser = "RegisteredUser";
            string role_Administrator = "Administrator";
            // create the default roles (if they don't exist yet)
            if (await roleManager.FindByNameAsync(role_RegisteredUser) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
            }

            if (await roleManager.FindByNameAsync(role_Administrator) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role_Administrator));
            }

            // create a list to track the newly added users
            var addedUserList = new List<ApplicationUser>();

            // check if the admin user already exists
            var email_Admin = "admin@email.com";
            if (await userManager.FindByNameAsync(email_Admin) == null)
            {
                // create a new admin ApplicationUser account
                var user_Admin = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin,
                };
                // insert the admin user into the DB
                await userManager.CreateAsync(user_Admin, "MySecr3t$");
                // assign the "RegisteredUser" and "Administrator" roles
                await userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await userManager.AddToRoleAsync(user_Admin, role_Administrator);
                // confirm the e-mail and remove lockout
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;
                // add the admin user to the added users list
                addedUserList.Add(user_Admin);
            }

            // check if the standard user already exists
            var email_User = "user@email.com";
            if (await userManager.FindByNameAsync(email_User) == null)
            {
                // create a new standard ApplicationUser account
                var user_User = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };
                // insert the standard user into the DB
                await userManager.CreateAsync(user_User, "MySecr3t$");
                // assign the "RegisteredUser" role
                await userManager.AddToRoleAsync(user_User,
                 role_RegisteredUser);
                // confirm the e-mail and remove lockout
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;
                // add the standard user to the added users list
                addedUserList.Add(user_User);
            }
            // if we added at least one user, persist the changes into the DB
            if (addedUserList.Count > 0)
            {
                await repo.SaveAsync();
            }
            return $"Seeded, created users count: {addedUserList.Count}, Users: {addedUserList.ToString()}";
        }

        public async Task<string> SeedAsync()
        {
            var count = await repo.Country.CountAsync();
            if (count != 0)
            {
                return $"Country are created nothing will be seeded !";
            }

            using var stream = GetFileStream("worldcities.xlsx");
            using var excelPackage = new ExcelPackage(stream);
            // get the first worksheet 
            var worksheet = excelPackage.Workbook.Worksheets[0];
            // define how many rows we want to process 
            var nEndRow = worksheet.Dimension.End.Row;
            // initialize the record counters 
            var numberOfCountriesAdded = 0;
            var numberOfCitiesAdded = 0;
            var countriesByName = new Dictionary<string, Country>();
            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];
                var countryName = row[nRow, 5].GetValue<string>();
                var iso2 = row[nRow, 6].GetValue<string>();
                var iso3 = row[nRow, 7].GetValue<string>();
                // create the Country entity and fill it with xlsx data 
                var country = new Country
                {
                    Name = countryName,
                    ISO2 = iso2,
                    ISO3 = iso3
                };
                if (countriesByName.ContainsKey(countryName))
                {
                    continue;
                }
                countriesByName[countryName] = country;
                // add the new country to the DB context 
                await repo.Country.CreateEntityAsync(country);
                // increment the counter 
                numberOfCountriesAdded++;
            }

            await repo.SaveAsync();


            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];
                var name = row[nRow, 1].GetValue<string>();
                var nameAscii = row[nRow, 2].GetValue<string>();
                var lat = row[nRow, 3].GetValue<decimal>();
                var lon = row[nRow, 4].GetValue<decimal>();
                var countryName = row[nRow, 5].GetValue<string>();
                // retrieve country Id by countryName
                var countryId = countriesByName[countryName].Id;

                // create the City entity and fill it with xlsx data 
                var city = new City
                {
                    Name = name,
                    Name_ASCII = nameAscii,
                    Lat = lat,
                    Lon = lon,
                    CountryId = countryId
                };
                // add the new city to the DB context 
                await repo.City.CreateEntityAsync(city);
                // increment the counter 
                numberOfCitiesAdded++;
            }
            await repo.SaveAsync();

            return $"Seeded, created countries: {numberOfCountriesAdded}, cities: {numberOfCitiesAdded}";
        }

        private Stream? GetFileStream(string fileName)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"WorldCities.Implementations.Source.{fileName}");
        }
    }
}

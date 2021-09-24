using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Implementations.Contracts;
using WorldCities.Models.Models;

namespace WorldCities.Implementations.Repository
{
    internal class DbSeeder : IDbSeeder
    {
        private readonly IRepositoryManager repo;

        public DbSeeder(IRepositoryManager repo)
        {
            this.repo = repo;
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

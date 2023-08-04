using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Runtime;
using System.Security;
using webapi.Data.Models;
using WorldCities_Web_API.Data;

namespace WorldCities_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        public SeedController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet("Import")]
        public async Task<ActionResult> Import()
        {
            if (!_env.IsDevelopment())
                throw new SecurityException("Not allowed");

            var path = Path.Combine(_env.ContentRootPath, "Data/worldcities.xlsx");

            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            //get the first worksheet
            var worksheet = excelPackage.Workbook.Worksheets[0];

            //define how many rows we want to process
            var nEndRow = worksheet.Dimension.End.Row;

            //initialize the record counters
            var numberOfCountriesAdded = 0;
            var numberOfCitiesAdded = 0;

            //Create a lookup dictionary containing all the countries already existing
            //into the Database (it will be emply first run).
            var countriesByName = _context.Countries
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
            
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[

                    nRow, 1, nRow, worksheet.Dimension.End.Column];
                var countryName = row[nRow, 5].GetValue<string>();
                var iso2 = row[nRow, 6].GetValue<string>();
                var iso3 = row[nRow, 7].GetValue<string>();

                //skip this country if it already exist inthe database
                if (countriesByName.ContainsKey(countryName))
                {
                    continue;
                }

                //create teh Country entity and fill it with xlsx data
                var country = new Country
                {
                    Name = countryName,
                    ISO2 = iso2,
                    ISO3 = iso3
                };

                //add the new country to the DB context 
                await _context.Countries.AddAsync(country);

                //store teh country in our lookup to retrieve its id later on
                countriesByName.Add(countryName, country);

                //increament the counter
                numberOfCountriesAdded++;
            }

            //save all the countries into the Database

            if (numberOfCountriesAdded > 0)

                await _context.SaveChangesAsync();

            //create a lookup dictionary containing all the cities already existing
            //into the database (it will be empty first run )
            var cities = _context.Cities.AsNoTracking().ToDictionary(x =>
            (Name: x.Name,
            Lat: x.Lat,
            Lon: x.Lon,
            CountryId: x.CountryId));

            //itereate through all rows, skipping the first one
            for (int nRow = 2; nRow < nEndRow; nRow++)
            {
                var row = worksheet.Cells[nRow, 1, nRow, worksheet.Dimension.End.Column];
                var name = row[nRow, 1].GetValue<string>();
                var nameAscii = row[nRow, 2].GetValue<string>();
                var lat = row[nRow, 3].GetValue<decimal>();
                var lon = row[nRow, 4].GetValue<decimal>();
                var countryName = row[nRow, 5].GetValue<string>();

                //retrieve country Id by countryName 
                var countryId = countriesByName[countryName].Id;

                //skip thiscity if ti already exists in the databease
                if (cities.ContainsKey((Name: name,
                    Lat: lat,
                    Lon: lon,
                    CountryId: countryId)))
                    continue;


                //create the city entity and fill it with xlsx data
                var city = new City
                {
                    Name = name,
                    Lat = lat,
                    Lon = lon,
                    CountryId = countryId
                };

                //add the new city to the DB context 
                _context.Cities.Add(city);

                //increment the counter
                numberOfCitiesAdded++;

            }


            //save all the citites into the Database
            if (numberOfCitiesAdded > 0)

                await _context.SaveChangesAsync();
            return new JsonResult(new
            {
                Cities = numberOfCitiesAdded,
                Countries = numberOfCountriesAdded
            });



        }

    }


}

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DataLibrary;
using EnsekGlobal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<EnsekDbContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/meter-reading-uploads", async ([FromServices] EnsekDbContext db, CsvModel model) =>
{
    var meterReadings = new List<MeterReadingsDM>();
    var csvMeterReadings = new List<MeterReadingsDM>();

    using (var ms = new MemoryStream(System.Convert.FromBase64String(model.Base64)))
    using (var sr = new StreamReader(ms))
    using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
    {
        csv.Context.RegisterClassMap<MeterReadingsDMMap>();
        csvMeterReadings = csv.GetRecords<MeterReadingsDM>().OrderByDescending(o => o.MeterReadingDateTime).Distinct().ToList();
        var existing = await db.MeterReadings.ToListAsync();
        var accounts = await db.Accounts.Where(x => csvMeterReadings.Select(s => s.AccountId).Contains(x.AccountId)).ToListAsync();

        csvMeterReadings.ForEach(x =>
        {
            string message = "";
            x.Account = accounts.FirstOrDefault(f => f.AccountId.Equals(x.AccountId));
            if (x.Account == null)
            {
                x.IsError = true;
                message += "Account does not exist. ";
            }
            if (!x.MeterReadValue.HasValue)
            {
                x.IsError = true;
                message += "Failed to read meter reading. ";
            }
            var existingReading = existing.OrderByDescending(o => o.MeterReadingDateTime).FirstOrDefault(f => f.AccountId.Equals(x.AccountId));
            if (existingReading != null && existingReading.MeterReadingDateTime >= x.MeterReadingDateTime)
            {
                x.IsError = true;
                if (existingReading.MeterReadingDateTime == x.MeterReadingDateTime)
                    message += "Meter reading already exists. ";
                else
                    message += "Future meter reading already exists. ";
            }

            x.Message = message;
            meterReadings.Add(x);
            if (!x.IsError)
                existing.Add(x);
        });

        await db.MeterReadings.AddRangeAsync(meterReadings.Where(x => !x.IsError));
        await db.SaveChangesAsync();
        return Results.Ok(meterReadings);
    };
})
.WithName("MeterReadingUploads");

app.Run();
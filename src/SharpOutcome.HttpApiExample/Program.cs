using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.Utils;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "DataSource=book_db.sqlite3;Cache=Shared";

var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
optionsBuilder.UseSqlite(connectionString);


await using (var dbContext = new BookDbContext(optionsBuilder.Options))
{
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.Database.OpenConnection();
    dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=DELETE;");
    dbContext.Database.CloseConnection();
}

builder.Services.AddDbContext<BookDbContext>(opts => opts.UseSqlite(connectionString));


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(
        namingPolicy: JsonNamingPolicy.CamelCase,
        allowIntegerValues: false)
    );
});


builder.Services.MapApiEndpointServices(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.SupportNonNullableReferenceTypes());


ValidatorOptions.Global.DisplayNameResolver = (_, member, _) =>
    member is not null ? JsonNamingPolicy.CamelCase.ConvertName(member.Name) : null;

var app = builder.Build();

app.Lifetime.ApplicationStopping.Register(() =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "book_db.sqlite3");
    if (File.Exists(dbPath)) File.Delete(dbPath);
});

app.UseSwagger();
app.UseSwaggerUI(o => o.EnableTryItOutByDefault());
app.MapApiEndpoints(Assembly.GetExecutingAssembly());
app.Run();
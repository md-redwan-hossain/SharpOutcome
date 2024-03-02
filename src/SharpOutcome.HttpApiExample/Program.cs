using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.Services;
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

builder.Services.AddScoped<IBookService, BookService>();
builder.Services
    .AddControllers(opts =>
    {
        opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        opts.OutputFormatters.RemoveType<StringOutputFormatter>();
    })
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.SupportNonNullableReferenceTypes());

var app = builder.Build();

app.Lifetime.ApplicationStopping.Register(() =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "book_db.sqlite3");
    if (File.Exists(dbPath)) File.Delete(dbPath);
});

app.UseSwagger();
app.UseSwaggerUI(o => o.EnableTryItOutByDefault());

app.MapControllers();
app.Run();
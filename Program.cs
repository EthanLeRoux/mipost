using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mongoConnection = builder.Configuration["MongoDB:ConnectionString"];
var mongoDatabase = builder.Configuration["MongoDB:DatabaseName"];

var mongoClient = new MongoClient(mongoConnection);
var database = mongoClient.GetDatabase(mongoDatabase);

builder.Services.AddSingleton(database);
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");

// app.Run();

using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using HealthKoppeling.Managers;
using HealthKoppeling.Database;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.Cosmos;

static async Task<UserDbService> InititializeUserInstanceAsync(string databaseName, CosmosClient client)
{
    var cosmosDbService = new UserDbService(client, databaseName);
    return cosmosDbService;
}
static async Task<StepDbService> InititializeStepsInstanceAsync(string databaseName, CosmosClient client)
{
    var cosmosDbService = new StepDbService(client, databaseName);
    return cosmosDbService;
}
static async Task<BurnedCaloriesDbService> InititializeBurnedCaloriesInstanceAsync(string databaseName, CosmosClient client)
{
    var cosmosDbService = new BurnedCaloriesDbService(client, databaseName);
    return cosmosDbService;
}

static async Task<MoveMinutesDbService> InititializeMoveMinutesInstanceAsync(string databaseName, CosmosClient client)
{
    var cosmosDbService = new MoveMinutesDbService(client, databaseName);
    return cosmosDbService;
}

var builder = WebApplication.CreateBuilder(args);
var configurationsection = builder.Configuration.GetSection("CosmosDb");
var databaseName = configurationsection["DatabaseName"];
var account = configurationsection["Account"];
var key = configurationsection["Key"];
var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
// Add services to the container.
builder.Services.AddSingleton<ICosmosDbService<UserModel>>(InititializeUserInstanceAsync(databaseName, client).GetAwaiter().GetResult());
builder.Services.AddSingleton<ICosmosDbService<StepModel>>(InititializeStepsInstanceAsync(databaseName, client).GetAwaiter().GetResult());
builder.Services.AddSingleton<ICosmosDbService<BurnedCaloriesModel>>(InititializeBurnedCaloriesInstanceAsync(databaseName, client).GetAwaiter().GetResult());
builder.Services.AddSingleton<ICosmosDbService<MoveMinutesModel>>(InititializeMoveMinutesInstanceAsync(databaseName, client).GetAwaiter().GetResult());
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddTransient<IManager<UserModel>, UserManager>();
builder.Services.AddTransient<IManager<StepModel>, StepManager>();
builder.Services.AddTransient<IManager<BurnedCaloriesModel>, BurnedCaloriesManager>();
builder.Services.AddTransient<IManager<MoveMinutesModel>, MoveMinutesManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

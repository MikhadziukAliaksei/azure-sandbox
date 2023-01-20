using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Configuration.AddAzureAppConfiguration("Endpoint=https://azure-test-appconfig.azconfig.io;Id=0ovR-l9-s0:4BLwpXdIiK8GPQ41n6mG;Secret=MepBHD4pf/jHVnalt4EFNCv+3yP1cPr11+pvFBH62zw=");
// builder.Configuration.AddAzureAppConfiguration(options =>
// {
//     options.Connect(
//             "Endpoint=https://azure-test-appconfig.azconfig.io;Id=0ovR-l9-s0:4BLwpXdIiK8GPQ41n6mG;Secret=MepBHD4pf/jHVnalt4EFNCv+3yP1cPr11+pvFBH62zw=")
//         .ConfigureKeyVault(kv => { kv.SetCredential(new DefaultAzureCredential()); });
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
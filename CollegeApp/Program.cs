using CollegeApp.MyLogging;
using Serilog;


var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/log.text", rollingInterval: RollingInterval.Day)
    .CreateLogger();


//builder.Host.UseSerilog();  // this will add only the serilog

builder.Logging.AddSerilog();  // this will enable both built in Logger and Serilog

#region Clearing the default Logging Provider and add only the required one. By default we have 4 Providers

//COMMENTING TO CHECK the SERILOG options

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();


#endregion





// Add services to the container.



#region Giving Error with respone Code 406
//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).
//    AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); 
#endregion


//this is used for PATCH verb
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IMyLogger,LogToFile>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

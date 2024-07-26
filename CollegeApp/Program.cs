using CollegeApp.Configuration;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.MyLogging;
using Microsoft.EntityFrameworkCore;
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




builder.Services.AddDbContext<CollegeDBContext>(x =>

x.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"))

) ;



// Add services to the container.

builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>),typeof(CollegeRepository<>));



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


builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

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

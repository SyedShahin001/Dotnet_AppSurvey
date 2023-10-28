using AppSurveyTrustspot.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//react
builder.Services.AddCors();

//DB Connect
builder.Services.AddDbContext<TrustspotContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(options =>
{
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
	options.AllowAnyHeader();
});

app.UseRouting();

app.UseAuthorization();


app.MapControllers();


app.Run();

using Microsoft.EntityFrameworkCore;
using NetDemo.Configurations;
using NetDemo.Data;
using NetDemo.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Connect db
builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
}
);

// Add services to the container.
builder.Services.AddControllers();

// Automapper configuration.
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

// Configure Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

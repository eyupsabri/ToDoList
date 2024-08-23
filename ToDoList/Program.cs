using Business;
using DAL;
using Entities;
using ToDoList.ActionFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtIssuer = builder.Configuration.GetSection("JWT_ISSUER").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWT_KEY").Get<string>();
builder.Services.AddTransient<DapperContext>(c =>
{
    return new DapperContext(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton(new MyAuthActionFilter(jwtKey, jwtIssuer));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

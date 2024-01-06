using GraphQL.Api.DataLoaders;
using GraphQL.Api.Repositories;
using GraphQL.Api.Schema.Queries;
using GraphQL.Api.Schema.Mutations;
using GraphQL.Api.Schema.Subscriptions;
using GraphQL.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Local")!));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<TeachersRepository>();
builder.Services.AddScoped<TeacherDataLoader>();


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

app.MapGraphQLWebSocket();

app.MapGraphQL();

app.Run();
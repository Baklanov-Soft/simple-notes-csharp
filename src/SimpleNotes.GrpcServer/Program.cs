using SimpleNotes.Application.Extensions.DependencyInjection;
using SimpleNotes.GrpcServer.Services;
using SimpleNotes.Infrastructure.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Local");

builder.Services.AddApplicationServices();
builder.Services.AddNotesDbContext(connectionString);
builder.Services.AddInfrastructureServices();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<NotesService>();

app.Run();
using GrpcTodoService.Services;
using Data;
using GrpcTodoService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(opt =>
{
    opt.Interceptors.Add<ExeptionInterceptor>();
});
builder.Services.AddData();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Todo", optinos =>
    {
        optinos.AllowAnyHeader();
        optinos.AllowAnyMethod();
        optinos.WithOrigins("https://localhost:5002", "http://localhost:5000");
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<TodoDbContext>();
    DbInitializer.Initialize(context);
}

app.UseRouting();
app.UseCors("Todo");
app.UseGrpcWeb();

app.MapGrpcService<TodosService>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

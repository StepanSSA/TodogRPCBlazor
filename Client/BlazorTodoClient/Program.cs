using BlazorTodoClient;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton(services =>
{
	var baseUri = "https://localhost:5001";

	var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions()
	{
		HttpHandler = new GrpcWebHandler(new HttpClientHandler())
	});

	return new Todos.TodosClient(channel);
});

await builder.Build().RunAsync();

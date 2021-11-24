using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseGrpcWeb(new GrpcWebOptions()
{
    DefaultEnabled = true
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GameServerImpl>();
});

var task = app.RunAsync();
Console.WriteLine("Press any key to stop the server...");
Console.ReadKey();
app.StopAsync();

class GameServerImpl : GameService.GameServiceBase
{
    public override Task<FullMonsterStateResponse> getMatchResult(GetMatchResultRequest request,
      ServerCallContext context)
    {
        Console.WriteLine($"Getting match request for clientid: {request.ClientId}");

        var monsterState = new MonsterState { MonsterName = "Ben", XCoord = 0, YCoord = 2 };
        var fullState = new FullMonsterStateResponse();
        fullState.AllMonsters.Add(monsterState);
        return Task.FromResult(fullState);
    }
}

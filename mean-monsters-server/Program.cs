using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace mean_monsters_server
{
  class GameServerImpl : GameService.GameServiceBase
  {
    public override Task<FullMonsterStateResponse> getMatchResult(GetMatchResultRequest request,
      ServerCallContext context)
    {
      Console.WriteLine($"Getting match request for clientid: {request.ClientId}");

      var monsterState = new MonsterState {MonsterName = "Ben", XCoord = 0, YCoord = 2};
      var fullState = new FullMonsterStateResponse();
      fullState.AllMonsters.Add(monsterState);
      return Task.FromResult(fullState);
    }
  }

  class Program
  {
    private const int Port = 9091;

    static void Main(string[] args)
    {
    ServerServiceDefinition gameServiceService = GameService.BindService(new GameServerImpl());

      Server server = new Server
      {
        Services = {gameServiceService},
        Ports = {new ServerPort("localhost", Port, ServerCredentials.Insecure)},
      };
      server.Start();

      Console.WriteLine("Greeter server listening on port " + Port);

      Console.WriteLine("Press any key to stop the server...");
      Console.ReadKey();
      server.ShutdownAsync().Wait();

    }
  }
}

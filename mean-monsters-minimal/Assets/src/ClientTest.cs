using System.Net.Http;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using UnityEngine;

namespace Assets.src
{
  public class ClientTest : MonoBehaviour
  {
    void Start()
    {

      var webHandler = new GrpcWebHandler(new HttpClientHandler());
      var option = new GrpcChannelOptions
      {
        Credentials = ChannelCredentials.Insecure,
        HttpHandler = webHandler,
      };
      var channel = GrpcChannel.ForAddress("http://127.0.0.1:9091", option);
      var client = new GameService.GameServiceClient(channel);
      FullMonsterStateResponse reply = client.getMatchResult(new GetMatchResultRequest
      {
        ClientId = "ben",
      });
      Debug.Log(reply);
    }
  }
}

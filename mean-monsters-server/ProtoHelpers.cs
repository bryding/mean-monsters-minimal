using System.IO;
using Google.Protobuf;

namespace mean_monsters_server {
  public static class ProtoHelpers {
    public static byte[] GetBytes<T>(this IMessage<T> proto) where T : IMessage<T>
    {
      using var stream = new MemoryStream();
      proto.WriteTo(stream);
      return stream.ToArray();
    }
  }
}
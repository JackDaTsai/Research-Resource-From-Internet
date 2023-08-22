/// <summary>
/// How to create a TCP socket server using C#
/// </summary>

using System.Net.Sockets;
using System.Text;

public class TcpSocketServer
{
    private readonly IPEndPoint _ipEndPoint;

    public TcpSocketServer(IPEndPoint ipEndPoint)
    {
        _ipEndPoint = ipEndPoint;
    }

    public async Task StartAsync()
    {
        using var listener = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(_ipEndPoint);
        listener.Listen(100);

        var handler = await listener.AcceptAsync();
        var response = await ReceiveMessageAsync(handler);

        if (IsEndOfMessage(response))
        {
            Console.WriteLine($"Socket server received message: \"{CleanMessage(response)}\"");
            await SendAcknowledgmentAsync(handler);
        }
    }

    private async Task<string> ReceiveMessageAsync(Socket handler)
    {
        var buffer = new byte[1024];
        var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
        return Encoding.UTF8.GetString(buffer, 0, received);
    }

    private bool IsEndOfMessage(string response)
    {
        return response.Contains(""); // 結束消息的標記
    }

    private string CleanMessage(string response)
    {
        return response.Replace("", "");
    }

    private async Task SendAcknowledgmentAsync(Socket handler)
    {
        var ackMessage = ""; // 確認消息的內容
        var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
        await handler.SendAsync(echoBytes, 0);
        Console.WriteLine($"Socket server sent acknowledgment: \"{ackMessage}\"");
    }
}

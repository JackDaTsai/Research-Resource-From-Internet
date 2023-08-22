/// <summary>
/// How to create a TCP socket client using C#
/// </summary>

using System.Net.Sockets;
using System.Text;

public class TcpSocketClient
{
    private readonly IPEndPoint _ipEndPoint;

    public TcpSocketClient(IPEndPoint ipEndPoint)
    {
        _ipEndPoint = ipEndPoint;
    }

    public async Task StartAsync()
    {
        using var client = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        await client.ConnectAsync(_ipEndPoint);

        while (true)
        {
            await SendMessageAsync(client, "Hi friends 👋!");
            var response = await ReceiveAcknowledgmentAsync(client);

            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine($"Socket client received acknowledgment: \"{response}\"");
                break;
            }
        }

        client.Shutdown(SocketShutdown.Both);
    }

    private async Task SendMessageAsync(Socket client, string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(messageBytes, SocketFlags.None);
        Console.WriteLine($"Socket client sent message: \"{message}\"");
    }

    private async Task<string> ReceiveAcknowledgmentAsync(Socket client)
    {
        var buffer = new byte[1024];
        var received = await client.ReceiveAsync(buffer, SocketFlags.None);
        return Encoding.UTF8.GetString(buffer, 0, received);
    }
}
